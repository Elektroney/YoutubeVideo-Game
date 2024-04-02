using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToPosition : MonoBehaviour
{

    private Transform target;
    private Vector3 origin;
    //TODO: Implement that in customevents you get the caller object and go from there and move this function to custom functions then so you dont have to put this script 
    //      every time on an object


    bool waitForOneFrame = true;
    private void Awake()
    {
        target = null;
    }
    private void Update()
    {
        if (waitForOneFrame)
        {
            waitForOneFrame = false;
        }
        else
        {
            if(target != null)           transform.position = Vector3.Lerp(transform.position, target.position, 1 * Time.deltaTime);
        }
    }
    public void LerpToTarget(Transform target)
    {
        origin = transform.position;
        this.target = target; 
    }
    public void LerpToOrigin()
    {
        origin = transform.position;
    }
}
