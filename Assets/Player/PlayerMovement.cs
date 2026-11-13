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
    
    private Vector2 virtualMousePosition;
    [Header("Mouse aiming")]
    [Tooltip("Basically, how mouse movement works in this game is that it confines a virtual mouse to the size of a circle around the player, this determines that ones size. Recommended size is 0.5f.")]
    [SerializeField] private float sizeOfMouseConfineRing;
    
    private void Start()
    {
        moveAction =  InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        if (rb == null)
        {
            Debug.Log(this.gameObject.name + " has no rigidbody attached");
            rb = GetComponent<Rigidbody2D>();
        }

    }

    [SerializeField] private float MoveSpeed;
    [SerializeField] private float RotateSpeed;
    private void FixedUpdate()
    {
        //Normal Movement
        rb.AddForce(moveAction.ReadValue<Vector2>() * 10f);
        DoLookWithMouse(lookAction.ReadValue<Vector2>());
        //Tank Movement
        //rb.AddForce(new Vector2(0, moveAction.ReadValue<Vector2>().y) * MoveSpeed);
        //rb.AddTorque(moveAction.ReadValue<Vector2>().x * RotateSpeed * -1);
    }

    private void DoLookWithMouse(Vector2 mouseInput)
    {
        virtualMousePosition += mouseInput * Time.deltaTime;

        if (virtualMousePosition.magnitude > sizeOfMouseConfineRing) virtualMousePosition = virtualMousePosition.normalized * sizeOfMouseConfineRing;
        
        float angleRadians = Mathf.Atan2(virtualMousePosition.y, virtualMousePosition.x);
        transform.rotation = Quaternion.Euler(0, 0, angleRadians * Mathf.Rad2Deg - 90f);
    }
}
