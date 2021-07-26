using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRobot : MonoBehaviour
{
    public float hitSpeed;
    public GameObject floatPoint;
    public GameObject coin;
    public GameObject health;
    public int coinMax = 3;
    public GameObject nextLevel;
    public GameObject mail;

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
        //ע�⣡��һ��������ΪhitAnimation
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
            //gb.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(255, 0, 0, 0);
        }

        //���������Ż� �ѵ��˵Ĳ���λ���ƶ�һ��
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
        mail.SetActive(true);
        nextLevel.SetActive(true);
        Destroy(health.transform.parent.gameObject);
        getCoin();//һ��Ҫд�ɺ��� ����������ʧ�� �޷�ʰȡ���
        Destroy(gameObject);
    }

    void getCoin()
    {
        int coinNum = Random.Range(1, coinMax + 1);
        for (int i = 0; i < coinNum; i++)
        {
            Instantiate(coin, new Vector2(transform.position.x + i - coinNum / 2, transform.position.y+1), Quaternion.identity);
        }
    }
}
