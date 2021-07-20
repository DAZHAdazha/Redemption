using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GFparameters
{
    public Animator ani;

    public int leftTime = 3;

    public float damage = 1f;

    
    
}

public class GroundFire : MonoBehaviour
{
    public GFparameters p;
    private PlayerController playerController;
    private bool isHurt = false;

    // Start is called before the first frame update
    void Start()
    {
        p.ani = GetComponent<Animator>();
        
    }


    private void growFire()
    {
        //Debug.Log("growed");
        p.ani.SetBool("growed", true);

    }

    private void closeTheFire()
    {
        if(p.leftTime > 0)
        {
            p.leftTime--;
            //Debug.Log(leftTime);
        }
        else
        {
            p.ani.SetTrigger("Disappear");
        }
    }

    private void destoryFire()
    {
        //Debug.Log("destoried");
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D") {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            //ע������Player����ֻ����һ��Player Tag �������岻��ʹ��Player tag�� �����˺��ᴥ�����
            if (!isHurt &&  !playerController.isDefense)
            {
                isHurt = true;
                playerController.getDamage(p.damage);
                playerController.blinkPlayer();
            }
        }
    }
}
