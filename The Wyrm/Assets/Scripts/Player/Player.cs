using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditor.U2D;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    [Header("Debugger")]
    [Tooltip("Turns on Jump Debugging"), SerializeField]
    private bool jumpDebugging;
    

    [SerializeField] private bool hiding = false;
    [SerializeField] private bool canHide = false;
    Vector3 prevPosition;
    Vector3 hidingPos;

    // Start is called before the first frame update
    void Start()
    {
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
        if (!Cursor.visible && controller != null && !hiding)
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
        if (Input.GetKeyDown(KeyCode.F) && canHide && !hiding)
        {
            
                //Debug.Log("hidingCode");
                prevPosition = this.transform.position;
                this.transform.position = new Vector3(hidingPos.x, hidingPos.y, hidingPos.z);
                hiding = true;
                Debug.Log(prevPosition + " before hiding");
            

        }
        else if (Input.GetKeyDown(KeyCode.F) && hiding)
        {
            Debug.Log(prevPosition + " after hiding");
            this.transform.position = prevPosition;
            hiding = false;
            
        }

        if (CM != null)
        {
            if (!CM.GetCameraPerspective() && transform.rotation != new Quaternion(0, 0, 0, 0))
                transform.rotation = new Quaternion(0,0, 0, 0);
        }
        else
            Debug.LogWarning("CM not set up correctly for player");

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
    // Respawn and Health



    /// <summary>
    /// Respawns the player
    /// </summary>
    public void Respawn()
    {
        controller.enabled = false;
        transform.position = respawnPos;
        controller.enabled = true;
    }



    private void OnTriggerStay(Collider other) //Feel free to change this for more efficiency most important part if the dropper object and the comparisions and calls involving it
    {
        Drop dropper = other.GetComponent<Drop>();
        //Debug.Log("in trigger" + other.tag);
        if (other.tag == "drop" && Input.GetKeyDown(KeyCode.F) && !dropper.down)//May be best to change these to a boolean true false change then check input elsewhere
        {
            dropper.dropThis();
        }
        else if (other.tag == "drop" && Input.GetKeyDown(KeyCode.F) && dropper.down)
        {
            dropper.upThis();
        }
        

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hide" && !canHide)
        {
            Debug.Log("canHide");
            hidingPos = new Vector3(other.transform.position.x, other.transform.position.y + 1.5f, other.transform.position.z);
            canHide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hide" && canHide)
        {
            Debug.Log("cantHide");
            //hidingPos = new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z);
            canHide = false;
        }
    }

}

