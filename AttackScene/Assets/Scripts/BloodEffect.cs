using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    public float timeToDistroy;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,timeToDistroy);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
