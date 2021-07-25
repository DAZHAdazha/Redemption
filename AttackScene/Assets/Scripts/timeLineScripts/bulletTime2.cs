using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;


public class bulletTime2 : BasicPlayableBehaviour
{
    public float targetTimeScale;
    private float _originalTimeScale;

    public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (playable.GetTime() == 0)
        {
            return;
        }
        Time.timeScale = Mathf.Lerp(_originalTimeScale, targetTimeScale, (float)(playable.GetTime() / playable.GetDuration()));
    }

    public virtual void OnPlayStateChanged(FrameData info, PlayState newState)
    {
        if (newState == PlayState.Playing)
        {
            _originalTimeScale = Time.timeScale;
        }
    }

}
