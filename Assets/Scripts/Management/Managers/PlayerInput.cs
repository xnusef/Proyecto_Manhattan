using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector]public static PlayerInput iM;
    private bool ignoring = false;
    private float horizontalMove = 0f;

    void Awake()
    {
        if (iM != null)
            Destroy(this.gameObject);
        else
            iM = this;
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.isPaused && GameManager.gM.pM.playerObject != null && !GameManager.gM.pM.playerScript.stateScript.GetState("Drinking"))
            GameManager.gM.pM.playerScript.movementScript.SetMoveDir((int)this.horizontalMove);
    }
    void OnMovement(InputValue value)
    {
        this.horizontalMove = ((Vector2)value.Get()).x;
    }
    void OnDash()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && GameManager.gM.pM.playerScript.movementScript.GetDashCooldown() && !GameManager.gM.pauseScript.isPaused && GameManager.gM.pM.playerObject != null && GameManager.gM.GetAbilities("Dash") && !GameManager.gM.pM.playerScript.stateScript.GetState("Drinking"))
            GameManager.gM.pM.playerScript.movementScript.Dash();
    }
    void OnJump()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.isPaused && GameManager.gM.pM.playerObject != null && !GameManager.gM.pM.playerScript.stateScript.GetState("Drinking"))
            GameManager.gM.pM.playerScript.movementScript.Jump();
    }
    void OnAttack()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pM.playerScript.stateScript.GetState("Attacking") && GameManager.gM.pM.playerScript.attackScript.GetAttackCooldown() && !GameManager.gM.pauseScript.isPaused && GameManager.gM.pM.playerObject != null && !GameManager.gM.pM.playerScript.stateScript.GetState("Drinking"))
            GameManager.gM.pM.playerScript.stateScript.SetState("Attacking", true);
    }
    void OnSpecialAttack()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.isPaused && GameManager.gM.pM.playerObject != null)
        {
            Player playerScript = GameManager.gM.pM.playerScript;
            switch(GameManager.gM.pauseScript.abilityNum)
            {
                case 1: 
                    if (GameManager.gM.GetAbilities("Vodka") && !playerScript.specialScript.drunk && !playerScript.specialScript.hangover && playerScript.specialScript.GetDrinkingCooldown() && !playerScript.stateScript.GetState("Drinking") && !playerScript.stateScript.GetState("Attacking") && !playerScript.stateScript.GetState("Jumping"))
                        playerScript.stateScript.SetState("Drinking", true);
                    break;
                case 3:
                    if (GameManager.gM.GetAbilities("Arquebus") && playerScript.specialScript.GetShootingCooldown() && !playerScript.stateScript.GetState("Shooting") && !playerScript.stateScript.GetState("Attacking"))
                        playerScript.stateScript.SetState("Shooting", true);
                    break;
            }
        }
    }
    void OnPause()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && GameManager.gM.pauseScript.isPaused && GameManager.gM.pM.playerObject != null)
            GameManager.gM.pauseScript.Resume();
        else if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.isPaused)
            GameManager.gM.pauseScript.Pause();
    }
    void OnDown()
    {
        ignoring = !ignoring;
        if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.isPaused && GameManager.gM.pM.playerObject != null && !GameManager.gM.pM.playerScript.stateScript.GetState("Drinking"))
            GameManager.gM.pM.playerScript.movementScript.pressedDown = ignoring;
    }
}