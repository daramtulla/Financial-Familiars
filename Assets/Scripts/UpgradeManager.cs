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
        foreach (Upgrade upgrade in db.currentPlayer.unpurchased)
        {
            GameObject newItem = Instantiate(upgradePrefab, upgradeContent);

            TextMeshProUGUI[] texts = newItem.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].text = upgrade.name;

            float cost = upgrade.cost;

            //HIRE ID 1, 7: Affects prices of upgrades
            if (db.CheckEmployee(1))
            {
                if (db.CheckEmployee(15))
                {
                    cost *= .98f;
                }
                else
                {
                    cost *= .95f;
                }
            }
            if (db.CheckEmployee(8))
            {
                if (db.CheckEmployee(15))
                {
                    cost *= 1.05f;
                }
                else
                {
                    cost *= 1.10f;
                }
            }
            texts[1].text = "$" + cost;

            texts[2].text = upgrade.description;

            Button buyButton = newItem.transform.Find("BuyButton").GetComponent<Button>();
            buyButton.onClick.AddListener(() => buyUpgrade(upgrade.cost, upgrade.id));
        }
    }

    private void buyUpgrade(float cost, int id)
    {
        if (db.CheckEmployee(1))
        {
            if (db.CheckEmployee(15))
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
            if (db.CheckEmployee(13) && new System.Random().Next(1, 100) >= 95)
            {
                // TODO: ADD FEEDBACK FOR THE ITEMS BEING LOST
                db.currentPlayer.currentMoney -= cost;
            }
            else
            {
                db.currentPlayer.currentMoney -= cost;
                Upgrade upgradeToBuy = db.currentPlayer.unpurchased.Find(upgrade => upgrade.id == id);
                if (upgradeToBuy != null)
                {
                    db.AddUpgrade(upgradeToBuy);
                    db.currentPlayer.unpurchased.Remove(upgradeToBuy);
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
