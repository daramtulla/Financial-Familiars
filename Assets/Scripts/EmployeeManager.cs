using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class EmployeeManager : MonoBehaviour
{
    public SoundManager soundManager;

    public GameObject choicePanel;

    //todo
    public GameObject hirePanel;
    public GameObject firePanel;

    public Transform employeeContent;
    public GameObject employeeItemPrefab;

    public Transform employeeManagerContent;
    public GameObject employeeManagerPrefab;


    public JSONDatabaseOperations db;
    private List<Employee> employees;
    void Start()
    {
        choicePanel.SetActive(false);
        hirePanel.SetActive(false);
        firePanel.SetActive(false);
        UpdateEmployeeUI();
        UpdateEmployeeManagerUI();
    }


    public void ToggleMenu()
    {
            choicePanel.SetActive(!choicePanel.activeSelf);
            
            UpdateEmployeeUI();
            UpdateEmployeeManagerUI();
    }

    public void UpdateEmployeeManagerUI()
    { 
        // Clear existing
        foreach (Transform child in employeeManagerContent)
        {
            Destroy(child.gameObject);
        }

        // Load placeholder image
        string placeholderPath = Path.Combine(Application.streamingAssetsPath, "placeholder.png");
        Texture2D placeholderTexture = null;
        if (File.Exists(placeholderPath))
        {
            byte[] placeholderData = File.ReadAllBytes(placeholderPath);
            placeholderTexture = new Texture2D(2, 2); // Create a temporary texture
            placeholderTexture.LoadImage(placeholderData); // Load the image into the texture
        }
        else
        {
            Debug.LogWarning("Placeholder image not found: " + placeholderPath);
        }

        foreach (Employee emp in db.currentPlayer.employees)
        {
            GameObject newEmployee = Instantiate(employeeManagerPrefab, employeeManagerContent);

            // Get text components
            TextMeshProUGUI[] texts = newEmployee.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = emp.name;
            texts[1].text = emp.position;
            texts[2].text = "$" + emp.salary.ToString("N2") + " / day";
            texts[3].text = emp.personality;
            texts[4].text = emp.benefits;
            texts[5].text = emp.qualifications;

            // Load image if available
            string imagePath = Path.Combine(Application.streamingAssetsPath, emp.imageSource);
            if (File.Exists(imagePath))
            {
                byte[] imageData = File.ReadAllBytes(imagePath);
                Texture2D texture = new Texture2D(2, 2); // Create a temporary texture
                texture.LoadImage(imageData); // Load the image into the texture

                // Get the second image (the child image you want to modify)
                Image[] imageComponents = newEmployee.GetComponentsInChildren<Image>();
                if (imageComponents.Length > 1) // Make sure we have at least two images
                {
                    Image targetImage = imageComponents[1]; // Second image in the hierarchy
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    targetImage.sprite = sprite; // Set the sprite to the second image
                }
            }
            else
            {
                // Use placeholder if the image is not found
                Image[] imageComponents = newEmployee.GetComponentsInChildren<Image>();
                if (imageComponents.Length > 1 && placeholderTexture != null)
                {
                    Image targetImage = imageComponents[1]; // Second image in the hierarchy
                    Sprite placeholderSprite = Sprite.Create(placeholderTexture, new Rect(0, 0, placeholderTexture.width, placeholderTexture.height), new Vector2(0.5f, 0.5f));
                    targetImage.sprite = placeholderSprite; // Set the placeholder image to the second image
                }
                else
                {
                    Debug.LogWarning("No valid image or placeholder found for employee: " + emp.name);
                }
            }

            Button hireButton = newEmployee.transform.Find("HireButton").GetComponent<Button>();
            hireButton.onClick.AddListener(() => fireEmployee(emp.id));
            hireButton.onClick.AddListener(soundManager.ButtonClickSound);

            Debug.Log("REMOVING EMPLOYEE: " + emp.name + ", " + emp.position + ", " + emp.salary + ", " + emp.personality + ", " + emp.benefits + ", " + emp.qualifications);
        }
    }
    public void UpdateEmployeeUI()
    {
        // Clear existing
        foreach (Transform child in employeeContent)
        {
            Destroy(child.gameObject);
        }

        // Load placeholder image
        string placeholderPath = Path.Combine(Application.streamingAssetsPath, "placeholder.png");
        Texture2D placeholderTexture = null;
        if (File.Exists(placeholderPath))
        {
            byte[] placeholderData = File.ReadAllBytes(placeholderPath);
            placeholderTexture = new Texture2D(2, 2); // Create a temporary texture
            placeholderTexture.LoadImage(placeholderData); // Load the image into the texture
        }
        else
        {
            Debug.LogWarning("Placeholder image not found: " + placeholderPath);
        }

        foreach (Employee emp in db.currentPlayer.unemployed)
        {
            GameObject newEmployee = Instantiate(employeeItemPrefab, employeeContent);

            // Get text components
            TextMeshProUGUI[] texts = newEmployee.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = emp.name;
            texts[1].text = emp.position;
            texts[2].text = "$" + emp.salary.ToString("N2") + " / day";
            texts[3].text = emp.personality;
            texts[4].text = emp.benefits;
            texts[5].text = emp.qualifications;

            // Load image if available
            string imagePath = Path.Combine(Application.streamingAssetsPath, emp.imageSource);
            if (File.Exists(imagePath))
            {
                byte[] imageData = File.ReadAllBytes(imagePath);
                Texture2D texture = new Texture2D(2, 2); // Create a temporary texture
                texture.LoadImage(imageData); // Load the image into the texture

                // Get the second image (the child image you want to modify)
                Image[] imageComponents = newEmployee.GetComponentsInChildren<Image>();
                if (imageComponents.Length > 1) // Make sure we have at least two images
                {
                    Image targetImage = imageComponents[1]; // Second image in the hierarchy
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    targetImage.sprite = sprite; // Set the sprite to the second image
                }
            }
            else
            {
                // Use placeholder if the image is not found
                Image[] imageComponents = newEmployee.GetComponentsInChildren<Image>();
                if (imageComponents.Length > 1 && placeholderTexture != null)
                {
                    Image targetImage = imageComponents[1]; // Second image in the hierarchy
                    Sprite placeholderSprite = Sprite.Create(placeholderTexture, new Rect(0, 0, placeholderTexture.width, placeholderTexture.height), new Vector2(0.5f, 0.5f));
                    targetImage.sprite = placeholderSprite; // Set the placeholder image to the second image
                }
                else
                {
                    Debug.LogWarning("No valid image or placeholder found for employee: " + emp.name);
                }
            }

            Button hireButton = newEmployee.transform.Find("HireButton").GetComponent<Button>();
            hireButton.onClick.AddListener(() => hireEmployee(emp.id));
            hireButton.onClick.AddListener(soundManager.ButtonClickSound);

            Debug.Log("ADDING EMPLOYEE: " + emp.name + ", " + emp.position + ", " + emp.salary + ", " + emp.personality + ", " + emp.benefits + ", " + emp.qualifications);
        }
    }
    private void hireEmployee(int id)
    {
        Employee hiring = db.currentPlayer.unemployed.Find(employee => employee.id == id);
        if (hiring != null)
        {
            db.addEmployee(hiring);
            db.currentPlayer.unemployed.Remove(hiring);
            UpdateEmployeeUI();
            UpdateEmployeeManagerUI();
        }
        else
        {
            Debug.Log("Employee not found");
        }

    }
    private void fireEmployee(int id)
    {
        Employee firing = db.currentPlayer.employees.Find(employee => employee.id == id);
        if (firing != null)
        {
            db.removeEmployee(firing);
            db.currentPlayer.employees.Remove(firing);
            UpdateEmployeeUI();
            UpdateEmployeeManagerUI();
        }
        else
        {
            Debug.Log("Employee not found");
        }

    }

}
