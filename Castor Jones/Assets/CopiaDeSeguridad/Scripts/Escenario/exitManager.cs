using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitManager : MonoBehaviour
{
    public Transform Exit;
    public GameObject PressEText;
    public GameObject PressXText;
    private GameObject Player;
    private bool CanTeleport;
    private Rigidbody2D PRB;
    public ControllerType controller;
    // Start is called before the first frame update
    void Start()
    {
        PressEText.SetActive(false);
        PressXText.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
        PRB = Player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == ControllerType.TECLADO)
        {
            if (Input.GetKey(KeyCode.E) && CanTeleport)
            {
                Player.transform.position = Exit.transform.position;
                PRB.velocity = new Vector2(0,0);
            }
        }
        else if(controller == ControllerType.MANDO)
        {
            if (Input.GetButtonDown("Interaction") && CanTeleport)
            {
                Player.transform.position = Exit.transform.position;
                PRB.velocity = new Vector2(0,0);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (controller == ControllerType.TECLADO)
        {
            PressEText.SetActive(true);
        }
        else if (controller == ControllerType.MANDO)
        {
            PressXText.SetActive(true);
        }
        CanTeleport = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (controller == ControllerType.TECLADO)
        {
            PressEText.SetActive(false);
        }
        else if (controller == ControllerType.MANDO)
        {
            PressXText.SetActive(false);
        }
        CanTeleport = false;
    }
}
