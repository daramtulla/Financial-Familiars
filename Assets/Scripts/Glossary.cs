using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Glossary : MonoBehaviour
{
    public GameObject glossaryScreen;

    public Transform glossaryContent;
    public GameObject termPrefab;

    private List<Term> termList = new List<Term>();

    void Start()
    {
        glossaryScreen.SetActive(false);
        LoadTermFromCSV();
        UpdateGlossaryUI();
    }

    private void LoadTermFromCSV()
    {
        termList = CSVLoader.LoadTermsFromCSV("Glossary.csv");
    }

    public void UpdateGlossaryUI()
    {
        // Clear existing UI items
        foreach (Transform child in glossaryContent)
        {
            Destroy(child.gameObject);
        }

        // Populate the inventory UI
        foreach (Term term in termList)
        {
            GameObject newTerm = Instantiate(termPrefab, glossaryContent);

            // Find TextMeshPro components directly in the prefab
            TextMeshProUGUI[] texts = newTerm.GetComponentsInChildren<TextMeshProUGUI>();
            
            texts[0].text = term.idNumber.ToString();
            texts[1].text = term.word;
            texts[2].text = term.definition;
        }
    }

    public void OpenGlossary()
    {
        glossaryScreen.SetActive(true);
    }

    public void CloseGlossary()
    {
        glossaryScreen.SetActive(false);
    }

}
