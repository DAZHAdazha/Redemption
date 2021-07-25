using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum nightmareStateType2
{
    Idle, Patrol,Vanish, Appear, Attack, Hit, Death,Sweep,Defense
}

[Serializable]
public class nightmareParameter2
{
    public float attackPoint;
    public float sweepPoint;
    public int health;
    public float moveSpeed;
    public Transform target;
    public Animator animator;
    public Collider2D collider;
    public Rigidbody2D rigidbody;
    public bool getHit;

    public float awakeArea;
    public float patrolTime;
    public float sweepArea;
    public float attackArea;
    public float vanishTime;
    public float attackCoolDown;
    public float sweepCoolDown;
    public float dangerHealth;
    public float defenseCoolDownTime;
    public bool isDefense = false;
    public int healPoint;


    public Transform worldBoundaryLeft, worldBoundaryRight;
    public GameObject healthSystem;
    public GameObject healthCanvas;
}
public class FSM_Nightmare2 : MonoBehaviour
{

    private IState currentState;
    private Dictionary<nightmareStateType2, IState> states = new Dictionary<nightmareStateType2, IState>();

    public nightmareParameter2 parameter;
    void Start()
    {
        parameter.animator = transform.GetComponent<Animator>();
        parameter.target = GameObject.Find("Player").transform;
        parameter.rigidbody = transform.GetComponent<Rigidbody2D>();
        parameter.collider = transform.GetComponent<Collider2D>();
        parameter.healthSystem = transform.GetComponent<NightMare2>().health;
        states.Add(nightmareStateType2.Idle, new IdleStateNightmare2(this));
        states.Add(nightmareStateType2.Patrol, new PatrolStateNightmare2(this));
        states.Add(nightmareStateType2.Vanish, new VanishStateNightmare2(this));
        states.Add(nightmareStateType2.Appear, new AppearStateNightmare2(this));
        states.Add(nightmareStateType2.Attack, new AttackStateNightmare2(this));
        states.Add(nightmareStateType2.Hit, new HitStateNightmare2(this));
        states.Add(nightmareStateType2.Death, new DeathStateNightmare2(this));
        states.Add(nightmareStateType2.Sweep, new SweepStateNightmare2(this));
        states.Add(nightmareStateType2.Defense, new DefenseStateNightmare2(this));

        TransitionState(nightmareStateType2.Idle);


    }

    private void Awake()
    {
        parameter.health = GameSaver.difficultySetting[GameSaver.difficulty]["nightmare2Health"];
        parameter.attackPoint = GameSaver.difficultySetting[GameSaver.difficulty]["nightmare2Attack"];
        parameter.sweepPoint = GameSaver.difficultySetting[GameSaver.difficulty]["nightmare2Sweep"];
    }

    void Update()
    {
        currentState.OnUpdate();

    }

    public void TransitionState(nightmareStateType2 type)
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

}