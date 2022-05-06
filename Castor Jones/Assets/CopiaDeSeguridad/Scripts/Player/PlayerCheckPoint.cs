using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPoint : MonoBehaviour
{
    Vector3 spawnPoint;
    public bool YouDied;
    public Grappler grapplerRope;
    private Rigidbody2D RB2D;
    public ParticleSystem Blood;

    [Header("Vagoneta 01")]
    public GameObject vagoneta01;
    public Vector2 posV1;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = gameObject.transform.position;
        RB2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (YouDied)
        {
            Instantiate(Blood, transform.position, Quaternion.identity); 
            YouDied = false;
            gameObject.transform.position = spawnPoint;
            grapplerRope.desactivateGrappler();
            RB2D.velocity = new Vector2(0,0);
            MoveObjects();
            
        }
    }

    private void MoveObjects()
    {
        vagoneta01.transform.position = posV1;
        vagoneta01.transform.localRotation = Quaternion.Euler(0,0,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            spawnPoint = collision.transform.position;
        }
        if (collision.gameObject.tag == "DMG")
        {            
            YouDied = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DMG")
        {            
            YouDied = true;
        }
    }
}
