using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEditor.U2D;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private GameManager GM;
    private CameraManager CM;
    private UIManager UI;
    private PlayerInteraction PI;

    private CharacterController controller;
    private GroundChecker gC;

    private Vector3 playerVelocity;
    private Vector3 respawnPos;

    [Header("Area")]
    [Tooltip("What area the player is in"), Range(1, 4), SerializeField]
    private int currentAreaNum;

    [Header("Movement")]
    [Tooltip("How fast the player moves"), Min(0), SerializeField]
    private float speed = 3;
    [SerializeField]
    private float currentSpeed;
    [Tooltip("How high the player jumps"), Min(0), SerializeField]
    private float jumpForce = 1;
    [Tooltip("What the gravity on the player is"), SerializeField]
    private float gravity = -9.81f;

    [Header("Inventory")]
    [SerializeField]
    private GameObject relic;
    private GameObject[] inventory = new GameObject[3];
    private int inventoryIndex = 0;

    [Header("Debugger")]
    [Tooltip("Turns on Jump Debugging"), SerializeField]
    private bool jumpDebugging;
    

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        CM = transform.GetChild(0).GetComponent<CameraManager>();
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();

        controller = GetComponent<CharacterController>();
        gC = GetComponent<GroundChecker>();

        respawnPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // allows movement if cursor is hidden and controller is working
        if (!Cursor.visible && controller != null)
        {
            Movement(); // control of the x and z axis

            if (gC != null)
                Jump(); // control of the y axis
            else
                Debug.LogWarning("gC for the jumping is null");
        }
        else if (controller == null)
            Debug.LogWarning("controller for the player is null");

        // once falling below 100 on the y axis send player back up
        if (transform.position.y < -100)
        {
            Respawn();
        }

        if (CM != null)
        {
            if (!CM.GetCameraPerspective() && transform.rotation != new Quaternion(0, 0, 0, 0))
                transform.rotation = new Quaternion(0,0, 0, 0);
        }
        else
            Debug.LogWarning("CM not set up correctly for player");

        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveInventory();
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
            currentSpeed = speed * 2; // running speed
        }
        else
        {
            currentSpeed = speed; // walking speed
        }

        Vector3 newPostion = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized; // sets new postion
        newPostion = transform.TransformDirection(newPostion); // changes it to move where the character is looking
        controller.Move(currentSpeed * Time.deltaTime * newPostion); // sends out the movement of the x and z axis
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
    // Inventory
    
    public void AddToInventory(GameObject Item)
    {
        if (Item != null && inventoryIndex <= 2)
        {
            Debug.Log("heeeeeeeyyyyy I am workinnnnngggg");

            UI.UpdateItemCount(1);
            inventory[inventoryIndex] = Item;
            inventoryIndex++;

            if (inventoryIndex == 2)
            {
                LoadSceneManager lSM = GameObject.Find("Game Manager").GetComponent<LoadSceneManager>();
                if (1 <= SceneManager.sceneCountInBuildSettings && lSM != null)
                    lSM.LoadScene(1);
            }
        }
    }

    public void RemoveInventory()
    {
        if (inventoryIndex > 0)
        {
            Debug.Log("See told you were are here");

            UI.UpdateItemCount(-1);
            inventoryIndex--;
            inventory[inventoryIndex] = null;

            Debug.Log("For you");
            Instantiate(relic, new Vector3(transform.position.x + 1, transform.position.y - (transform.position.y / 2) - .25f, transform.position.z), transform.rotation);
        }
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
        controller.enabled = false;
        transform.position = respawnPos;
        controller.enabled = true;
    }

}

