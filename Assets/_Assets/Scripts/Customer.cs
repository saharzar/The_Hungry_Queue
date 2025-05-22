using UnityEngine;

public class Customer : MonoBehaviour
{
    public float lifetime = 5f; // 80 yerine test i�in 5 yapt�k

    void Start()
    {
        Debug.Log("Customer spawned!");
        Destroy(gameObject, lifetime);
    }

    void OnDestroy()
    {
        Debug.Log("Customer destroyed!");
    }
}
