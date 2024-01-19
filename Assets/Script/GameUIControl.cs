using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIControl : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject winPanelUI;
    [SerializeField] GameObject home;
    [SerializeField] GameObject restart;
    [SerializeField] TextMeshProUGUI wintext;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    private void Update()
    {
        ActiveateWinPanel();
    }

    

    public void TogglePause()
    {
       
        isPaused = !isPaused;
        pauseUI.SetActive(isPaused);
        
        if (isPaused)
        {
            //AudioPlayer.instance.PlayClip(AudioPlayer.instance.menuButtonsClip, AudioPlayer.instance.menuButtonsVolume);
            Time.timeScale = 0;
            Debug.Log("Игра на паузе");
        }
        else
        {
            //AudioPlayer.instance.PlayClip(AudioPlayer.instance.menuButtonsClip, AudioPlayer.instance.menuButtonsVolume);
            Time.timeScale = 1;
            Debug.Log("Игра продолжается");
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 0;
        SceneLoader.instance.LoadLevel("Menu");
    }
    public void LoadCurrentLevel()
    {
        SceneLoader.instance.LoadCurrentScene();
    }


    public void ActiveateWinPanel()
    {
        if (winPanelUI.active)
        {
            return;
        }
        if (ComboManager.instance.countToLose == 2)
        {

            winPanelUI.SetActive(true);
           // restart.SetActive(true);
           // home.SetActive(true);
            Time.timeScale = 0;
            //AudioPlayer.instance.PlayClip(AudioPlayer.instance.winClip, AudioPlayer.instance.winVolume);
            wintext.text = "Score: " + ScoreManager.instance.score;
            //LevelManager.instance.OnLevelComplete(int.Parse(SceneManager.GetActiveScene().name));
            //CurrencyManager.instance.AddGameCurrency(100 * int.Parse(SceneManager.GetActiveScene().name) * Random.Range(1, 2));
        }
        
    }


}
