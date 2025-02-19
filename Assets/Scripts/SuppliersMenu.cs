using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class SuppliersMenu: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject suppliersPanel;

    void Start()
    {
        suppliersPanel.SetActive(false);
    }

        void Update()
    {
        //press P to open purchasing of goods
        if(Input.GetKeyDown(KeyCode.P)){
            ToggleMenu();
        }
    }

    public void ToggleMenu(){
        suppliersPanel.SetActive(!suppliersPanel.activeSelf);
    }

    public void CloseMenu(){
        suppliersPanel.SetActive(false);

    }


}
