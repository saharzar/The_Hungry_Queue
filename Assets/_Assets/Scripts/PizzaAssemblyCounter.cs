using System;
using System.Collections.Generic;
using UnityEngine;

public class PizzaAssemblyCounter : BaseCounter
{
    [SerializeField] private PizzaRecipeSO[] pizzaRecipeSOArray;

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public event EventHandler OnCook;

    [SerializeField] private int cookProgressMax = 3;
    private int currentCookProgress;

    private List<KitchenObject> placedObjects = new List<KitchenObject>();
    private KitchenObject assembledResult;

    public override void Interact(Player player)
    {
        // Pick up final result
        if (assembledResult != null)
        {
            if (!player.HasKitchenObject())
            {
                assembledResult.SetKitchenObjectParent(player);
                assembledResult = null;
            }
            return;
        }

        // Add new ingredient
        if (!player.HasKitchenObject()) return;

        KitchenObject playerObject = player.GetKitchenObject();
        KitchenObjectSO heldSO = playerObject.GetKitchenObjectSO();

        if (IsIngredientValid(heldSO) && !ContainsSO(heldSO))
        {
            playerObject.SetKitchenObjectParent(this);
            placedObjects.Add(playerObject);

            currentCookProgress = 0;
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = 0f
            });

            // Sort: dough always on bottom
            placedObjects.Sort((a, b) =>
            {
                string aName = a.GetKitchenObjectSO().name.ToLower();
                string bName = b.GetKitchenObjectSO().name.ToLower();

                if (aName.Contains("dough") && !bName.Contains("dough")) return -1;
                if (!aName.Contains("dough") && bName.Contains("dough")) return 1;
                return 0;
            });

            // Stack ingredients visually
            for (int i = 0; i < placedObjects.Count; i++)
            {
                KitchenObject obj = placedObjects[i];
                string name = obj.GetKitchenObjectSO().name.ToLower();

                float height = 0f;
                if (name.Contains("dough")) height = 0.01f;
                else if (name.Contains("tomato")) height = 0.06f;
                else if (name.Contains("cheese")) height = 0.07f;
                else height = 0.05f + 0.01f * i;

                obj.transform.localPosition = new Vector3(0f, height, 0f);
            }
        }
        else
        {
            Debug.Log("Invalid or duplicate ingredient.");
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (assembledResult != null || placedObjects.Count == 0) return;

        foreach (PizzaRecipeSO recipe in pizzaRecipeSOArray)
        {
            if (RecipeMatches(recipe))
            {
                currentCookProgress++;

                OnCook?.Invoke(this, EventArgs.Empty);

                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    progressNormalized = (float)currentCookProgress / cookProgressMax
                });

                if (currentCookProgress >= cookProgressMax)
                {
                    foreach (KitchenObject obj in placedObjects)
                    {
                        obj.DestroySelf();
                    }

                    assembledResult = KitchenObject.SpawnKitchenObject(recipe.output, this);
                    placedObjects.Clear();
                    currentCookProgress = 0;

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });

                    Debug.Log("Pizza Assembled!");
                }

                return;
            }
        }

        Debug.Log("No matching recipe.");
    }

    private bool RecipeMatches(PizzaRecipeSO recipe)
    {
        if (recipe.inputList.Length != placedObjects.Count) return false;

        foreach (KitchenObjectSO required in recipe.inputList)
        {
            if (!ContainsSO(required)) return false;
        }

        return true;
    }

    private bool ContainsSO(KitchenObjectSO checkSO)
    {
        foreach (KitchenObject obj in placedObjects)
        {
            if (obj.GetKitchenObjectSO() == checkSO)
                return true;
        }
        return false;
    }

    private bool IsIngredientValid(KitchenObjectSO input)
    {
        foreach (PizzaRecipeSO recipe in pizzaRecipeSOArray)
        {
            foreach (var item in recipe.inputList)
            {
                if (item == input) return true;
            }
        }
        return false;
    }
}
