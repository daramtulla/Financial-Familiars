using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementController : MonoBehaviour
{
    //public static MovementController instance;

    [SerializeField] Boolean debug;

    //For vertical and horizontal movement
    public Rigidbody playerModel;
    public float movementSpeed;
    private Vector3 input;
    [SerializeField] GameObject upperStep;
    [SerializeField] GameObject lowerStep;
    public float stepHeight;

    void Awake()
    {
        //TODO fix to include macro for playerheight
        upperStep.transform.position = new Vector3(playerModel.transform.position.x, upperStep.transform.position.y + stepHeight, playerModel.transform.position.z);

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
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");

        //Needed so diagonal movement isn't faster than horizontal or vertical movement
        input.Normalize();

        playerModel.linearVelocity = new Vector3(input.x * movementSpeed, playerModel.linearVelocity.y, input.z * movementSpeed);

        isClimbable();
    }

    void isClimbable()
    {
        //May need to adjust raycast distance depending on player model dimensions. TODO add radius of model automatically
        if (Physics.Raycast(lowerStep.transform.position, transform.TransformDirection(Vector3.forward), .6f))
        {
            if (debug) print("LowerHit");
            if (!Physics.Raycast(upperStep.transform.position, transform.TransformDirection(Vector3.forward), .7f) && isMoving())
            {
                if (debug) print("UpperMiss");
                playerModel.linearVelocity = new Vector3(0f, 5f, 0f);
            }
            else
            {
                if (debug) print("UpperHit");
            }
        }

        if (Physics.Raycast(lowerStep.transform.position, transform.TransformDirection(Vector3.back), .6f))
        {
            if (debug) print("LowerHit");
            if (!Physics.Raycast(upperStep.transform.position, transform.TransformDirection(Vector3.back), .7f) && isMoving())
            {
                if (debug) print("UpperMiss");
                playerModel.linearVelocity = new Vector3(0f, 5f, 0f);
            }
            else
            {
                if (debug) print("UpperHit");
            }
        }

        if (Physics.Raycast(lowerStep.transform.position, transform.TransformDirection(Vector3.left), .6f))
        {
            if (debug) print("LowerHit");
            if (!Physics.Raycast(upperStep.transform.position, transform.TransformDirection(Vector3.left), .7f) && isMoving())
            {
                if (debug) print("UpperMiss");
                playerModel.linearVelocity = new Vector3(0f, 5f, 0f);
            }
            else
            {
                if (debug) print("UpperHit");
            }
        }

        if (Physics.Raycast(lowerStep.transform.position, transform.TransformDirection(Vector3.right), .6f))
        {
            if (debug) print("LowerHit");
            if (!Physics.Raycast(upperStep.transform.position, transform.TransformDirection(Vector3.right), .7f) && isMoving())
            {
                if (debug) print("UpperMiss");
                playerModel.linearVelocity = new Vector3(0f, 5f, 0f);
            }
            else
            {
                if (debug) print("UpperHit");
            }
        }
    }

    Boolean isMoving()
    {
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            return false;
        }
        else
        {
            return true;
        }
    }



}
