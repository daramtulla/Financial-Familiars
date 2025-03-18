using TMPro;
using UnityEngine;

public class LoansManager : MonoBehaviour
{
    [SerializeField] GameObject loansmenu;

    [SerializeField] TMP_InputField textInputField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && !textInputField.isFocused)
        {
            HandleThis();
        }
    }
    void HandleThis()
    {
        if (!loansmenu.activeInHierarchy)
        {
            loansmenu.SetActive(true);
        }
        else
        {
            loansmenu.SetActive(false);
        }

    }
}
