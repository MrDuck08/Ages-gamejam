using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;

    EnemyMove enemyMove;

    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
    }

    public void TakeDamage(float damage, float staggerTime)
    {
        health -= damage;
        if (health <= 0) {OnDeath.Invoke();}
        else
        {
            if(enemyMove != null)
            {
                StartCoroutine(enemyMove.StaggerRoutine(staggerTime));
            }
        }
    }

    public UnityEvent OnDeath;
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
