using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;


public class bulletTime : BasicPlayableBehaviour
{
    public float targetTimeScale;
    private float _originalTimeScale = 1f;

    public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if(playable.GetTime() == 0)
        {
            return;
        }
        Time.timeScale = Mathf.Lerp(_originalTimeScale, targetTimeScale, (float)(playable.GetTime() / playable.GetDuration()));
    }

    public virtual void OnPlayStateChanged(FrameData info, PlayState newState)
    {
        _originalTimeScale = Time.timeScale;
    }
    
}
