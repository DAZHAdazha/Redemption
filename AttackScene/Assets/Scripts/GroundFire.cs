using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFire : MonoBehaviour
{
    [Header("动画器")]
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
        //注意这里Player物体只能有一个Player Tag （子物体不能使用Player tag） 否则伤害会触发多次
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D" && !playerController.isDefense)
        {
            playerController.getDamage(damage);
            playerController.blinkPlayer();
        }
    }
}
