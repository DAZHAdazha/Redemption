using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class stopTheGame : BasicPlayableBehaviour
{
    public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        
    }

    public virtual void OnPlayStateChanged(FrameData info, PlayState newState)
    {
        if (newState == PlayState.Playing)
        {
            Debug.Log("Here");
            //添加切换场景的代码
            SceneManager.LoadScene(0);
        }
    }

}
