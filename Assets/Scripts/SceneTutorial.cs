using UnityEngine;
using UnityEngine.UI;

public class SceneTutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorialPopup;
    [SerializeField] Button closeButton;
    [SerializeField] string tutorialKey = "SceneStartTutorial";
    [SerializeField] JSONDatabaseOperations db;

    void Start()
    {
        if (!db.currentPlayer.completedTutorials.Contains(tutorialKey))
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
        db.currentPlayer.completedTutorials.Add(tutorialKey);
        db.SaveData();
        tutorialPopup.SetActive(false);
    }
}
