using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Rendering.PostProcessing;

public class MovementController : MonoBehaviour
{
    public float sensetivity = 100f;
    [SerializeField]
    public Transform PlayerBody;
    public float xRotation;

    [SerializeField]
    public Vignette Vignette;
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

    private Outline lastOutline;
    private void Awake()
    {
        PlayerBody = transform;
        Vignette.active = false;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }

    // Update is called once per frame
    void Update()
    {
        //mouseX = Input.GetAxis("Mouse X") * sensetivity * Time.deltaTime;
        //mouseY = Input.GetAxis("Mouse Y") * sensetivity * Time.deltaTime;

        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //PlayerBody.Rotate(Vector3.up * mouseX);

        bool triggerValue;
        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) &&
            triggerValue &&
            MovementRod.instance.isHitting && 
            !isMoving)
        {
            Debug.Log("Trigger button is pressed.");

            DestKnot = MovementRod.instance.Hit.transform.GetComponent<MovementKnot>();
            LerpStart = transform.position;
            LerpDest = MoveToKnot(DestKnot);
            isMoving = true;
            Vignette.active = true;
        }

        if (fraction < 1 && isMoving == true)
        {
            fraction += Time.deltaTime * LerpSpeed;
            transform.position = Vector3.Lerp(LerpStart, LerpDest, fraction);


            if (fraction >= 1)
            {
                fraction = 0;
                isMoving = false;
                CurrentKnot = DestKnot;
                Vignette.active = true;
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
