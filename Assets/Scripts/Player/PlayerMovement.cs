using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Chars chars;
    PlayerControls inputActions;

    public delegate void onUpdateDel();
    public delegate void MovementDirectionChanged(Vector2 vector2);
    internal delegate void MovementDel(float value);
    public event onUpdateDel onUpdate;
    internal event MovementDel MovementPerformed;
    internal event MovementDel RotationPerformed;

    protected GameObject playerVector;
    protected GameObject playerModel;

    Rigidbody rb;

    public float[] velModifiers = new float[2] { 1, 2 };//модификаторы скорости шаг и бег
    //public float additionalModifier = 0f;//дополнительный модификатор (от способности, навыка и т.д.) ПЕРЕНЕСЕН В CHARS
    //формула макс скорости: скорость от модификатора режима ходьбы + дополнительный модификатор
    public int currentModifier = 0;

    //спринт и пригнувшись: что если сделать их как режим бега и ходьбы соответственно, но с разным модификатором скорости?
    //например, спринт: переключение режима на бег + модификатор спринта (условно 0.3)
    //пригнувшись: шаг + модификатор пригнувшись (-0.3)
    //переключение режима: (currentModifier+1)%2
    //скорость подката и др. способностей ограничена мод скорости и доп мод скорости

    public Vector2 movementDirection { get; private set; }
    Vector3 newVelocity;
    Vector3 stockVelocity = new Vector3(0, -10, 0);

    float angle;
    float movementDirectionAngle;

    bool isChangeSpeedFunctionPerforming = true;

    // Start is called before the first frame update
    void Start()
    {
        chars = GetComponent<Chars>();
        inputActions = GetComponentInParent<PlayerBeh>().inputActions;
        playerModel = GetComponentInChildren<PlayerAnimator>().gameObject;
        playerVector = GetComponentInChildren<PlayerVector>().gameObject;
        rb = GetComponent<Rigidbody>();
        onUpdate += ChangeSpeed;
        onUpdate += nothing;

        inputActions.MovementMap.Movement.performed += Movement;
        inputActions.MovementMap.Movement.canceled += Movement;
        inputActions.MouseMap.RightStick.performed += RightStick;
        inputActions.MouseMap.MouseMoved.performed += MouseMoved;
        inputActions.MovementMap.Movement.Enable();
        inputActions.MouseMap.MouseMoved.Enable();
        inputActions.MouseMap.RightStick.Enable();
    }

    private void RightStick(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 rightStickVector = obj.ReadValue<Vector2>();
        if (rightStickVector == Vector2.zero) { return; }
        Debug.Log(rightStickVector);
        angle = Vector2.SignedAngle(rightStickVector, Vector2.up);

        Vector3 rotation = playerModel.transform.eulerAngles;
        rotation.y = angle;
        playerModel.transform.eulerAngles = rotation;

        RotationPerformed(((-angle + movementDirectionAngle) + 360) % 360);

    }

    private void MouseMoved(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Camera camera = Camera.main;

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        Physics.Raycast(ray, out raycastHit, 1000f, LayerMask.GetMask("AimPlane"));

        Vector3 vector3 = raycastHit.point - playerVector.transform.position;
        vector3.y = 0;
        Vector3 vec_trans = playerVector.transform.forward;

        angle = Vector3.SignedAngle(vec_trans, vector3, playerVector.transform.up);
        angle = (angle + 360) % 360;

        Vector3 rotation = playerModel.transform.eulerAngles;
        rotation.y = angle;
        playerModel.transform.eulerAngles = rotation;

        RotationPerformed(((-angle + movementDirectionAngle) + 360) % 360);
    }

    private void Movement(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        movementDirection = obj.ReadValue<Vector2>();

        if (movementDirection == Vector2.zero)
        {
            onUpdate -= ChangeSpeed;
            isChangeSpeedFunctionPerforming = false;
            MovementPerformed(0);
            return;
        }
        else if (!isChangeSpeedFunctionPerforming)
        {
            onUpdate += ChangeSpeed;
            isChangeSpeedFunctionPerforming = true;
        }


        newVelocity = playerVector.transform.forward * movementDirection.y + playerVector.transform.right * movementDirection.x;

        movementDirectionAngle = (Vector2.SignedAngle(movementDirection, Vector2.up) + 360) % 360;

        RotationPerformed(((movementDirectionAngle - angle) + 360) % 360);
        MovementPerformed(movementDirection.magnitude * velModifiers[currentModifier]);
    }

    void ChangeSpeed()
    {
        Vector3 vector3 = newVelocity * (chars.Speed + chars.AdditionalSpeed);
        vector3.y = -10f;
        rb.velocity = vector3;
    }


    private void FixedUpdate()
    {
        onUpdate();
        //вручную замедляем скорость для симуляции инерции
        rb.velocity = Vector3.Lerp(rb.velocity, stockVelocity, 5f * Time.deltaTime); 
    }

    void nothing()
    {

    }
}
