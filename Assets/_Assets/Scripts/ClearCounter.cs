using UnityEngine;

public class ClearCounter : BaseCounter 
{


    //this line is to add the tomato reference to the counter
    [SerializeField] private  KitchenObjectSO kitchenObjectSO;
    

    

   public override void Interact(Player player)
    {
        //we added these lines to put the object that the player is holding back in the counter
        //and to take it back
        if (!HasKitchenObject())
        {
            //there is no KitchenObject here
           if ( player.HasKitchenObject())
            {
                //player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
    
}
