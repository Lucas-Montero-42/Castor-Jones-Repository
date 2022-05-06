using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public CoinsCounter _coinscounter;
    // Start is called before the first frame update
    void Start()
    {
        //_coinscounter = GetComponent<CoinsCounter>();                        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            CoinsCounter.coins += 1;             
            Destroy(gameObject); 
                    
        }
    }
}
