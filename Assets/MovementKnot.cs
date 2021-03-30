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

    private Outline Outline;
    public bool isPointedAt;
    private void Awake()
    {
        InitialPosition = transform.position;
        Outline = transform.GetComponent<Outline>();
        Outline.OutlineWidth = 0;
    }
    public void OutlineSwitch(bool value)
    {
        isPointedAt = value;
        Outline.OutlineWidth = isPointedAt ? 5f : 0f;
    }
}
