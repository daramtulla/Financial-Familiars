using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;


public class Loans{
public static float totalam=0;
public static float totalint=0;
public string type;
public float amount;
public float interest;
 public Loans(string t, float a, float i){
    this.type = t;
    this.amount = a;
    this.interest = i;
    totalam+=a;
    totalint+=i;
 }
 public string interestDisp(){
    return numDisp(interest);
 }
  public string amountDisp(){
    return numDisp(amount);
 }
 public string tinterestDisp(){
    return numDisp(totalam);
 }
  public string tamountDisp(){
    return numDisp(totalint);
 }
 public string numDisp(float numba){
    string preturnable;
    string returnable="";
    bool dotfound = false;
    preturnable = MathF.Round(numba, 2,0).ToString();
    if(preturnable.Length<3){
        return "$"+preturnable+".00";
    }
    int digitcount=1;
    for(int i=preturnable.Length-1; i>=0; i--){
        if(preturnable[i]=='.'){
            if(returnable.Length==1){
                returnable=returnable+"0";
            }
            digitcount=0;
            dotfound=true;
        }
        returnable = preturnable[i]+returnable;
        if(digitcount==3){
            if(i!=0){
                returnable = ","+returnable;
            }
            
            if(!dotfound){
                returnable = returnable+".00";
                //not found, but we ain looking no more
                dotfound=true;
            }
            digitcount=1;
        }
        else{
            digitcount++;
        }
        

    }

    
    ///returnable[returnable.Length];
    return "$"+returnable;
 }

}
public class LoansMenu : MonoBehaviour
{
    [SerializeField] Button bob;

    [SerializeField] Button up;
    [SerializeField] Button down;

    [SerializeField] Button newest;
    [SerializeField] Button oldest;

    [SerializeField] Button paynow;

    [SerializeField] Toggle addone;

    [SerializeField] Toggle addten;

    [SerializeField] Toggle addhundred;

    [SerializeField] Toggle thousandtimes;
    [SerializeField] GameObject loansmenu;

    [SerializeField] GameObject totalloans;
    [SerializeField] GameObject totalinterest;
    [SerializeField] GameObject currenttype;
    [SerializeField] GameObject currentamount;
    [SerializeField] GameObject currentinterest;

    private int currentindex=0;

    Color ogcolor = new Color(53, 41, 42, 255);
    Color target = new Color(118, 89, 60,255);
    Color current = new Color(53, 41, 42, 255);
    //Loans  leon;

    float moneyspent=0;
    List<Loans> theloans = new List<Loans>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SampleLoans();
        bob.onClick.AddListener(CloseThis);
       up.onClick.AddListener(Increaser);
       down.onClick.AddListener(Decreaser);
       newest.onClick.AddListener(setset2);
       oldest.onClick.AddListener(setset1);
       paynow.onClick.AddListener(ClearLoan);
       //highlighttext();

