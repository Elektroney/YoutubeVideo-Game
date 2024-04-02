using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockReferenceOnObject : MonoBehaviour
{

    public int id;
    public int health = 100;
    public void SpawnSelf(){
        GameObject item = Instantiate(BlockInteraction._ITEM_PREFAB, transform.position, Quaternion.identity) ;
        item.GetComponent<Item>().id =id;
        item.GetComponent<Item>().isBlock =true;
        health = BlockReference._BLOCK_IDENTITYS[id].Strength * 100 + 50;
        Destroy(gameObject);
    }

}
