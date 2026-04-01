using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private GameManager GM;
    private CameraManager CM;
    private UIManager UI;
    private PlayerInteraction PI;
    private PlayerInventory PInv;
    private postProcessOnOff PostProOnOff;
    private PlayerAudioManager PAM;
    private AudioSource Crickets;

    private CharacterController controller;
    private GroundChecker gC;

    private TutorialManager TM;

    private Vector3 playerVelocity;
    private Vector3 respawnPos;
    private Quaternion ThirdPerPlayerRotation;

    [Header("Movement")]
    [Tooltip("How fast the player moves"), Min(0), SerializeField]
    private float speed = 3;
    [SerializeField]
    private float currentSpeed;
    [Tooltip("How high the player jumps"), Min(0), SerializeField]
    private float jumpForce = 4.5f;
    [Tooltip("What the gravity on the player is"), SerializeField]
    private float gravity = -9.81f;

    // Modified Movement
    // Easier to multiply movespeed here
    [SerializeField] private float runMultiplier = 1.75f;
    [SerializeField] private float acceleration = 12f;
    [SerializeField] private float airControl = 0.4f;

    private Vector3 moveVelocity;

    [Header("Debugger")]
    [Tooltip("Turns on Jump Debugging"), SerializeField]
    private bool jumpDebugging;
    [Tooltip("Turns on hiding Debugging"), SerializeField]
    private bool hidingDebugging;
    [Tooltip("Turns on Drop Obsticle Debugging"), SerializeField]
    private bool dropDebugging;

    [Header("Hiding and Drop")]
    [SerializeField] private bool hiding;
    [SerializeField] private bool canHide;
    Vector3 prevPosition;
    Vector3 hidingPos;
    [SerializeField] private bool canDrop;
    private FallablePlatform nearbyDropper;

    [SerializeField] private GameObject dialogBox;

    [SerializeField] private int maxLives = 3;
    private int lives;

    [Header("Footsteps")]
    [SerializeField] private float walkStepInterval = 0.5f;
    [SerializeField] private float runStepInterval = 0.3f;

    private float stepTimer;

    // Start is called before the first frame update
    void Start()
    {
        PAM = GetComponent<PlayerAudioManager>();
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        CM = transform.GetChild(0).GetComponent<CameraManager>();
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        PI = transform.GetComponent<PlayerInteraction>();
        try
        {
            Crickets = GameObject.Find("CricketAudio").GetComponent<AudioSource>();
        }
        catch (Exception e)
        {
            Debug.Log("No Crickets");
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
            TM = GameObject.Find("Tutorial").GetComponent<TutorialManager>();

        if (GameObject.Find("Scene Manager") != null)
            PInv = GameObject.Find("Scene Manager").GetComponent<PlayerInventory>();
        else
            Debug.LogWarning("Scene Manager not within the scene (can be ignored if testing)");

        if (GameObject.Find("PostProcessingVolume") != null)
            PostProOnOff = GameObject.Find("PostProcessingVolume").GetComponent<postProcessOnOff>();
        else
            Debug.LogWarning("PostProcessingVolume not within the scene (can be ignored if testing)");

        controller = GetComponent<CharacterController>();
        gC = GetComponent<GroundChecker>();

        respawnPos = transform.position;
        ThirdPerPlayerRotation = transform.rotation;

        lives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        // allows movement if cursor is hidden and controller is working
        if (!Cursor.visible && controller != null && !GM.GetTalking() && !GM.GetLoadingState() && dialogBox.activeInHierarchy == false)
        {
            HandleMovement();
        }
        else if (controller == null)
        {
            Debug.LogWarning("controller for the player is null");
        }
            

        // once falling below 100 on the y axis send player back up
        if (transform.position.y < -100)
        {
            Respawn();
        }

        if (CM != null)
        {
            if (!CM.GetCameraPerspective() && transform.rotation != ThirdPerPlayerRotation)
                transform.rotation = ThirdPerPlayerRotation;
        }
        else
            Debug.LogWarning("CM not set up correctly for player");

        if (Input.GetKeyDown(KeyCode.H))
        {
            //PlayerKilled();
            //PInv.ListInventory();
        }

        if (Input.GetKeyDown(KeyCode.F) && canHide && !hiding)
        {
            PostProOnOff.changeVignState();
            controller.enabled = false;
            //Debug.Log("hidingCode");
            prevPosition = this.transform.position;
            //this.transform.SetPositionAndRotation(hidingPos, this.transform.rotation);
            this.transform.position = hidingPos;
            hiding = true;
            //Debug.Log(prevPosition + " before hiding");

            if (TM != null && !TM.HasHidden())
            {
                StartCoroutine(TM.IsHiding());
            }
            else if (TM == null && SceneManager.GetActiveScene().buildIndex == 0)
                Debug.LogError("Tutorial for Dropping couldn't be found");
        }
        else if (Input.GetKeyDown(KeyCode.F) && hiding)
        {
            PostProOnOff.changeVignState();
            //Debug.Log(prevPosition + " after hiding");
            //this.transform.SetPositionAndRotation(prevPosition, this.transform.rotation);
            this.transform.position = prevPosition;
            hiding = false;
            controller.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.F) && canDrop && nearbyDropper != null)
        {
            nearbyDropper.FallableControl();
        }

        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            if (transform.position.y < 46.5)
            {
                PlayerKilled();
            } 
        }
    }


    public int GetLivesLeft()
    {
        return lives;
    }

    public void SetLives(int newLiveCount)
    {
        if (newLiveCount > maxLives)
            newLiveCount = maxLives;

        lives = newLiveCount;
    }


    //----------------------------------------------------------------------------------------------------------------------
    // Movement

    public bool CheckIfHiding()
    {
        return hiding;
    }

    // New movement script
    // Lowkey just used one of my movements with tweaks to appeal to this system
    private void HandleMovement()
    {
        // Ground Check
        bool grounded = gC.GroundCheck(transform);

        if (grounded && playerVelocity.y < 0)
            playerVelocity.y = -4f; // keeps player grounded

        float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * runMultiplier : speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (TM != null && !TM.HasRan())
            {
                StartCoroutine(TM.IsRunning());
            }
            else if (TM == null && SceneManager.GetActiveScene().buildIndex == 0)
                Debug.LogError("Tutorial for Player Movement couldn't be found");
        }

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);

        // Camera relative movement
        Vector3 camForward = CM.transform.forward;
        Vector3 camRight = CM.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        // Get direction of movement
        Vector3 moveDir = (camForward * input.z + camRight * input.x).normalized;

        // Air control
        float control = grounded ? 1f : airControl;

        // Velocity lerps, this can be changed later to 
        //  be a little less slippery, if we wish
        moveVelocity = Vector3.Lerp(moveVelocity, moveDir * targetSpeed, acceleration * control * Time.deltaTime);

        bool isMoving = moveVelocity.magnitude > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // Just handle jump here, one line
        if (Input.GetButtonDown("Jump") && grounded)
        {
            PAM.playJump();
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Falling velocity
        playerVelocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = moveVelocity + Vector3.up * playerVelocity.y;

        // Actually move thru player controller
        controller.Move(finalMove * Time.deltaTime);

        if (grounded && isMoving)
        {
            float interval = isRunning ? runStepInterval : walkStepInterval;

            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PAM.playFootstep();
                stepTimer = interval;
            }
        }
        else
        {
            // Reset so it doesn't instantly fire when you start moving again
            stepTimer = 0f;
        }

        // Copied tutorial checks, in case they're needed
        if (TM != null && !TM.HasMoved() && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            StartCoroutine(TM.IsMoving());
        }
        else if (TM == null && SceneManager.GetActiveScene().buildIndex == 0)
            Debug.LogError("Tutorial for Player Movement couldn't be found");
    }

    /// <summary>
    /// The Player's Movement: X and Z Axis Movement
    /// </summary>
    private void Movement()
    {
        

        // Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = speed * 1.75f; // running speed

            if (TM != null && !TM.HasRan())
            {
                StartCoroutine(TM.IsRunning());
            }
            else if (TM == null && SceneManager.GetActiveScene().buildIndex == 0)
                Debug.LogError("Tutorial for Player Movement couldn't be found");
        }
        else
        {
            currentSpeed = speed; // walking speed
        }

        Vector3 newPostion = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized; // sets new postion
        newPostion = transform.TransformDirection(newPostion); // changes it to move where the character is looking

        if (controller.enabled)
        {
            controller.Move(currentSpeed * Time.deltaTime * newPostion); // sends out the movement of the x and z axis
        }
        else if (!hiding)
        {
            Debug.Log("Controller not enabled");
        }
        
        if (TM != null && !TM.HasMoved() && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)) {
            StartCoroutine(TM.IsMoving());
        }
        else if (TM == null && SceneManager.GetActiveScene().buildIndex == 0)
            Debug.LogError("Tutorial for Player Movement couldn't be found");
    }

    /// <summary>
    /// The Player's Jump: Y Axis Movement
    /// </summary>
    private void Jump()
    {
        // reset the jump when landed
        if (gC.GroundCheck(transform) && playerVelocity.y < 0f)
            playerVelocity.y = 0f;

        if (jumpDebugging)
            Debug.Log("We are touching the ground: " + gC.GroundCheck(transform));

        // lets you jump when you hit space, the player is touching the ground, and only once every jumpRate
        if (Input.GetButtonDown("Jump") && gC.GroundCheck(transform))
        {
            //Debug.Log("We be jumping");
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravity); // calculates the jump height
        }

        playerVelocity.y += gravity * Time.deltaTime; // applys gravity moving them down over time

        // caps the amount of upward velocity at 6.5
        if (playerVelocity.y > 6.5)
        {
            playerVelocity.y = 6.5f;
        }

        if (jumpDebugging)
            Debug.Log("Vertical velocity is:" + playerVelocity.y);

        if (controller.enabled)
            controller.Move(playerVelocity * Time.deltaTime); // sets movement of the y axis
        else if (!hiding)
        {
            Debug.Log("Controller not enabled");
        }
    }

    //----------------------------------------------------------------------------------------------------------------------
    // Respawn and Health

    public void PlayerKilled()
    {
        
        if (Crickets != null)
            Crickets.Stop();

        if(GM.GetDeadState() == false)
            PAM.playRoar();
        GM.PlayerKilledState(true);
    }

    /// <summary>
    /// Respawns the player
    /// </summary>
    public void Respawn()
    {
        if (canHide)
            canHide = false;

        lives--;

        controller.enabled = false;
        transform.position = respawnPos;
        controller.enabled = true;

        if (lives <= 0)
        {
            LoadSceneManager lSM = GameObject.Find("Scene Manager").GetComponent<LoadSceneManager>();
            lSM.Restart();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hide") && !canHide)
        {
            if (hidingDebugging)
                Debug.Log("canHide");
            UI.swapHideState();
            hidingPos = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
            canHide = true;
        }
        if (other.CompareTag("drop") && !canDrop)
        {
            nearbyDropper = other.GetComponent<FallablePlatform>();
            if (dropDebugging)
                Debug.Log("canDrop");
            canDrop = true;
            //UI.CrosshairFToggle(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hide") && canHide)
        {
            if (hidingDebugging)
                Debug.Log("cantHide");
            UI.swapHideState();
            //hidingPos = new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z);
            canHide = false;
        }
        if (other.CompareTag("drop") && canDrop)
        {
            nearbyDropper = null;
            if (dropDebugging)
                Debug.Log("canDrop");
            canDrop = false;
            //UI.CrosshairFToggle(false);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (controller.enabled)
        {
            if (hit.gameObject.CompareTag("Wyrm"))
            {
                PlayerKilled();

                if (hit.gameObject.TryGetComponent<WyrmSoundManager>(out WyrmSoundManager WSM))
                    WSM.BiteSound();
            }
        }
    }

    public void setNewRespawn(Vector3 newRespawnPos) 
    {
        respawnPos = newRespawnPos;
    }

    public void Unstuck() //Respawn without taking a life
    {
        if (canHide)
            canHide = false;

        controller.enabled = false;
        transform.position = respawnPos;
        controller.enabled = true;

        if (lives <= 0)
        {
            LoadSceneManager lSM = GameObject.Find("Scene Manager").GetComponent<LoadSceneManager>();
            lSM.Restart();
        }
    }
}



