using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementKnot : MonoBehaviour
{
    public Vector3 InitialPosition;
    [SerializeField]
    public float xBounds = 1f;
    [SerializeField]
    public float zBounds = 1f;

    public LayerMask defaultLayer;
    public LayerMask ignoredLayer;

    private Outline Outline;
    public bool isPointedAt;
    private Collider Collider;
    private void Awake()
    {
        InitialPosition = transform.position;
        Outline = transform.GetComponent<Outline>();
        Outline.OutlineWidth = 0;
        Collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        Outline.OutlineWidth = isPointedAt ? 5 : 0;
    }

    public void PlayerEntered()
    {
        //Collider.enabled=false;
        gameObject.layer = 2;
    }

    public void PlayerExited()
    {
        //Collider.enabled = true;
        gameObject.layer = 0;
    }

}
