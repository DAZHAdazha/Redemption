﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private bool isHit;
    private AnimatorStateInfo info;

    private Animator animator;
    private Animator hitAnimator;
    new private Rigidbody2D rigidbody;
    public GameObject health;
    public GameObject floatPoint;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
        hitAnimator = transform.GetChild(0).GetComponent<Animator>();
        rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        info = animator.GetCurrentAnimatorStateInfo(0);
        if (isHit)
        {
            rigidbody.velocity = direction * speed;
            if (info.normalizedTime >= .6f)
                isHit = false;
        }
    }

    public void GetHit(Vector2 direction, int damage, bool isCritical)
    {
        transform.localScale = new Vector3(-direction.x, 1, 1);
        isHit = true;

        GameObject gb = Instantiate(floatPoint,new Vector2(transform.position.x-0.5f,transform.position.y + 1f),Quaternion.identity);
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        if(isCritical){
            gb.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(255,0,0,255);
        }
        
        //！！！可优化 把敌人的参数位置移动一下
        gameObject.GetComponent<FSM>().parameter.getHit = true;
        gameObject.GetComponent<FSM>().parameter.health -= damage;
        if(gameObject.GetComponent<FSM>().parameter.health<0){
            gameObject.GetComponent<FSM>().parameter.health = 0;
        }

        health.GetComponent<health>().callUpdateHealth();

        this.direction = direction;
        animator.SetTrigger("Hit");
        hitAnimator.SetTrigger("Hit");
    }

    public void destory(){
        Destroy(health.transform.parent.gameObject);
        Destroy(gameObject);
    }

    public void callStartDefenseTime(){
        GameObject.Find("Player").GetComponent<PlayerController>().startDefenseTime();
    }

    public void callCloseDefenseTime(){
        GameObject.Find("Player").GetComponent<PlayerController>().closeDefenseTime();
    }
    
}