using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUpEffect : MonoBehaviour
{
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").gameObject.transform;
    }

    public void hidePlayer()
    {
        playerTransform.gameObject.SetActive(false);
    }

    public void destroyEffect()
    {
        Destroy(gameObject);
        playerTransform.gameObject.SetActive(true);
    }
}
