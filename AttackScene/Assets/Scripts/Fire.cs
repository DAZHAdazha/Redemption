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
        //ע������Player����ֻ����һ��Player Tag �������岻��ʹ��Player tag�� �����˺��ᴥ�����
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
