using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Chunk 
{
    
    public Dictionary<Vector2,Block> blocks = new Dictionary<Vector2,Block>();
    public GameObject gameobjectReference;public
float xPOS;
public int ID;

   public  void SetChunkLoadStatus(bool load){
        gameobjectReference.SetActive(load);
    }
    public Chunk (int x ){
ID = x;
        gameobjectReference = new GameObject("Chunk" );    
        gameobjectReference.transform.parent = Terrain.terrainParent.transform;
        gameobjectReference.transform.position = new Vector2(xPOS,0);
        for (int i = 0; i < Terrain._BLOCKS_PER_CHUNK; i++)
        {
            int realX = i- (8 -1) +  Terrain._BLOCKS_PER_CHUNK  * x;  
            int maxY = Noise.GetMaxTerrainGenerationHeightForXCoordinate(realX);

            Vector2 pos = Vector2.one*10000;
            for (int y = 0; y < math.abs(maxY); y++)
            {
                Vector2 oldPos = pos;
                pos = new Vector2(realX*(16.0f/Terrain._BLOCKS_PER_CHUNK ) ,  (-Terrain._MAX_NEGATIVE_HEIGHT +y)*(16.0f/Terrain._BLOCKS_PER_CHUNK )  );
                if(pos == oldPos)
                return;
                //Place Dirt / Grass Blocks              
                if(!Noise.isCave(pos) )
{
                Block block = new Block(pos, 16.0f/  Terrain._BLOCKS_PER_CHUNK , DetermineBlock(pos,maxY,new Vector2(x,y)));  
                block.gameobjectReference.transform.parent = gameobjectReference.transform;
 
                if(i == math.round( Terrain._BLOCKS_PER_CHUNK /2))
                xPOS = block.gameobjectReference.transform.position.x;
                blocks.Add(pos *(16.0f/Terrain._BLOCKS_PER_CHUNK ) ,block );
                }
                //Further Blocks
            }     
        }
        
    }



  


    int DetermineBlock(Vector2 pos, int maxY,Vector2 chunkPos){
        int id = 0;
        if(chunkPos.y == maxY -1)
            id = 1;
        if(chunkPos.y*0.35f*9 * 0.5f +  UnityEngine.Random.Range(1, 10)  < maxY-1)
            id = 2;
     if(chunkPos.y*0.35f*9 * 0.5f +  UnityEngine.Random.Range(1, 10000)  < maxY-1)
            id = 3;
if(chunkPos.y*0.35f*9 * 0.5f    < maxY-1 && Noise.isSaphire(pos))
        id = 4;
        return id;
    }
}
