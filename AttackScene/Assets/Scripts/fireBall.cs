using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour
{


    public float attackPoint;

    private Rigidbody2D rb;
    

    private Animator ani;

    [Header("Íæ¼Ò")]
    public LayerMask player;
    public Transform playerTransform;

    [Header("Åö×²Æ÷")]
    public Collider2D colid;

    [Header("»ðÇòËÙ¶È")]
    public float fireBallSpeed;
    private Vector2 moveDirection;

    public GameObject Groundfire;

    private bool touchedPlayer = false;
    private Vector2 fireballVelo;

    private Vector2 shootLocation;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        if (GameObject.Find("Player"))
        {
            playerTransform = GameObject.Find("Player").GetComponent<Transform>();
            moveDirection = playerTransform.position - transform.position;
        }
        else
        {
            moveDirection = new Vector2 (0,transform.position.y-1 - transform.position.y);
        }
            
        float longSide = Mathf.Sqrt(moveDirection.x * moveDirection.x + moveDirection.y * moveDirection.y);
        float xSpeed = fireBallSpeed * (moveDirection.x/longSide);
        float ySpeed = fireBallSpeed * (moveDirection.y / longSide);
        //Debug.Log(xSpeed);
        //Debug.Log(ySpeed);
        fireballVelo = new Vector2(xSpeed,ySpeed);
        
        shootLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!touchedPlayer)
        {
            changeDirection();
            rb.velocity = fireballVelo;
        }
        float xDistance = Mathf.Abs(transform.position.x - shootLocation.x);
        float yDistance = Mathf.Abs(transform.position.y - shootLocation.y);
        float distance;
        distance = Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance);
        if(distance > 200)
        {
            Destroy(gameObject);
        }
        
    }

    void changeDirection()
    {
        transform.right = fireballVelo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rb.velocity = new Vector2(0, 0);
            touchedPlayer = true;
            ani.SetTrigger("explode");
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.velocity = new Vector2(0, 0);
            touchedPlayer = true;
            ani.SetTrigger("explode");
            Vector2 collisionPosition = gameObject.transform.position;
            GameObject Fire = (GameObject)Instantiate(Groundfire, collisionPosition, Quaternion.identity);

        }
    }

    private void destoryFireBall()
    {
        Destroy(gameObject);
    }
}
