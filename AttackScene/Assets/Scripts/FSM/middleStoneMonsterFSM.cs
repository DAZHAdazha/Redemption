using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MiddlestateType
{
    idle, attack, chase, hit, death, patrol
}

[Serializable]
public class parameterMiddle
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
    public float dodgeDistance;
    public float jumpSpeed;
    public float dodgeCd;
}

public class middleStoneMonsterFSM : MonoBehaviour
{

    private IState currentState;
    private Dictionary<MiddlestateType, IState> states = new Dictionary<MiddlestateType, IState>();
    public parameterMiddle p;
    
    [Header("…À∫¶∑∂Œß")]
    public GameObject damageRegionA1P1;//attack1 phase1
    public GameObject damageRegionA1P2;
    public GameObject damageRegionA1P3;
    public GameObject damageRegionA2l;//left
    public GameObject damageRegionA2r;//right

    // Start is called before the first frame update
    void Start()
    {
        states.Add(MiddlestateType.idle, new MiddleidleState(this));
        states.Add(MiddlestateType.patrol, new MiddlepatrolState(this));
        states.Add(MiddlestateType.chase, new MiddlechaseState(this));
        states.Add(MiddlestateType.attack, new MiddleattackState(this));
        states.Add(MiddlestateType.hit, new MiddlehitState(this));
        states.Add(MiddlestateType.death, new MiddledeathState(this));
        p.leftPatrolPosition = p.patrolPoints[0].position.x;
        p.rightPatrolPosition = p.patrolPoints[1].position.x;
        p.leftChasePosition = p.chasePoints[0].position.x;
        p.rightChasePosition = p.chasePoints[1].position.x;
        for (int i = 0; i < p.patrolPoints.Length; i++)
        {
            Destroy(p.patrolPoints[i].gameObject);
            Destroy(p.chasePoints[i].gameObject);

        }

        currentState = new MiddleidleState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    public void transitionState(MiddlestateType s)
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

    private void turnOnDamageRegionA1P1()
    {
        damageRegionA1P1.SetActive(true);
    }

    private void turnOnDamageRegionA1P2()
    {
        damageRegionA1P2.SetActive(true);
    }

    private void turnOnDamageRegionA1P3()
    {
        damageRegionA1P3.SetActive(true);
    }

    private void turnOffDamageRegionA1P1()
    {
        damageRegionA1P1.SetActive(false);
    }

    private void turnOffDamageRegionA1P2()
    {
        damageRegionA1P2.SetActive(false);
    }

    private void turnOffDamageRegionA1P3()
    {
        damageRegionA1P3.SetActive(false);
    }

    private void turnOnDamageRegionA2()
    {
        damageRegionA2l.SetActive(true);
        damageRegionA2r.SetActive(true);
    }

    private void turnOffDamageRegionA2()
    {
        damageRegionA2l.SetActive(false);
        damageRegionA2r.SetActive(false);
    }

    //private void DestoryMonster()
    //{
    //    Destroy(gameObject);
    //}
}
