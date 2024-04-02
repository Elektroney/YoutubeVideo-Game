using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public bool isBlock;
    public int count  = 0;
public Sprite    GetSprite(){
    return isBlock? BlockReference._BLOCK_IDENTITYS[id].sprite: ItemReferences._ITEM_IDENTITYS[id].sprite;
}
void Start(){
if(count == 0)
    count =1;
    GetComponentInChildren<SpriteRenderer>().sprite = isBlock? BlockReference._BLOCK_IDENTITYS[id].sprite: ItemReferences._ITEM_IDENTITYS[id].sprite;
}
}
 