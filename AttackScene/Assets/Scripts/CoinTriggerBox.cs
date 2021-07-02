using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTriggerBox : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            //静态成员可以直接调用
            CoinUI.coinNum += 1;
            transform.parent.GetComponent<CoinUI>().setCoinNumText();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
