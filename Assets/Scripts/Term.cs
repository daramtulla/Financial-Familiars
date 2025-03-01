using UnityEngine;

public class Term
{
    public int idNumber;
    public string word;
    public string definition;

    public Term(int idNumber, string word, string definition)
    {
        this.idNumber = idNumber;
        this.word = word;
        this.definition = definition;
    }
}
