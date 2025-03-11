using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class EmployeeManager : MonoBehaviour
{

    public GameObject employeePanel;
    public Transform employeeContent;
    public GameObject employeeItemPrefab;
    public JSONDatabaseOperations db;
    private List<Employee> employees;
    void Start()
    {
        employeePanel.SetActive(false);
        UpdateEmployeeUI();
    }


    public void ToggleMenu()
    {
        employeePanel.SetActive(!employeePanel.activeSelf);
        if (employeePanel.activeSelf)
        {
            UpdateEmployeeUI();
        }
    }


    public void UpdateEmployeeUI()
    {
        // Clear existing
        foreach (Transform child in employeeContent)
        {
            Destroy(child.gameObject);
        }

        foreach (Employee emp in db.currentPlayer.unemployedEmployees)
        {
            GameObject newEmployee = Instantiate(employeeItemPrefab, employeeContent);

            // Get text components
            TextMeshProUGUI[] texts = newEmployee.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = emp.name;
            texts[1].text = emp.position;
            texts[2].text = "$" + emp.salary.ToString();
            texts[3].text = emp.personality;
            texts[4].text = emp.benefits;
            texts[5].text = emp.qualifications;
            Debug.Log("ADDING EMPLOYEE" + emp.name + "," + emp.position + ", " + emp.salary + ", " + emp.personality + ", " + emp.benefits + "," + emp.qualifications);
            //todo: button functionality
        }
    }
}
