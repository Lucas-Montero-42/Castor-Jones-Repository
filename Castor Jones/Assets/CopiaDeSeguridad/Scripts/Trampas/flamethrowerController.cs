using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flamethrowerController : MonoBehaviour
{
    private float cooldownTime = 6;
    private float nextFireTime = 0;
    public bool Working = false;
    public bool notSync = false;
    public GameObject Fire;
    public GameObject Prefire;

    // Start is called before the first frame update
    void Start()
    {
        Working = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Working)
        {
            if (Time.time > nextFireTime)
            {
                StartCoroutine(Fuego(notSync));
                nextFireTime = Time.time + cooldownTime;
            }
        }
    }
    IEnumerator Fuego(bool NSync)
    {
        if (NSync)
        {
            yield return new WaitForSeconds(3);
        }
        else
        {
            yield return new WaitForSeconds(0);
        }
        Prefire.SetActive(true);
        yield return new WaitForSeconds(1f);
        Prefire.SetActive(false);
        Fire.SetActive(true);
        yield return new WaitForSeconds(2f);
        Fire.SetActive(false);

    }
}
