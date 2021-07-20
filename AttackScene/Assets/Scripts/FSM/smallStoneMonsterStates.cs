using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleState : IState
{

    private smallStoneMonsterFSM manager;
    private parameter p;

    private float timer;
    public idleState(smallStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("smallIdle");
    }

    public void OnExit()
    {
        timer = 0;
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;
        if (p.getHit == true)
        {
            manager.transitionState(stateType.hit);
            return;
        }
        if (p.target != null &&
            p.target.position.x >= p.leftChasePosition &&
            p.target.position.x <= p.rightChasePosition)
        {
            manager.transitionState(stateType.chase);
        }
        if(timer > p.idleTime)
        {
            manager.transitionState(stateType.patrol);
        }
    }
}

public class patrolState : IState
{
    
    private smallStoneMonsterFSM manager;
    private parameter p;

    private int patrolTarget;

    public patrolState(smallStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("smallWalk");
    }

    public void OnExit()
    {
        patrolTarget++;
        if(patrolTarget >= 2)
        {
            patrolTarget = 0;
        }
    }

    public void OnUpdate()
    {
        if (p.getHit == true)
        {
            manager.transitionState(stateType.hit);
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

        if (p.getHit == true)
        {
            manager.transitionState(stateType.hit);
        }

        if(p.target != null && p.target.position.x >= p.leftChasePosition &&
            p.target.position.x <= p.rightChasePosition)
        {
            manager.transitionState(stateType.chase);
        }

        manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            new Vector2(f,manager.transform.position.y), p.moveSpeed * Time.deltaTime);

        if(Vector2.Distance(manager.transform.position,new Vector2(f, manager.transform.position.y)) <= .1f)
        {
            manager.transitionState(stateType.idle);
        }
        
    }
}

public class chaseState : IState
{

    private smallStoneMonsterFSM manager;
    private parameter p;
    private float distance;

    public chaseState(smallStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("smallWalk");
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        if (p.getHit == true)
        {
            manager.transitionState(stateType.hit);
            return;
        }

        if (p.target == null ||
            manager.transform.position.x < p.leftChasePosition ||
            manager.transform.position.x > p.rightChasePosition)
        {
            manager.transitionState(stateType.idle);
            return;
        }

        manager.flipTo(p.target.position.x);

        if (p.target)
        {
            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
               new Vector2(p.target.transform.position.x, manager.transform.position.y), p.chaseSpeed * Time.deltaTime);
        }

        

        if (Physics2D.OverlapCircle(p.attackPoint.position, p.attackArea, p.targetLayer))
        {
            //Debug.Log("enter the Attack zone");
            manager.transitionState(stateType.attack);
        }


    }
}

public class attackState : IState
{


    private smallStoneMonsterFSM manager;
    private parameter p;

    private AnimatorStateInfo info;
    public attackState(smallStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("smallAttack");
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        if (p.getHit == true)
        {
            manager.transitionState(stateType.hit);
            return;
        }
        info = p.ani.GetCurrentAnimatorStateInfo(0);
        if(info.normalizedTime > .95)
        {
            manager.transitionState(stateType.chase);
        }
    }
}

public class hitState : IState
{

    private smallStoneMonsterFSM manager;
    private parameter p;

    private AnimatorStateInfo info;
    public hitState(smallStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("smallHit");
        //p.health--;
    }

    public void OnExit()
    {
        p.getHit = false;
    }

    public void OnUpdate()
    {
        info = p.ani.GetCurrentAnimatorStateInfo(0);
        if(p.health <= 0)
        {
            manager.transitionState(stateType.death);
        }
        if(info.normalizedTime > .95f)
        {
            p.target = GameObject.FindWithTag("Player").transform;

            manager.transitionState(stateType.chase);
        }
    }
}

public class deathState : IState
{

    private smallStoneMonsterFSM manager;
    private parameter p;


    public deathState(smallStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("smallDeath");
        
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {

    }
}
