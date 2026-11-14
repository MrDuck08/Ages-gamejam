using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damageFromTouchingEnemyForOneSecond;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerStay2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Blir träffad av en fiende
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageFromTouchingEnemyForOneSecond * Time.deltaTime, 1337);
        }
    }
}
