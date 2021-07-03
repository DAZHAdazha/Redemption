using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float moveSpeed;
    public float duckSpeed;
    public float jumpForce;
    public LayerMask[] layers;
    public int lightDamage = 1;
    public int heavyDamage = 2;
    public Transform worldBoundaryLeft,worldBoundaryRight;
    public float health = 3;
    public float mana = 3;
    public GameObject healthSystem;
    public bool isStop = false;
    public float restoreTime;
    public bool isMovingPlatform;
    public float hitboxTime = 1.2f;
    public int numBlinks = 3;
    public float blinkTime = 0.1f;
    public bool isDefense = false;
    public Vector3 check;
    public float critical;

    private float timer;
    private bool isAttack;
    private string attackType;
    private int comboStep;
    new private Rigidbody2D rigidbody;
    private Animator animator;
    private bool isGround;
    private bool isDuck;
    private bool ableToDuck = true;
    public float slowAirSpeed;
    private bool bonusTime = false;
    private bool isBonus = false;
    private int attackReinfore = 1;
    private bool bonusActive = true;
    private bool isHurt = false;
    private bool defenseTime = false;
    private ScreenFlash screenFlash;
    private bool isOneWayPlatform; 
    private PlayerInputActions controls;
    private Vector2 move;
    private PolygonCollider2D polygonCollider2D;
    private Renderer myRender;

    private void Awake() {

        //支持手柄
        controls = new PlayerInputActions();

        controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += ctx => move = Vector2.zero;
    }

    void OnEnable(){
        controls.GamePlay.Enable();
    }

    void OnDisable(){
        controls.GamePlay.Disable();
    }
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthSystem.GetComponent<HealthSystem>().setHealth(health);
        healthSystem.GetComponent<HealthSystem>().setMana(mana);
        healthSystem.GetComponent<HealthSystem>().UpdateGraphics();
        screenFlash = GetComponent<ScreenFlash>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        myRender = GetComponent<Renderer>();
    }

    void Update()
    {
        updateFunction();
    }

    private void updateFunction(){
        //layer中oneWayPlatform的index为1
        isOneWayPlatform = Physics2D.OverlapCircle(transform.position + new Vector3(check.x, check.y, 0), check.z, layers[1]);
        if(isOneWayPlatform){
            isGround = true;
        }else{
            //layer中Ground的index为0
            isGround = Physics2D.OverlapCircle(transform.position + new Vector3(check.x, check.y, 0), check.z, layers[0]);
        }

        if(isGround && !ableToDuck && !isAttack){
            ableToDuck = true;
        }
        animator.SetFloat("Horizontal", rigidbody.velocity.x);
        animator.SetFloat("Vertical", rigidbody.velocity.y);
        animator.SetBool("isGround", isGround);
        if(!isStop){
            Move();
            Attack();
        }
    }

    void Move()
    {
        if(controls.GamePlay.Duck.triggered){
            if (!isDuck && isGround && ableToDuck)
            {
                if(isHurt && defenseTime){
                    isDefense = true;
                    defenseTime = false;
                    transform.Find("defense").gameObject.SetActive(true);
                    transform.Find("defense").GetComponent<Animator>().Play("Hit");
                    isHurt = false;
                    animator.SetBool("Hurt",false);
                    rigidbody.constraints = RigidbodyConstraints2D.None;
                }
                if(bonusActive && bonusTime && !isBonus){
                    isBonus = true;
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
            defenseTime = false;
        }

        if (!isAttack){
            if(!isDuck && !isHurt){
                rigidbody.velocity = new Vector2(move.x * moveSpeed,  rigidbody.velocity.y);
            }
        }
        else
        {
            if (attackType == "Light"){
                if(gameObject.layer == LayerMask.NameToLayer("Player")){
                    rigidbody.velocity = new Vector2(transform.localScale.x * lightSpeed, rigidbody.velocity.y * slowAirSpeed);
                }else{
                    //在DropFromOneWay layer时攻击velocity不会慢下落
                    rigidbody.velocity = new Vector2(transform.localScale.x * lightSpeed, rigidbody.velocity.y);
                }
            }
            else if (attackType == "Heavy"){
                if(gameObject.layer == LayerMask.NameToLayer("Player")){
                    rigidbody.velocity = new Vector2(transform.localScale.x * heavySpeed, rigidbody.velocity.y * slowAirSpeed);
                }else{
                    //在DropFromOneWay layer时攻击velocity不会慢下落
                    rigidbody.velocity = new Vector2(transform.localScale.x * heavySpeed, rigidbody.velocity.y);
                }
            }
        }

        if (!oneWayPlatformDrop() && controls.GamePlay.Jump.triggered && isGround && !isDuck)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }

        if (rigidbody.velocity.x < -0.1f && !isHurt)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (rigidbody.velocity.x > 0.1f && !isHurt)
            transform.localScale = new Vector3(1, 1, 1);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x,worldBoundaryLeft.position.x,worldBoundaryRight.position.x),transform.position.y);
    }

    void Attack()
    {
        if (controls.GamePlay.LightAttack.triggered && !isAttack && !isDuck)
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
        if (controls.GamePlay.HeavyAttack.triggered && !isAttack && !isDuck)
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
        if (other.CompareTag("Enemy"))
        {
            int damage = 0;
            bool isCritical = false;
            if(attackReinfore>1){
            isCritical = true;
        } else{
            //判断暴击率
                float r =Random.Range(0f,1f);
                if(r<=critical){
                    attackReinfore = 2;
                    isCritical = true;
                }
        }
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
            
            attackReinfore = 1;

            if (transform.localScale.x > 0)
                other.GetComponent<Enemy>().GetHit(Vector2.right,damage,isCritical);
            else if (transform.localScale.x < 0)
                other.GetComponent<Enemy>().GetHit(Vector2.left,damage,isCritical);
        }
        if(other.CompareTag("EnemyAttack")){
            if(!isDefense){
                getHit();
            }
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
        isHurt = true;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetBool("Hurt",true);
    }

    public void resolveHurt(){
        isHurt = false;
        animator.SetBool("Hurt",false);
        rigidbody.constraints = RigidbodyConstraints2D.None;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        if(!animator.GetBool("isGround")){
            transform.localPosition = new Vector2(transform.localPosition.x,transform.localPosition.y + 0.001f);
        }
        
    }

    public void startDefenseTime(){
        defenseTime = true;
    }

    public void closeDefenseTime(){
        defenseTime = false;
    }

    public void unableDefense(){
        isDefense = false;
    }

    private void restart(){
        //重新激活当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void startover(){
        Invoke("restart",.2f);
    }

    public void callStartOver(){
        startover();
    }

    public void getDamage(float damage = 1f){
        //这里扣血！！！ 注意这里在player 的Hurt 动画时间 要大于 敌人防反帧的时间长度
        screenFlash.FlashScreen();
        healthSystem.GetComponent<HealthSystem>().TakeDamage(damage);
        polygonCollider2D.enabled = false;
        StartCoroutine("showPlayerHitbox");
    }

    IEnumerator showPlayerHitbox(){
        yield return new WaitForSeconds(hitboxTime);
        polygonCollider2D.enabled = true;
    }

    private bool oneWayPlatformDrop(){
        float moveY = move.y;
        if(isOneWayPlatform && moveY < -0.1f && controls.GamePlay.Jump.triggered){
            gameObject.layer = LayerMask.NameToLayer("DropFromOneWay");
            Invoke("restorePlayer",restoreTime);
            return true;
        }
        return false;
    }

    private void restorePlayer(){
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void blinkPlayer(){
        StartCoroutine(blink(numBlinks,blinkTime));
    }

    IEnumerator blink(int numBlinks, float seconds){
        for(int i=0;i<numBlinks * 2;i++){
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }

}