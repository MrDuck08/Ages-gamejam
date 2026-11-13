using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) {OnDeath.Invoke();}
        else
        {
            //StartCoroutine(enemyMove.StaggerRoutine(staggerTime));
        }
    }

    public UnityEvent OnDeath;
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
