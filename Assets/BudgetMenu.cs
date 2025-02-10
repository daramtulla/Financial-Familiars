using UnityEngine;

public class BudgetMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject budgetPanel;
    void Start()
    {
        budgetPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //press B to open budget
        if(Input.GetKeyDown(KeyCode.B)){
            ToggleBudgetMenu();
        }
        
    }

    public void ToggleBudgetMenu(){
        budgetPanel.SetActive(!budgetPanel.activeSelf);
    }

    public void CloseMenu(){
        budgetPanel.SetActive(false);

    }
}
