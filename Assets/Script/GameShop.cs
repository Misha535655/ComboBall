using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameShop : MonoBehaviour
{
    public static GameShop instance;
    public TextMeshProUGUI[] displayText;
    public Button[] buyButtons;
    public int[] initialPrices;
    public float priceIncreaseFactor = 1.1f;

    public float timeToResetPlinko = 0f;
    public float timeToResetHole = 0f;
    public float timeToResetMovePlanet = 0f;
    public float additionTime = 1f;

    private int[] currentPrices;


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


    private void Start()
    {
        
        // «агрузка цен из PlayerPrefs, если они там сохранены
        LoadPrices();
        // ≈сли цены не были загружены (первый запуск), устанавливаем начальные цены
        if (currentPrices == null || currentPrices.Length != initialPrices.Length)
        {
            currentPrices = initialPrices.Clone() as int[];
            SavePrices();
        }
        UpdateDisplay();

        
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < displayText.Length; i++)
        {
            if (displayText[i] != null)
            {
                Debug.Log(currentPrices[i]);
                displayText[i].text = $"{currentPrices[i]}";
            }
            else
            {
                Debug.LogWarning($"displayText[{i}] is null.");
            }
        }
    }

    public void BuyItem(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < currentPrices.Length)
        {
            int price = currentPrices[itemIndex];
            if (true)
            {
                //AudioPlayer.instance.PlayClip(AudioPlayer.instance.buyClip, AudioPlayer.instance.buyVolume);
                Debug.Log($"¬ы приобрели товар {itemIndex} за {price} монет.");

                // ќбновл€ем цену с учетом увеличени€
                currentPrices[itemIndex] = Mathf.RoundToInt(price * priceIncreaseFactor);

                // —охран€ем цены в PlayerPrefs
                SavePrices();

                // ќбновл€ем отображение
                UpdateDisplay();

                switch (itemIndex)
                {
                    case 0:
                        Debug.Log(additionTime);
                        additionTime += 1f;
                        PlayerPrefs.SetFloat("Timer", additionTime);
                        break;
                    case 1:
                        Debug.Log(timeToResetPlinko);
                        timeToResetPlinko += 0.1f;
                        PlayerPrefs.SetFloat("HoleScale", timeToResetPlinko);
                        break;
                    case 2:
                        Debug.Log(timeToResetHole);
                        timeToResetHole += .3f;
                        PlayerPrefs.SetFloat("HoleScale", timeToResetHole);
                        break;
                        
                    case 3:
                        Debug.Log(timeToResetMovePlanet);
                        timeToResetMovePlanet += .4f;
                        PlayerPrefs.SetFloat("PlanetStop", timeToResetMovePlanet);
                        break;

                        default: break;
                }
                PlayerPrefs.Save();

            }
            else
            {
               // AudioPlayer.instance.PlayClip(AudioPlayer.instance.cancelBuyClip, AudioPlayer.instance.cancelBuyVolume);
                return;
            }

        }
        else
        {
            Debug.LogWarning("“акого товара нет в магазине.");
        }
    }

    private void SavePrices()
    {
        // ѕреобразовываем массив цен в строку и сохран€ем в PlayerPrefs
        string pricesString = string.Join(",", currentPrices);
        PlayerPrefs.SetString("ShopPrices", pricesString);
    }

    private void LoadPrices()
    {
        // «агружаем строку цен из PlayerPrefs и преобразуем в массив
        string pricesString = PlayerPrefs.GetString("ShopPrices", "");

        if (!string.IsNullOrEmpty(pricesString))
        {
            string[] pricesArray = pricesString.Split(',');
            currentPrices = new int[pricesArray.Length];

            for (int i = 0; i < pricesArray.Length; i++)
            {
                if (int.TryParse(pricesArray[i], out currentPrices[i]))
                {
                    Debug.Log($"Loaded price for index {i}: {currentPrices[i]}");
                }
                else
                {
                    Debug.LogWarning($"Failed to parse price for index {i}: {pricesArray[i]}");

                    // ≈сли не удалось распарсить цену, устанавливаем начальную цену
                    currentPrices[i] = initialPrices[i];
                }
            }
        }
        else
        {
            // ≈сли строка цен пуста, устанавливаем начальные цены
            currentPrices = initialPrices.Clone() as int[];
        }

            timeToResetPlinko = PlayerPrefs.GetFloat("PlinkoInvisible");
            timeToResetHole = PlayerPrefs.GetFloat("HoleScale");
            timeToResetMovePlanet = PlayerPrefs.GetFloat("PlanetStop");
            additionTime = PlayerPrefs.GetFloat("Timer");
    }
    private void ResetPrices()
    {
        for (int i = 0; i < currentPrices.Length; i++)
        {
            currentPrices[i] = 100;
        }

        // —охран€ем цены в PlayerPrefs
        SavePrices();

        // ќбновл€ем отображение
        UpdateDisplay();
    }
}