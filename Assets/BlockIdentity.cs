using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Block", menuName = "Block", order = 1)]

public class BlockIdentity : ScriptableObject
{
    public Sprite sprite;
    public int Strength;
    public int item_ID;
    
}
