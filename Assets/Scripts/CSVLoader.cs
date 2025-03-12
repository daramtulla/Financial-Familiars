using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CSVLoader : MonoBehaviour
{
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
