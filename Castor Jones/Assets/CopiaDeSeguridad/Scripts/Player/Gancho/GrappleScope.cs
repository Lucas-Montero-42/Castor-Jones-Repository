using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleScope : MonoBehaviour
{
    [SerializeField] private Transform grappleCheckTransform;
    [SerializeField] float radius;
    [SerializeField] LayerMask whatIsGrapplable;

    private bool isGrapplable;

    private SpriteRenderer colorRender;

    private void Start()
    {
        colorRender = gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckIsGrapplable();
    }

    void CheckIsGrapplable()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(grappleCheckTransform.position, radius, whatIsGrapplable);
        isGrapplable = colliders.Length > 0;

        if (isGrapplable)
        {
            colorRender.color = Color.green;
        }
        else
        {
            colorRender.color = Color.red;
        }

    }
}
