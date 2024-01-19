using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public Transform ball; 
    public TextMeshProUGUI scoreText;
    public static ScoreManager instance;
    public int score = 0;
    public int maxScore = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadScore();
        UpdateScoreDisplay();
    }

    // Метод для оновлення виведення лічильника на екран
    private void UpdateScoreDisplay()
    {
        scoreText.text = score.ToString();
        SaveScore();

    }

    // Метод для додавання балів до лічильника
    public void AddScore()
    {

            if (ball != null)
            {
                // Проверяем, поднимается ли шарик вверх (по оси Y)
                if (ball.position.y > transform.position.y)
                {

                    score += ComboManager.instance.score;

                }
            }
        
        UpdateScoreDisplay();
    }

    void SaveScore()
    {
        if(maxScore < score) 
        {
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.Save();
        }

    }

    // Загружаем счет из PlayerPrefs
    void LoadScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            maxScore = PlayerPrefs.GetInt("Score");
        }
    }
}