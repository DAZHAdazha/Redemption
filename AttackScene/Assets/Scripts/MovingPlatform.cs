using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public float waitTime;
    public Transform[] targetPos;


    private float waitTimeCurrent;
    private Transform currentDefTransform;
    private int posIndex;
    private PlayerController playerController;

    void Start()
    {
        posIndex = 1;
        //将左右两个点的子类分离
        transform.DetachChildren();
        waitTimeCurrent = waitTime;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos[posIndex].position,speed*Time.deltaTime);
        if(Vector2.Distance(transform.position,targetPos[posIndex].position) < 0.1f ){
            if(waitTimeCurrent < 0.0f){
                if(posIndex == 1){
                    posIndex = 0;
                }else{
                    posIndex = 1;
                }
                waitTimeCurrent = waitTime;
            } else{
                waitTimeCurrent -= Time.deltaTime;
            }
        }

        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        currentDefTransform = other.transform.parent;
        if(other.transform.tag =="Player"){
            playerController.isMovingPlatform = true;
            if(!playerController.myShadow.getExist())
                other.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.transform.tag=="Player"){
            playerController.isMovingPlatform = false;
            if(!playerController.myShadow.getExist())
                other.gameObject.transform.parent = currentDefTransform;

        }
    }



}
