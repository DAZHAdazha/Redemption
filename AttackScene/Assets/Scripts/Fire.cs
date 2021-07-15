using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    private Animator anim;
    private PlayerController playerController;
    private bool isHurt = false;

    public float damage = 1f;


    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //注意这里Player物体只能有一个Player Tag （子物体不能使用Player tag） 否则伤害会触发多次
        if (!isHurt && other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {
            playerController.getDamage(damage);
            playerController.blinkPlayer();
            isHurt = true;
        }
    }

    public void setHurt()
    {
        isHurt = false;
    }
}
