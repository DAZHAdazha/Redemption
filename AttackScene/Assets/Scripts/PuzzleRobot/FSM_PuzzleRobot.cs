using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum myStateType
{
    Idle, Patrol, Dash, React, Attack, Hit, Death
}

[Serializable]
public class myParameter
{
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
    public Animator animator;
    public Collider2D collider;
    public Rigidbody2D rigidbody;
    public bool getHit;
    public bool isAwake;
    public float dangerArea;
    public float runArea;
    public float chaseArea;
    public float dashLength;
    public float dashCoolDown;
}
public class FSM_PuzzleRobot : MonoBehaviour
{

    private IState currentState;
    private Dictionary<myStateType, IState> states = new Dictionary<myStateType, IState>();

    public myParameter parameter;
    void Start()
    {
        parameter.animator = transform.GetComponent<Animator>();
        parameter.target = GameObject.Find("Player").transform;
        parameter.rigidbody = transform.GetComponent<Rigidbody2D>();
        parameter.collider = transform.GetComponent<Collider2D>();
        states.Add(myStateType.Idle, new myIdleState(this));
        states.Add(myStateType.Patrol, new myPatrolState(this));
        states.Add(myStateType.Dash, new myDashState(this));
        states.Add(myStateType.React, new myReactState(this));
        states.Add(myStateType.Attack, new myAttackState(this));
        states.Add(myStateType.Hit, new myHitState(this));
        states.Add(myStateType.Death, new myDeathState(this));

        TransitionState(myStateType.Idle);


    }

    void Update()
    {
        currentState.OnUpdate();

    }

    public void TransitionState(myStateType type)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter();
    }

    public void FlipTo(Transform target,bool backwards=false)
    {
        if (target != null)
        {
            if (transform.position.x > target.position.x)
            {
                if (!backwards)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                
            }
            else if (transform.position.x < target.position.x)
            {
                if (!backwards)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                    
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        parameter.target = other.transform;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        parameter.target = null;
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(parameter.attackPoint.position, parameter.attackArea);
    //}
}