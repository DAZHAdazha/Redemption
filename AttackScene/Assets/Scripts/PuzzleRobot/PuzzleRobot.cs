using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRobot : MonoBehaviour
{
    public float hitSpeed;
    public GameObject floatPoint;
    public GameObject coin;
    public GameObject health;

    private Vector2 direction;
    private bool isHit;
    private AnimatorStateInfo info;
    new private Rigidbody2D rigidbody;
    private Animator animator;
    private Animator hitAnimator;
    // Start is called before the first frame update
    void Start()
    {

        animator = gameObject.GetComponent<Animator>();
        //注意！第一个子物体为hitAnimation
        hitAnimator = transform.GetChild(0).GetComponent<Animator>();
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

        GameObject gb = Instantiate(floatPoint, new Vector2(transform.position.x - 0.5f, transform.position.y + 1f), Quaternion.identity);
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        if (isCritical)
        {
            gb.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(255, 0, 0, 255);
        }

        //！！！可优化 把敌人的参数位置移动一下
        gameObject.GetComponent<FSM_PuzzleRobot>().parameter.getHit = true;
        gameObject.GetComponent<FSM_PuzzleRobot>().parameter.health -= damage;
        if (gameObject.GetComponent<FSM_PuzzleRobot>().parameter.health < 0)
        {
            gameObject.GetComponent<FSM_PuzzleRobot>().parameter.health = 0;
        }

        health.GetComponent<health>().callUpdateHealth();

        this.direction = direction;

        hitAnimator.SetTrigger("Hit");
    }


    public void callAwakeAnim()
    {
        animator.Play("Awake");
    }


    public void destory()
    {
        Destroy(health.transform.parent.gameObject);
        getCoin();//一定要写成函数 否则父物体消失后 无法拾取金币
        Destroy(gameObject);
    }

    void getCoin()
    {
        Instantiate(coin, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity);
    }
}
