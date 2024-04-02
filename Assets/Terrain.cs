using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class Terrain : MonoBehaviour
{
    [SerializeField]
    int maxNegativeHeight = 10;
    [SerializeField]
    int seed;
    [SerializeField]
    int blocksPerChunk = 32;
    [SerializeField]

    int terrainSpread = 1;
[Range(0.0f, 1.0f)]
    [SerializeField]
    float terrainRoughness = 1;


    [SerializeField]
    GameObject blockPrefab;

    public static int _TERRAIN_SPREAD;
    public static int _MAX_NEGATIVE_HEIGHT;
        public static float _TERRAIN_ROUGHNESS;
        public static int _BLOCKS_PER_CHUNK;
    public static GameObject _BLOCK_PREFAB;
    public static int _SEED;
public static  GameObject terrainParent;
    public int nextChunkToGenerateLeftwards;
    public int nextChunkToGenerateRightwards;
     
    // Chunk , x
    Dictionary< int,Chunk> chunks = new Dictionary< int,Chunk>();
    // Start is called before the first frame update

    public static Terrain instance;
    void Start()
    {
        seed = UnityEngine.Random.Range(0,10000);
        Destroy(FindObjectsOfType<Terrain>().Length >= 2? gameObject:null);
    DontDestroyOnLoad(gameObject);
        instance = this;
        Init();
    }


    public void Init(){
        chunks = new Dictionary< int,Chunk>();
        terrainParent =new GameObject("terrain");
        _BLOCK_PREFAB = blockPrefab;
        _SEED = seed;
        _MAX_NEGATIVE_HEIGHT = maxNegativeHeight;
        _BLOCKS_PER_CHUNK = blocksPerChunk;
        _TERRAIN_SPREAD = terrainSpread;
        _TERRAIN_ROUGHNESS =terrainRoughness;
        for (int i = 0; i < 8; i++)
        {
                    chunks.Add ( i-4,new Chunk(i-4));  
        }
        nextChunkToGenerateRightwards = 4;
        nextChunkToGenerateLeftwards = -5;

    }
    // Update is called once per frame
public static Vector2 FindNearestPointInGrid(Vector2 targetPosition) {
    float cellSize = 0.4f;

    float remainderX = Mathf.Abs(targetPosition.x % cellSize);
    float differenceX = cellSize - remainderX;

    float remainderY = Mathf.Abs(targetPosition.y % cellSize);
    float differenceY = cellSize - remainderY;

    Vector2 gridPosition = Vector2.zero;

    if (remainderX > 0.2f) {
        if (targetPosition.x >= 0)
            gridPosition.x = targetPosition.x + differenceX;
        else
            gridPosition.x = targetPosition.x - differenceX;
    } else {
        if (targetPosition.x >= 0)
            gridPosition.x = targetPosition.x - remainderX;
        else
            gridPosition.x = targetPosition.x + remainderX;
    }

    if (remainderY > 0.2f) {
        if (targetPosition.y >= 0)
            gridPosition.y = targetPosition.y + differenceY;
        else
            gridPosition.y = targetPosition.y - differenceY;
    } else {
        if (targetPosition.y >= 0)
            gridPosition.y = targetPosition.y - remainderY;
        else
            gridPosition.y = targetPosition.y + remainderY;
    }

    return gridPosition;
}


    public Chunk GetClosestChunkToX(float x){
      Chunk closest  = chunks[0];
      float closestDistance = Mathf.Infinity;
 foreach (Chunk chunk in chunks.Values)
        {
           float dist = Vector2.Distance( new Vector2( chunk.xPOS,0),new Vector2(x,0)); 
            if(dist > closestDistance)
continue;
closestDistance = dist;
closest = chunk;

      }

         return  closest;
    }
    void Update()
    {
        if( GetClosestChunkToX( Player.instance.gameObject.transform.position.x).ID >= nextChunkToGenerateRightwards-1)
            {
                chunks.Add(nextChunkToGenerateRightwards,new Chunk(nextChunkToGenerateRightwards));
                nextChunkToGenerateRightwards= nextChunkToGenerateRightwards +1;
            }
                    if( GetClosestChunkToX( Player.instance.gameObject.transform.position.x).ID <= nextChunkToGenerateLeftwards+1)
            {
                chunks.Add(nextChunkToGenerateLeftwards,new Chunk(nextChunkToGenerateLeftwards));
                nextChunkToGenerateLeftwards= nextChunkToGenerateLeftwards -1;
            }
        foreach (Chunk chunk in chunks.Values)
        {
            
        if(Vector2.Distance(new Vector2(Player.instance.transform.position.x,0), new Vector2(chunk.xPOS,0)) > _BLOCKS_PER_CHUNK * 2 -7)
        chunk.SetChunkLoadStatus(false);
        else
        chunk.SetChunkLoadStatus(true);

        }
    }

    //8 Blocks distance between chunks so x*7*2 == real x 
  

    

}
