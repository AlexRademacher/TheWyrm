using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager GM;
    private CameraManager CM;
    private UIManager UI;
    private PlayerInteraction PI;
    private PlayerInventory PInv;
    private postProcessOnOff PostProOnOff;

    private CharacterController controller;
    private GroundChecker gC;

    private Vector3 playerVelocity;
    private Vector3 respawnPos;
    private Quaternion ThirdPerPlayerRotation;

    [Header("Movement")]
    [Tooltip("How fast the player moves"), Min(0), SerializeField]
    private float speed = 3;
    [SerializeField]
    private float currentSpeed;
    [Tooltip("How high the player jumps"), Min(0), SerializeField]
    private float jumpForce = 1;
    [Tooltip("What the gravity on the player is"), SerializeField]
    private float gravity = -9.81f;

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
    Drop nearbyDropper;
        

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        CM = transform.GetChild(0).GetComponent<CameraManager>();
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        PI = transform.GetComponent<PlayerInteraction>();
        if (GameObject.Find("Scene Manager") != null)
            PInv = GameObject.Find("Scene Manager").GetComponent<PlayerInventory>();
        else
            Debug.LogWarning("Scene Manager not within the scene (can be ignored if testing)");
        PostProOnOff = GameObject.Find("PostProcessingVolume").GetComponent<postProcessOnOff>();

        controller = GetComponent<CharacterController>();
        gC = GetComponent<GroundChecker>();

        respawnPos = transform.position;
        ThirdPerPlayerRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // allows movement if cursor is hidden and controller is working
        if (!Cursor.visible && controller != null && !GM.GetTalking() && !GM.GetLoadingState())
        {
            Movement(); // control of the x and z axis

            if (gC != null)
                Jump(); // control of the y axis
            else
                Debug.LogWarning("gC for the jumping is null");
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            //PI.DropItem();
        }

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
            Debug.Log(prevPosition + " before hiding");


        }
        else if (Input.GetKeyDown(KeyCode.F) && hiding)
        {
            PostProOnOff.changeVignState();
            Debug.Log(prevPosition + " after hiding");
            //this.transform.SetPositionAndRotation(prevPosition, this.transform.rotation);
            this.transform.position = prevPosition;
            hiding = false;
            controller.enabled = true;

        }

        if (Input.GetKeyDown(KeyCode.F) && canDrop && nearbyDropper != null)
        {
            if (!nearbyDropper.down)
                nearbyDropper.dropThis();
            else if (nearbyDropper.down)
                nearbyDropper.upThis();

        }


    }

    //----------------------------------------------------------------------------------------------------------------------
    // Movement

    /// <summary>
    /// The Player's Movement: X and Z Axis Movement
    /// </summary>
    private void Movement()
    {
        // Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = speed * 1.75f; // running speed
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
        else
        {
            Debug.Log("Controller not enabled");
        }
        
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

        controller.Move(playerVelocity * Time.deltaTime); // sets movement of the y axis
    }

    //----------------------------------------------------------------------------------------------------------------------
    // Respawn and Health

    public void PlayerKilled()
    {
        GM.PlayerKilledState(true);
    }

    /// <summary>
    /// Respawns the player
    /// </summary>
    public void Respawn()
    {
        if (canHide)
            canHide = false;

        controller.enabled = false;
        transform.position = respawnPos;
        controller.enabled = true;
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
            nearbyDropper = other.GetComponent<Drop>();
            if (dropDebugging)
                Debug.Log("canDrop");
            canDrop = true;
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
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (controller.enabled)
        {
            if (hit.gameObject.CompareTag("Wyrm"))
            {
                PlayerKilled();
            }
        }
    }
}



