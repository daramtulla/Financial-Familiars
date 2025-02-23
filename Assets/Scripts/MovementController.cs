using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementController : MonoBehaviour
{
    [SerializeField] Boolean debug;

    //For vertical and horizontal movement
    public Rigidbody playerModel;
    public float movementSpeed;
    private Vector3 input;
    [SerializeField] GameObject upperStep;
    [SerializeField] GameObject lowerStep;
    public Vector3 stepSpeed;
    public float stepHeight;
    public static Boolean interacting;

    void Awake()
    {
        upperStep.transform.position = new Vector3(playerModel.transform.position.x, upperStep.transform.position.y + stepHeight, playerModel.transform.position.z);

        interacting = false;

        /*
        //Only ever should be one instance. Destroys prior instance if already made
        if (instance != null)
        {
            Destroy(gameObject);
        }

        //Create new instance
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        */
    }



    // Update is called once per frame
    void Update()
    {
        if (!interacting)
        {
            input.x = Input.GetAxis("Horizontal");
            input.z = Input.GetAxis("Vertical");

            //Needed so diagonal movement isn't faster than horizontal or vertical movement
            input.Normalize();

            playerModel.linearVelocity = new Vector3(input.x * movementSpeed, playerModel.linearVelocity.y, input.z * movementSpeed);

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
