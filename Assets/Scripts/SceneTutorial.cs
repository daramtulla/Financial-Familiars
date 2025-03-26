using UnityEngine;
using UnityEngine.UI;

public class SceneTutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorialPopup;
    [SerializeField] Button closeButton;
    [SerializeField] string tutorialKey = "SceneStartTutorial";

    void Start()
    {
        if (PlayerPrefs.GetInt(tutorialKey, 0) == 0)
        {
            ShowTutorial();
        }
    }

    void ShowTutorial()
    {
        tutorialPopup.SetActive(true);
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(CloseTutorial);

    }
    void CloseTutorial()
    {
        PlayerPrefs.SetInt(tutorialKey, 1);
        PlayerPrefs.Save();
        tutorialPopup.SetActive(false);
    }
}
