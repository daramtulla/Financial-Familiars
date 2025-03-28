using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;

public class Self_Deletion : MonoBehaviour
{
    bool die;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        die = false;
        StartCoroutine(deletion());
    }

    // Update is called once per frame
    void Update()
    {
        if (die)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator deletion()
    {
       
        yield return new WaitForSeconds(1);
        die = true;
    }
}
