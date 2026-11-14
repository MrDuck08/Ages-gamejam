using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [NonSerialized] public InputAction moveAction;
    [NonSerialized] public InputAction lookAction;
    
    private BaseLegs currentLegs;
    [SerializeField] private BaseLegs baseLegs;
    
    private Vector2 virtualMousePosition;
    [Header("Mouse aiming")]
    [Tooltip("Basically, how mouse movement works in this game is that it confines a virtual mouse to the size of a circle around the player, this determines that ones size. Recommended size is 0.5f.")]
    [SerializeField] private float sizeOfMouseConfineRing;

    private Camera cam;
    private void Start()
    {
        SetNewLegs(baseLegs);
        
        cam = Camera.main;
        moveAction =  InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        AimType = GameSettings.AimType;
        if (AimType == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private float MoveSpeed;
    private float MaxSpeed;
    private void FixedUpdate()
    { 
        //Normal Movement
        
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector2 velocityToSet = rb.linearVelocity;
        
        velocityToSet += input * (MoveSpeed * rb.linearDamping * Time.fixedDeltaTime);

        if (velocityToSet.magnitude > MaxSpeed * rb.linearDamping)
        {
            //we need to limit it. normalize the vector and then make it the same length as max speed.
            velocityToSet = rb.linearVelocity.normalized * (MaxSpeed * rb.linearDamping);
        }

        rb.linearVelocity = velocityToSet;
        
        //Tank Movement
        //rb.AddForce(new Vector2(0, moveAction.ReadValue<Vector2>().y) * MoveSpeed);
        //rb.AddTorque(moveAction.ReadValue<Vector2>().x * RotateSpeed * -1);
        switch (AimType)
        {
            case 0:
                Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(new Vector3(
                    Mouse.current.position.ReadValue().x,
                    Mouse.current.position.ReadValue().y,
                    cam.nearClipPlane // Set Z to the camera's near clip plane
                ));
                Aim(transform, mouseWorldPosition);
                break;
            case 1:
                DoLookWithMouse(lookAction.ReadValue<Vector2>());
                break;
        }
        
    }
    
    [SerializeField] private int AimType;
    private void Aim(Transform trans, Vector3 pos) // rotates the whole player towards the mouse
    {
        trans.rotation = Quaternion.Euler(0,0,0) * Quaternion.LookRotation(Vector3.forward, pos - transform.position);
    }
    
    private void DoLookWithMouse(Vector2 mouseInput)
    {
        virtualMousePosition += mouseInput * Time.deltaTime;

        if (virtualMousePosition.magnitude > sizeOfMouseConfineRing) virtualMousePosition = virtualMousePosition.normalized * sizeOfMouseConfineRing;
        
        float angleRadians = Mathf.Atan2(virtualMousePosition.y, virtualMousePosition.x);
        transform.rotation = Quaternion.Euler(0, 0, angleRadians * Mathf.Rad2Deg - 90f);
    }

    public void SetNewLegs(BaseLegs legs)
    {
        if(legs != baseLegs) legs.BePickedUp(transform);
        currentLegs = legs;

        rb.linearDamping = legs.LinearDampening;
        MoveSpeed = legs.Speed;
        MaxSpeed = legs.MaxSpeed;
    }

    public void TryPickUpNewLegs(BaseLegs legsWeTrynnaPickUp)
    {
        if (currentLegs != baseLegs)
        {
            //we can drop these
            currentLegs.DropSelf();
        }
        SetNewLegs(legsWeTrynnaPickUp);
    }
    
    public void LegsTakeDurabilityDamage(float damage)
    {
        if (currentLegs.TakeDurabilityDamage(damage))
        {
            //ouch! our legs are gone!
            SetNewLegs(baseLegs);
        }
    }
}
