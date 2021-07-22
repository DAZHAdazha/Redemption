using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlatform : MonoBehaviour
{
    public float regenerateTime;

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


    void ableCollider()
    {
        boxCollider2D.enabled = true;
    }

    void setRegenerate()
    {
        anim.SetBool("Regenerate", false);
    }


    void destroyPlatform(){
        gameObject.SetActive(false);
        Invoke("regneratePlatform", regenerateTime);
    }

    private void regneratePlatform()
    {
        gameObject.SetActive(true);
        anim.SetBool("Regenerate", true);
    }


    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D" &&  other.transform.GetComponent<PlayerController>().check.y + other.transform.localPosition.y >= transform.position.y){
            
            anim.SetTrigger("Collapse");
        }
    }
}
