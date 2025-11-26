using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            //There is no KitchenObject here
            if(player.HasKitchenObject())
            {
                //Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player not carrying anything
            }
        }
        else
        {
           //There is KitchenObject here 
           if(player.HasKitchenObject())
            {
                //Player is carrying something
                if(player.GetKitchenObject() is PlateKitchenObject)
                {
                    //Player is holding a Plate
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if(plateKitchenObject.TryAddIgredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                    
                }
            }
            else
            {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    
}
