using UnityEngine;

public class ClothChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Material clothing;
    private Material clothing2;
    void Start()
    {
        
        clothing2 = new Material(clothing);
        //clothing2.SetColor("_BaseColor", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        //GetComponent<Renderer>().material = clothing;
        for (int i = 0;i< GetComponent<Renderer>().materials.Length; i++){
            Debug.Log(GetComponent<Renderer>().materials[i].name + " IN THE LIST");
            string temp = clothing.name + " (Instance)";
            Debug.Log(temp + "  is the output");
            if (GetComponent<Renderer>().materials[i].name == temp )
            {
                GetComponent<Renderer>().materials[i] = new Material(clothing);
                GetComponent<Renderer>().materials[i].SetColor("_BaseColor", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
                clothing = GetComponent<Renderer>().materials[i];
                break;
            }
        }
        Debug.Log(clothing.name + " ITS HERE");
        //clothing2.SetColor("_BaseColor",clothing.color);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
