using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava : MonoBehaviour
{
    [Header("²»Í¬½×¶ÎµÄÅö×²Æ÷")]
    public GameObject colid1;
    public GameObject colid2;
    public GameObject colid3;

    public GameObject groundFire;
    
    private void activateColid1()
    {
        colid1.SetActive(true);
    }

    private void activateColid2()
    {
        colid2.SetActive(true);
    }

    private void activateColid3()
    {
        colid3.SetActive(true);
    }

    private void deactivateColid1()
    {
        colid1.SetActive(false);
    }

    private void deactivateColid2()
    {
        colid2.SetActive(false);
    }

    private void deactivateColid3()
    {
        colid3.SetActive(false);
    }

    private void generateGroundFire()
    {
        GameObject Fire = Instantiate(groundFire, gameObject.transform.position - new Vector3(0, 1.5f, 0), Quaternion.identity);
        Fire.GetComponentsInChildren<GroundFireFake>()[0].leftTime = 0;
        Fire.GetComponentsInChildren<GroundFire>()[0].p.leftTime = 0;
        Fire.GetComponentsInChildren<GroundFireFake>()[1].leftTime = 0;
    }
}
