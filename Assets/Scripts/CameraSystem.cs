using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<CameraTargets> cameraTargets = new List<CameraTargets>();
    [SerializeField]
    private GameObject cam;
    void Awake()
    {


        Instance = gameObject.GetComponent<CameraSystem>();
        DontDestroyOnLoad(gameObject);
    }
    private static CameraSystem instance = null;

public float speed;
    public static CameraSystem Instance;
    
    public event Action<bool> onCutscene;

    // Update is called once per frame
    void Update()
    {
        if (cameraTargets == null)
            return;
            if (cameraTargets.Count != 0) 
        { 
            cam.transform.position = Vector3.Lerp(   cam.transform.position,cameraTargets[cameraTargets.Count-1].target.position,speed*Time.deltaTime);
            cam.transform.rotation = cameraTargets[cameraTargets.Count-1].target.rotation;
        }
    }
    public void AppendCameraEvent(CameraTargets camEvent)
    {

        cameraTargets.Add(camEvent);
        print(camEvent);
        if (camEvent.secondsUntilRemove == 0)
            return;
        onCutscene(true);
        StartCoroutine(WaitOutCameraEvent(camEvent));
    }
    private IEnumerator WaitOutCameraEvent(CameraTargets camEvent)
    {
        yield return new WaitForSeconds(camEvent.secondsUntilRemove);

        cameraTargets.Remove(camEvent);
        onCutscene(false);
    }
    
    [Serializable]
    public class CameraTargets
    {
        public CameraTargets(Transform target,float secondsUntilRemove=0)
        {
            this.target = target;   
            this.secondsUntilRemove = secondsUntilRemove;
            }

        public bool instant;
        public float speed;
        public float secondsUntilRemove;
        public Transform target;
    }
}
