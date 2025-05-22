using UnityEngine;

public class Customer : MonoBehaviour
{
    public float lifetime = 80f;

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
