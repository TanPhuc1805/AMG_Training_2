using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;

public class Reaper : MonoBehaviour
{

    public HealthView healthView;  // Tham chiếu đến HealthView của Reaper
    private HealthModel healthModel;
    private HealthPresenter healthPresenter;

    private StateMachine ReaperFSM;
    public Transform knight; // Biến để tham chiếu đến knight
    public float moveSpeed = 2f; // Tốc độ di chuyển của Reaper
    private Animator animator;
    private Rigidbody rb; 

    public GameObject scythePrefab;  // Prefab của lưỡi hái

    private float currentHealth;
    public float maxHealth = 50f;

    public Animator Animator => animator;
    public Rigidbody Rigidbody => rb; 

    public bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>(); // Lấy Animator component
        rb = GetComponent<Rigidbody>(); // Lấy Rigidbody component
        currentHealth = maxHealth;
        // Kiểm tra xem knight đã được gán chưa
        if (knight == null)
        {
            knight = GameObject.FindGameObjectWithTag("Player").transform; // Tìm knight trong scene nếu chưa gán
        }
        SetUpStateMachine();

        healthModel = new HealthModel(150f); 
        healthPresenter = new HealthPresenter(healthModel, healthView);
        healthPresenter.UpdateHealthBar();  // Cập nhật thanh máu ban đầu
        
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



    void Update()
    {
        ReaperFSM.OnLogic();
    }

    void SetUpStateMachine(){

        ReaperFSM = new StateMachine();

        ReaperFSM.AddState("Spawn",new ReaperSpawnState(this));
        ReaperFSM.AddState("Walking",new ReaperWalkingState(this));
        ReaperFSM.AddState("Attack1",new ReaperAttack1State(this));
        ReaperFSM.AddState("Attack2",new ReaperAttack2State(this));
        ReaperFSM.AddState("Death",new ReaperDeathState(this));


        ReaperFSM.AddTransitionFromAny("Walking");
        ReaperFSM.AddTransitionFromAny("Death", t => isDead);
        ReaperFSM.AddTransition("Death", "Death", t => isDead);

        ReaperFSM.AddTransition("Walking","Attack1", t => Vector3.Distance(transform.position, knight.position) <= 3f);
        ReaperFSM.AddTransition("Walking","Attack2", t => Vector3.Distance(transform.position, knight.position) >= 8f);




        ReaperFSM.SetStartState("Spawn");
        ReaperFSM.Init();

    }
}
