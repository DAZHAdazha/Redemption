using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleidleState : IState
{

    private middleStoneMonsterFSM manager;
    private parameterMiddle p;

    private float timer;
    public MiddleidleState(middleStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("middleIdle");
    }

    public void OnExit()
    {
        timer = 0;
    }

    public void OnUpdate()
    {
        if (p.getHit == true)
        {
            manager.transitionState(MiddlestateType.hit);
            return;
        }
        timer += Time.deltaTime;
        
        if (p.target != null &&
            p.target.position.x >= p.leftChasePosition &&
            p.target.position.x <= p.rightChasePosition)
        {
            manager.transitionState(MiddlestateType.chase);
        }
        if (timer > p.idleTime)
        {
            manager.transitionState(MiddlestateType.patrol);
        }
    }
}

public class MiddlepatrolState : IState
{

    private middleStoneMonsterFSM manager;
    private parameterMiddle p;

    private int patrolTarget;

    public MiddlepatrolState(middleStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("middleWalk");
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
            manager.transitionState(MiddlestateType.hit);
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
            manager.transitionState(MiddlestateType.chase);
        }

        manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            new Vector2(f, manager.transform.position.y), p.moveSpeed * Time.deltaTime);

        if (Vector2.Distance(manager.transform.position, new Vector2(f, manager.transform.position.y)) <= .1f)
        {
            manager.transitionState(MiddlestateType.idle);
        }

    }
}

public class MiddlechaseState : IState
{

    private middleStoneMonsterFSM manager;
    private parameterMiddle p;


    public MiddlechaseState(middleStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("middleWalk");
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        if (p.getHit == true)
        {
            manager.transitionState(MiddlestateType.hit);
            return;
        }
        if (p.target == null ||
            manager.transform.position.x < p.leftChasePosition ||
            manager.transform.position.x > p.rightChasePosition)
        {
            manager.transitionState(MiddlestateType.idle);
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
            Debug.Log("enter the Attack zone");
            manager.transitionState(MiddlestateType.attack);
        }


    }
}

public class MiddleattackState : IState//¹¥»÷×´Ì¬
{

    private middleStoneMonsterFSM manager;
    private parameterMiddle p;

    private AnimatorStateInfo info;
    private int nextAttack;
    private float timer;
    public MiddleattackState(middleStoneMonsterFSM manager)
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
        
        info = p.ani.GetCurrentAnimatorStateInfo(0);
        if(info.normalizedTime > .95
            && Physics2D.OverlapCircle(p.attackPoint.position, p.attackArea, p.targetLayer)
            && timer <= p.attackCD){
            if (p.getHit == true)
            {
                manager.transitionState(MiddlestateType.hit);
                return;
            }
            p.ani.Play("middleIdle");
        }
        if (info.normalizedTime > .95 
            && Physics2D.OverlapCircle(p.attackPoint.position, p.attackArea, p.targetLayer)
            && timer > p.attackCD)
        {
            timer = 0;
            chooseAttack();
            
        }
        if(info.normalizedTime > .95
            && !Physics2D.OverlapCircle(p.attackPoint.position, p.attackArea, p.targetLayer))
        {
            if (p.getHit == true)
            {
                manager.transitionState(MiddlestateType.hit);
                return;
            }
            manager.transitionState(MiddlestateType.chase);
        }
        timer += Time.deltaTime;
    }

    private void chooseAttack()
    {
        nextAttack = Random.Range(1, 3);
        if (nextAttack == 1)
        {
            p.ani.Play("middleAttack1");
            if (p.getHit == true)
            {
                manager.transitionState(MiddlestateType.hit);
                return;
            }
        }
        else
        {
            p.ani.Play("middleAttack2");
        }
    }
}



public class MiddlehitState : IState
{

    private middleStoneMonsterFSM manager;
    private parameterMiddle p;

    private AnimatorStateInfo info;
    public MiddlehitState(middleStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("damaged");
        //p.health--;
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
            manager.transitionState(MiddlestateType.death);
        }
        if (info.normalizedTime > .95f)
        {
            p.target = GameObject.FindWithTag("Player").transform;

            manager.transitionState(MiddlestateType.chase);
        }
    }
}

public class MiddledeathState : IState
{

    private middleStoneMonsterFSM manager;
    private parameterMiddle p;


    public MiddledeathState(middleStoneMonsterFSM manager)
    {
        this.manager = manager;
        this.p = manager.p;
    }
    public void OnEnter()
    {
        p.ani.Play("death");

    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {

    }
}
