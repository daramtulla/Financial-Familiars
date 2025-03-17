using System;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Material morningSky;
    public Material noonSky;
    public Material eveningSky;
    public Material nightSky;

    private Material[] skyboxes;
    private int currentSkyboxIndex;
    [SerializeField] Boolean debug;

    [SerializeField] JSONDatabaseOperations db;

    void Awake()
    {
        currentSkyboxIndex = db.currentPlayer.cycleNum;
    }

    void Start()
    {
        skyboxes = new Material[] { morningSky, noonSky, eveningSky }; //, nightSky 

        RenderSettings.skybox = skyboxes[currentSkyboxIndex];
        DynamicGI.UpdateEnvironment();
    }

    void Update()
    {
        // press [ to make the sky timetravel
        /*
        if (debug && Input.GetKeyDown(KeyCode.LeftBracket))
        {
            currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxes.Length;

            RenderSettings.skybox = skyboxes[currentSkyboxIndex];

            DynamicGI.UpdateEnvironment();
        }
        */

        switch (db.currentPlayer.cycleNum)
        {
            case 1:
                SetMorning();
                break;
            case 2:
                SetNoon();
                break;
            case 3:
                SetEvening();
                break;
            default:
                Debug.Log("Invalid cycleNum");
                break;
        }
    }

    //Since there is only 3 game phases as of now, not using nightSky
    public void SetMorning()
    {
        if (db.currentPlayer.cycleNum != 0)
        {
            Debug.Log("Incorrect Phase");
        }

        currentSkyboxIndex = db.currentPlayer.cycleNum;

        RenderSettings.skybox = skyboxes[currentSkyboxIndex];
        DynamicGI.UpdateEnvironment();
    }

    public void SetNoon()
    {
        if (db.currentPlayer.cycleNum != 1)
        {
            Debug.Log("Incorrect Phase");
        }

        currentSkyboxIndex = db.currentPlayer.cycleNum;

        RenderSettings.skybox = skyboxes[currentSkyboxIndex];
        DynamicGI.UpdateEnvironment();
    }

    public void SetEvening()
    {
        if (db.currentPlayer.cycleNum != 2)
        {
            Debug.Log("Incorrect Phase");
        }

        currentSkyboxIndex = db.currentPlayer.cycleNum;

        RenderSettings.skybox = skyboxes[currentSkyboxIndex];
        DynamicGI.UpdateEnvironment();
    }
}
