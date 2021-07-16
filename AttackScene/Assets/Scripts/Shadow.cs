using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private Transform playerTransform;
    public static bool isExist = false;
    public float speed;
    private Vector3 target;
    private Vector2 move;
    private float face;
    public GameObject effect;
    
    private void setTarget(Vector2 direction){
        transform.localScale = new Vector3(face,transform.localScale.y,transform.localScale.z);
        if(face>0){
                        //注意-0.4f与playercontroller里面实例化部分耦合！
                target = new Vector3(playerTransform.position.x+5f,playerTransform.position.y-0.4f,transform.position.z);
            }else{
                target = new Vector3(playerTransform.position.x-5f,playerTransform.position.y-0.4f,transform.position.z);
            }

        if(direction.y>0){
            var rotationVector = transform.rotation.eulerAngles;
            if(direction.x==0){
                //旋转90度
                rotationVector.z = 90;

                target = new Vector3(playerTransform.position.x,target.y,target.z);
            }else{
                if(face>0){
                    rotationVector.z = 45;
                }else{
                    rotationVector.z = -225;
                }
                
            }
            
            transform.rotation = Quaternion.Euler(rotationVector);
            transform.localScale = new Vector3(1f,transform.localScale.y,transform.localScale.z);
            target = new Vector3(target.x,target.y + 3f,target.z);
        }else if(direction.y<0){
            var rotationVector = transform.rotation.eulerAngles;
            if(direction.x==0){
                //旋转90度
                rotationVector.z = -90;
                
                target = new Vector3(playerTransform.position.x,target.y,target.z);
            }else{
                if(face>0){
                    rotationVector.z = -45;
                }else{
                    rotationVector.z = 225;
                }
            }
            transform.rotation = Quaternion.Euler(rotationVector);
            transform.localScale = new Vector3(1f,transform.localScale.y,transform.localScale.z);
            target = new Vector3(target.x,target.y - 3f,target.z);
        }
    }

    public bool getExist(){
        return isExist;
    }


    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        isExist = true;
        setTarget(move);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target,speed*Time.deltaTime);
        if(Vector3.Distance(transform.position,target)<0.1f){
            Destroy(gameObject);
            isExist = false;
            Instantiate(effect, transform.position, Quaternion.identity);
            playerTransform.GetComponent<PlayerController>().playerShowUp(new Vector2(transform.position.x, transform.position.y + 0.4f));

        }
    }

    public void setMove(Vector2 myMove,float myFace){
        move = myMove;
        face = myFace;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer==LayerMask.NameToLayer("Ground") || other.gameObject.layer==LayerMask.NameToLayer("OneWayPlatform")
        || other.gameObject.layer==LayerMask.NameToLayer("WorldBoundary")){
            Destroy(gameObject);
            isExist = false;

            Instantiate(effect, transform.position, Quaternion.identity);
            playerTransform.GetComponent<PlayerController>().playerShowUp(new Vector2(transform.position.x, transform.position.y + 0.4f));
        }
    }


}
