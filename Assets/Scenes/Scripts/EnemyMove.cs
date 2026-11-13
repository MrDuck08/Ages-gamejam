using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] GameObject targetObj;


    float currentMoveSpeed = 1f;
    [SerializeField] float moveSpeed = 3f;

    Rigidbody2D rigidbody2D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        currentMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 direction = targetObj.transform.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

    }

    private void FixedUpdate()
    {
        if(targetObj == null) { return; }


        transform.position = Vector2.MoveTowards(transform.position, targetObj.transform.position, currentMoveSpeed * Time.deltaTime);

        //rigidbody2D.AddForce(transform.up * (moveSpeed/5) * Time.deltaTime);

    }

    public IEnumerator StaggerRoutine(float staggerTime)
    {
        currentMoveSpeed = 0;

        yield return new WaitForSeconds(staggerTime);

        currentMoveSpeed = moveSpeed;
    }
}
