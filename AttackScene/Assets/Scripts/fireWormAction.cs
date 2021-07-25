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

    [Header("左右巡逻点")]
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("玩家位置")]
    public Transform playerPoint;

    private float left;
    private float right;

    private bool facingLeft;

    [Header("移动速度")]
    public float moveSpeed;

    [Header("动画器")]
    public Animator ani;

    [Header("检查区域")]
    public GameObject observeRegion;//敌人检测敌人的区域
    private Collider2D observe;//敌人检测区域的触发器

    [Header("玩家图层")]
    public LayerMask player;

    [Header("怪物血量")]
    public int maxHealth;
    public float currentHealth;


    [Header("音效")]
    public AudioSource Attack;
    public AudioSource Alert;
    public AudioSource Move;
    public AudioSource hurt;

    [Header("火球")]
    public GameObject fireball;
    public Transform fireballAppearPosition;//我在火虫头部挂了个空物体作为火球生成的位置

    public GameObject health;
    public GameObject floatPoint;
    public GameObject coin;//掉落物品
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
        observe = observeRegion.GetComponent<Collider2D>();//获取检查区域
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

        if (!observe.IsTouchingLayers(player))//侦测到了玩家
        {
            Move.Play();
            if (facingLeft)//火虫头部朝左
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                ani.SetBool("walking", true);
                ani.SetBool("idle", false);
                if (transform.position.x < left)//如果巡逻过了左端点，就转头
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
