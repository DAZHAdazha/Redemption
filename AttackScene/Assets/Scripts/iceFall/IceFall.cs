using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFall : MonoBehaviour
{

    public Animator ani;
    public GameObject smallIce;
    public GameObject colid;


    // Start is called before the first frame update
    void Start()
    {
        ani.Play("iceFall");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateSmallIce()
    {
        GameObject gb = (GameObject)Instantiate(smallIce,transform.position,transform.rotation);
    }

    private void turnOncolider()
    {
        colid.SetActive(true);
    }
    private void turnOffColider()
    {
        colid.SetActive(false);
    }

    
}
