using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using static UnityEngine.ParticleSystem;

public class UpgradeManager : MonoBehaviour
{
    public SoundManager soundManager;

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
        if (upgradeUI.activeSelf)
        {
            UpdateUpgradeUI();
        }
    }

    public void CloseMenu()
    {
        Debug.Log("Upgrade Menu Close");
        if (InteractionManager.GetInteractState() == true)
        {
            Debug.Log("(UpgradeManager): GetInteractState() is true");
            interactionManager.SwitchInteractState();
        }
        upgradeUI.SetActive(false);
    }

    //HIRE ID 1
    public void UpdateUpgradeUI()
    {
        foreach (Transform child in upgradeContent)
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

            float cost = upgrade.Cost;

            //HIRE ID 1, 7: Affects prices of upgrades
            if (db.checkEmployee(1))
            {
                if (db.checkEmployee(15))
                {
                    cost *= .98f;
                }
                else
                {
                    cost *= .95f;
                }
            }
            if (db.checkEmployee(8))
            {
                if (db.checkEmployee(15))
                {
                    cost *= 1.05f;
                }
                else
                {
                    cost *= 1.10f;
                }
            }
            texts[1].text = "$" + cost;
  
            texts[2].text = upgrade.Description;



            //TODO: add button with correct name
            Button buyButton = newItem.transform.Find("BuyButton").GetComponent<Button>();
            buyButton.onClick.AddListener(() => buyUpgrade(upgrade.Cost, upgrade.id));
        }
    }

    private void buyUpgrade(float cost, int id)
    {
        if (db.checkEmployee(1))
        {
            if (db.checkEmployee(15))
            {
                cost *= .98f;
            }
            else
            {
                cost *= .95f;
            }
        }
        if (db.currentPlayer.currentMoney >= cost)
        {
            // HIRE ID 13: Chance to not buy an upgrade
            if (db.checkEmployee(13) && new System.Random().Next(1, 100) >= 95)
            {
                // TODO: ADD FEEDBACK FOR THE ITEMS BEING LOST
                db.currentPlayer.currentMoney -= cost;
            }
            else
            {
                db.currentPlayer.currentMoney -= cost;
                Upgrade upgradeToBuy = db.currentPlayer.unpurchasedUpgrades.Find(upgrade => upgrade.id == id);
                if (upgradeToBuy != null)
                {
                    db.AddUpgrade(upgradeToBuy);
                    db.currentPlayer.unpurchasedUpgrades.Remove(upgradeToBuy);
                    UpdateUpgradeUI();
                }
                else
                {
                    Debug.Log("Upgrade not found");
                }
            }
            soundManager.ButtonClickSound();
        }
        else
        {
            soundManager.soundAudioSource.PlayOneShot(soundManager.itemPurchaseError, 0.2f);
        }
    }
}
