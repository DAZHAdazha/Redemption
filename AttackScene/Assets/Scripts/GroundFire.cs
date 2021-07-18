using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFire : MonoBehaviour
{
    [Header("������")]
    public Animator ani;

    public int leftTime = 3;

    public float damage = 1f;

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        

    }


    private void growFire()
    {
        //Debug.Log("growed");
        ani.SetBool("growed", true);

    }

    private void closeTheFire()
    {
        if(leftTime > 0)
        {
            leftTime--;
            //Debug.Log(leftTime);
        }
        else
        {
            ani.SetTrigger("Disappear");
        }
    }

    private void destoryFire()
    {
        //Debug.Log("destoried");
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        //ע������Player����ֻ����һ��Player Tag �������岻��ʹ��Player tag�� �����˺��ᴥ�����
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D" && !playerController.isDefense)
        {
            playerController.getDamage(damage);
            playerController.blinkPlayer();
        }
    }
}
