using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour

{

    void Start(){
    
        InventoryUpdate();
    }
   public static  void InventoryUpdate(){
        for (int i = 0; i <  6  ; i++)
        {
                   GameObject.Find("Item" + (i +1).ToString()).GetComponent<Image>().sprite = null;
            GameObject.Find("Item" + (i +1).ToString()).GetComponentInChildren<TextMeshProUGUI>().text = "";
try
{
             if(Player.inventory[i] == null){
            continue;
            }
            GameObject.Find("Item" + (i +1).ToString()).GetComponent<Image>().sprite = Player.inventory[i].GetSprite();
            GameObject.Find("Item" + (i +1).ToString()).GetComponentInChildren<TextMeshProUGUI>().text = Player.inventory[i].count.ToString();   
}
catch (System.Exception)
{
    
}

        }
    }
}
