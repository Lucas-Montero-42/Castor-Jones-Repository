using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WallCheckerL : MonoBehaviour
{
    [SerializeField] private Transform WallLCheckTransform;
    [SerializeField] float radius;
    [SerializeField] LayerMask whatIsWall;

    public bool IsWallL => isWallL;
    private bool isWallL;

    private bool wasWallL;

    public static Action onWalledL;

    private void FixedUpdate()
    {
        CheckWallL();
    }

    void CheckWallL()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(WallLCheckTransform.position, radius, whatIsWall);
        isWallL = colliders.Length > 0;

        if (!wasWallL && isWallL)
        {
            JustWalledL();
        }

        wasWallL = isWallL;
    }

    private void JustWalledL()
    {
        onWalledL?.Invoke();
    }
}
