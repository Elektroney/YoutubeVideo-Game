using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public GameObject gameobjectReference;
    public Block (Vector2 pos, float size,int id ){
        gameobjectReference = Terrain.Instantiate(Terrain._BLOCK_PREFAB, pos, Quaternion.identity);
        gameobjectReference.GetComponentInChildren<SpriteRenderer>().sprite = BlockReference._BLOCK_IDENTITYS[id].sprite;
        gameobjectReference.transform.localScale = Vector2.one*size;
        gameobjectReference.AddComponent<BlockReferenceOnObject>().id = id;
        gameobjectReference.transform.parent = Terrain.terrainParent.transform ;
    }
}
