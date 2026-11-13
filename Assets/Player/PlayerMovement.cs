using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [NonSerialized] public InputAction moveAction;
    private void Start()
    {
        moveAction =  InputSystem.actions.FindAction("Move");
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
        Rotate();
        //Tank Movement
        //rb.AddForce(new Vector2(0, moveAction.ReadValue<Vector2>().y) * MoveSpeed);
        //rb.AddTorque(moveAction.ReadValue<Vector2>().x * RotateSpeed * -1);
    }

    private void Rotate()
    {
        Vector3 targetPos = transform.position + (Vector3)rb.linearVelocity;
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}
