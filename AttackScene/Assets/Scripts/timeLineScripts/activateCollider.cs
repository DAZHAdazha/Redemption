using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;


public class activateCollider : PlayableBehaviour
{
    public GameObject boss;

    public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        
    }

    public virtual void OnPlayStateChanged(FrameData info, PlayState newState)
    {
        boss = GameObject.Find("bossForCG");
        boss.GetComponent<Collider2D>().enabled = true;
    }

}
