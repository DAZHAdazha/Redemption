﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("补偿速度")]
    public float lightSpeed;
    public float heavySpeed;
    [Header("打击感")]
    public float shakeTime;
    public int lightPause;
    public float lightStrength;
    public int heavyPause;
    public float heavyStrength;

    [Space]
    public float interval = 2f;
    private float timer;
    private bool isAttack;
    private string attackType;
    private int comboStep;

    public float moveSpeed;
    public float duckSpeed;
    public float jumpForce;
    new private Rigidbody2D rigidbody;
    private Animator animator;
    private float input;
    private bool isGround;
    [SerializeField] private LayerMask layer;

    [SerializeField] private Vector3 check;

    public int lightDamage = 1;
    public int heavyDamage = 2;

    public Transform worldBoundaryLeft,worldBoundaryRight;

    private bool isDuck;

    private bool ableToDuck = true;
    public float slowAirSpeed;
    private bool bonusTime = false;
    private bool isBonus = false;
    private int attackReinfore = 1;
    private bool bonusActive = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        isGround = Physics2D.OverlapCircle(transform.position + new Vector3(check.x, check.y, 0), check.z, layer);
        if(isGround && !ableToDuck && !isAttack){
            ableToDuck = true;
        }
        animator.SetFloat("Horizontal", rigidbody.velocity.x);
        animator.SetFloat("Vertical", rigidbody.velocity.y);
        animator.SetBool("isGround", isGround);

        Move();
        Attack();
    }

    void Move()
    {
        if(Input.GetKeyDown("l")){
            if (!isDuck && ableToDuck)
            {
                if(bonusActive && bonusTime && !isBonus){
                    isBonus = true;
                }
                if(isBonus){
                    transform.GetComponent<SpriteRenderer>().color = new Color32(255,81,81,255);
                    transform.Find("Bonus").gameObject.SetActive(false);
                    transform.Find("BonusDuck").gameObject.SetActive(true);
                }
                isAttack = false;
                isDuck = true;
                animator.SetBool("Duck",true);
                rigidbody.velocity = new Vector2((transform.localScale.x>0?1:-1) * duckSpeed, 0);
                rigidbody.isKinematic = true;
                if(!isGround){
                    ableToDuck = false;
                }
            }
            bonusActive = false;
        }

        if (!isAttack){
            if(!isDuck){
                rigidbody.velocity = new Vector2(input * moveSpeed,  rigidbody.velocity.y);
            }
        }
        else
        {
            if (attackType == "Light")
                rigidbody.velocity = new Vector2(transform.localScale.x * lightSpeed, rigidbody.velocity.y * slowAirSpeed);
            else if (attackType == "Heavy")
                rigidbody.velocity = new Vector2(transform.localScale.x * heavySpeed, rigidbody.velocity.y * slowAirSpeed);
        }

        if (Input.GetButtonDown("Jump") && isGround && !isDuck)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }

        if (rigidbody.velocity.x < -0.1f)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (rigidbody.velocity.x > 0.1f)
            transform.localScale = new Vector3(1, 1, 1);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x,worldBoundaryLeft.position.x,worldBoundaryRight.position.x),transform.position.y);
    }

    void Attack()
    {
        if (Input.GetKeyDown("j") && !isAttack && !isDuck)
        {
            bonusActive = true;
            if(isBonus){
                attackReinfore = 2;
                isBonus = false;
                Invoke("setBonusVisible",.35f);
            } else{
                attackReinfore = 1;
            }
            ableToDuck = false;
            isAttack = true;
            attackType = "Light";
            comboStep++;
            if (comboStep > 3)
                comboStep = 1;
            timer = interval;
            animator.SetTrigger("LightAttack");
            animator.SetInteger("ComboStep", comboStep);
        }
        if (Input.GetKeyDown("k") && !isAttack && !isDuck)
        {
            bonusActive = true;
            if(isBonus){
                attackReinfore = 2;
                isBonus = false;
                Invoke("setBonusVisible",.7f);
            } else{
                attackReinfore = 1;
            }
            ableToDuck = false;
            isAttack = true;
            attackType = "Heavy";
            comboStep++;
            if (comboStep > 3)
                comboStep = 1;
            timer = interval;
            animator.SetTrigger("HeavyAttack");
            animator.SetInteger("ComboStep", comboStep);
        }


        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                comboStep = 0;
            }
        }
    }

    public void AttackOver()
    {
        isAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int damage = 0;
        if (other.CompareTag("Enemy"))
        {
            if (attackType == "Light")
            {
                AttackSense.Instance.HitPause(lightPause);
                AttackSense.Instance.CameraShake(shakeTime, lightStrength);
                damage = lightDamage * attackReinfore;
            }
            else if (attackType == "Heavy")
            {
                AttackSense.Instance.HitPause(heavyPause);
                AttackSense.Instance.CameraShake(shakeTime, heavyStrength);
                damage = heavyDamage * attackReinfore;
            }

            if (transform.localScale.x > 0)
                other.GetComponent<Enemy>().GetHit(Vector2.right,damage);
            else if (transform.localScale.x < 0)
                other.GetComponent<Enemy>().GetHit(Vector2.left,damage);
        }
        if(other.CompareTag("EnemyAttack")){
            getHit();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(check.x, check.y, 0), check.z);
    }

    public void ResolveDuck(){
        isDuck = false;
        animator.SetBool("Duck",false);
        rigidbody.isKinematic = false;
        transform.Find("BonusDuck").gameObject.SetActive(false);
        if(isBonus){
            transform.Find("Bonus").gameObject.SetActive(true);
        }
    }

    public void setAbleToDuck(){
        ableToDuck = true;
        bonusTime = true;
    }

    public void bonusTimeOver(){
        bonusTime = false;
        bonusActive = true;
    }

    private void setBonusVisible(){
        if(!isBonus){
            transform.Find("Bonus").gameObject.SetActive(false);
            transform.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
        }
    }

    public void getHit(){
        animator.SetBool("Hurt",true);
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void resolveHurt(){
        animator.SetBool("Hurt",false);
        rigidbody.constraints = RigidbodyConstraints2D.None;
    }

}