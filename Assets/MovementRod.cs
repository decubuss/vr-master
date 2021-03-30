using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRod : MonoBehaviour
{
    private Ray Ray;
    public RaycastHit Hit;
    public bool isHitting;
    public static MovementRod instance;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(Ray, out Hit))
        {
            Debug.Log("hitting it");
            isHitting = true;
        }
        else
            isHitting = false;

    }

}
