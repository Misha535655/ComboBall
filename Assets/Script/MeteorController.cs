using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeteorController : MonoBehaviour
{
    public float meteorSpeed = 5f;
    public float destroyYThreshold = -10f;
    public int poolSize = 10;  // ������ ���� ��������
    public GameObject meteorPrefab;

    private List<GameObject> meteorPool;

    void Start()
    {
        // ������������� ���� ��������
        InitializeMeteorPool();

        // ��������� ������� ��������� ����������
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
            yield return new WaitForSeconds(Random.Range(.1f, .1f));  // �������� ��������� ����������
            GameObject meteor = GetPooledMeteor();

            if (meteor != null)
            {
                float screenHeight = Screen.height;
                Vector3 topOfScreen = new Vector3(Screen.width / 2f, screenHeight, 0f);

                // ����������� ���������� ������ � ������� ����������
                Vector3 worldTopOfScreen = Camera.main.ScreenToWorldPoint(topOfScreen);

                // ������������� �������� � ��������� �����
                meteor.transform.position = new Vector3(Random.Range(-3f, 3f), worldTopOfScreen.y, 0);
                meteor.SetActive(true);
            }
        }
    }

    GameObject GetPooledMeteor()
    {
        // ���� ���������� �������� � ����
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
        // ������� �������� ��������� ���� �� ������
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