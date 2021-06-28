using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float damage = 1f;

    private PlayerController playerController;
    

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) { 
        //注意这里Player物体只能有一个Player Tag （子物体不能使用Player tag） 否则伤害会触发多次
        if(other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D" && !playerController.isDefense){
            playerController.getDamage(damage);
            playerController.blinkPlayer();
        }
    }


        
}
