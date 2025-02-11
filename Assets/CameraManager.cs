using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public Boolean followMode;

    //Fixed Cam. Inludes position for every scene. Rotation is always looking 60 degrees down
    [SerializeField] GameObject mainCam;
    public float camX;
    public float camY;
    public float camZ;

    //Follow Cam
    [SerializeField] Vector3 followDistance;
    public float followSpeed;
    public GameObject playerModel;
    private Vector3 currentVelocity = Vector3.zero;

    void Awake()
    {
        followDistance = new Vector3(camX, camY, camZ);
        mainCam.transform.position = new Vector3(camX, camY, camZ);
        mainCam.transform.rotation = Quaternion.Euler(60, 0, 0);

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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    //TODO Implement zoom in feature when interacting with objects

    // Update is called once per frame
    void Update()
    {
        if (followMode)
        {
            Vector3 camPos = playerModel.transform.position + followDistance;
            mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position, camPos, ref currentVelocity, followSpeed);
        }
    }
}
