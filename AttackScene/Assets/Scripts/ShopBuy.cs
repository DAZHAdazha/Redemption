using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuy : MonoBehaviour
{
    private GameObject buttonObj; //buy按钮
     public GameObject deposittext;//余额框
    public GameObject insufficient; //余额不足面板

    //当前金币数
    //private float deposit;
    //物品价格
    public int price;


    private void Update()
    {
        

        //buttonObj = GameObject.Find("bt_buy");        
        //buttonObj.GetComponent<Button>().onClick.AddListener(buy);
    }




    public void buy()
    {

        Debug.Log(gameObject.transform.parent.parent.name);

        if(GameSaver.coinNum < price){
            insufficient.SetActive(true);
            //延时消失??
            Invoke("insufficientHide", 1f);

        }
        else{
             insufficient.SetActive(false);
             int remain = GameSaver.coinNum - price;
             GameSaver.coinNum = remain;
             deposittext.GetComponent<Text>().text=remain.ToString();
            
        }  
    }

    private void insufficientHide()
    {
        insufficient.SetActive(false);
    }

}