        ogcolor = new Color(53, 41, 42, 255);
        target = new Color(195, 141, 87,255);
        current = new Color(53, 41, 42, 255);
         //leon = new Loans("Business", 1000.01f, 1000.01f);
    }

    // Update is called once per frame
    void Update()
    {
       LoansDisplay(); 
       //Debug.Log(leon.amountDisp());
       if(theloans[currentindex].interest>0){
            highlighttext(currentinterest,currentamount);
       }
       else{
        highlighttext(currentamount,currentinterest);

       }
       
       Debug.Log(moneyspent);
    }
    void RemoveFromSave(){
        //keep or dont keep. keep if you want paid lones gone.
    }

    bool goodforit(float value){
        //add code checking how much money you got and if you can pay that amount
        return true;
    }
    void ClearLoan(){
        if(theloans[currentindex].interest>0){
            ClearLoanA();
       }
       else{
            ClearLoanB();

       }
    }
    void ClearLoanA(){
        float init = theloans[currentindex].interest;
        float thevalue = 0;

        thevalue += (addone.isOn? 1:0);
        thevalue += (addten.isOn? 10:0);
        thevalue += (addhundred.isOn? 100:0);
        thevalue *= (thousandtimes.isOn? 1000:0);
        if((goodforit(thevalue)==false)||(init==0)){
            //play noise saying indicating you cant pay
            return;
        }
        theloans[currentindex].interest-=thevalue;
        if(theloans[currentindex].interest<0){
            theloans[currentindex].interest=0;
        }
        moneyspent+= init-theloans[currentindex].interest;
        RemoveFromSave();
    }

    void ClearLoanB(){
        float init = theloans[currentindex].amount;
        float thevalue = 0;

        thevalue += (addone.isOn? 1:0);
        thevalue += (addten.isOn? 10:0);
        thevalue += (addhundred.isOn? 100:0);
        thevalue *= (thousandtimes.isOn? 1000:0);
        if((goodforit(thevalue)==false)||(init==0)){
            //play noise saying indicating you cant pay
            return;
        }
        theloans[currentindex].amount-=thevalue;
        if(theloans[currentindex].amount<0){
            theloans[currentindex].amount=0;
        }
        moneyspent+= init-theloans[currentindex].amount;
        RemoveFromSave();
    }
    void CloseThis(){
        loansmenu.SetActive(false);
    }
    void OpenThis(){
        loansmenu.SetActive(true);
    }
    void Increaser(){
        currentindex+=1;
        if(currentindex>=theloans.Count){
            currentindex=0;
        }
    }
    void Decreaser(){
        currentindex-=1;
        if(currentindex<0){
            currentindex=theloans.Count-1;
        }
    }
    void setset1(){
        currentindex=0;
    }
    void setset2(){
        currentindex=theloans.Count-1;
    }
    

    float cycler = 0;
    void highlighttext(GameObject tom, GameObject jerry){
        cycler=Mathf.Sin((Time.time*1.25f)*Mathf.PI);
        float r = ogcolor.r + (target.r-ogcolor.r)*cycler;
        float g = ogcolor.g +(target.g-ogcolor.g)*cycler;
        float b = ogcolor.b + (target.b-ogcolor.b)*cycler;
        current=new Color(r, g, b);
        
        tom.GetComponent<TMP_Text>().color = new Color(current.r/255, current.g/255, current.b/255);
        //currentinterest.GetComponent<TMP_Text>().color = new Color(0,0,0,0);
        jerry.GetComponent<TMP_Text>().color = new Color(ogcolor.r/255, ogcolor.g/255, ogcolor.b/255 );
    }
 

    void SampleLoans(){
        //var decider = new Unity.Mathematics.Random(0x6E624EB7u);
        string[] loantypes = {"Business","Personal","Illegal"};
        for(int i=0; i<20;i++){
            //theloans.Add(new Loans(loantypes[decider.NextInt(2)],decider.NextFloat(10000.00f),0));
            theloans.Add(new Loans(loantypes[UnityEngine.Random.Range(0,3)],UnityEngine.Random.Range(0.00f,10000.00f),UnityEngine.Random.Range(0.00f,10000.00f)));
        }
        
    }
    void LoansDisplay(){
        
        if(currentindex<0){
                currentindex=0;
         }
        while(currentindex>=theloans.Count){
            currentindex--;
            if(currentindex<0){
                currentindex=0;
                break;
            }
        }
        
        if(theloans.Count>0){
            totalloans.GetComponent<TMP_Text>().text = theloans[0].tamountDisp();
            totalinterest.GetComponent<TMP_Text>().text = theloans[0].tinterestDisp();
            currentamount.GetComponent<TMP_Text>().text = theloans[currentindex].amountDisp();
            currentinterest.GetComponent<TMP_Text>().text = theloans[currentindex].interestDisp();
            currenttype.GetComponent<TMP_Text>().text = theloans[currentindex].type;
        }
        else{
            totalloans.GetComponent<TMP_Text>().text = "N/A";
            totalinterest.GetComponent<TMP_Text>().text = "N/A";
            currentamount.GetComponent<TMP_Text>().text = "N/A";
            currentinterest.GetComponent<TMP_Text>().text = "N/A";
            currenttype.GetComponent<TMP_Text>().text = "N/A";
        }
        
        
    }
}

