using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public float waitTime;
    public Transform[] targetPos;


    private float waitTimeCurrent;
    private Transform playerDefTransform;
    private int posIndex;

    // Start is called before the first frame update
    void Start()
    {
        posIndex = 1;
        //将左右两个点的子类分离
        transform.DetachChildren();
        waitTimeCurrent = waitTime;
        playerDefTransform = GameObject.Find("Player").transform.parent;
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
        if(other.transform.tag=="Player"){
            other.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.transform.tag=="Player"){
            other.gameObject.transform.parent = playerDefTransform;
        }
    }
}
