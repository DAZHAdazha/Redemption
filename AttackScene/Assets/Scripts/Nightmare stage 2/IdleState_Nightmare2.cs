using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleStateNightmare2 : IState
{
    private FSM_Nightmare2 manager;
    private nightmareParameter2 parameter;
    private float distance;
    private AnimatorStateInfo info;
    private bool flag = false;//代表Appear动画是否播放完

    //private float timer;
    public IdleStateNightmare2(FSM_Nightmare2 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Appear");
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (!flag && info.normalizedTime >= .95f)
        {
            flag = true;
            parameter.animator.Play("Idle");
        }

        distance = Vector2.Distance(manager.transform.position, parameter.target.position);
        if (distance < parameter.awakeArea)
        {
            manager.TransitionState(nightmareStateType2.Patrol);
        }

    }

    public void OnExit()
    {
        flag = false;
    }
}

public class PatrolStateNightmare2 : IState
{
    private FSM_Nightmare2 manager;
    private nightmareParameter2 parameter;

    private float timer;
    private float distance;
    private float defenseCoolDown = 100f;


    public PatrolStateNightmare2(FSM_Nightmare2 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Move");
    }

    public void OnUpdate()
    {
        //受伤
        if (parameter.getHit)
        {
            manager.TransitionState(nightmareStateType2.Hit);
        }

        //限制位置
        manager.transform.position = new Vector2(Mathf.Clamp(manager.transform.position.x, parameter.worldBoundaryLeft.position.x, parameter.worldBoundaryRight.position.x), manager.transform.position.y);
        timer += Time.deltaTime;
        defenseCoolDown += Time.deltaTime;
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


        //if (timer > parameter.patrolTime)
        //{
        //    manager.TransitionState(nightmareStateType2.Vanish);
        //}

        if (parameter.health<=parameter.dangerHealth && defenseCoolDown >= parameter.defenseCoolDownTime)
        {
            manager.TransitionState(nightmareStateType2.Defense);
            defenseCoolDown = 0;
            return;
        }

        if (distance <= parameter.sweepArea)
        {
            manager.TransitionState(nightmareStateType2.Sweep);
        }else if(distance> parameter.sweepArea && distance <= parameter.attackArea)
        {
            manager.TransitionState(nightmareStateType2.Attack);
        }else if(distance > parameter.attackArea && timer > parameter.patrolTime)
        {
            manager.TransitionState(nightmareStateType2.Vanish);
        }

    }

    public void OnExit()
    {
        timer = 0;
    }
}

public class VanishStateNightmare2 : IState
{
    private FSM_Nightmare2 manager;
    private nightmareParameter2 parameter;
    private AnimatorStateInfo info;

    private float timer;

    public VanishStateNightmare2(FSM_Nightmare2 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Vanish");
        manager.gameObject.tag = "Untagged";
        parameter.healthCanvas.SetActive(false);
        //无法收到伤害 改变layer
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= .95f && timer > parameter.vanishTime)
        {
            manager.TransitionState(nightmareStateType2.Appear);
        }
     

    }

    public void OnExit()
    {
        timer = 0;
    }
}


public class AppearStateNightmare2 : IState
{
    private FSM_Nightmare2 manager;
    private nightmareParameter2 parameter;

    private AnimatorStateInfo info;
    //private float timer;
    public AppearStateNightmare2(FSM_Nightmare2 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Appear");
        manager.transform.position = parameter.target.position;//瞬移
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= .95f)
        {
            manager.TransitionState(nightmareStateType2.Sweep);
        }
    }

    public void OnExit()
    {
        //无敌结束 将Layer改回来
        manager.gameObject.tag = "Nightmare2";
        parameter.healthCanvas.SetActive(true);
    }

}


public class AttackStateNightmare2 : IState
{
    private FSM_Nightmare2 manager;
    private nightmareParameter2 parameter;
    private float timer;
    private AnimatorStateInfo info;
    public AttackStateNightmare2(FSM_Nightmare2 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Attack");
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;
        if (parameter.getHit)
        {
            manager.TransitionState(nightmareStateType2.Hit);
        }
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
       
        if (info.normalizedTime >= .95f && timer > parameter.attackCoolDown)
        {
            manager.TransitionState(nightmareStateType2.Patrol);
        }
    }

    public void OnExit()
    {
        
        timer = 0;
    }
}

public class HitStateNightmare2 : IState
{
    private FSM_Nightmare2 manager;
    private nightmareParameter2 parameter;

    private AnimatorStateInfo info;
    public HitStateNightmare2(FSM_Nightmare2 manager)
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
            manager.TransitionState(nightmareStateType2.Death);
        }
        else
        {
            if ( parameter.health <= parameter.dangerHealth)//注意此处要用else否则 transitionState会被覆盖
            {
                if (info.normalizedTime >= .95f)
                {
                    manager.TransitionState(nightmareStateType2.Sweep);
                }
            }
            else
            {
                if (info.normalizedTime >= .95f)
                {
                    manager.TransitionState(nightmareStateType2.Patrol);
                }
            }
        }

        
    }

        

    public void OnExit()
    {
        parameter.getHit = false;
    }
}

public class DeathStateNightmare2 : IState
{
    //private FSM_Nightmare2 manager;
    private nightmareParameter2 parameter;

    public DeathStateNightmare2(FSM_Nightmare2 manager)
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

public class SweepStateNightmare2 : IState
{
    private FSM_Nightmare2 manager;
    private nightmareParameter2 parameter;
    private float timer;
    private AnimatorStateInfo info;

    public SweepStateNightmare2(FSM_Nightmare2 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Sweep");
    }

    public void OnUpdate()
    {
        if (parameter.getHit)
        {
            manager.TransitionState(nightmareStateType2.Hit);
        }
        timer += Time.deltaTime;
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= .95f && timer > parameter.sweepCoolDown)
        {
            manager.TransitionState(nightmareStateType2.Patrol);
        }
    }

    public void OnExit()
    {
        timer = 0;
    }
}

public class DefenseStateNightmare2 : IState
{
    private FSM_Nightmare2 manager;
    private nightmareParameter2 parameter;
    private AnimatorStateInfo info;

    public DefenseStateNightmare2(FSM_Nightmare2 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Defense");
        parameter.isDefense = true;
        if (manager.transform.localScale.x > 0)
            manager.gameObject.GetComponent<NightMare2>().GetHit(Vector2.right, parameter.healPoint, false);
        else
            manager.gameObject.GetComponent<NightMare2>().GetHit(Vector2.left, parameter.healPoint, false);
    }

    public void OnUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= .95f)
        {
            manager.TransitionState(nightmareStateType2.Patrol);
        }
    }

    public void OnExit()
    {
        parameter.isDefense = false;
    }
}