using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoCarretilla : MonoBehaviour
{
    private Transform CarretaTra;
    private Rigidbody2D CarretaRB;
    private Vector3 Dir;
    public float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        CarretaTra = GetComponent<Transform>();
        CarretaRB = GetComponent<Rigidbody2D>();
        Dir = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Mover(Dir);
        }
    }
    private void Mover(Vector3 dir)
    {
        //CarretaTra.position = CarretaTra.position + 0.5f*dir*Time.deltaTime;
        CarretaRB.AddForce(dir*speed* Time.deltaTime, ForceMode2D.Impulse);
    }
    public void Dirección(Vector3 Direction)
    {
        Dir = Direction;
    }
}
