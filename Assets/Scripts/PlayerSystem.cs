using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    // Start is called before the first frame update
    private static PlayerSystem instance = null;

    public static PlayerSystem Instance;
    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("PlayerSystem").Length > 1) { Destroy(gameObject); return; }


        Instance = gameObject.GetComponent<PlayerSystem>();
        DontDestroyOnLoad(gameObject);
        print("Initiated Player System!");
    }
    private void Start()
    {
        CameraSystem.Instance.onCutscene += ListenForCutscene;
    }


    private void ListenForCutscene(bool isInCutscene)
    {
       // Make Character Freeze his velocity
    }

    private bool playable;
    [SerializeField]
    private float sensitivity = 1.0f;
    private void Update()
    {
        MovementPacket packet = new MovementPacket(Vector2.zero,false);


        
        //Make it so the whole game pauses During

            Rotation(sensitivity);
        
        if (Input.GetKeyDown(KeyCode.Space))
        JumpUp();

        if (Input.GetKey(KeyCode.Space))
            JumpStay();
        if (Input.GetKeyUp(KeyCode.Space))
            JumpDown();

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        
        bool movementModifier = Input.GetKey(KeyCode.LeftShift);
         packet =  new MovementPacket(new Vector2(h, v), movementModifier);
        
        Movement(packet);


    }

    public class MovementPacket
    {
       public MovementPacket(Vector2 dir, bool movementModifier) {
            this.dir = dir;
            this.movementModifier = movementModifier;
        }
        public Vector2 dir;
        public bool movementModifier;
    }

    public event Action<MovementPacket> onMovement;

    public MovementPacket oldPacket;

    public void Movement(MovementPacket packet)
    {
        if (onMovement != null && oldPacket != null)
            onMovement(packet);
        oldPacket = packet;


    }
    public event Action<float> onRotation;
    public void Rotation(float sensitivity)
    {
        if (onRotation != null )
            onRotation(sensitivity);
    }

    public event Action onJumpEnter;

    public void JumpDown()
    {
        if (onJumpEnter != null)
            onJumpEnter();

    }
    public event Action onJumpDown;

    public void JumpUp()
    {
        if (onJumpDown != null)
            onJumpDown();

    }
    public event Action onJumpStay;

    public void JumpStay()
    {
        if (onJumpStay != null)
            onJumpStay();

    }
    public event Action onDamage;

    public void Damage(int amount)
    {
        if (onDamage != null)
            onDamage();
    }
    public event Action onHeal;

    public void Heal(int amount )
    {
        if (onHeal != null)
            onHeal();
    }
    public event Action onInteract1;
    public event Action onInteract1Up;
    public event Action onInteract1Down;
    public void Interact1()
    {
        if (onInteract1 != null)
            onInteract1();
    }

    public void Interact1Up()
    {
        if (onInteract1Up != null)
            onInteract1Up();
    }

    public void Interact1Down()
    {
        if (onInteract1Down != null)
            onInteract1Down();
    }

    public event Action onInteract2;
    public event Action onInteract2Up;
    public event Action onInteract2Down;
    public void Interact2()
    {
        if (onInteract2 != null)
            onInteract2();
    }

    public void Interact2Up()
    {
        if (onInteract2Up != null)
            onInteract2Up();
    }

    public void Interact2Down()
    {
        if (onInteract2Down != null)
            onInteract2Down();
    }

    public event Action onInteract3;
    public event Action onInteract3Up;
    public event Action onInteract3Down;
    public void Interact3()
    {
        if (onInteract3 != null)
            onInteract3();
    }

    public void Interact3Up()
    {
        if (onInteract3Up != null)
            onInteract3Up();
    }

    public void Interact3Down()
    {
        if (onInteract3Down != null)
            onInteract3Down();
    }

    // Update is called once per frame

}
