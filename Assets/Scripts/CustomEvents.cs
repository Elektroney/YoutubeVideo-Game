using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CustomEvents  : MonoBehaviour 
{
  public void MoveCam(Transform target,float secondsUntilSwitchBack)
    {
        CameraSystem.Instance.AppendCameraEvent(new CameraSystem.CameraTargets( target,secondsUntilSwitchBack));
    }
    public void CamShowHighlight(Transform target)
    {
        MoveCam(target, 3);
        
    }
}
