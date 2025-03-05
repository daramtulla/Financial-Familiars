using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CSVLoader : MonoBehaviour
{
    /*
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
                int quantity = int.Parse(values[2]);
                float baseCost = float.Parse(values[3]);
                float markup = float.Parse(values[4]);
                float demandSlope = float.Parse(values[5]);
                float demandIntercept = float.Parse(values[6]);

                //InventoryItem newItem = new InventoryItem(id, name, quantity, baseCost, markup);
                //InventoryItem newItem = new InventoryItem(id, name, quantity, baseCost, markup, demandSlope, demandIntercept);
                //items.Add(newItem);
            }
        }
        else
        {
            Debug.LogError("CSV File not found: " + filePath);
        }

        return items;
    }
    */

    public static List<Term> LoadTermsFromCSV(string fileName)
    {
        List<Term> terms = new List<Term>();

        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split('|');

                int id = int.Parse(values[0]);
                string word = values[1];
                string definition = values[2];

                Term term = new Term(id, word, definition);
                terms.Add(term);
            }
        }
        else
        {
            Debug.LogError("CSV File not found: " + filePath);
        }

        return terms;
    }
}
