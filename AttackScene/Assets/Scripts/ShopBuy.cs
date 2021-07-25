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

    private void Start()
    {
        deposittext.GetComponent<Text>().text = GameSaver.coinNum.ToString();
    }


    public void buy()
    {

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
             switch (gameObject.transform.parent.parent.name)
                {
                    case "redsmall":
                        GameSaver.smallHealth = true;
                        break;
                    case "redbig":
                        GameSaver.bigHealth = true;
                        break;
                    case "bluesmall":
                        GameSaver.smallMana = true;
                        break;
                    case "bluebig":
                        GameSaver.bigMana = true;
                        break;
                }
            gameObject.transform.parent.parent.gameObject.SetActive(false);

        }  
    }

    private void insufficientHide()
    {
        insufficient.SetActive(false);
    }

}
