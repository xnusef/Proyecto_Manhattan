using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]public PlayerState stateScript;
    [HideInInspector]public PlayerMovement movementScript;
    [HideInInspector]public PlayerAttack attackScript;
    [HideInInspector]public Animator playerAnimator;
    private HealthBar healthBar;

    #region Getters & Setters
    public void SetAnimationBool(string name, bool value)
    {
        playerAnimator.SetBool(name, value);
    }
    #endregion

    void Awake()
    {
        this.stateScript = this.GetComponent<PlayerState>();
        this.movementScript = this.GetComponent<PlayerMovement>();
        this.attackScript = this.GetComponent<PlayerAttack>();
        this.playerAnimator = this.GetComponent<Animator>();
        this.healthBar = GameObject.Find("/UI/Canvas/HealthBar").GetComponent<HealthBar>();
    }
    void Start()
    {
        this.healthBar.SetMaxHealth(GameManager.gM.maxPlayerHealth);
    }
    public void Damaged(int damage)
    {
        AudioManager.aM.Play("Hurt");
        this.playerAnimator.SetTrigger("Hurt");
        GameManager.gM.currentPlayerHealth = GameManager.gM.currentPlayerHealth - damage;
        this.healthBar.SetHealth(GameManager.gM.currentPlayerHealth);
        if (GameManager.gM.currentPlayerHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        this.stateScript.SetState("IsDead", true);
    }
    public void EndDie()
    {
        GameManager.gM.SetMaxHealth();
        LevelManager.lM.RestartLevel();
    }
}
