using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coins, keys, gems;

    // Start is called before the first frame update
    void Start()
    {
        // Set Coins
        coins.text = PlayerPrefs.HasKey("Coins") ? $"{PlayerPrefs.GetInt("Coins")}" : "0";

        // Set Keys
        if (!PlayerPrefs.HasKey("Keys"))
        {
            keys.text = $"0/6";
        }
        else
        {
            keys.text = $"{PlayerPrefs.GetInt("Keys")}/6";
        }

        // Set Gems
        if (!PlayerPrefs.HasKey("Gems"))
        {
            gems.text = $"0/2";
        }
        else
        {
            gems.text = $"{PlayerPrefs.GetInt("Gems")}/2";
        }

    }

    public static void SetKeys()
    {
        // Set Keys
        int keys = 0;
        foreach (var item in GameManager.instance.collectables)
        {
            if (item.Key.Contains("Key") && item.Value == true)
            {
                keys++;
            }
        }

        PlayerPrefs.SetInt("Keys", keys);
    }

    public static void SetGems()
    {
        // Set Keys
        int gems = 0;
        foreach (var item in GameManager.instance.collectables)
        {
            if (item.Key.Contains("Gem") && item.Value == true)
            {
                gems++;
            }
        }

        PlayerPrefs.SetInt("Gems", gems);
    }
}
