using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;

    private float[] logicArray;
    [SerializeField]
    private int logicArraySize;
    void Awake()
    {
        logicArray = new float[logicArraySize];
        Instance = gameObject.GetComponent<GameSystem>();

    }

    void Start()
    {
        StartCoroutine(LoadSave());
    }

    IEnumerator LoadSave()
    {
        yield return new WaitForSeconds(0.1f);

        //Apply Loaded Save to every Listener

        LogicUpdate(0, 0, operation._ADD);
    }
 
    public event Action<float[]> onLogicUpdate;
    public enum operation {_SET,_ADD, _SUBTRACT,_DIVIDE,_MULTIPLY};
    public void LogicUpdate(float value, int logicIndex,operation logicOperation)
    {

        switch (logicOperation) {
            case operation._SET:
                logicArray[logicIndex] = value; 
                break;
            case operation._ADD:
            logicArray[logicIndex] += value;
                break;
    case operation._SUBTRACT:
                logicArray[logicIndex] -= value;
                break;
                case operation._DIVIDE:
                logicArray[logicIndex] /= value;
                break;
                case operation._MULTIPLY:
                logicArray[logicIndex] *= value;
                    break;

        }
        
        if (onLogicUpdate != null)
            onLogicUpdate(logicArray);
        
    }

}
