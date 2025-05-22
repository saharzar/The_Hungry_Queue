using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;      // Spawnlanacak müþteri prefab'ý
    public Transform spawnPoint;           // Müþteri nerede spawn olacak

    public float minSpawnTime = 2f;        // Ýlk minimum spawn süresi
    public float maxSpawnTime = 5f;        // Ýlk maksimum spawn süresi

    public float minLimit = 0.5f;          // Zamanla ne kadar hýzlý olabilir, alt sýnýr
    public float difficultyIncreaseRate = 0.1f; // Her spawn sonrasý ne kadar hýzlanacak

    void Start()
    {
        SpawnCustomer(); // Oyunun baþýnda ilk spawn
    }

    void SpawnCustomer()
    {
        Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);

        // Spawn sürelerini düþürerek oyunu zorlaþtýr
        minSpawnTime = Mathf.Max(minLimit, minSpawnTime - difficultyIncreaseRate);
        maxSpawnTime = Mathf.Max(minLimit + 0.5f, maxSpawnTime - difficultyIncreaseRate);

        // Yeni spawn zamanýný ayarla
        Invoke("SpawnCustomer", Random.Range(minSpawnTime, maxSpawnTime));
    }
}
