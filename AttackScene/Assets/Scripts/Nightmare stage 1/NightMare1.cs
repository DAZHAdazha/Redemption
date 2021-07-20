using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightMare1 : MonoBehaviour
{
    public float hitSpeed;
    public GameObject floatPoint;
    public GameObject health;
    private Animator animator;

    private Vector2 direction;
    private bool isHit;
    private AnimatorStateInfo info;
    private Animator hitAnimator;
    private FSM_Nightmare1 fsm;
    private bool dangerMark = false;
    new private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        //注意！第一个子物体为hitAnimation
        hitAnimator = transform.GetChild(0).GetComponent<Animator>();
        fsm = gameObject.GetComponent<FSM_Nightmare1>();
        rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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
        if (fsm.parameter.unattackable)
        {
            damage = 0;
        }
        isHit = true;

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

        this.direction = direction;

        hitAnimator.SetTrigger("Hit");
    }


    public void destory()
    {
        Destroy(health.transform.parent.gameObject);
        Destroy(gameObject);
    }


}
