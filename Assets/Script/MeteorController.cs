using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeteorController : MonoBehaviour
{
    public float meteorSpeed = 5f;
    public float destroyYThreshold = -10f;
    public int poolSize = 10;  // Размер пула объектов
    public GameObject meteorPrefab;

    private List<GameObject> meteorPool;

    void Start()
    {
        // Инициализация пула объектов
        InitializeMeteorPool();

        // Запускаем процесс появления метеоритов
        StartCoroutine(SpawnMeteors());
    }

    void InitializeMeteorPool()
    {
        meteorPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject meteor = Instantiate(meteorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            meteor.SetActive(false);
            meteorPool.Add(meteor);
        }
    }

    IEnumerator SpawnMeteors()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(.1f, .1f));  // Интервал появления метеоритов
            GameObject meteor = GetPooledMeteor();

            if (meteor != null)
            {
                float screenHeight = Screen.height;
                Vector3 topOfScreen = new Vector3(Screen.width / 2f, screenHeight, 0f);

                // Преобразуем координаты экрана в мировые координаты
                Vector3 worldTopOfScreen = Camera.main.ScreenToWorldPoint(topOfScreen);

                // Позиционируем метеорит в начальной точке
                meteor.transform.position = new Vector3(Random.Range(-3f, 3f), worldTopOfScreen.y, 0);
                meteor.SetActive(true);
            }
        }
    }

    GameObject GetPooledMeteor()
    {
        // Ищем неактивный метеорит в пуле
        foreach (GameObject meteor in meteorPool)
        {
            if (!meteor.activeInHierarchy)
            {
                return meteor;
            }
        }

        return null;
    }

    void Update()
    {
        // Двигаем активные метеориты вниз по экрану
        foreach (GameObject meteor in meteorPool)
        {
            if (meteor.activeInHierarchy)
            {
                meteor.transform.Translate(Vector3.down * meteorSpeed * Time.deltaTime);

                var screenHeight = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height - Screen.height * 2, 0));
                if (meteor.transform.position.y < screenHeight.y)
                {
                    meteor.SetActive(false);
                }
            }
        }
    }
}