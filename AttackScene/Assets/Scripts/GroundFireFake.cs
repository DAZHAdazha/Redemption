using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFireFake : MonoBehaviour
{
    [Header("¶¯»­Æ÷")]
    public Animator ani;

    public int leftTime = 3;

    //public float damage = 1f;

    //private bool isHurt=false;
    //private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        //playerController = GameObject.Find("Player").GetComponent<PlayerController>();
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

}
