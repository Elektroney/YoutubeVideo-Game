using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockReference : MonoBehaviour
{
    [SerializeField]
    List<BlockIdentity> blockIdentitys = new List<BlockIdentity>();
    public static List<BlockIdentity> _BLOCK_IDENTITYS = new List<BlockIdentity>();
    void Awake(){
        _BLOCK_IDENTITYS = blockIdentitys;
    }
}
