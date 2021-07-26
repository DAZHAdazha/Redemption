using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    //攻击
    public int bathealth;
    public int batdamage;
    private SpriteRenderer sr;
    private Color originalColor;
    public float flashtime;

    //移动
    public float speed;
    public float startWaitTime;
    private float waitTime;
    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightupPos;
    public float hitSpeed;
    public float hitTime;
    //private Vector2 direction;

    public GameObject bloodEffect;

    public GameObject health;
    public GameObject floatPoint;
    public GameObject coin;//掉落物品

    private PlayerController pc;
    private bool getHit;
    private Vector2 direction;
    new private Rigidbody2D rigidbody;
    private Animator hitAnimator;




    // Start is called before the first frame update
    public void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sr=GetComponent<SpriteRenderer>();
        originalColor=sr.color;

        waitTime=startWaitTime;
        movePos.position = GetRandomPos();
        rigidbody = transform.GetComponent<Rigidbody2D>();
        hitAnimator = transform.GetChild(0).GetComponent<Animator>();
    }
    // Update is called once per frame
    public void Update()
    {
        if (bathealth <= 0)
            destory();
       Move();
    }
   
    void Move()
    {
        
        if (!getHit)
        {
            if (movePos.position.x < transform.position.x)
                transform.localScale = new Vector2(-1, 1);
            else
                transform.localScale = new Vector2(1, 1);
            transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, movePos.position) < 0.1f)
            {
                if (waitTime <= 0)
                {
                    movePos.position = GetRandomPos();
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
        else
        {
            rigidbody.velocity = direction * hitSpeed;
        }
        
    }
    void Flashcolor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor",time);
    }
    void ResetColor()
    {
        sr.color = originalColor;
    }

    private void resolveHit()
    {
        getHit = false;
        rigidbody.velocity = new Vector2(0, 0);
    }

    public void GetHit(Vector2 direction, int damage, bool isCritical)
    {
        getHit = true;
        this.direction = direction;

        bathealth -= damage;
        Flashcolor(flashtime);
        Instantiate(bloodEffect,transform.position,Quaternion.identity);
        GameObject gb = Instantiate(floatPoint, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        if (isCritical)
        {
            gb.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(255, 0, 0, 255);
            //gb.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(255, 0, 0, 0);
        }
        hitAnimator.SetTrigger("Hit");
        //health.GetComponent<health>().callUpdateHealth();
        Invoke("resolveHit", hitTime);

    }
    Vector2 GetRandomPos()
    {
        Vector2 rndPos=new Vector2(
            Random.Range(leftDownPos.position.x,rightupPos.position.x),
            Random.Range(leftDownPos.position.y,rightupPos.position.y));
        return rndPos;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && other.GetType().ToString() =="UnityEngine.PolygonCollider2D")
        {
            //pc.getDamage(batdamage);
            pc.getHit();
            pc.getDamage(batdamage - 1);
        }
    }
     public void destory()
    {
        //Destroy(health.transform.parent.gameObject);
        getCoin();
        Destroy(gameObject);
    }
     void getCoin()
    {
        Instantiate(coin, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity);
    }
}
