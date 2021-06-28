using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlatform : MonoBehaviour
{

    private BoxCollider2D boxCollider2D;
    private Animator anim;
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void disableCollider(){
        boxCollider2D.enabled = false;
    }

    void destroyPlatform(){
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {


        if(other.gameObject.CompareTag("Player") && other.transform.GetComponent<PlayerController>().check.y + other.transform.localPosition.y >= transform.position.y){
            anim.SetTrigger("Collapse");
        }
    }
}
