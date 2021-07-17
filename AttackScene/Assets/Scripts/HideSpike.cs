using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSpike : MonoBehaviour
{
    public GameObject triggerBox;
    public float attackTime;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other) { 
        //注意这里Player物体只能有一个Player Tag （子物体不能使用Player tag） 否则伤害会触发多次
        if(other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D"){
            StartCoroutine(spikeAttack());
        }
    }

    IEnumerator spikeAttack(){
        yield return new WaitForSeconds(attackTime);
        SoundManager.soundManagerInstance.hidenSpikeAudio();
        anim.SetTrigger("Attack");
        Instantiate(triggerBox,transform.position,Quaternion.identity);
    }
}
