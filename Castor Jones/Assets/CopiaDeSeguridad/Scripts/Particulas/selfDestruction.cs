using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruction : MonoBehaviour
{
    private float cooldown;

    void Update()
    {
        cooldown = cooldown + Time.deltaTime;
        if(cooldown > 1)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    
}
