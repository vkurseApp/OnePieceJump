using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Resources;

public class Spawner : MonoBehaviour
{
    public float minX, maxX;
    public float YrangeMin, YrangeMax;
    public float cameraDistance;
    public Transform platformPrefab, platformMovePrefab, platformDestroyPrefab;
    public float percentSpawn;
    private Transform cam;
    private float lastSpawnY;
    private float rangeIncreaser;
    public TextMeshProUGUI ScoreTxt;
    public int score;
    private Doodler ddlr;
    public Transform GomuGomuFruit, GashaGashaFruit, MokuMokuFruit, HitoHitoFruit;
    public float percentSpawnFruct;

    void Start()
    {
        cam = Camera.main.transform;
        lastSpawnY = 0;
        ResetGame();
        ddlr = FindObjectOfType<Doodler>();
    }

    void Update()
    {

        ScoreTxt.text = "Score: " + (ddlr.score + score).ToString();

        if (lastSpawnY < 250)
        {
            rangeIncreaser = Mathf.Floor(lastSpawnY / 50);
        }

        if (cam.position.y + cameraDistance > lastSpawnY)
        {
            Transform platform;

            if (Random.value < percentSpawn)
            {
                float randomValue = Random.value;

                if (randomValue < 0.33f)
                    platform = Instantiate(platformDestroyPrefab);
                else
                    platform = Instantiate(platformPrefab);
            }
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
                            fruct = Instantiate(GomuGomuFruit);
                            break;
                        case 2:
                            fruct = Instantiate(GashaGashaFruit);
                            break;
                        case 3:
                            fruct = Instantiate(MokuMokuFruit);
                            break;
                        case 4:
                            fruct = Instantiate(HitoHitoFruit);
                            break;
                        default:
                            fruct = Instantiate(GomuGomuFruit);
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
                    {
                        float randomValue = Random.value;

                        if (randomValue < 0.33f)
                            newPlatform = Instantiate(platformDestroyPrefab);
                        else if (randomValue < 0.66f)
                            newPlatform = Instantiate(platformDestroyPrefab);
                        else
                            newPlatform = Instantiate(platformMovePrefab);
                    }
                    else
                        newPlatform = Instantiate(platformDestroyPrefab);

                    newPlatform.position = new Vector3(
                        Random.Range(minX, maxX),
                        lastSpawnY + Random.Range(YrangeMin + (rangeIncreaser * 0.9f), YrangeMax + (rangeIncreaser * 1.1f)),
                        0);

                    lastSpawnY = newPlatform.position.y;

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
        cam.position = new Vector3(0, 0, -10);
        ScoreTxt.text = "Scores: 0";
    }
}