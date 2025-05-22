using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;

    public float minSpawnTime = 2f;
    public float maxSpawnTime = 5f;

    public int minCustomersPerWave = 1; // minimum kaç müþteri gelsin
    public int maxCustomersPerWave = 3; // maksimum kaç müþteri gelsin

    void Start()
    {
        Invoke("SpawnCustomerWave", Random.Range(minSpawnTime, maxSpawnTime));
    }

    void SpawnCustomerWave()
    {
        int customerCount = Random.Range(minCustomersPerWave, maxCustomersPerWave + 1); // kaç kiþi gelsin
        for (int i = 0; i < customerCount; i++)
        {
            Vector3 spawnOffset = new Vector3(0, 0, i * 2f); // Z eksenine doðru kaydýrma
            Instantiate(customerPrefab, spawnPoint.position + spawnOffset, Quaternion.identity);
        }

        Invoke("SpawnCustomerWave", Random.Range(minSpawnTime, maxSpawnTime));
    }
}
