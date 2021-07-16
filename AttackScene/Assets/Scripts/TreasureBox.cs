using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameObject coin;//掉落物品
    public float delayTime = 0.5f;//物品掉落延迟时间
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
            anim.SetTrigger("Opening");
            isOpen = true;
            Invoke("getCoin",delayTime);
        }
    }

    void getCoin(){
        Instantiate(coin,transform.position,Quaternion.identity);
    }
    
}
