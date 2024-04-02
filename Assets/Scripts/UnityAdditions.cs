using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnityAdditions
{
    [Serializable]
public class CurveAnimator
    {
        [SerializeField]
        private AnimationCurve curve;
        private float time = -5;
        private float lastTime;
        [System.NonSerialized]
        public float oldEval;
        [System.NonSerialized]
        public float eval;
        
        public void ResetTime() { time = Time.time; }
        
        public float Eval() {
            if (oldEval != eval)
                oldEval = eval;
            eval = curve.Evaluate( Time.time - time );
            return eval;  
        }
        public void Destroy() { Debug.LogError("Destroy Function Not Implemented For Curve Animator!"); }
    }
}
