using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightMare2 : MonoBehaviour
{
    
    public GameObject floatPoint;
    public GameObject health;
    public float hitSpeed;
    public GameObject coin;
    public int coinMax = 5;

    private Vector2 direction;
    new private Rigidbody2D rigidbody;
    private bool isHit;
    private AnimatorStateInfo info;
    private Animator animator;
    private Animator hitAnimator;
    private FSM_Nightmare2 fsm;
    private bool dangerMark = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        //注意！第一个子物体为hitAnimation
        hitAnimator = transform.GetChild(0).GetComponent<Animator>();
        fsm = gameObject.GetComponent<FSM_Nightmare2>();
        rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isHit)
        {
            info = animator.GetCurrentAnimatorStateInfo(0);
            rigidbody.velocity = direction * hitSpeed;
            if (info.normalizedTime >= .6f)
                isHit = false;
        }
    }


    public void GetHit(Vector2 direction, int damage, bool isCritical)
    {
        isHit = true;
        int r=255, g=255, b=255;
        if (fsm.parameter.isDefense)
        {
            damage = -damage;
        }

        GameObject gb = Instantiate(floatPoint, new Vector2(transform.position.x - 0.5f, transform.position.y + 1f), Quaternion.identity);
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();

        if (isCritical)//unity字体颜色有bug只能显示纯色
        {
            r = 255;
            g = 0;
            b = 0;
        }

        if(fsm.parameter.isDefense )
        {
                r = 0;
                g = 255;
            b = 0;
        }

        gb.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(r,g, b, 255);
        //gb.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(r,g, b, 0);


        //！！！可优化 把敌人的参数位置移动一下
        fsm.parameter.getHit = true;
        fsm.parameter.health -= damage;
        if (fsm.parameter.health < 0)
        {
            fsm.parameter.health = 0;
        }

        if(!dangerMark && fsm.parameter.health <= fsm.parameter.dangerHealth)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(251, 139, 139, 255);
            dangerMark = true;
        }


        health.GetComponent<health>().callUpdateHealth();

        this.direction = direction;

        hitAnimator.SetTrigger("Hit");
    }


    public void destory()
    {
        Destroy(health.transform.parent.gameObject);
        //getCoin();
        Destroy(gameObject);

        GameObject.Find("GameManager").GetComponent<GameManager>().FinalCG();

    }

    void getCoin()
    {
        int coinNum = Random.Range(3, coinMax + 1);
        for (int i = 0; i < coinNum; i++)
        {
            Instantiate(coin, new Vector2(transform.position.x + i - coinNum / 2, transform.position.y + 1), Quaternion.identity);
        }
    }

}
