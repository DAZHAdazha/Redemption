using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class myIdleState : IState
{
    private FSM_PuzzleRobot manager;
    private myParameter parameter;

    //private float timer;
    public myIdleState(FSM_PuzzleRobot manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.healthSystem.SetActive(false);
        parameter.animator.Play("Idle");
    }

    public void OnUpdate()
    {
        if (parameter.isAwake)
        {
            manager.TransitionState(myStateType.React);
        }
        //timer += Time.deltaTime;

        //if (parameter.getHit)
        //{
        //    manager.TransitionState(myStateType.Hit);
        //}
        //if (parameter.target != null &&
        //    parameter.target.position.x >= parameter.chasePoints[0].position.x &&
        //    parameter.target.position.x <= parameter.chasePoints[1].position.x)
        //{
        //    manager.TransitionState(myStateType.React);
        //}
        //if (timer >= parameter.idleTime)
        //{
        //    manager.TransitionState(myStateType.
        //    );
        //}
    }

    public void OnExit()
    {
        //timer = 0;
    }
}

public class myReactState : IState
{
    private FSM_PuzzleRobot manager;
    private myParameter parameter;

    //private AnimatorStateInfo info;
    private float timer;
    public myReactState(FSM_PuzzleRobot manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.healthSystem.SetActive(true);
        parameter.animator.Play("Awake");
    }

    public void OnUpdate()
    {
         timer += Time.deltaTime;
        //info = parameter.animator.GetCurrentAnimatorStateInfo(0);

        if (parameter.getHit)
        {
            manager.TransitionState(myStateType.Hit);
        }

        if (timer >= parameter.idleTime)
        {
            manager.TransitionState(myStateType.Patrol);
        }


        //if (info.normalizedTime >= .95f)
        //{
        //    manager.TransitionState(myStateType.Chase);
        //}
    }

    public void OnExit()
    {
        timer = 0;
    }

}

public class myPatrolState : IState
{
    private FSM_PuzzleRobot manager;
    private myParameter parameter;
    //private int patrolPosition;
    private float distance;
    private float dashCoolDown;
    private float dashCoolDownTimer = 1000f;

    public myPatrolState(FSM_PuzzleRobot manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        dashCoolDown = parameter.dashCoolDown;
        parameter.animator.Play("Move");
    }

    public void OnUpdate()
    {
        manager.transform.position = new Vector2(Mathf.Clamp(manager.transform.position.x, parameter.worldBoundaryLeft.position.x, parameter.worldBoundaryRight.position.x), manager.transform.position.y);
        dashCoolDownTimer += Time.deltaTime;
        distance = Vector2.Distance( manager.transform.position, parameter.target.position);
        //Debug.Log(distance);
        if (distance < parameter.dangerArea && dashCoolDownTimer > dashCoolDown)
        {
            manager.FlipTo(parameter.target);
            dashCoolDownTimer = 0f;
            //Debug.Log("Dash");
            manager.TransitionState(myStateType.Dash);

        }
        else if(distance>= parameter.dangerArea && distance <= parameter.runArea)
        {
            //Debug.Log("Run");
            manager.FlipTo(parameter.target, true);
            if (parameter.target.position.x > manager.transform.position.x)
            {
                parameter.rigidbody.velocity = new Vector2(-parameter.moveSpeed, parameter.rigidbody.velocity.y);
            }
            else
            {
                parameter.rigidbody.velocity = new Vector2(parameter.moveSpeed, parameter.rigidbody.velocity.y);
            }
            
        }
        else if(distance>parameter.runArea && distance < parameter.chaseArea)
        {
            manager.FlipTo(parameter.target);
            //Debug.Log("Shot");
            manager.TransitionState(myStateType.Attack);
        }
        else
        {
            manager.FlipTo(parameter.target);
            if (parameter.target.position.x > manager.transform.position.x)
            {
                parameter.rigidbody.velocity = new Vector2(parameter.moveSpeed, parameter.rigidbody.velocity.y);
            }
            else
            {
                parameter.rigidbody.velocity = new Vector2(-parameter.moveSpeed, parameter.rigidbody.velocity.y);
            }
        }
        
    //manager.FlipTo(parameter.patrolPoints[patrolPosition]);

    //manager.transform.position = Vector2.MoveTowards(manager.transform.position,
    //    parameter.patrolPoints[patrolPosition].position, parameter.moveSpeed * Time.deltaTime);

    //if (parameter.getHit)
    //{
    //    manager.TransitionState(myStateType.Hit);
    //}
    //if (parameter.target != null &&
    //    parameter.target.position.x >= parameter.chasePoints[0].position.x &&
    //    parameter.target.position.x <= parameter.chasePoints[1].position.x)
    //{
    //    manager.TransitionState(myStateType.React);
    //}
    //if (Vector2.Distance(manager.transform.position, parameter.patrolPoints[patrolPosition].position) < .1f)
    //{
    //    manager.TransitionState(myStateType.Idle);
    //}
}

