using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;


public class timeNormalization : BasicPlayableBehaviour
{
    
    public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
    {

    }

    public virtual void OnPlayStateChanged(FrameData info, PlayState newState)
    {
        Time.timeScale = 1;
    }

}
