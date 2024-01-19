using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // ������ ��� ������
    public int numberOfBlocks = 7; // ���������� ������ �� ������ �������
    public float yOffset = 1f; // ������������ ������ �� ���� ������
    public float spawnHeight = 10f; // ������, ��� ���������� ������� ����� ���������� �����
    private List<Transform> spawnTransform = new List<Transform>();

    private void Start()
    {
        SpawnBlocksOnSide(true, false);
        SpawnBlocksOnSide(false, false);
    }
    private void Update()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && player.transform.position.y > spawnHeight / 10f)
        {
            DeleteBlockOutSide();
            // ����� ������
            SpawnBlocksOnSide(true); 
            SpawnBlocksOnSide(false); 

            spawnHeight += 10f;
        }
    }

    private void SpawnBlocksOnSide(bool isRightSide, bool firstPlay = true)
    {
        float xOffset = isRightSide ? Screen.width + 80f : -80f;
        float stepY = (Screen.height - 2 * yOffset) / (numberOfBlocks - 1);

        for (int i = 0; i < numberOfBlocks; i++)
        {
            float yPosition = yOffset + i * stepY;

            if (firstPlay)
            {
                yPosition = spawnTransform[spawnTransform.Count - 1].position.y + i * stepY;
            }

            Vector3 spawnPoint = Camera.main.ScreenToWorldPoint(new Vector3(xOffset, yPosition, 1f));

            // �������� �� ����������� � ������� �������
            if (!IsBlockOverlap(spawnPoint))
            {
                var block = Instantiate(prefabs[Random.Range(0,4)], spawnPoint, Quaternion.identity);
                spawnTransform.Add(block.transform);

                if (isRightSide)
                {
                    block.transform.Rotate(new Vector3(0, 0, -15));
                }
                else
                {
                    block.transform.Rotate(new Vector3(0, 0, 15));
                }
            }
        }
    }

    private bool IsBlockOverlap(Vector3 position)
    {
        // �������� �� ����������� � ������� �������
        foreach (Transform blockTransform in spawnTransform)
        {
            float distance = Vector3.Distance(position, blockTransform.position);
            if (distance < 1.5f)  // ������������, ��� ����� ����� ������� 1.0f
            {
                return true;  // ���� �����������
            }
        }

        return false;  // ��� �����������
    }

    private void DeleteBlockOutSide()
    {
        for (int i = spawnTransform.Count - 1; i >= 0; i--)
        {
            var screenHeight = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height - Screen.height * 2, 0));
            if (spawnTransform[i].position.y < screenHeight.y)
            {
                Destroy(spawnTransform[i].gameObject);
                spawnTransform.RemoveAt(i);
            }
        }
    }
}
