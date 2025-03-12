using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradeUI;
    public Transform upgradeContent;
    public GameObject upgradePrefab;

    public JSONDatabaseOperations db;

    [SerializeField] InteractionManager interactionManager;

    void Start()
    {
        upgradeUI.SetActive(false);
        UpdateUpgradeUI();
    }
    void Update()
    {

    }

    public void ToggleMenu()
    {
        upgradeUI.SetActive(!upgradeUI.activeSelf);
        if(upgradeUI.activeSelf)
        {
            UpdateUpgradeUI();
        }
    }

    public void CloseMenu()
    {
        Debug.Log("Upgrade Menu Close");
        if(InteractionManager.GetInteractState() == true)
        {
            Debug.Log("(UpgradeManager): GetInteractState() is true");
            interactionManager.switchInteractState();
        }
        upgradeUI.SetActive(false);
    }

    public void UpdateUpgradeUI()
    {
        foreach(Transform child in upgradeContent)
        {
            Destroy(child.gameObject);
        }
        foreach (Upgrade upgrade in db.currentPlayer.unpurchasedUpgrades)
        {
            GameObject newItem = Instantiate(upgradePrefab, upgradeContent);

            TextMeshProUGUI[] texts = newItem.GetComponentsInChildren<TextMeshProUGUI>();
            //todo: Add texts
            texts[0].text = upgrade.Name;
            //TODO: make this more efficient?
            if (db.currentPlayer.employees.Any(employee => employee.id == 1)) {
                texts[1].text = "$" + (upgrade.Cost * 0.95f);
            }
            else
            {
                texts[1].text = "$" + upgrade.Cost;
            }
            texts[2].text = upgrade.Description;

            //TODO: add button with correct name
            Button buyButton = newItem.transform.Find("BuyButton").GetComponent<Button>();
            buyButton.onClick.AddListener(() => buyUpgrade(upgrade.Cost, upgrade.id));
        }
    }

    private void buyUpgrade(float cost, int id)
    {
        if (db.currentPlayer.employees.Any(employee => employee.id == 1)) {
            cost *= 0.95f;
        }
        if (db.currentPlayer.currentMoney >= cost)
        {
            db.currentPlayer.currentMoney -= cost;
            Upgrade upgradeToBuy = db.currentPlayer.unpurchasedUpgrades.Find(upgrade => upgrade.id == id);
            if(upgradeToBuy != null )
            {
                db.addUpgrade(upgradeToBuy);
                db.currentPlayer.unpurchasedUpgrades.Remove(upgradeToBuy);
                UpdateUpgradeUI();
            }
            else
            {
                Debug.Log("Upgrade not found");
            }

        }
    }
}
