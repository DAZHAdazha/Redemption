using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTriggerBox : MonoBehaviour
{
    private bool isUsed;

    private void Start()
    {
        isUsed = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && !isUsed){
            
            
            
            Destroy(gameObject.transform.parent.gameObject);
            //静态成员可以直接调用
            CoinUI.coinNum += 1;
            //调用音效
            SoundManager.soundManagerInstance.pickCoinAudio();
            transform.parent.GetComponent<CoinUI>().setCoinNumText();
            isUsed = true;
        }
    }
}
