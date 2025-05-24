using UnityEngine;
using UnityEngine.UI; // Sprite kullan�m� i�in

public class Customer : MonoBehaviour
{
    public float lifetime = 80f;

    // M��teri iste�ini g�sterecek sprite renderer
    public Sprite[] possibleOrders; // Inspector�dan sprite�lar� buraya ekle
    public SpriteRenderer OrderImage; // Kafas�ndaki sprite objesi

    void Start()
    {
        Debug.Log("Customer spawned!");
        Destroy(gameObject, lifetime);

        // Rastgele bir sprite se� ve m��terinin sipari� g�rseline ata
        if (possibleOrders.Length > 0 && OrderImage != null)
        {
            Sprite selected = possibleOrders[Random.Range(0, possibleOrders.Length)];
            OrderImage.sprite = selected;
        }
        else
        {
            Debug.LogWarning("OrderImage veya possibleOrders eksik!");
        }
    }

    void OnDestroy()
    {
        Debug.Log("Customer destroyed!");
    }
}
