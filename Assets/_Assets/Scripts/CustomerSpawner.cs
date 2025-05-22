using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;      // Spawnlanacak m��teri prefab'�
    public Transform spawnPoint;           // M��teri nerede spawn olacak

    public float minSpawnTime = 2f;        // �lk minimum spawn s�resi
    public float maxSpawnTime = 5f;        // �lk maksimum spawn s�resi

    public float minLimit = 0.5f;          // Zamanla ne kadar h�zl� olabilir, alt s�n�r
    public float difficultyIncreaseRate = 0.1f; // Her spawn sonras� ne kadar h�zlanacak

    void Start()
    {
        SpawnCustomer(); // Oyunun ba��nda ilk spawn
    }

    void SpawnCustomer()
    {
        Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);

        // Spawn s�relerini d���rerek oyunu zorla�t�r
        minSpawnTime = Mathf.Max(minLimit, minSpawnTime - difficultyIncreaseRate);
        maxSpawnTime = Mathf.Max(minLimit + 0.5f, maxSpawnTime - difficultyIncreaseRate);

        // Yeni spawn zaman�n� ayarla
        Invoke("SpawnCustomer", Random.Range(minSpawnTime, maxSpawnTime));
    }
}
