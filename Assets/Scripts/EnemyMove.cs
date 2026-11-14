using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    float currentMoveSpeed = 1f;
    [SerializeField] float moveSpeed = 3f;

    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = FindObjectOfType<PlayerMovement>().gameObject.transform;

        currentMoveSpeed = moveSpeed;
    }
    
    void Update()
    {

        /*
        if (target == null) return;
        Vector2 direction = target.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

        */
    }

    private void FixedUpdate()
    {
        if(target == null) { return; }
        //transform.position = Vector2.MoveTowards(transform.position, target.position, currentMoveSpeed * Time.deltaTime);
        
        Vector3 targetPosition = target.position;
        float angleRadians = Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x);
        rb.rotation = angleRadians * Mathf.Rad2Deg - 90f;

        rb.linearVelocity = transform.up * (currentMoveSpeed);

    }

    public IEnumerator StaggerRoutine(float staggerTime)
    {
        currentMoveSpeed = 0;

        yield return new WaitForSeconds(staggerTime);

        currentMoveSpeed = moveSpeed;
    }
}
