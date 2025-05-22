using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;

    public float minSpawnTime = 2f;
    public float maxSpawnTime = 5f;

    public int minCustomersPerWave = 1;
    public int maxCustomersPerWave = 3;

    void Start()
    {
        Invoke("SpawnCustomerWave", Random.Range(minSpawnTime, maxSpawnTime));
    }

    void SpawnCustomerWave()
    {
        int customerCount = Random.Range(minCustomersPerWave, maxCustomersPerWave + 1);
        for (int i = 0; i < customerCount; i++)
        {
            Vector3 spawnOffset = new Vector3(0, 0, i * 2f); // Z eksenine do�ru s�ral�
            Quaternion faceX = Quaternion.Euler(0, 90f, 0); // M��teri X y�n�ne baks�n
            Instantiate(customerPrefab, spawnPoint.position + spawnOffset, faceX);
        }

        Invoke("SpawnCustomerWave", Random.Range(minSpawnTime, maxSpawnTime));
    }
}
