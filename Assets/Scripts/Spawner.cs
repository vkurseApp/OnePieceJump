using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public float minX, maxX;
    public float YrangeMin, YrangeMax;
    public float cameraDistance;
    public Transform platformPrefab, platformMovePrefab;
    public float percentSpawn;
    private Transform cam;
    private float lastSpawnY;
    private float rangeIncreaser;
    public TextMeshProUGUI ScoreTxt;
    private int score;

    public Transform fruct1, fruct2, fruct3, fruct4;
    public float percentSpawnFruct;

    void Start()
    {
        cam = Camera.main.transform;
        lastSpawnY = 0;
        ResetGame(); // ��������� ����� ���� ��� ������
    }

    void Update()
    {
        ScoreTxt.text = "Очки: " + score;

        if (lastSpawnY < 250)
        {
            rangeIncreaser = Mathf.Floor(lastSpawnY / 50);
        }

        if (cam.position.y + cameraDistance > lastSpawnY)
        {
            Transform platform;

            if (Random.value < percentSpawn)
                platform = Instantiate(platformPrefab);
            else
                platform = Instantiate(platformMovePrefab);

            platform.position = new Vector3(
                Random.Range(minX, maxX),
                lastSpawnY + Random.Range(YrangeMin + (rangeIncreaser * 0.9f), YrangeMax + (rangeIncreaser * 1.1f)),
                0);

            lastSpawnY = platform.position.y;

            if (lastSpawnY - 12 > 0)
                score = Mathf.CeilToInt((lastSpawnY - 12) * 100);

            if (Random.value < percentSpawn)
            {
                if (Random.value < percentSpawnFruct)
                {
                    Transform fruct;
                    int randomFruct = Random.Range(1, 5);

                    switch (randomFruct)
                    {
                        case 1:
                            fruct = Instantiate(fruct1);
                            break;
                        case 2:
                            fruct = Instantiate(fruct2);
                            break;
                        case 3:
                            fruct = Instantiate(fruct3);
                            break;
                        case 4:
                            fruct = Instantiate(fruct4);
                            break;
                        default:
                            fruct = Instantiate(fruct1);
                            break;
                    }

                    fruct.position = new Vector3(
                        Random.Range(minX, maxX),
                        lastSpawnY + Random.Range(YrangeMin + (rangeIncreaser * 0.9f), YrangeMax + (rangeIncreaser * 1.1f)),
                        0);
                }
                else
                {
                    Transform newPlatform;
                    if (Random.value < percentSpawn)
                        platform = Instantiate(platformPrefab);
                    else
                        platform = Instantiate(platformMovePrefab);

                    platform.position = new Vector3(
                        Random.Range(minX, maxX),
                        lastSpawnY + Random.Range(YrangeMin + (rangeIncreaser * 0.9f), YrangeMax + (rangeIncreaser * 1.1f)),
                        0);

                    lastSpawnY = platform.position.y;

                    if (lastSpawnY - 12 > 0)
                        score = Mathf.CeilToInt((lastSpawnY - 12) * 100);
                }
            }
        }
    }

    void ResetGame()
    {
        lastSpawnY = 0;
        score = 0;
        cam.position = new Vector3(0, 0, -10); // ���������� ������ �� ��������� �������
        ScoreTxt.text = "Очки: " + score; // ��������� ����� � ������
    }
}