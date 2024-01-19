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

    // ����� ��� ��������� ��������� ��������� �� �����
    private void UpdateScoreDisplay()
    {
        scoreText.text = score.ToString();
        SaveScore();

    }

    // ����� ��� ��������� ���� �� ���������
    public void AddScore()
    {

            if (ball != null)
            {
                // ���������, ����������� �� ����� ����� (�� ��� Y)
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

    // ��������� ���� �� PlayerPrefs
    void LoadScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            maxScore = PlayerPrefs.GetInt("Score");
        }
    }
}