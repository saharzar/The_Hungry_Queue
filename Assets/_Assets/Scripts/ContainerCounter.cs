using UnityEngine;
using System;
public class ContainerCounter : BaseCounter 
{


    //so we can know when it should start animation 
    public event EventHandler OnPlayerGrabbedObject;



    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact(Player player)
    {
      
             if (!player.HasKitchenObject())
        {
            //player is not carrying anything
            //spawn an object an gives it to the player
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);


            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);

        }
            





    }
    
}
