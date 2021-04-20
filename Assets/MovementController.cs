using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Valve.VR;
using UnityEngine.Rendering.PostProcessing;

public class MovementController : MonoBehaviour
{
    public float sensetivity = 100f;
    [SerializeField]
    public Transform PlayerBody;
    public float xRotation;

    [SerializeField]
    public PostProcessVolume Vignette;
    //[SerializeField]
    //public CharacterController CC;
    //[SerializeField]
    //public float Speed = 12f;

    private bool isMoving = false;
    public float LerpSpeed = 5f;

    private float fraction = 0;
    private Vector3 LerpDest;
    private Vector3 LerpStart;
    [SerializeField]
    public MovementKnot CurrentKnot;
    private MovementKnot DestKnot;

    private InputDevice device;
    public XRNode inputSource;

    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grabPinchAction;
    
    private Outline lastOutline;
    private void Awake()
    {
        PlayerBody = transform;
        Vignette.enabled = false;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }

    // Update is called once per frame
    void Update()
    {
        bool triggerValue;
        if (grabPinchAction.GetState(handType)&&
            MovementRod.instance.isHitting && 
            !isMoving)
        {
            Debug.Log("Trigger button is pressed.");

            DestKnot = MovementRod.instance.Hit.transform.GetComponent<MovementKnot>();
            LerpStart = transform.position;
            LerpDest = MoveToKnot(DestKnot);
            isMoving = true;
            Vignette.enabled = true;
        }

        if (fraction < 1 && isMoving == true)
        {
            fraction += Time.deltaTime * LerpSpeed;
            transform.position = Vector3.Lerp(LerpStart, LerpDest, fraction);


            if (fraction >= 1)
            {
                fraction = 0;
                isMoving = false;
                CurrentKnot.PlayerExited();
                CurrentKnot = DestKnot;
                CurrentKnot.PlayerEntered();
                Vignette.enabled = false;
            }
        }
        //MovementClamp();
    }
    private Vector3 MoveToKnot(MovementKnot knot)
    {
        if (knot == CurrentKnot) { Debug.Log("same knot, not moving there"); return Vector3.zero; }

        float distance = Vector3.Distance(knot.InitialPosition,CurrentKnot.InitialPosition);//TODO: replace by static distance, cuz rooms have same size - nah fam
        Vector3 direction = (knot.InitialPosition - CurrentKnot.InitialPosition).normalized;
        return PlayerBody.position + (direction * distance);
    }

    //private void MovementClamp()//previously wasd movemnt
    //{
    //    float x = Input.GetAxis("Horizontal");
    //    float z = Input.GetAxis("Vertical");

    //    Vector3 move = transform.right * x + transform.forward * z;
    //    CC.Move(move * Speed * Time.deltaTime);
    //    Vector3 clampedPos = transform.position;

    //    clampedPos.x = Mathf.Clamp(
    //        transform.position.x,
    //        CurrentKnot.InitialPosition.x - CurrentKnot.xBounds,
    //        CurrentKnot.InitialPosition.x + CurrentKnot.xBounds);

    //    clampedPos.z = Mathf.Clamp(
    //        transform.position.z,
    //        CurrentKnot.InitialPosition.z - CurrentKnot.zBounds,
    //        CurrentKnot.InitialPosition.z + CurrentKnot.zBounds);

    //    transform.position = clampedPos;
    //}
}
