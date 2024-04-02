using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
public  List<Item> debugInventory = new List<Item>();
public int selectedInventorySpot = 0;
public static Player instance;

public void SelectInventorySpot(int id) {
selectedInventorySpot = id;}
void Start(){
    instance = this;
}
public void RemoveSelectedBlock(){

       if(inventory.Count <= selectedInventorySpot)
return;
 inventory[ selectedInventorySpot].count--;
 if(inventory[selectedInventorySpot].count <= 0)
    inventory.RemoveAt(selectedInventorySpot);
    GUI.InventoryUpdate();
}

public static List<Item> inventory = new List<Item>();
    void OnTriggerEnter2D(Collider2D col){

        if(col.tag != "Item" || inventory.Count >= 6)
        return;
            Item itemToAdd= col.GetComponent<Item>();

            bool didAdd = false;
            foreach (Item item in inventory)
            {
                if( item.id !=itemToAdd.id)
                continue;
                item.count = item.count + 1;
                didAdd = true;

            }

if(!didAdd)
            inventory.Add( itemToAdd);
            debugInventory = inventory;
            col.gameObject.SetActive(false);
            GUI.InventoryUpdate();        

}

public void PlaceSelectedBlock(Vector2 pos)
{
    if(inventory.Count >  selectedInventorySpot)
if (inventory[ selectedInventorySpot ].isBlock )
    Terrain.instance.GetClosestChunkToX(pos.x).blocks.Add(pos,new Block( pos,16.0f/  Terrain._BLOCKS_PER_CHUNK,inventory[ selectedInventorySpot ].id));
}
}