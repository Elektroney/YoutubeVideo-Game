using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    // Update is called once per frame (more performant for click detection)
     public GameObject ItemPrefab;
    public static GameObject _ITEM_PREFAB;
void Awake
(){
    _ITEM_PREFAB = ItemPrefab;
    particleSystem =  GameObject.Find("Cursor").GetComponentInChildren<ParticleSystem>();
}
ParticleSystem particleSystem;
public LayerMask ToIgnore;
   void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction,100f);
        GameObject.Find("Cursor").transform.position = Terrain.FindNearestPointInGrid( hit.point); 
        if(Vector2.Distance(GameObject.Find("Cursor").transform.position,Player.instance.gameObject.transform.position) < 3)
            GameObject.Find("Cursor").GetComponent<SpriteRenderer>().color = new Color32(255,255,100,150);
        else {
           GameObject.Find("Cursor").GetComponent<SpriteRenderer>().color = new Color32(255,255,255,50);
            return;
}
        // Check for left mouse button click (can be adjusted for touch or other input methods)
        if (Input.GetMouseButton(0))
        {
            // Perform a raycast from the main camera to the mouse position
             ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             hit = Physics2D.Raycast(ray.origin, ray.direction,100f,~ToIgnore);
            // Check if the raycast hit this object's collider (assuming a collider component is attached)
            if (hit.collider != null && hit.collider.gameObject != gameObject && hit.collider.gameObject.tag == "Block")
            {
                if(hit.collider.gameObject.GetComponent<BlockReferenceOnObject>().health < 0)
{
                    hit.collider.gameObject.GetComponent<BlockReferenceOnObject>().SpawnSelf();
                  
                  if(particleSystem.isPlaying ){ particleSystem.loop = false;
}       particleSystem.Stop();
}
                else{
                    hit.collider.gameObject.GetComponent<BlockReferenceOnObject>().health =   hit.collider.gameObject.GetComponent<BlockReferenceOnObject>().health -1;
                    if(particleSystem.isPlaying == false){
                    particleSystem.loop = true;
                    particleSystem.Play();

}
                }

            }else
            particleSystem.Stop();
        }
        if(Input.GetMouseButtonUp(0)){
          
                   particleSystem.Stop();

        }
            if (Input.GetMouseButton(1))
        {

            // Perform a raycast from the main camera to the mouse position

            
            // Check if the raycast hit this object's collider (assuming a collider component is attached)
            if (hit.collider != null && hit.collider.tag == "BG")
            {
                // Destroy the clicked object

                    Player.instance.PlaceSelectedBlock(Terrain.FindNearestPointInGrid( hit.point));
                    Player.instance.RemoveSelectedBlock();
            }
        }
    }

    
}