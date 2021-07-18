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
    //private Vector2 direction;

    public GameObject bloodEffect;

    private PlayerController pc;


    public GameObject health;
    public GameObject floatPoint;
    public GameObject coin;//掉落物品


    // Start is called before the first frame update
    public void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sr=GetComponent<SpriteRenderer>();
        originalColor=sr.color;

        waitTime=startWaitTime;
        movePos.position = GetRandomPos();
        
    }
    // Update is called once per frame
    public void Update()
    {
        if(bathealth <= 0)
            destory();
       Move();
    }
   
    void Move()
    {
        if(movePos.position.x < transform.position.x)
            transform.localScale= new Vector2(-1,1);
        else
            transform.localScale = new Vector2(1,1);
        transform.position=Vector2.MoveTowards(transform.position,movePos.position,speed*Time.deltaTime);
        if(Vector2.Distance(transform.position,movePos.position)<0.1f)
        {
            if(waitTime<=0)
            {
                movePos.position=GetRandomPos();
                waitTime=startWaitTime;
            }
            else
            {
                waitTime-=Time.deltaTime;
            }
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
    public void GetHit(int damage, bool isCritical)
    {
        bathealth -= damage;
        Flashcolor(flashtime);
        Instantiate(bloodEffect,transform.position,Quaternion.identity);
        GameObject gb = Instantiate(floatPoint, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        if (isCritical)
        {
            gb.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(255, 0, 0, 255);
        }
        //health.GetComponent<health>().callUpdateHealth();
        
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
            pc.getDamage(batdamage);
            pc.getHit();
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
