using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int coins = 0;
    public int totalCoins;
    private int lives = 5;
    private static int arrows = 0;
    public int keys;
    private int level = 1;
    public static GameManager instance;
    public bool gameOver = false;
    private PlayerController playerController;
    public bool stopPlayer = false;

    // Collectables Dictionary
    public Dictionary<string, bool> collectables;

    // Game GUI
    [SerializeField] private TextMeshProUGUI levelTextGUI, coinsTextGUI, arrowsTextGUI;
    [SerializeField] private Image livesImage, redKey, yellowKey, blueKey, gem;
    [SerializeField] private Image dJGem, punchMode, arrowMode;


    // Start is called before the first frame update
    void Awake()
    {
        // Ensure only one instance exists
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        // Collectables initialization
        collectables = new Dictionary<string, bool>();

        // Collectables fillment
        collectables.Add("KeyBlue1", false);
        collectables.Add("KeyRed1", false);
        collectables.Add("KeyYellow1", false);
        collectables.Add("Gem1", false);

        collectables.Add("KeyBlue2", false);
        collectables.Add("KeyRed2", false);
        collectables.Add("KeyYellow2", false);
        collectables.Add("Gem2", false);
        // End Collectables fillment

        UpdateLevelText();
        UpdateLives(lives);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void AddArrows()
    {
        arrows += 5;
        UpdateArrows();
    }

    public void SubArrows()
    {
        arrows--;
        UpdateArrows();
    }

    public void UpdateLives(int lives)
    {
        livesImage.GetComponent<RectTransform>().sizeDelta = new Vector2(48 * lives, 50);
    }

    public void UpdateCoins(int coins)
    {
        coinsTextGUI.text = $"{coins}";
    }

    public void AddCoins(int points)
    {
        totalCoins += points;
        if (coins <= 99)
        {
            coins += points;           
        }
        else
        {
            AddLives(1);
            SoundManager.instance.SoundPlayerGotArrowsNLives();
            coins = 0;
        }
        
        UpdateCoins(coins);
    }

    public void UpdateArrows()
    {
        arrowsTextGUI.text = $"{arrows}";
    }

    public static int GetArrows()
    {
        return arrows;
    }

    public void AddLives(int livesToAdd)
    {
        if (lives < 8)
        {
            lives += livesToAdd;
            if (lives >= 8)
            {
                lives = 8;
            }
            UpdateLives(lives);
        }   
    }

    public void SubLives(int livesToSub)
    {
        lives -= livesToSub;
        UpdateLives(lives);
    }

    public void UpdateLevelText()
    {
        levelTextGUI.text = $"L{level}";
    }

    public void NextLevel()
    {
        level++;
        PlayerPrefs.SetInt("EnableStage2", 1);
        //UpdateLevelText();
    }

    public void SetKey(string color)
    {
        switch (color)
        {
            case "Blue":
                EnableGotItem(blueKey);
                break;
            case "Yellow":
                EnableGotItem(yellowKey);
                break;
            default:
                EnableGotItem(redKey);
                break;
        }
    }

    public void UnSetKey()
    {
        DisableGotItem(blueKey);
        DisableGotItem(yellowKey);
        DisableGotItem(redKey);
    }

    public void EnableGotItem(Image itemImage)
    {
        itemImage.color = new Color(1, 1, 1, 1);
    }

    public void DisableGotItem(Image itemImage)
    {
        itemImage.color = new Color(1, 1, 1, 0.3921569f);
    }

    public void DisableItem(Image itemImage)
    {
        itemImage.color = new Color(1, 1, 1, 0);
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int n)
    {
        level = n;
    }

    public void SetDoubleJumpGem()
    {
        EnableGotItem(dJGem);
    }

    public void DisableDoubleJumpGem()
    {
        DisableGotItem(dJGem);
    }

    //Gems
    public void SetGem()
    {
        EnableGotItem(gem);
    }

    public void DisableGem()
    {
        DisableGotItem(gem);
    }

    //End Gems settable

    public void EnablePunchAttackItem()
    {
        EnableGotItem(punchMode);
        DisableItem(arrowMode);
    }

    public void EnableArrowAttackItem()
    {
        EnableGotItem(arrowMode);
        DisableItem(punchMode);
    }

    public int GetLives()
    {
        return lives;
    }

    public void RestartCoinsAndAttackOptions()
    {
        coins = 0;
        arrows = 0;
        UpdateCoins(coins);
        UpdateArrows();
        UnSetKey();
        DisableGem();
        playerController.RestartAttackOptions();
    }

    public void ResetAllGame()
    {
        arrows = 0;
        coins = 0;
        lives = 5;
        UpdateLives(lives);
        UpdateCoins(coins);
        UpdateArrows();
        DisableGem();
        UpdateLevelText();
        gameOver = false;
        HasTouchGround.enableReturn = false;
        PlayerController.canDoubleJump = false;
        playerController.RestartAttackOptions();
        UnSetKey();
        playerController.ResetLayer();
        playerController.ResetAnimator();
    }

    public void FinishGame()
    {
        if (gameOver)
        {
            if (playerController != null)
            {
                playerController.SetDeadAnimation();
                playerController.gameObject.layer = 8;
                GameOverCanvasSingleton.instance.OpenGameOverCanvas();
                SoundManager.instance.GameOverSound();
                HasTouchGround.enableReturn = true;
            }
        }
    }
}
