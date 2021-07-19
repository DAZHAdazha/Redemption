using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum nightmareStateType1
{
    Idle, Patrol,Vanish, Appear, Attack, Hit, Death
}

[Serializable]
public class nightmareParameter1
{
    public int health;
    public float moveSpeed;
    public Transform target;
    public Animator animator;
    public Collider2D collider;
    public Rigidbody2D rigidbody;
    public bool getHit;

    public float awakeArea;
    public float patrolTime;
    public float attackArea;
    public float vanishTime;
    public float attackCoolDown;
    public float dangerHealth;


    public Transform worldBoundaryLeft, worldBoundaryRight;
    public GameObject healthSystem;
    public GameObject healthCanvas;
}
public class FSM_Nightmare1 : MonoBehaviour
{

    private IState currentState;
    private Dictionary<nightmareStateType1, IState> states = new Dictionary<nightmareStateType1, IState>();

    public nightmareParameter1 parameter;
    void Start()
    {
        parameter.animator = transform.GetComponent<Animator>();
        parameter.target = GameObject.Find("Player").transform;
        parameter.rigidbody = transform.GetComponent<Rigidbody2D>();
        parameter.collider = transform.GetComponent<Collider2D>();
        parameter.healthSystem = transform.GetComponent<NightMare1>().health;
        states.Add(nightmareStateType1.Idle, new IdleStateNightmare1(this));
        states.Add(nightmareStateType1.Patrol, new PatrolStateNightmare1(this));
        states.Add(nightmareStateType1.Vanish, new VanishStateNightmare1(this));
        states.Add(nightmareStateType1.Appear, new AppearStateNightmare1(this));
        states.Add(nightmareStateType1.Attack, new AttackStateNightmare1(this));
        states.Add(nightmareStateType1.Hit, new HitStateNightmare1(this));
        states.Add(nightmareStateType1.Death, new DeathStateNightmare1(this));

        TransitionState(nightmareStateType1.Idle);


    }

    void Update()
    {
        currentState.OnUpdate();

    }

    public void TransitionState(nightmareStateType1 type)
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

    //public void generatePuzzleAttack(float direction)
    //{
    //    GameObject ob = Instantiate(parameter.puzzleAttack, new Vector2(transform.position.x, transform.position.y + 0.7f), Quaternion.identity);
    //    ob.GetComponent<PuzzleAttack>().direction = direction;
    //}

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