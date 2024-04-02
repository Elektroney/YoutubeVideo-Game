using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    [SerializeField]
    private int logicIndex;
    private void OnCollisionEnter(Collision collision)
    {
        GameSystem.Instance.LogicUpdate(1, logicIndex,GameSystem.operation._ADD);
        Destroy(gameObject);

    }
}
