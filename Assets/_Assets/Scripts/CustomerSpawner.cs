using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;

    public float minSpawnTime = 2f;
    public float maxSpawnTime = 5f;

    public int minCustomersPerWave = 1; // minimum ka� m��teri gelsin
    public int maxCustomersPerWave = 3; // maksimum ka� m��teri gelsin

    void Start()
    {
        Invoke("SpawnCustomerWave", Random.Range(minSpawnTime, maxSpawnTime));
    }

    void SpawnCustomerWave()
    {
        int customerCount = Random.Range(minCustomersPerWave, maxCustomersPerWave + 1); // ka� ki�i gelsin
        for (int i = 0; i < customerCount; i++)
        {
            Vector3 spawnOffset = new Vector3(0, 0, i * 2f); // Z eksenine do�ru kayd�rma
            Instantiate(customerPrefab, spawnPoint.position + spawnOffset, Quaternion.identity);
        }

        Invoke("SpawnCustomerWave", Random.Range(minSpawnTime, maxSpawnTime));
    }
}
