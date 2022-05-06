using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float MovementHorizontal { get; private set; }
    public float MovementVertical { get; private set; }
    public ControllerType controller;


    void Update()
    {
        if (controller == ControllerType.TECLADO)
        {
            MovementHorizontal = Input.GetAxis("Horizontal");
            MovementVertical = Input.GetAxis("Vertical");
        }

        if (controller == ControllerType.MANDO)
        {
            MovementHorizontal = Input.GetAxis("Controller Horizontal");
        }
    }
}
