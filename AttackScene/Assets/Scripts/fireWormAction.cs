using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireWormAction : MonoBehaviour
{
    public float hitSpeed;
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool isHit;
    private AnimatorStateInfo info;
    private Animator hitAnimator;

    [Header("����Ѳ�ߵ�")]
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("���λ��")]
    public Transform playerPoint;

    private float left;
    private float right;

    private bool facingLeft;

    [Header("�ƶ��ٶ�")]
    public float moveSpeed;

    [Header("������")]
    public Animator ani;

    [Header("�������")]
    public GameObject observeRegion;//���˼����˵�����
    private Collider2D observe;//���˼������Ĵ�����

    [Header("���ͼ��")]
    public LayerMask player;

    [Header("����Ѫ��")]
    public int maxHealth;
    public float currentHealth;


    [Header("��Ч")]
    public AudioSource Attack;
    public AudioSource Alert;
    public AudioSource Move;
    public AudioSource hurt;

    [Header("����")]
    public GameObject fireball;
    public Transform fireballAppearPosition;//���ڻ��ͷ�����˸���������Ϊ�������ɵ�λ��

    public GameObject health;
    public GameObject floatPoint;
    public GameObject coin;//������Ʒ
    public int coinMax = 2;

    public GameObject nextLevel;
    public GameObject mail;
    void Start()
    {
        facingLeft = false;
        rb = GetComponent<Rigidbody2D>();
        left = leftPoint.position.x;
        right = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
        observe = observeRegion.GetComponent<Collider2D>();//��ȡ�������
        currentHealth = maxHealth;
        hitAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        if (isHit)
        {
            info = ani.GetCurrentAnimatorStateInfo(0);
            rb.velocity = direction * hitSpeed;
            if (info.normalizedTime >= .6f)
                isHit = false;
        }
    }

    private void movement()
    {

        if (!observe.IsTouchingLayers(player))//��⵽�����
        {
            Move.Play();
            if (facingLeft)//���ͷ������
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                ani.SetBool("walking", true);
                ani.SetBool("idle", false);
                if (transform.position.x < left)//���Ѳ�߹�����˵㣬��תͷ
                {

                    facingLeft = false;
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    ani.SetBool("walking", false);
                    ani.SetBool("idle", true);
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                ani.SetBool("walking", true);
                ani.SetBool("idle", false);
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                if (transform.position.x > right)
                {

                    facingLeft = true;
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    ani.SetBool("walking", false);
                    ani.SetBool("idle", true);
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }
        else
        {
            Move.Pause();
            Alert.Play();
            //
            if(transform.position.x < playerPoint.position.x)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                ani.SetBool("walking", true);
                ani.SetBool("idle", false);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                ani.SetBool("walking", true);
                ani.SetBool("idle", false);
            }
            if(Mathf.Abs(transform.position.x - playerPoint.position.x) < 30)
            {
                attack();
            }
        }
        
    }

    public void gotHit(Vector2 direction, float damage, bool isCritical)
    {
        isHit = true;

        hurt.Play();
        ani.SetBool("Hited", true);
        currentHealth-= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        GameObject gb = Instantiate(floatPoint, new Vector2(transform.position.x - 0.5f, transform.position.y + 1f), Quaternion.identity);
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        if (isCritical)
        {
            gb.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(255, 0, 0, 255);
        }
        health.GetComponent<health>().callUpdateHealth();

        if (currentHealth <= 0)
        {
            ani.SetTrigger("death");
        }

        this.direction = direction;

        hitAnimator.SetTrigger("Hit");
    }

    public void destoryEnemy()
    {
        mail.SetActive(true);
        nextLevel.SetActive(true);
        Destroy(health.transform.parent.gameObject);
        getCoin();
        Destroy(gameObject);
    }

    public void recoverFromHurt()
    {
        ani.SetBool("Hited", false);
    }

    public void attack()
    {
        rb.velocity= new Vector2(0, rb.velocity.y);
        Attack.Play();
        ani.SetBool("attacking", true);
        
    }

    void getCoin()
    {
        int coinNum = Random.Range(1, coinMax + 1);
        for (int i = 0; i < coinNum; i++)
        {
            Instantiate(coin, new Vector2(transform.position.x + i - coinNum / 2, transform.position.y+1), Quaternion.identity);
        }
    }




public void shootFireBall()
    {
        GameObject fireBall = (GameObject)Instantiate(fireball, fireballAppearPosition.position, transform.rotation);
        
    }

    public void stopAttack()
    {
        ani.SetBool("attacking", false);
    }
}
