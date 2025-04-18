using UnityEngine;

public class HairChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Material clothing;
    private Material clothing2;
    private int index = 0;

    [SerializeField] float r;
    [SerializeField] float g;
    [SerializeField] float b;
    void Start()
    {

        clothing2 = new Material(clothing);
        //clothing2.SetColor("_BaseColor", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        //GetComponent<Renderer>().material = clothing;
        for (int i = 0; i < GetComponent<Renderer>().materials.Length; i++)
        {
            Debug.Log(GetComponent<Renderer>().materials[i].name + " IN THE LIST");
            string temp = clothing.name + " (Instance)";
            Debug.Log(temp + "  is the output");
            if (GetComponent<Renderer>().materials[i].name == temp)
            {
                GetComponent<Renderer>().materials[i] = new Material(clothing);
                //Scale 0 to 
                GetComponent<Renderer>().materials[i].SetColor("_BaseColor", new Color(r / 255f, g / 255f, b / 255f));
                clothing = GetComponent<Renderer>().materials[i];
                index = i;
                break;
            }
        }
        Debug.Log(clothing.name + " ITS HERE");
        //clothing2.SetColor("_BaseColor",clothing.color);
    }
    public void DestroyTheCopy()
    {
        Destroy(GetComponent<Renderer>().materials[index]);
    }
    private void OnDestroy()
    {
        DestroyTheCopy();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
