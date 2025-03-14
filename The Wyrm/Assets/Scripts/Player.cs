using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    [Header("Pick Up")]
    public float reachDistance = 3f; // How close you have to be
    private GameObject currentItem;
    public bool isHeld;

    [Header("Debugger")]
    [Tooltip("Turns on Jump Debugging"), SerializeField]
    private bool jumpDebugging;

    // Start is called before the first frame update
    void Start()
    {
        isHeld = false;
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

    }

    //----------------------------------------------------------------------------------------------------------------------
    // Movement

    /// <summary>
    /// The Player's Movement: X and Z Axis Movement
    /// </summary>
    private void Movement()
    {
        // Running
        if (isHeld)
        {
            currentSpeed = speed / 2;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
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

        if (isHeld == true)
        {
            currentSpeed = speed / 2;
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

