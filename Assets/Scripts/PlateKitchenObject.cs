using UnityEngine;
using System.Collections.Generic;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIgredient(KitchenObjectSO kitchenObjectSO)
    {
        
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Not a valid ingredient
            return false;
        }
        
        if(kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Already has this type
            return false;
        }
        
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
        
    }
}
