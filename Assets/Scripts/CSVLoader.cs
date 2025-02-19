using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CSVLoader: MonoBehaviour
{
    public static List<InventoryItem> LoadItemsFromCSV(string fileName)
    {
        List<InventoryItem> items = new List<InventoryItem>();

        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');

                int id = int.Parse(values[0]);
                string name = values[1];
                float baseCost = float.Parse(values[2]);
                float markup = float.Parse(values[3]);

                InventoryItem newItem = new InventoryItem(id, name, 10, baseCost, markup);
                items.Add(newItem);
            }
        }
        else
        {
            Debug.LogError("CSV File not found: " + filePath);
        }

        return items;
    }
}
