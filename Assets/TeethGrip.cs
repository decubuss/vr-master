using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Rendering.PostProcessing;

public class TeethGrip : MonoBehaviour
{
    // Start is called before the first frame update
    private SphereCollider sphere;
    private InputDevice device;

    [HideInInspector]
    public GameObject grabbedGO;
    public XRNode inputSource;

    void Awake()
    {
        sphere = GetComponent<SphereCollider>();
        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
