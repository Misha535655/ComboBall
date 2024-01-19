using TMPro;
using UnityEngine;

public class ComboManager : MonoBehaviour
{

    
    private int comboValue = 1;
    private float comboMultiplier = 1;
    private int comboOldValue;
    private float comboOldMultiplier;
    public TextMeshProUGUI scoreText;
    public static ComboManager instance;
    public int countToLose = 0;
    public int score = 1;
    [SerializeField] public GameObject ability1;
    [SerializeField] public GameObject ability2;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ����������� ��� ������� �������� ������ �'��� �� ���������
    public void OnSuccessfulBounce()
    {
        // ��������� ����� �� ����
        comboValue++;
        score = (int)(comboValue * comboMultiplier);
        scoreText.text = score + "X";
        
    }

    public void MultiplyCombo() 
    {
        ability2.SetActive(false);
        comboValue *= 2;
        OnSuccessfulBounce();
    }


    // ����� ��������� �������� �����
    public void IncreaseComboMultiplier()
    {
        comboMultiplier += .1f;
        Debug.Log(comboMultiplier);
    }

    // ����� �������� �����
    public void ResetCombo()
    {

        comboOldValue = comboValue;
        comboOldMultiplier = comboMultiplier;

        countToLose++;
        comboValue = 1;
        comboMultiplier = 1;
        Debug.Log("Combo reset!");
    }

    public void RestartCombo()
    {
        Debug.Log(comboMultiplier + " " + comboOldValue);
        if(comboOldValue <= 0 || comboOldMultiplier <= 0)
        {
            ResetCombo();
            OnSuccessfulBounce();
        }
        else
        {

            comboValue = comboOldValue;
            comboMultiplier = comboOldMultiplier;
            ability1.SetActive(false);
            OnSuccessfulBounce();
        }

    }
}