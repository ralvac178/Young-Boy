using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private string message = "Loading";
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private Image loadBar;
    [SerializeField] private TextMeshProUGUI title;
    [HideInInspector] public static int levelToGo;
    public static bool retry = false;
    private void Start()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.stopPlayer = false;
            GameManager.instance.UpdateLevelText();
            GameManager.instance.RestartCoinsAndAttackOptions();
            Time.timeScale = 1f;
            if (retry)
            {
                GameManager.instance.SetLevel(1);
                levelToGo = 1;
            }
            else
            {
                levelToGo = GameManager.instance.GetLevel();
            }           
        }

        title.text = $"CHAPTER {levelToGo}";
        StartCoroutine(nameof(LoadingPoints));
        StartCoroutine(nameof(LoadingAsycScene));
    }

    public IEnumerator LoadingPoints()
    {
        string textToShow = message;
        while (true)
        {          
            for (int i = 0; i < 4; i++)
            {
                loadingText.text = textToShow;
                textToShow = textToShow.Insert(textToShow.Length, ".");                
                yield return new WaitForSecondsRealtime(0.8f);
            }
            yield return null;
            textToShow = message;
        }
    }

    public IEnumerator LoadingAsycScene()
    {
        // Crear una operación de carga asíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToGo);

        // No permitir que la escena se active inmediatamente
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            // Obtener el progreso de carga (de 0.0 a 1.0)
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // Actualizar la barra de carga
            loadBar.fillAmount = progress;

            // Si la carga está casi completa (0.9f es casi completo en LoadSceneAsync)
            if (asyncLoad.progress >= 0.9f)
            {
                // Permitir que la escena se active
                yield return new WaitForSecondsRealtime(3);
                if (retry || levelToGo == 1)
                {
                    if (GameManager.instance != null) GameManager.instance.ResetAllGame();
                    retry = false;
                }
                
                asyncLoad.allowSceneActivation = true;              
            }

            yield return null;
        }
    }

}
