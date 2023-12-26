using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int coins = 0;
    private int totalCoins;
    private int lives = 5;
    private int arrows = 0;
    public int keys;
    private int level = 1;
    public static GameManager instance;
    public bool gameOver = false;

    [SerializeField] private TextMeshProUGUI levelTextGUI, coinsTextGUI, arrowsTextGUI;
    [SerializeField] private Image livesImage, redKey, yellowKey, blueKey;
    [SerializeField] private Image dJGem, punchMode, arrowMode;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        UpdateLevelText();
        UpdateLives(lives);
    }

    // Update is called once per frame
    void Update()
    {
  
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
        livesImage.GetComponent<RectTransform>().sizeDelta = new Vector2(48*lives,50);
    }

    public void UpdateCoins(int coins)
    {
        coinsTextGUI.text = $"{coins}";
    }

    public void AddPoints(int points)
    {
        coins += points;
        UpdateCoins(coins);
    }

    public void UpdateArrows()
    {
        arrowsTextGUI.text = $"{arrows}";
    }

    public int GetArrows()
    {
        return arrows;
    }

    public void AddLives(int livesToAdd)
    {
        lives += livesToAdd;
        UpdateLives(lives);
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
        UpdateLevelText();
    }

    public void SetKey(string color)
    {
        switch (color)
        {
            case "blue":
                EnableGotItem(blueKey);
                break;
            case "yellow":
                EnableGotItem(yellowKey);
                break;
            default:
                EnableGotItem(redKey);
                break;
        }
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

    public void SetDoubleJumpGem()
    {
        EnableGotItem(dJGem);
    }

    public void DisableDoubleJumpIem()
    {
        DisableGotItem(dJGem);
    }

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

    public void RestartItems()
    {
        arrows = 0;
        coins = 0;
        PlayerController.canDoubleJump = false;
    }
}
