using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class myIdleState : IState
{
    private FSM_PuzzleRobot manager;
    private myParameter parameter;

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

    }

    public void OnExit()
    {
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
            return;
        }

        if (timer >= parameter.idleTime)
        {
            manager.TransitionState(myStateType.Patrol);
        }

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
            return;

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
            return;
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
        
}

    public void OnExit()
    {

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

        if (parameter.getHit)
        {
            manager.TransitionState(myStateType.Hit);
            return;
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
            return;
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
            return;
        }
        if (info.normalizedTime >= .95f)
        {
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