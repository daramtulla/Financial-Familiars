using UnityEngine;

public class InventoryBoxes : MonoBehaviour
{
    [SerializeField] JSONDatabaseOperations db;

    private GameObject[] boxes;

    void Start()
    {
        // Get all child boxes.
        boxes = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            boxes[i] = transform.GetChild(i).gameObject;
        }
    }

    // for now, this is on update(), but we should link it up to sell/buy items to reduce log
    void Update()
    {
        int itemCount = 0;

        // addthe quantities of all  items
        foreach (var item in db.currentPlayer.merch)
        {
            itemCount += item.quantity;
        }

        int activeBoxes = Mathf.CeilToInt((float)itemCount / 20);

        // Activate boxes based on the count.
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].SetActive(i < activeBoxes);
        }
    }
}