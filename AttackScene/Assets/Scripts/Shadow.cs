using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private Transform playerTransform;
    public static bool isExit = false;
    public float speed;
    private Vector3 target;
    private Vector2 move;
    private float face;
    private float lowY;
    // Start is called before the first frame update
    
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

    public bool getExit(){
        return isExit;
    }


    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        isExit = true;
        setTarget(move);
        lowY = GameObject.Find("LowestPoint").transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target,speed*Time.deltaTime);
        if(Vector3.Distance(transform.position,target)<0.1f){
            Destroy(gameObject);
            isExit = false;
            if(transform.position.y + 0.4<lowY){
                //lowestPoint应当放到player头顶贴图位置（或者按W看位置）
                playerTransform.GetComponent<PlayerController>().playerShowUp(new Vector2(transform.position.x, lowY));
                return;
            }
            //此处0.4f耦合
            playerTransform.GetComponent<PlayerController>().playerShowUp(new Vector2(transform.position.x, transform.position.y + 0.4f));
        }
    }

    public void setMove(Vector2 myMove,float myFace){
        move = myMove;
        face = myFace;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer==LayerMask.NameToLayer("Ground") || other.gameObject.layer==LayerMask.NameToLayer("OneWayPlatform")){
            Destroy(gameObject);
            isExit = false;
            //此处0.4f耦合
            if(transform.position.y + 0.4<lowY){
                playerTransform.GetComponent<PlayerController>().playerShowUp(new Vector2(transform.position.x, lowY));
                return;
            }
            playerTransform.GetComponent<PlayerController>().playerShowUp(new Vector2(transform.position.x, transform.position.y + 0.4f));
        
        }
    }

}
