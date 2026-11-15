using UnityEngine;

public class InstantianteTemporaryObjectScript : MonoBehaviour
{
    [Tooltip("The prefab of the object you wish to spawn when SpawnObject is called.")]
    [SerializeField] private GameObject prefabOfObjectToSpawn;
    public void SpawnObject()
    {
        Instantiate(prefabOfObjectToSpawn, transform.position, transform.rotation);
    }
}
