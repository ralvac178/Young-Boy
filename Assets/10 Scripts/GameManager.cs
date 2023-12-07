using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int coins = 0;
    private int lives = 5;
    private int arrows = 0;
    public int keys;
    private int level = 1;
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI levelTextGUI, coinsTextGUI, arrowsTextGUI;
    [SerializeField] private Image livesImage, redKey, yellowKey, blueKey;

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
                EnableKeyGet(blueKey);
                break;
            case "yellow":
                EnableKeyGet(yellowKey);
                break;
            default:
                EnableKeyGet(redKey);
                break;
        }
    }

    public void EnableKeyGet(Image keyImage)
    {
        keyImage.color = new Color(255, 255, 255, 255);
    }
}
