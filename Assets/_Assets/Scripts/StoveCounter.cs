using UnityEngine;
using System;
public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;


    private float fryingTimer;
   // private FryingRecipeSo fryingRecipeSO;
    
    private void Update()
    {
        if(HasKitchenObject())
        {
            fryingTimer += Time.deltaTime;
            FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            if (fryingTimer > fryingRecipeSO.fryingTimerMax)
            {
                //fried
                fryingTimer = 0f;
                Debug.Log("Fried!");
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
            }
            Debug.Log(fryingTimer);
        }
    }
    public override void Interact(Player player)
    {
    
        //we added these lines to put the object that the player is holding back in the counter
        //and to take it back
        if (!HasKitchenObject())
        {
            //there is no KitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //Player carrying something that can be Baked in  the oven
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                   
                }
            }
            else
            {
                //the player not carrying anything 
            }
        }
        else
        {
            //there is a KitchenObject here
            if (player.HasKitchenObject())
            {
                //player is carrying something
            }
            else
            {
                //player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)

    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        return fryingRecipeSO != null;

    }



    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO) { return fryingRecipeSO; }
        }
        return null;
    }


}
