using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Glossary : MonoBehaviour
{
    public GameObject glossaryScreen;
    public GameObject pauseMenu;

    public GameManager gameManager;

    public Transform glossaryContent;
    public GameObject termPrefab;

    private List<Term> termList = new List<Term>();
    private List<GameObject> gameObjectTermList = new List<GameObject>();

    [SerializeField] TMP_InputField textInputField;

    void Start()
    {
        glossaryScreen.SetActive(false);
        LoadTermFromCSV();
        UpdateGlossaryUI();
    }

    private void Update()
    {
        //press G to open glossary
        if (Input.GetKeyDown(KeyCode.G) && !textInputField.isFocused)
        {
            if (glossaryScreen.activeSelf)
            {
                if (Time.timeScale < 0.1f) //Used 0.1 to account for floating point errors
                {
                    Time.timeScale = 1.0f;
                }
                CloseGlossary();
                if (pauseMenu.activeSelf)
                {
                    gameManager.PauseGame();
                }
            }
            else
            {
                if(Time.timeScale > 0.1f) //Used 0.1 to account for floating point errors
                {
                    Time.timeScale = 0.0f;
                }
                OpenGlossary();
            }
        }
    }

    private void LoadTermFromCSV()
    {
        termList = CSVLoader.LoadTermsFromCSV("Glossary.csv");
    }

    public void UpdateGlossaryUI()
    {
        gameObjectTermList.Clear();
        // Clear existing UI items
        foreach (Transform child in glossaryContent)
        {
            Destroy(child.gameObject);
        }

        // Populate the inventory UI
        foreach (Term term in termList)
        {
            GameObject newTerm = Instantiate(termPrefab, glossaryContent);
            gameObjectTermList.Add(newTerm);

            // Find TextMeshPro components directly in the prefab
            TextMeshProUGUI[] texts = newTerm.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].text = term.idNumber.ToString();
            texts[1].text = term.word;
            texts[2].text = term.definition;
        }
    }

    public void SearchForWords(string input)
    {
        for (int i = 0; i < gameObjectTermList.Count; i++)
        {
            TextMeshProUGUI[] texts = gameObjectTermList[i].GetComponentsInChildren<TextMeshProUGUI>();
            if (!texts[1].text.ToLower().Contains(input))
            {
                gameObjectTermList[i].SetActive(false);
            }
            else
            {
                gameObjectTermList[i].SetActive(true);
            }
        }
    }

    public void OpenGlossary()
    {
        glossaryScreen.SetActive(true);
    }

    public void CloseGlossary()
    {
        if (!pauseMenu.activeSelf)
        {
            if (Time.timeScale < 0.1f) //Used 0.1 to account for floating point errors
            {
                Time.timeScale = 1.0f;
            }
        }
        glossaryScreen.SetActive(false);
    }

}