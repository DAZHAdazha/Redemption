using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleStoneMonster : MonoBehaviour
{
    public float hitSpeed;
    public GameObject health;
    public GameObject floatPoint;


    public GameObject bigStone;
    public GameObject bigStoneCanvas;

    private Vector2 direction;
    private bool isHit;
    private AnimatorStateInfo info;
    private Animator animator;
    private Animator hitAnimator;
    new private Rigidbody2D rigidbody;
    private middleStoneMonsterFSM fsm;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        //注意！第一个子物体为hitAnimation
        hitAnimator = transform.GetChild(0).GetComponent<Animator>();
        rigidbody = transform.GetComponent<Rigidbody2D>();
        fsm = gameObject.GetComponent<middleStoneMonsterFSM>();
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
        isHit = true;

        GameObject gb = Instantiate(floatPoint, new Vector2(transform.position.x - 0.5f, transform.position.y + 1f), Quaternion.identity);
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        if (isCritical)
        {
            gb.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(255, 0, 0, 255);
            //gb.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(255, 0, 0, 0);
        }

        //！！！可优化 把敌人的参数位置移动一下
        fsm.p.getHit = true;
        fsm.p.health -= damage;
        if (fsm.p.health < 0)
        {
            fsm.p.health = 0;
        }

        health.GetComponent<health>().callUpdateHealth();

        this.direction = direction;
        hitAnimator.SetTrigger("Hit");
    }

    public void destory()
    {
        bigStone.SetActive(true);
        bigStoneCanvas.SetActive(true);
        bigStone.gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 5f);


        Destroy(health.transform.parent.gameObject);
        Destroy(gameObject);
    }

}
