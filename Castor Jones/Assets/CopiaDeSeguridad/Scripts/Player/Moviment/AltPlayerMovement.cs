using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPlayerMovement : MonoBehaviour
{
    public bool IsMoving => _isMoving;


    [SerializeField]
    private float Speed = 5;

    [SerializeField]
    private float jumpVelocity = 5;

    private bool _isMoving;
    PlayerInput _input;
    Rigidbody2D _rigidbody;

    public bool isJumping;
    private bool canMove;
    private bool cantMoveWall;
    public bool walledL;
    public bool walledR;

    public GroundChecker groundChecker;
    public WallCheckerL wallCheckerL;
    public WallCheckerR wallCheckerR;
    public Grappler Grappler;

    bool vista = true;

    public ControllerType controller;


    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canMove && !Grappler._playerCanMove)
        {
            canMove = false;
        }
        
        if ((groundChecker.IsGrounded || canMove) && Grappler._playerCanMove)
        {
            Move();

            if (controller == ControllerType.TECLADO)
            {
                if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
                {
                    isJumping = true;
                    canMove = true;
                    Jump();
                }
            }
            else if (controller == ControllerType.MANDO)
            {
                if (Input.GetButtonDown("ControllerJump") && isJumping == false)
                {
                    isJumping = true;
                    canMove = true;
                    Jump();
                }
            }

        }
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpVelocity);
    }

    private void OnEnable()
    {
        GroundChecker.onLanded += OnLanded;
        WallCheckerL.onWalledL += OnWalledL;
        WallCheckerR.onWalledR += OnWalledR;
    }
    private void OnDisable()
    {
        GroundChecker.onLanded -= OnLanded;
        WallCheckerL.onWalledL -= OnWalledL;
        WallCheckerR.onWalledR -= OnWalledR;
    }
     
    private void OnLanded()
    {
        isJumping = false;
        canMove = false;
        walledL = false;
        walledR = false;
    }
    private void OnWalledL()
    {
        walledL = true;
    }
    private void OnWalledR()
    {
        walledR = true;
    }

    private void Move()
    {
        float realSpeed;
        if (Grappler._moveSlowPlayer)
            realSpeed = Speed / 3;
        else
            realSpeed = Speed;

        cantMoveWall = false;

        if (_input.MovementHorizontal * realSpeed < 0 && walledR)
        {
            cantMoveWall = true;
        }
        if (_input.MovementHorizontal * realSpeed > 0 && walledL)
        {
            cantMoveWall = true;
        }

        if (!cantMoveWall || groundChecker.IsGrounded)
        {
            Vector2 direction = new Vector2(_input.MovementHorizontal * realSpeed, _rigidbody.velocity.y);
            _rigidbody.velocity = direction;
            _isMoving = direction.magnitude > 0.01f;
        }     
    }
}
