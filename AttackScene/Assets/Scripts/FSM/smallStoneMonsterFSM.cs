using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum stateType
{
    idle,attack,chase,hit,death,patrol
}

[Serializable]
public class parameter
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
    //public GameObject healthSystem;
}

public class smallStoneMonsterFSM : MonoBehaviour
{

    private IState currentState;
    private Dictionary<stateType, IState> states = new Dictionary<stateType, IState>();
    public parameter p;

    public GameObject damageRegion1;
    //public GameObject damageRegion2;

    // Start is called before the first frame update
    void Start()
    {
        states.Add(stateType.idle, new idleState(this));
        states.Add(stateType.patrol, new patrolState(this));
        states.Add(stateType.chase, new chaseState(this));
        states.Add(stateType.attack, new attackState(this));
        states.Add(stateType.hit, new hitState(this));
        states.Add(stateType.death, new deathState(this));
        p.rb = transform.GetComponent<Rigidbody2D>();

        p.leftPatrolPosition = p.patrolPoints[0].position.x;
        p.rightPatrolPosition = p.patrolPoints[1].position.x;
        p.leftChasePosition = p.chasePoints[0].position.x;
        p.rightChasePosition = p.chasePoints[1].position.x;
        for(int i = 0; i < p.patrolPoints.Length; i++)
        {
            Destroy(p.patrolPoints[i].gameObject);
            Destroy(p.chasePoints[i].gameObject);

        }

        currentState = new idleState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    public void transitionState(stateType s)
    {
        if(currentState != null)
        {
            currentState.OnExit();
        }
        currentState = states[s];
        currentState.OnEnter();
    }

    private void Awake()
    {
        p.health = GameSaver.difficultySetting[GameSaver.difficulty]["smallStoneHealth"];
        p.attackValue = GameSaver.difficultySetting[GameSaver.difficulty]["smallStoneAttack"];
    }

    public void flipTo(float x)
    {
        if(transform.position.x > x)
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

    private void turnOnDamageRegion1()
    {
        damageRegion1.SetActive(true);
    }

    //private void turnOnDamageRegion2()
    //{
    //    damageRegion2.SetActive(true);
    //}

    private void turnOffDamageRegion1()
    {
        damageRegion1.SetActive(false);
    }

    //private void turnOffDamageRegion2()
    //{
    //    damageRegion2.SetActive(false);
    //}

}