    public void OnExit()
    {
        //patrolPosition++;

        //if (patrolPosition >= parameter.patrolPoints.Length)
        //{
        //    patrolPosition = 0;
        //}

    }
}

public class myDashState : IState
{
    private FSM_PuzzleRobot manager;
    private myParameter parameter;
    private AnimatorStateInfo info;
    private bool direction;

    public myDashState(FSM_PuzzleRobot manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        
        parameter.animator.Play("Dash");
        if (manager.transform.position.x < parameter.target.transform.position.x)
        {
            direction = true;
        }
        else
        {
            direction = false;
        }
    }

    public void OnUpdate()
    {
        //manager.FlipTo(parameter.target);
        //if (parameter.target)
        //    manager.transform.position = Vector2.MoveTowards(manager.transform.position,
        //    new Vector2(parameter.target.position.x, manager.transform.position.y), parameter.chaseSpeed * Time.deltaTime);
        if (parameter.getHit)
        {
            manager.TransitionState(myStateType.Hit);
        }
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= .95f)
        {

            if (!direction)
            {
                manager.transform.position = new Vector2(manager.transform.position.x - parameter.dashLength
                        , manager.transform.position.y);
            }
            else
             {
                 manager.transform.position = new Vector2(manager.transform.position.x + parameter.dashLength
                        , manager.transform.position.y);
             }

            manager.TransitionState(myStateType.Patrol);
            
        }
        //if (parameter.target == null ||
        //    manager.transform.position.x < parameter.chasePoints[0].position.x ||
        //    manager.transform.position.x > parameter.chasePoints[1].position.x)
        //{
        //    manager.TransitionState(myStateType.Idle);
        //}
        //if (Physics2D.OverlapCircle(parameter.attackPoint.position, parameter.attackArea, parameter.targetLayer))
        //{
        //    manager.TransitionState(myStateType.Attack);
        //}
    }

    public void OnExit()
    {
    }
}



public class myAttackState : IState
{
    private FSM_PuzzleRobot manager;
    private myParameter parameter;

    private AnimatorStateInfo info;
    public myAttackState(FSM_PuzzleRobot manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Shoot");
        manager.generatePuzzleAttack(-manager.transform.localScale.x);
    }

    public void OnUpdate()
    {

        
        manager.FlipTo(parameter.target);
        if (parameter.getHit)
        {
            manager.TransitionState(myStateType.Hit);
        }
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= .95f)
        {
            manager.TransitionState(myStateType.Patrol);
        }
    }

    public void OnExit()
    {

    }
}

public class myHitState : IState
{
    private FSM_PuzzleRobot manager;
    private myParameter parameter;

    private AnimatorStateInfo info;
    public myHitState(FSM_PuzzleRobot manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Hurt");
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);

        if (parameter.health <= 0)
        {
            manager.TransitionState(myStateType.Death);
        }
        if (info.normalizedTime >= .95f)
        {
            //parameter.target = GameObject.FindWithTag("Player").transform;

            manager.TransitionState(myStateType.Patrol);
        }
    }

    public void OnExit()
    {
        parameter.getHit = false;
    }
}

public class myDeathState : IState
{
    //private FSM_PuzzleRobot manager;
    private myParameter parameter;

    public myDeathState(FSM_PuzzleRobot manager)
    {
        //this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Death");
        parameter.collider.enabled = false;
        parameter.rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}