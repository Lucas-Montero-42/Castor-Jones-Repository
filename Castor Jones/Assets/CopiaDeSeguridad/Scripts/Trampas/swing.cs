using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swing : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.rotation = rb.rotation + speed * Time.deltaTime;
        if (rb.rotation < -50)
        {
            speed = -speed;
        }
        else if (rb.rotation > 50)
        {
            speed = -speed;
        }

    }

}
