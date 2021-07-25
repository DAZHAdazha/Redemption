using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("解锁能力")]
    public bool attackLock = false;
    public bool duckLock = false;
    public bool shadowLock = false;
    public bool defenseLock = false;
    public bool bonusLock = false;
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

    [Header("属性")]
    public float health = 3;
    public float mana = 3;

    public float interval = 2f;
    public float moveSpeed;
    public float duckSpeed;
    public float jumpForce;
    public LayerMask[] layers;
    public int lightDamage = 1;
    public int heavyDamage = 2;
    public Transform worldBoundaryLeft,worldBoundaryRight;
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
    public GameObject shadow;
    public bool isPuzzled;
    public Shadow myShadow;
    public GameObject status;
    public Vector2 move;


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
    //private ScreenFlash screenFlash;
    private bool isOneWayPlatform; 
    private PlayerInputActions controls;
    private PolygonCollider2D polygonCollider2D;
    private Renderer myRender;
    private HealthSystem myHealSystem;
    private GameObject skills;


    private void Awake() {
        Time.timeScale = 1;
        //加载存档，需要在awake中
        health = GameSaver.healthMax;
        mana = GameSaver.manaMax;
        if (GameSaver.smallHealth)
        {
            health += 5;
        }
        if (GameSaver.bigHealth)
        {
            health += 10;
        }

        if (GameSaver.smallMana)
        {
            mana += 5;
        }
        if (GameSaver.bigMana)
        {
            mana += 10;
        }

        
        attackLock = GameSaver.attackLock;
        duckLock = GameSaver.duckLock;
        shadowLock = GameSaver.shadowLock;
        defenseLock = GameSaver.defenseLock;
        bonusLock = GameSaver.bonusLock;
        CoinUI.coinNum = GameSaver.coinNum;


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
        myHealSystem = healthSystem.GetComponent<HealthSystem>();
        myHealSystem.setHealth(health);
        myHealSystem.setMana(mana);
        myHealSystem.UpdateGraphics();
        //screenFlash = GetComponent<ScreenFlash>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        myRender = GetComponent<Renderer>();
        myShadow = shadow.GetComponent<Shadow>();
        Shadow.isExist = false;
        skills = GameObject.Find("Skills");
        for (int i = 0; i < GameSaver.unLockLevel; i++)
        {
            skills.transform.GetChild(i).gameObject.SetActive(true);
        }
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
        if( shadowLock && controls.GamePlay.Shadow.triggered && !myShadow.getExist() && myHealSystem.manaPoint>=1f && !isMovingPlatform){
            myHealSystem.UseMana(1f);//每次消耗一点魔法值
            GameObject thisShadow = Instantiate(shadow,new Vector2(transform.position.x,transform.position.y-0.4f) ,Quaternion.identity);
            thisShadow.GetComponent<Shadow>().setMove(move,transform.localScale.x);
        }

        if(duckLock && !myShadow.getExist() && controls.GamePlay.Duck.triggered ){
            if (!isDuck && isGround && ableToDuck)
            {
                if(defenseLock && isHurt && defenseTime){
                    isDefense = true;
                    defenseTime = false;
                    transform.Find("defense").gameObject.SetActive(true);
                    transform.Find("defense").GetComponent<Animator>().Play("Hit");
                    isHurt = false;
                    animator.SetBool("Hurt", false);
                    rigidbody.constraints = RigidbodyConstraints2D.None;
                }
                if(bonusLock && bonusActive && bonusTime && !isBonus){
                    isBonus = true;
                    transform.GetComponent<SpriteRenderer>().color = new Color32(255,81,81,255);
                    transform.Find("Bonus").gameObject.SetActive(false);
                    transform.Find("BonusDuck").gameObject.SetActive(true);
                }
                isAttack = false;
                isDuck = true;
                animator.SetBool("Duck",true);

                Invoke("ResolveDuck", 0.35f);//duck动画为0.25

                rigidbody.velocity = new Vector2((transform.localScale.x>0?1:-1) * duckSpeed, 0);
                //rigidbody.isKinematic = true;
                if(!isGround){
                    ableToDuck = false;
                }
            }
            bonusActive = false;
            defenseTime = false;
        }

        if (!isAttack){
            if(!isDuck && !isHurt){
                if (isPuzzled){
                    rigidbody.velocity = new Vector2(-move.x * moveSpeed, rigidbody.velocity.y);
                }
                else
                {
                    rigidbody.velocity = new Vector2(move.x * moveSpeed, rigidbody.velocity.y);
                }
                
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
        if (attackLock && controls.GamePlay.LightAttack.triggered && !isAttack && !isDuck)
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
        if (attackLock && controls.GamePlay.HeavyAttack.triggered && !isAttack && !isDuck)
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
        if (other.CompareTag("Skeleton") || other.CompareTag("PuzzleRobot") || other.CompareTag("Bat") || other.CompareTag("firewarm")
            || other.CompareTag("Nightmare1") || other.CompareTag("Nightmare2") || other.CompareTag("SmallStone")
            || other.CompareTag("MiddleStone") || other.CompareTag("BigStone"))
        {
            int type = 0;
            if (other.CompareTag("Skeleton"))
            {
                type = 0;
            }
            else if (other.CompareTag("PuzzleRobot"))
            {
                type = 1;
            }else if(other.CompareTag("Bat"))
            {
                type = 2;
            }
            else if (other.CompareTag("firewarm"))
            {
                type = 3;
            }
            else if (other.CompareTag("Nightmare1"))
            {
                type = 4;
            }
            else if (other.CompareTag("Nightmare2"))
            {
                type = 5;
            }
            else if (other.CompareTag("SmallStone"))
            {
                type = 6;
            }
            else if (other.CompareTag("MiddleStone"))
            {
                type = 7;
            }
            else if (other.CompareTag("BigStone"))
            {
                type = 8;
            }
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

            if (type == 0)//骷髅怪
            {
                HitSound.soundManagerInstance.skeletontAudio();
                if (transform.localScale.x > 0)
                    other.GetComponent<Enemy>().GetHit(Vector2.right, damage, isCritical);
                else 
                    other.GetComponent<Enemy>().GetHit(Vector2.left, damage, isCritical);
            }
            else if(type == 1)//puzzle robot
            {
                HitSound.soundManagerInstance.puzzlebotAudio();
                if (transform.localScale.x > 0)
                    other.GetComponent<PuzzleRobot>().GetHit(Vector2.right, damage, isCritical);
                else
                    other.GetComponent<PuzzleRobot>().GetHit(Vector2.left, damage, isCritical);
            }
            else if (type == 2)
            {
                HitSound.soundManagerInstance.batHitAudio();
                if (transform.localScale.x > 0)
                    other.GetComponent<Bat>().GetHit(Vector2.right, damage, isCritical);
                else
                    other.GetComponent<Bat>().GetHit(Vector2.left, damage, isCritical);
            }
            else if (type == 3)
            {
                HitSound.soundManagerInstance.firewarmAudio();
                if (transform.localScale.x > 0)
                    other.GetComponent<fireWormAction>().gotHit(Vector2.right, damage, isCritical);
                else
                    other.GetComponent<fireWormAction>().gotHit(Vector2.left, damage, isCritical);
            }
            else if (type == 4)
            {
                HitSound.soundManagerInstance.nightmare1HitAudio();
                if (transform.localScale.x > 0)
                    other.GetComponent<NightMare1>().GetHit(Vector2.right, damage, isCritical);
                else
                    other.GetComponent<NightMare1>().GetHit(Vector2.left, damage, isCritical);
            }
            else if (type == 5)
            {
                HitSound.soundManagerInstance.nightmare2HitAudio();
                if (transform.localScale.x > 0)
                    other.GetComponent<NightMare2>().GetHit(Vector2.right, damage, isCritical);
                else
                    other.GetComponent<NightMare2>().GetHit(Vector2.left, damage, isCritical);
            }
            else if (type == 6)
            {
                HitSound.soundManagerInstance.smallStoneMonsterAudio();
                if (transform.localScale.x > 0)
                    other.GetComponent<SmallStoneMonster>().GetHit(Vector2.right, damage, isCritical);
                else
                    other.GetComponent<SmallStoneMonster>().GetHit(Vector2.left, damage, isCritical);
            }
            else if (type == 7)
            {
                HitSound.soundManagerInstance.middleStoneMonsterAudio();
                if (transform.localScale.x > 0)
                    other.GetComponent<MiddleStoneMonster>().GetHit(Vector2.right, damage, isCritical);
                else
                    other.GetComponent<MiddleStoneMonster>().GetHit(Vector2.left, damage, isCritical);
            }
            else if (type == 8)
            {
                HitSound.soundManagerInstance.bigStoneMonsterHitAudio();
                if (transform.localScale.x > 0)
                    other.GetComponent<BigStoneMonster>().GetHit(Vector2.right, damage, isCritical);
                else
                    other.GetComponent<BigStoneMonster>().GetHit(Vector2.left, damage, isCritical);
            }

        }
        //TODO
        if(other.CompareTag("SkeletonAttack")){//fireball的flag也调成这个
            if(!isDefense){
                getHit();
            }
            return;
        }
        if (other.CompareTag("Nightmare1Attack"))
        {
            if (!isDefense)
            {
                getHit();
                getDamage(other.gameObject.GetComponentInParent<FSM_Nightmare1>().parameter.attackPoint-1);
            }
            return;
        }
        if (other.CompareTag("FireballAttack"))
        {
            if (!isDefense)
            {
                getHit();
                getDamage(other.gameObject.GetComponentInParent<fireBall>().attackPoint - 1);
            }
            return;
        }
        if (other.CompareTag("Nightmare2Attack"))
        {
            if (!isDefense)
            {
                getHit();
                getDamage(other.gameObject.GetComponentInParent<FSM_Nightmare2>().parameter.attackPoint - 1);
            }
            return;
        }
        if (other.CompareTag("Nightmare2Sweep"))
        {
            if (!isDefense)
            {
                getHit();
                getDamage(other.gameObject.GetComponentInParent<FSM_Nightmare2>().parameter.sweepPoint - 1);
            }
            return;
        }
        if (other.CompareTag("DamageArea"))
        {
            if (!isDefense)
            {
                getHit();
                getDamage(other.gameObject.GetComponentInParent<smallStoneMonsterFSM>().p.attackValue - 1);
            }
            return;
        }
        if (other.CompareTag("BigStoneAttack"))
        {
            if (!isDefense)
            {
                getHit();
                getDamage(other.gameObject.GetComponentInParent<bigStoneMonsterFSM>().p.attackValue - 1);
            }
            return;
        }

        if (other.CompareTag("iceFall"))
        {
            if (!isDefense)
            {
                getHit();//iceFall伤害为1
            }
            return;
        }

        if (other.CompareTag("PuzzleRobotAttack"))
        {
            if (!isDefense)
            {
                getHit();
                getDamage(other.gameObject.GetComponent<PuzzleAttack>().attackPoint - 1);
                other.GetComponent<PuzzleAttack>().anim.Play("Hit");
            }
            return;
        }
        if (other.CompareTag("MiddleStoneAttack"))
        {
            if (!isDefense)
            {
                getHit();
                getDamage(other.gameObject.GetComponentInParent<middleStoneMonsterFSM>().p.attackValue-1);
            }
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(check.x, check.y, 0), check.z);
    }

    public void ResolveDuck(){
        isDuck = false;
        animator.SetBool("Duck",false);
        //rigidbody.isKinematic = false;
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

    public void getHit() {
        //PlayerSound.soundManagerInstance.hurtAudioPlay();
        isHurt = true;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetBool("Hurt", true);

        ///!!!!修改僵直时长
        Invoke("resolveHurt",0.3f);

    }

    public void resolveHurt(){
        rigidbody.constraints = RigidbodyConstraints2D.None;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        isHurt = false;
        animator.SetBool("Hurt", false);
        if (!animator.GetBool("isGround")){
            //transform.localPosition = new Vector2(transform.localPosition.x,transform.localPosition.y + 0.001f);
            transform.localPosition = new Vector2(transform.localPosition.x,transform.localPosition.y);
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
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
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
        PlayerSound.soundManagerInstance.hurtAudioPlay();
        //这里扣血！！！ 注意这里在player 的Hurt 动画时间 要大于 敌人防反帧的时间长度
        if (!myShadow.getExist())
        {
            //screenFlash.FlashScreen();
        }

        StartCoroutine("showPlayerHitbox");
        myHealSystem.TakeDamage(damage);
        polygonCollider2D.enabled = false;
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
        if (!myShadow.getExist())
        {
            StartCoroutine(blink(numBlinks, blinkTime));
        }
    }

    IEnumerator blink(int numBlinks, float seconds){
        for(int i=0;i<numBlinks * 2;i++){
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }

    public void playerShowUp(Vector3 target) {
        AttackOver();
        transform.position = target;
        resolveHurt();
        polygonCollider2D.enabled = true;
    }

}