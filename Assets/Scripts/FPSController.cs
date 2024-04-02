using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityAdditions;

public class FPSController : PlayerController
{
    private Vector2 movementAxese;
    private bool movementModifier;
    private Rigidbody rb;
    [SerializeField]
    private GameObject cameraProp;



    [SerializeField]
    private CurveAnimator gravity;
    

    [SerializeField]
    private CurveAnimator dirVelocityCurve;
    [SerializeField]
    private float speedModifier;

    // Start is called before the first frame update
    private Vector3 oldVel;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;


        rb = GetComponent<Rigidbody>();


        CameraSystem.Instance.AppendCameraEvent(new CameraSystem.CameraTargets(cameraProp.transform));

        PlayerSystem.Instance.onMovement += Movement;
        PlayerSystem.Instance.onRotation += Rotation;
        PlayerSystem.Instance.onJumpDown += Jump;
    }
    private bool isAirborn;
    private bool airbornOneFrameCheck;
    private PlayerSystem.MovementPacket oldMovementPacket;
    private PlayerSystem.MovementPacket oldOldMovementPacket;
    private void Movement(PlayerSystem.MovementPacket movement)
    {
        gravity.Eval();


        
        airbornOneFrameCheck = isAirborn;
        Vector2 m = movement.dir * (Convert.ToInt32(movement.movementModifier) + 1);
        Vector2 oldM = PlayerSystem.Instance.oldPacket.dir * (Convert.ToInt32(PlayerSystem.Instance.oldPacket.movementModifier) + 1);

        float lostVelocity = gravity.eval - rb.velocity.y;
        
        Vector3 vel = Vector3.zero;


        //Cleanup old Velocities for not adding a velocity every frame so we can use curves
       
        
  
            vel.x = oldVel.x - oldM.x;

            vel.y = rb.velocity.y - gravity.oldEval;

            vel.z = oldVel.z - oldM.y;


            //Applying New Velocities
            vel.x += m.x;

            vel.y += gravity.eval + lostVelocity;

            vel.z += m.y;
        
        oldVel = vel;

        Vector3 cameraForward = cameraProp.transform.forward;
        Vector3 cameraRight = cameraProp.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;
        Vector3 rotDir = cameraForward * vel.z + cameraRight * vel.x;
        rotDir = rotDir * speedModifier;
        
        rotDir.y = vel.y;
        rb.velocity = rotDir;
        

    }

    private float smoothing = 2.0f;

    private Vector2 smoothV;

    private void Rotation(float sensitivity)
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        Vector2 mouseDelta = new Vector2(mouseX, mouseY);
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        smoothV.x = Mathf.Lerp(smoothV.x, mouseDelta.x, 1.0f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseDelta.y, 1.0f / smoothing);

        Vector3 eulerAngles = transform.localEulerAngles;
        eulerAngles.y += smoothV.x;
        eulerAngles.x -= smoothV.y;

        cameraProp.transform.localEulerAngles += eulerAngles;
    }

    private void Jump()
    {
        gravity.ResetTime();
    }


    // MAKE THIS BETTER

    private void OnCollisionStay(Collision collision)
    {
        isAirborn = false;
        
    }
    private void OnCollisionExit(Collision collision)
    {
        isAirborn = true;
    }
    private void OnDestroy()
    {
        PlayerSystem.Instance.onMovement -= Movement;
        PlayerSystem.Instance.onRotation -= Rotation;
        PlayerSystem.Instance.onJumpDown -= Jump;
    }
}
