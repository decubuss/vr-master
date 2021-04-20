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
        Ray = new Ray(transform.position,transform.forward*5f);
        if (Physics.Raycast(Ray,out Hit))
        {
            Debug.Log("hitting it " + Hit.collider.name);
            isHitting = true;
            var tmpKnot = Hit.transform.GetComponent<MovementKnot>();
            if (tmpKnot != null)
            {
                lastKnot = tmpKnot;
                lastKnot.isPointedAt = true;
            }
            else
            {
                if(lastKnot)
                    lastKnot.isPointedAt = false;
            }
        }
        else
        {
            isHitting = false;
            if(lastKnot)
                lastKnot.isPointedAt = false;
        }
    }

}
