using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WallCheckerR : MonoBehaviour
{
    [SerializeField] private Transform WallRCheckTransform;
    [SerializeField] float radius;
    [SerializeField] LayerMask whatIsWall;

    public bool IsWallR => isWallR;
    private bool isWallR;

    private bool wasWallR;

    public static Action onWalledR;

    private void FixedUpdate()
    {
        CheckWallR();
    }

    void CheckWallR()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(WallRCheckTransform.position , radius, whatIsWall);
        isWallR = colliders.Length > 0;

        if (!wasWallR && isWallR)
        {
            JustWalledR();
        }

        wasWallR = isWallR;
    }

    private void JustWalledR()
    {
        onWalledR?.Invoke();
    }
}
