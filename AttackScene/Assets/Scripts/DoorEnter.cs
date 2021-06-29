using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnter : MonoBehaviour
{
    public Transform backDoor;

    private bool isDoor;
    private Transform playerTransform;
    private PlayerInputActions controls;
    private Animator anim;
    private Animator otherAnim;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
        otherAnim = backDoor.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            isDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            isDoor = false;
        }
    }

    private void Awake() {

        //支持手柄
        controls = new PlayerInputActions();

        controls.GamePlay.EnterDoor.started += ctx => enterDoor();
    }

    void OnEnable(){
        controls.GamePlay.Enable();
    }

    void OnDisable(){
        controls.GamePlay.Disable();
    }

    void enterDoor(){
        Debug.Log("here");
        if(isDoor){
            anim.SetTrigger("Suck");
            playerTransform.gameObject.SetActive(false);
        }
    }

    void popAnim(){
        otherAnim.SetTrigger("Pop");
    }
    void changePosition(){
        
        playerTransform.position = backDoor.position;
    }

    void resumePlayer(){
        playerTransform.gameObject.SetActive(true);
    }

}
