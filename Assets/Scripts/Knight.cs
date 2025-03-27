using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityHFSM;

[RequireComponent(typeof(Animator))]
public class Knight : MonoBehaviour
{
    public HealthView healthView; 
    private HealthModel healthModel;
    private HealthPresenter healthPresenter;

    private StateMachine KnightFSM;
    public float moveSpeed = 3f; // Tốc độ di chuyển
    private Animator animator; // Animator của nhân vật
    private Rigidbody rb; // Rigidbody của nhân vật


    public bool isHit =false;
    public bool isDead =false;


    // Input Actions
    private HeroInputControl controls;  // PlayerControls là lớp được tạo tự động từ Input Actions Asset

    public Animator Animator => animator;

    public Rigidbody Rigidbody => rb;

    public HeroInputControl HeroInputControl => controls;

    // Biến để điều khiển hướng di chuyển

    void Awake()
    {
        controls = new HeroInputControl(); // Khởi tạo PlayerControls từ Input Action Asset
    }

    void OnEnable()
    {
        controls.Enable();  // Kích hoạt Input System
    }

    void OnDisable()
    {
        controls.Disable();  // Vô hiệu hóa Input System khi không dùng
    }

    void Start()
    {
        animator = GetComponent<Animator>(); // Lấy Animator component
        rb = GetComponent<Rigidbody>(); // Lấy Rigidbody component
        SetUpStateMachine();
        healthModel = new HealthModel(150f); 
        healthPresenter = new HealthPresenter(healthModel, healthView);
        healthPresenter.UpdateHealthBar();  // Cập nhật thanh máu ban đầu
    }

    void Update()
    {
        KnightFSM.OnLogic();
    }

    public void TakeDamage(float damage)
    {
        healthPresenter.TakeDamage(damage);  // Khi Reaper nhận sát thương
        if (healthModel.GetCurrentHealth() <= 0){
            isDead = true;
        }
 
    }

    public void Heal(float amount)
    {
        healthPresenter.Heal(amount);  // Khi Reaper hồi máu
    }


    void SetUpStateMachine(){
        KnightFSM = new StateMachine();

        KnightFSM.AddState("Idle",new KnightIdleState(this));
        KnightFSM.AddState("Walking",new KnightWalkingState(this));
        KnightFSM.AddState("Attack",new KnightAttackState(this));
        KnightFSM.AddState("GetHit",new KnightGetHitState(this));
        KnightFSM.AddState("Dash",new KnightIdleState(this));
        KnightFSM.AddState("Death",new KnightDeathState(this));

        KnightFSM.AddTransition("Idle", "Walking", t => controls.Player.Move.ReadValue<Vector2>().magnitude >= 0.1);
        KnightFSM.AddTransition("Walking", "Idle", t => controls.Player.Move.ReadValue<Vector2>().magnitude < 0.1);

        KnightFSM.AddTransition("Idle","Attack", t => controls.Player.Attack.triggered);
        KnightFSM.AddTransition("Walking","Attack", t => controls.Player.Attack.triggered);

        KnightFSM.AddTransition("Attack","Idle");
        KnightFSM.AddTransition("Attack","Walking");
        KnightFSM.AddTransition("GetHit","Idle");

        KnightFSM.AddTransitionFromAny("GetHit",t => isHit);
        KnightFSM.AddTransitionFromAny("Death",t => isDead);

        


        KnightFSM.SetStartState("Idle");
        KnightFSM.Init();

    }
}
