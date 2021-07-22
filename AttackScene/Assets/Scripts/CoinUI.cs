using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    private Text coinNumText;
    public static int coinNum;
    // Start is called before the first frame update

    private void Start() {
        coinNumText = GameObject.Find("CoinNumber").GetComponent<Text>();
        coinNum = GameSaver.coinNum;
        setCoinNumText();
    }

    public void setCoinNumText(){
        coinNumText.text = coinNum.ToString();
    }
}


