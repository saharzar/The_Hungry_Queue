using UnityEngine;

public class Customer : MonoBehaviour
{
    public float lifetime = 5f; // 80 yerine test için 5 yaptýk

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
