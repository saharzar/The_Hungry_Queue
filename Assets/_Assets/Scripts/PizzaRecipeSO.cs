using UnityEngine;

[CreateAssetMenu()]
public class PizzaRecipeSO : ScriptableObject
{
    public KitchenObjectSO[] inputList; // Ingredients needed
    public KitchenObjectSO output;      // The final pizza object
}

