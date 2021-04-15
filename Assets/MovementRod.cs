using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRod : MonoBehaviour
{
    private Ray Ray;
    public RaycastHit Hit;
    public bool isHitting;
    public static MovementRod instance;
    private MovementKnot lastKnot;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        Ray = new Ray(transform.position,transform.up);
        if (Physics.Raycast(Ray,out Hit))
        {
            Debug.Log("hitting it");
            isHitting = true;
            var tmpKnot = Hit.transform.GetComponent<MovementKnot>();
            if (tmpKnot != null)
            {
                lastKnot = tmpKnot;
                lastKnot.isPointedAt = true;
            }
            else
            {
                lastKnot.isPointedAt = false;
            }
        }
        else
        {
            isHitting = false;
            lastKnot.isPointedAt = false;
        }
    }

}
