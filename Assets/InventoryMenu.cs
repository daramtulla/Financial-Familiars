using UnityEngine;
using TMPro;
public class InventoryMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject inventoryPanel;
    void Start()
    {
        inventoryPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //press I to open inventory
        if(Input.GetKeyDown(KeyCode.I)){
            ToggleMenu();
        }
    }

    public void ToggleMenu(){
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    public void CloseMenu(){
        inventoryPanel.SetActive(false);

    }
}
