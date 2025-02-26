using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Material morningSky;
    public Material noonSky;
    public Material eveningSky;
    public Material nightSky;

    private Material[] skyboxes;
    private int currentSkyboxIndex = 0;

    void Start()
    {
        skyboxes = new Material[] {morningSky, noonSky, eveningSky, nightSky};

        RenderSettings.skybox = skyboxes[currentSkyboxIndex];
        DynamicGI.UpdateEnvironment();
    }

    void Update()
    {
        // press [ to make the sky timetravel
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxes.Length;

            RenderSettings.skybox = skyboxes[currentSkyboxIndex];

            DynamicGI.UpdateEnvironment();
        }
    }
}
