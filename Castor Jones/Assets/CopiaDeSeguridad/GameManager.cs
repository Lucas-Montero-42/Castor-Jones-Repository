using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerType
{
    TECLADO,
    MANDO
}

public class GameManager : MonoBehaviour
{

    public ControllerType Controller;
    public AltPlayerMovement Player;
    public PlayerInput PlayerIM;
    public Grappler PlayerGR;
    public List<exitManager> ExitList;


    // Start is called before the first frame update
    void Start()
    {
        //Controller = ControllerType.TECLADO;
    }

    // Update is called once per frame
    void Update()
    {
        Player.controller = Controller;
        PlayerIM.controller = Controller;
        PlayerGR.controller = Controller;
        SetController();
    }

    public void SetController()
    {
        foreach(exitManager i in ExitList)
        {
            i.controller = Controller;
        }
    }
}
