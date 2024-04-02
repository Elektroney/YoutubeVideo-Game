using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    // Start is called before the first frame updat



    [Serializable]
    private enum condition { _IS_EQUAL_TO, _IS_BIGGER_THAN, _IS_SMALLER_THAN };

    [Serializable]
    private enum triggerlogic { _ALL_TRUE, _ONE_TRUE};


    [SerializeField]
    private LogicCheck[] logicChecks;

    [SerializeField]
    private triggerlogic triggerLogic;

    [SerializeField]
    private int triggerDelaySecs;

    [Serializable]
    private class LogicCheck
    {
        [SerializeField]
        private int logicIndex;
        [SerializeField]
        private float conditionalValue;
        [SerializeField]
        private condition triggerOnCondition = condition._IS_EQUAL_TO;
        
        public bool logicCheck(float[] logicArray)
        {
            float logicOperand = logicArray[logicIndex];
            switch (triggerOnCondition)
            {
                case condition._IS_BIGGER_THAN:
                    if (logicOperand > conditionalValue)
                        return true;
                    break;
                case condition._IS_SMALLER_THAN:
                    if (logicOperand < conditionalValue)
                        return true;
                    break;
                case condition._IS_EQUAL_TO:
                    if (logicOperand == conditionalValue)
                        return true;
                    break;

            }
            return false;
        }


    }
    [SerializeField]
    private UnityEvent onTrigger;
    [SerializeField]
    private UnityEvent onExit;
    private void Start()
    {
        GameSystem.Instance.onLogicUpdate += OnTrigger;
    }
    bool lastInvokation;
    private async void OnTrigger(float[] logicArray)
    {
        //Checking if every Logical Condition is True
        bool invoke = false; 
       for(int i = 0; i < logicChecks.Length; i++)
        {
            switch (triggerLogic) {
                case triggerlogic._ALL_TRUE:
                
                if (!logicChecks[i].logicCheck(logicArray))
                return;
                    invoke = true;
                    break;
                case triggerlogic._ONE_TRUE:
                    if (logicChecks[i].logicCheck(logicArray))
                        invoke = true;
                        break;
        }
    }

       if(invoke && !lastInvokation) {
            await Task.Delay(triggerDelaySecs * 1000);
            onTrigger.Invoke();
        }
        if (!invoke && lastInvokation)
            onExit.Invoke();

        lastInvokation = invoke;
    }
   
    
}
