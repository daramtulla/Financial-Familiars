using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private Boolean followMode;

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
        mainCam.transform.position = new Vector3(camX, camY, camZ);
        mainCam.transform.rotation = Quaternion.Euler(60, 0, 0);
        followMode = false;
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (followMode)
            {
                mainCam.transform.position = new Vector3(camX, camY, camZ);
                followMode = !followMode;
            }
            else
            {
                followMode = !followMode;
            }
        }

    }
}
