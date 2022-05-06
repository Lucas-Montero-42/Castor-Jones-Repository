using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform groundRCheckTransform;
    [SerializeField] private Transform groundLCheckTransform;
    [SerializeField] float radius;
    [SerializeField] LayerMask whatIsGround;

    public bool IsGrounded => isGrounded;
    public bool isGrounded;

    public bool isGroundedR;
    public bool isGroundedL;

    private bool wasGround;

    public static Action onLanded;

    private void FixedUpdate()
    {
        CheckGrounded();
    }

    void CheckGrounded()
    {
        Collider2D[] collidersR = Physics2D.OverlapCircleAll(groundRCheckTransform.position , radius, whatIsGround);
        isGroundedR = collidersR.Length > 0;

        Collider2D[] collidersL = Physics2D.OverlapCircleAll(groundLCheckTransform.position , radius, whatIsGround);
        isGroundedL = collidersL.Length > 0;

        if (isGroundedL || isGroundedR)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (!wasGround && isGrounded)
        {
            JustLanded();
        }

        wasGround = isGrounded;
    }

    private void JustLanded()
    {
        onLanded?.Invoke();
    }
}
