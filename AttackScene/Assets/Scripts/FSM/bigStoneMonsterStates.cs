using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigidleState : IState
{

    private bigStoneMonsterFSM manager;
    private parameterBig p;

    private float timer;
    public BigidleState(bigStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("bigIdle");
    }

    public void OnExit()
    {
        timer = 0;
    }

    public void OnUpdate()
    {
        if (p.getHit == true)
        {
            manager.transitionState(BigstateType.hit);
            return;
        }
        timer += Time.deltaTime;
        
        if (p.target != null &&
            p.target.position.x >= p.leftChasePosition &&
            p.target.position.x <= p.rightChasePosition)
        {
            manager.transitionState(BigstateType.chase);
            return;
        }
        if (timer > p.idleTime)
        {
            manager.transitionState(BigstateType.patrol);
        }
    }
}

public class BigpatrolState : IState
{

    private bigStoneMonsterFSM manager;
    private parameterBig p;

    private int patrolTarget;

    public BigpatrolState(bigStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("bigWalk");
    }

    public void OnExit()
    {
        patrolTarget++;
        if (patrolTarget >= 2)
        {
            patrolTarget = 0;
        }
    }

    public void OnUpdate()
    {
        if (p.getHit == true)
        {
            manager.transitionState(BigstateType.hit);
            return;
        }
        float f = 0;

        switch (patrolTarget)
        {
            case 0:
                manager.flipTo(p.leftPatrolPosition);
                f = p.leftPatrolPosition;
                break;
            case 1:
                manager.flipTo(p.rightPatrolPosition);
                f = p.rightPatrolPosition;
                break;
        }


        if (p.target != null && p.target.position.x >= p.leftChasePosition &&
            p.target.position.x <= p.rightChasePosition)
        {
            manager.transitionState(BigstateType.chase);
            return;
        }

        manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            new Vector2(f, manager.transform.position.y), p.moveSpeed * Time.deltaTime);

        if (Vector2.Distance(manager.transform.position, new Vector2(f, manager.transform.position.y)) <= .1f)
        {
            manager.transitionState(BigstateType.idle);
        }

    }
}

public class BigchaseState : IState
{

    private bigStoneMonsterFSM manager;
    private parameterBig p;


    public BigchaseState(bigStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("bigWalk");
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        if (p.getHit == true)
        {
            manager.transitionState(BigstateType.hit);
            return;
        }
        if (p.target == null ||
            manager.transform.position.x < p.leftChasePosition ||
            manager.transform.position.x > p.rightChasePosition)
        {
            manager.transitionState(BigstateType.idle);
            return;
        }

        manager.flipTo(p.target.position.x);

        if (p.target)
        {
            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                p.target.transform.position, p.chaseSpeed * Time.deltaTime);
        }



        if (Physics2D.OverlapCircle(p.attackPoint.position, p.attackArea, p.targetLayer))
        {
            //Debug.Log("enter the Attack zone");
            manager.transitionState(BigstateType.attack);
        }


    }
}

public class BigattackState : IState//¹¥»÷×´Ì¬
{

    private bigStoneMonsterFSM manager;
    private parameterBig p;

    private AnimatorStateInfo info;
    //private int nextAttack;
    private float timer;
    public BigattackState(bigStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        timer = 0;
        chooseAttack();
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        if (p.getHit == true)
        {
            p.health--;
            p.getHit = false;
            if(p.health <= 0)
            {
                manager.transitionState(BigstateType.death);
                return;
            }
        }

        info = p.ani.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime > .95
            && Physics2D.OverlapCircle(p.attackPoint.position, p.attackArea, p.targetLayer)
            && timer <= p.attackCD)
        {
            p.ani.Play("bigIdle");
        }
        if (info.normalizedTime > .95
            && Physics2D.OverlapCircle(p.attackPoint.position, p.attackArea, p.targetLayer)
            && timer > p.attackCD)
        {
            timer = 0;
            chooseAttack();

        }
        if (info.normalizedTime > .95
            && !Physics2D.OverlapCircle(p.attackPoint.position, p.attackArea, p.targetLayer))
        {
            manager.transitionState(BigstateType.chase);
        }
        timer += Time.deltaTime;
    }

    private void chooseAttack()
    {
        if(p.target == null)
        {
            manager.transitionState(BigstateType.chase);
            return;
        }
        if (Mathf.Abs(manager.transform.position.x - p.target.position.x) < 5) 
        {
            manager.flipTo(p.target.position.x);
            p.ani.Play("bigAttack1");
        }
        else
        {
            p.ani.Play("bigAttack2");
        }
    }
}



public class BighitState : IState
{

    private bigStoneMonsterFSM manager;
    private parameterBig p;

    private AnimatorStateInfo info;
    public BighitState(bigStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("bigHit");
        p.health--;
    }

    public void OnExit()
    {
        p.getHit = false;
    }

    public void OnUpdate()
    {
        info = p.ani.GetCurrentAnimatorStateInfo(0);
        if (p.health <= 0)
        {
            manager.transitionState(BigstateType.death);
            return;
        }
        if (info.normalizedTime > .95f)
        {
            p.target = GameObject.FindWithTag("Player").transform;

            manager.transitionState(BigstateType.chase);
        }
    }
}

public class BigdeathState : IState
{

    private bigStoneMonsterFSM manager;
    private parameterBig p;


    public BigdeathState(bigStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("bigDeath");

    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {

    }
}
