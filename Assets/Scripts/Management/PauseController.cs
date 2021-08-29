using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseObject;
    [SerializeField] private Image abilityImage;
    [SerializeField] private Sprite noneSprite;
    [SerializeField] private Sprite vodkaSprite;
    [SerializeField] private Sprite saberSprite;
    [SerializeField] private Sprite arquebusSprite;
    private bool isPaused = false;
    private int abilityNum = 0;
    private string selectedItem = "none";

    #region Getters & Setters
    public bool GetPause()
    {
        return this.isPaused;
    }
    #endregion

    void Start()
    {
        GameManager.gM.SetPauseScript();
        this.pauseObject.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        this.isPaused = true;
        this.pauseObject.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        this.isPaused = false;
        GameManager.gM.playerScript.movementScript.SetMoveDir(0);
        this.pauseObject.SetActive(false);
    }
    public void Menu()
    {
        GameManager.gM.SetMaxHealth();
        Time.timeScale = 1f;
        this.isPaused = false;
        SceneManager.LoadScene(0);
    }
    public void NextAbility()
    {
        int nextNum = this.abilityNum + 1;
        while (nextNum != this.abilityNum)
        {
            if (nextNum >= GameManager.gM.GetAbilitiesDictionary().Count)
                nextNum = 0;
            if (nextNum != 0 && GameManager.gM.GetAbilityAt(nextNum))
                this.abilityNum = nextNum;
            else
                nextNum++;
        }
        Abilities();
    }
    public void PreviousAbility()
    {
        int nextNum = this.abilityNum - 1;
        while (nextNum != this.abilityNum)
        {
            if (nextNum < 0)
                nextNum = 3;
            if (nextNum != 0 && GameManager.gM.GetAbilityAt(nextNum))
                this.abilityNum = nextNum;
            else
                nextNum--;
        }
        Abilities();
    }
    private void Abilities()
    {
        switch (this.abilityNum)
        {
            case 0:
                this.abilityImage.sprite  = noneSprite;
                selectedItem = "None";
                break;
            case 1:
                this.abilityImage.sprite  = vodkaSprite;
                selectedItem = "Vodka";
                break;
            case 2:
                this.abilityImage.sprite  = saberSprite;
                selectedItem = "Saber";
                break;
            case 3:
                this.abilityImage.sprite  = arquebusSprite;
                selectedItem = "Arquebus";
                break;
        }
    }
}
