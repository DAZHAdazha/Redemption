using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BigstateType
{
    idle, attack, chase, hit, death, patrol
}

[Serializable]
public class parameterBig
{
    public float attackValue;
    public int health;
    public float moveSpeed;
    public float chaseSpeed;
    public float idleTime;
    public Transform[] patrolPoints;
    public Transform[] chasePoints;
    public Transform target;
    public LayerMask targetLayer;
    public Transform attackPoint;
    public float attackArea;
    public Animator ani;
    public bool getHit;
    public Rigidbody2D rb;
    public float leftPatrolPosition;
    public float rightPatrolPosition;
    public float leftChasePosition;
    public float rightChasePosition;
    public float attackCD;
    
}

public class bigStoneMonsterFSM : MonoBehaviour
{

    private IState currentState;
    private Dictionary<BigstateType, IState> states = new Dictionary<BigstateType, IState>();
    public parameterBig p;

    [Header("ÉËº¦·¶Î§")]
    public GameObject damageRegionA1;
    public GameObject damageRegionA2;
    
    [Header("ÌøÔ¾ËÙ¶È")]
    public float jumpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        states.Add(BigstateType.idle, new BigidleState(this));
        states.Add(BigstateType.patrol, new BigpatrolState(this));
        states.Add(BigstateType.chase, new BigchaseState(this));
        states.Add(BigstateType.attack, new BigattackState(this));
        states.Add(BigstateType.hit, new BighitState(this));
        states.Add(BigstateType.death, new BigdeathState(this));
        p.leftPatrolPosition = p.patrolPoints[0].position.x;
        p.rightPatrolPosition = p.patrolPoints[1].position.x;
        p.leftChasePosition = p.chasePoints[0].position.x;
        p.rightChasePosition = p.chasePoints[1].position.x;
        for (int i = 0; i < p.patrolPoints.Length; i++)
        {
            Destroy(p.patrolPoints[i].gameObject);
            Destroy(p.chasePoints[i].gameObject);

        }

        currentState = new BigidleState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    public void transitionState(BigstateType s)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = states[s];
        currentState.OnEnter();
    }

    public void flipTo(float x)
    {
        if (transform.position.x > x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            p.target = collision.transform;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            p.target = collision.transform;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            p.target = null;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(p.attackPoint.position, p.attackArea);
    }

    private void turnOnDamageRegionA1()
    {
        damageRegionA1.SetActive(true);
    }

    private void turnOnDamageRegionA2()
    {
        damageRegionA2.SetActive(true);
    }

    

    private void turnOffDamageRegionA1()
    {
        damageRegionA1.SetActive(false);
    }

    private void turnOffDamageRegionA2()
    {
        damageRegionA2.SetActive(false);
    }


    private void DestoryMonster()
    {
        Destroy(gameObject);
    }

    private void jumpToPlayer()
    {
        GameObject hero = GameObject.FindWithTag("Player");
        if(transform.position.x > hero.transform.position.x)
        {
            p.rb.velocity = new Vector2(-jumpSpeed,10);
        }
        else
        {
            p.rb.velocity = new Vector2(jumpSpeed, 10);
        }
        
    }
}
