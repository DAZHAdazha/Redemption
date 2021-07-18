using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    public float damage;
    public float destoryTime;

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,destoryTime);//销毁碰撞框
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other) { 
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        //注意这里Player物体只能有一个Player Tag （子物体不能使用Player tag） 否则伤害会触发多次
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D" && !playerController.isDefense){
            playerController.getDamage(damage);
            playerController.blinkPlayer();
        }
    }
}
