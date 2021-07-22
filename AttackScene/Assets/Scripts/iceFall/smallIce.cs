using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallIce : MonoBehaviour
{
    public Animator ani;
    public List<GameObject> colliders;
    // Start is called before the first frame update
    void Start()
    {
        ani.Play("smallIce");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destoryIce()
    {
        Destroy(gameObject);
    }

    private void turnOnCollider(int i)
    {
        colliders[i].SetActive(true);
    }

    private void turnOffColider(int i)
    {
        colliders[i].SetActive(false);
    }
}
