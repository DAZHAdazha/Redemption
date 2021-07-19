using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightMare1 : MonoBehaviour
{
    public GameObject floatPoint;
    public GameObject health;
    //private Animator animator;
    private Animator hitAnimator;
    private FSM_Nightmare1 fsm;
    private bool dangerMark = false;
    // Start is called before the first frame update
    void Start()
    {
        //animator = gameObject.GetComponent<Animator>();
        //注意！第一个子物体为hitAnimation
        hitAnimator = transform.GetChild(0).GetComponent<Animator>();
        fsm = gameObject.GetComponent<FSM_Nightmare1>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHit(int damage, bool isCritical)
    {
        //isHit = true;

        GameObject gb = Instantiate(floatPoint, new Vector2(transform.position.x - 0.5f, transform.position.y + 1f), Quaternion.identity);
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        if (isCritical)
        {
            gb.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(255, 0, 0, 255);
        }

        //！！！可优化 把敌人的参数位置移动一下
        fsm.parameter.getHit = true;
        fsm.parameter.health -= damage;
        if (fsm.parameter.health < 0)
        {
            fsm.parameter.health = 0;
        }

        if(!dangerMark && fsm.parameter.health <= fsm.parameter.dangerHealth)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(248, 105, 97, 255);
            dangerMark = true;
        }


        health.GetComponent<health>().callUpdateHealth();


        hitAnimator.SetTrigger("Hit");
    }


    public void destory()
    {
        Destroy(health.transform.parent.gameObject);
        Destroy(gameObject);
    }


}
