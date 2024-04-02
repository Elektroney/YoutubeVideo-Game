using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReferences : MonoBehaviour
{
    [SerializeField]
    List<ItemIdentity> itemIdentitys = new List<ItemIdentity>();
    public static List<ItemIdentity> _ITEM_IDENTITYS = new List<ItemIdentity>();
    void Awake(){
        _ITEM_IDENTITYS = itemIdentitys;
    }
}
