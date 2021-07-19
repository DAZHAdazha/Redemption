using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleStateNightmare1 : IState
{
    private FSM_Nightmare1 manager;
    private nightmareParameter1 parameter;
    private float distance;

    //private float timer;
    public IdleStateNightmare1(FSM_Nightmare1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Idle");
    }

    public void OnUpdate()
    {

        distance = Vector2.Distance(manager.transform.position, parameter.target.position);
        if (distance < parameter.awakeArea)
        {
            manager.TransitionState(nightmareStateType1.Patrol);
        }

    }

    public void OnExit()
    {
    }
}

public class PatrolStateNightmare1 : IState
{
    private FSM_Nightmare1 manager;
    private nightmareParameter1 parameter;

    private float timer;
    private float distance;


    public PatrolStateNightmare1(FSM_Nightmare1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Walk");
    }

    public void OnUpdate()
    {
        //受伤
        if (parameter.getHit && !parameter.unattackable)
        {
            manager.TransitionState(nightmareStateType1.Hit);
        }

        //限制位置
        manager.transform.position = new Vector2(Mathf.Clamp(manager.transform.position.x, parameter.worldBoundaryLeft.position.x, parameter.worldBoundaryRight.position.x), manager.transform.position.y);
        timer += Time.deltaTime;
        distance = Vector2.Distance(manager.transform.position, parameter.target.position);

        //移动
        manager.FlipTo(parameter.target);
        if (parameter.target.position.x > manager.transform.position.x)
        {
            parameter.rigidbody.velocity = new Vector2(parameter.moveSpeed, parameter.rigidbody.velocity.y);
        }
        else
        {
            parameter.rigidbody.velocity = new Vector2(-parameter.moveSpeed, parameter.rigidbody.velocity.y);
        }


        if (timer > parameter.patrolTime && distance >= parameter.attackArea)
        {
            manager.TransitionState(nightmareStateType1.Vanish);
        }

        if(distance < parameter.attackArea)
        {
            manager.TransitionState(nightmareStateType1.Attack);
        }


    }

    public void OnExit()
    {
        timer = 0;
    }
}

public class VanishStateNightmare1 : IState
{
    private FSM_Nightmare1 manager;
    private nightmareParameter1 parameter;
    private AnimatorStateInfo info;

    private float timer;

    public VanishStateNightmare1(FSM_Nightmare1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("vanish");
        manager.gameObject.tag = "Untagged";
        //parameter.unattackable = true;
        parameter.healthCanvas.SetActive(false);
        //无法收到伤害 改变layer
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (timer > parameter.vanishTime && info.normalizedTime >= .95f)
        {
            manager.TransitionState(nightmareStateType1.Appear);
        }
     

    }

    public void OnExit()
    {
        timer = 0;
    }
}


public class AppearStateNightmare1 : IState
{
    private FSM_Nightmare1 manager;
    private nightmareParameter1 parameter;

    private AnimatorStateInfo info;
    //private float timer;
    public AppearStateNightmare1(FSM_Nightmare1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("appear");
        manager.transform.position = parameter.target.position;//瞬移
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= .95f)
        {
            manager.TransitionState(nightmareStateType1.Attack);
        }
    }

    public void OnExit()
    {
        //无敌结束 将Layer改回来
        manager.gameObject.tag = "Nightmare1";
        //parameter.unattackable = false;
        parameter.healthCanvas.SetActive(true);
    }

}







public class AttackStateNightmare1 : IState
{
    private FSM_Nightmare1 manager;
    private nightmareParameter1 parameter;
    private float timer;

    private AnimatorStateInfo info;
    public AttackStateNightmare1(FSM_Nightmare1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Attack");
        if (parameter.health <= parameter.dangerHealth)
        {
            //manager.gameObject.tag = "Untagged";
            parameter.unattackable = true;
        }
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;
        if (parameter.getHit && !parameter.unattackable)
        {
            manager.TransitionState(nightmareStateType1.Hit);
        }
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
       
        if (parameter.health <= parameter.dangerHealth)
        {
            if (info.normalizedTime >= .95)
            {
                //manager.gameObject.tag = "Nightmare1";
                parameter.unattackable = false;
            }
            
        }
        if (info.normalizedTime >= .95f && timer > parameter.attackCoolDown)
        {
            manager.TransitionState(nightmareStateType1.Patrol);
        }
    }

    public void OnExit()
    {
        
        timer = 0;
    }
}

public class HitStateNightmare1 : IState
{
    private FSM_Nightmare1 manager;
    private nightmareParameter1 parameter;

    private AnimatorStateInfo info;
    public HitStateNightmare1(FSM_Nightmare1 manager)
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
            manager.TransitionState(nightmareStateType1.Death);
        }
        else
        {
            if ( parameter.health <= parameter.dangerHealth)//注意此处要用else否则 transitionState会被覆盖
            {
                if (info.normalizedTime >= .95f)
                {
                    manager.TransitionState(nightmareStateType1.Vanish);
                }
            }
            else
            {
                if (info.normalizedTime >= .95f)
                {
                    manager.TransitionState(nightmareStateType1.Patrol);
                }
            }
        }

        
    }

        

    public void OnExit()
    {
        parameter.getHit = false;
    }
}

public class DeathStateNightmare1 : IState
{
    //private FSM_Nightmare1 manager;
    private nightmareParameter1 parameter;

    public DeathStateNightmare1(FSM_Nightmare1 manager)
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