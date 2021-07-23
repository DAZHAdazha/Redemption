using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameObject coin;//掉落物品
    public float delayTime = 0.5f;//物品掉落延迟时间
    public int coinMax = 3;
    private bool ableToOpen;
    private bool isOpen;
    private Animator anim;
    private PlayerInputActions controls;

    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = false;
        ableToOpen = false;
    }

    private void Awake() {

        //支持手柄
        controls = new PlayerInputActions();
        controls.GamePlay.EnterDoor.started += ctx => openBox();
    }

    void OnEnable(){
        controls.GamePlay.Enable();
    }

    void OnDisable(){
        controls.GamePlay.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {
            ableToOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {
            ableToOpen = false;
        }
    }

    void openBox(){
        if(ableToOpen && !isOpen){
            SoundManager.soundManagerInstance.openBoxAudio();
            anim.SetTrigger("Opening");
            isOpen = true;
            Invoke("getCoin",delayTime);
        }
    }

    void getCoin(){
        int coinNum = Random.Range(0, coinMax + 1);
        for(int i = 0; i < coinNum; i++)
        {
            Instantiate(coin, new Vector2(transform.position.x + i-coinNum/2, transform.position.y) , Quaternion.identity);
        }
        
    }
    
}
