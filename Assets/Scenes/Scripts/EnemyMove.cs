using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    float currentMoveSpeed = 1f;
    [SerializeField] float moveSpeed = 3f;

    Rigidbody2D rigidbody2D;
    
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        currentMoveSpeed = moveSpeed;
    }
    
    void Update()
    {

        Vector2 direction = target.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

    }

    private void FixedUpdate()
    {
        if(target == null) { return; }


        transform.position = Vector2.MoveTowards(transform.position, target.position, currentMoveSpeed * Time.deltaTime);

        //rigidbody2D.AddForce(transform.up * (moveSpeed/5) * Time.deltaTime);

    }

    public IEnumerator StaggerRoutine(float staggerTime)
    {
        currentMoveSpeed = 0;

        yield return new WaitForSeconds(staggerTime);

        currentMoveSpeed = moveSpeed;
    }
}
