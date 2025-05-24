using UnityEngine;
using UnityEngine.UI; // Sprite kullanýmý için

public class Customer : MonoBehaviour
{
    public float lifetime = 80f;

    // Müþteri isteðini gösterecek sprite renderer
    public Sprite[] possibleOrders; // Inspector’dan sprite’larý buraya ekle
    public SpriteRenderer OrderImage; // Kafasýndaki sprite objesi

    void Start()
    {
        Debug.Log("Customer spawned!");
        Destroy(gameObject, lifetime);

        // Rastgele bir sprite seç ve müþterinin sipariþ görseline ata
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
