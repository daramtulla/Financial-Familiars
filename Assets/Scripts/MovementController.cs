using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementController : MonoBehaviour
{
    [SerializeField] Boolean debug;

    //For vertical and horizontal movement
    public Rigidbody playerModel;
    public Animator playerAnimator;
    public float movementSpeed;
    public float rotationSpeed;
    private Vector3 input;
    [SerializeField] GameObject upperStep;
    [SerializeField] GameObject lowerStep;
    public Vector3 stepSpeed;
    public float stepHeight;
    public static Boolean interacting;
    [SerializeField] GameObject startingPosition;

    [SerializeField] TMP_InputField textInputField;

    void Awake()
    {
        transform.position = startingPosition.transform.position;
        transform.rotation = startingPosition.transform.rotation;
        upperStep.transform.position = new Vector3(playerModel.transform.position.x, upperStep.transform.position.y + stepHeight, playerModel.transform.position.z);

        interacting = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !textInputField.isFocused)
        {
            transform.position = startingPosition.transform.position;
            transform.rotation = startingPosition.transform.rotation;
        }
        if (!interacting)
        {
            input.x = Input.GetAxis("Horizontal");
            input.z = Input.GetAxis("Vertical");

            //Needed so diagonal movement isn't faster than horizontal or vertical movement
            input.Normalize();

            playerModel.linearVelocity = new Vector3(input.x * movementSpeed, playerModel.linearVelocity.y, input.z * movementSpeed);
            playerAnimator.SetFloat("Speed_f", Math.Abs((input.x * movementSpeed)) + Math.Abs((input.z * movementSpeed)));

            isClimbable();

            moveDirection();

            stopRotation();
        }
    }

    public void PlayInteractAnimation()
    {
        playerAnimator.SetTrigger("Interact_trig");
    }

    //unstuck button
    public void unstick()
    {
        playerModel.transform.position = startingPosition.transform.position;
        playerModel.transform.rotation = startingPosition.transform.rotation;
    }

    void moveDirection()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            playerModel.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(input, Vector3.up), rotationSpeed);
        }
    }

    void stopRotation()
    {
        if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.D))
        {
            playerModel.angularVelocity = Vector3.zero;

            isClimbable();
        }
    }

    void isClimbable()
    {
        //May need to adjust raycast distance depending on player model dimensions TODO

        //Figure out which direction the player is moving and trigger the raycast in that direction
        if (Physics.Raycast(lowerStep.transform.position, transform.TransformDirection(Vector3.forward), .6f) && Input.GetKey(KeyCode.W))
        {
            if (debug) print("LowerHit");
            if (!Physics.Raycast(upperStep.transform.position, transform.TransformDirection(Vector3.forward), .7f))
            {
                if (debug) print("UpperMiss");
                playerModel.linearVelocity = stepSpeed;
            }
            else
            {
                if (debug) print("UpperHit");
            }
        }

        if (Physics.Raycast(lowerStep.transform.position, transform.TransformDirection(Vector3.back), .6f) && Input.GetKey(KeyCode.S))
        {
            if (debug) print("LowerHit");
            if (!Physics.Raycast(upperStep.transform.position, transform.TransformDirection(Vector3.back), .7f))
            {
                if (debug) print("UpperMiss");
                playerModel.linearVelocity = stepSpeed;
            }
            else
            {
                if (debug) print("UpperHit");
            }
        }

        if (Physics.Raycast(lowerStep.transform.position, transform.TransformDirection(Vector3.left), .6f) && Input.GetKey(KeyCode.A))
        {
            if (debug) print("LowerHit");
            if (!Physics.Raycast(upperStep.transform.position, transform.TransformDirection(Vector3.left), .7f))
            {
                if (debug) print("UpperMiss");
                playerModel.linearVelocity = stepSpeed;
            }
            else
            {
                if (debug) print("UpperHit");
            }
        }

        if (Physics.Raycast(lowerStep.transform.position, transform.TransformDirection(Vector3.right), .6f) && Input.GetKey(KeyCode.D))
        {
            if (debug) print("LowerHit");
            if (!Physics.Raycast(upperStep.transform.position, transform.TransformDirection(Vector3.right), .7f))
            {
                if (debug) print("UpperMiss");
                playerModel.linearVelocity = stepSpeed;
            }
            else
            {
                if (debug) print("UpperHit");
            }
        }
    }
}
