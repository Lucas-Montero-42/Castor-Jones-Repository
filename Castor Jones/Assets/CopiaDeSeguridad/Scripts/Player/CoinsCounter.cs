using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCounter : MonoBehaviour
{

    public Text counter;
    public static int coins = 0;

    void Start()
    {
        counter = GetComponent<Text>();
    }
    

    // Update is called once per frame
    void Update()
    {
        counter.text = coins.ToString();                
    }
    
}
