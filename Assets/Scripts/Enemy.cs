using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject powerPrefab; // ������ ����
    public Sprite[] bossesSprites;
    private SpriteRenderer bossRenderer; // ��� ��������� �������
    public float shootingInterval = 20f; // �������� ����� ����������
    public TextMeshProUGUI scoreText;
    private float shootingTimer;


    void Awake()
    {
        bossRenderer = GetComponent<SpriteRenderer>();
        bossRenderer.enabled = false; // ���������� ������ ���������
    }
    void Update()
    {
        bossRenderer.enabled = true;
        int index = DetermineSpriteIndex();
        if (index != -1)
        {
            bossRenderer.sprite = bossesSprites[index];
        }
        else
        {
            bossRenderer.enabled = false;
        }

        shootingTimer += Time.deltaTime;
        if (shootingTimer >= shootingInterval)
        {
            Shoot();
            shootingTimer = 0f;
        }
    }

    private int DetermineSpriteIndex()
    {
        int totalScore;
        int index = 0;
        if (int.TryParse(scoreText.text.Replace("Score: ", ""), out totalScore))
        {
            if (totalScore >= 50000)
            {
                index = 1;
            }
            if (totalScore >= 100000)
            {
                index = 2;
            }
            if (totalScore >= 150000)
            {
                index = 3;
            }
        }
        return index;
    }
    void Shoot()
    {
        GameObject power = Instantiate(powerPrefab);
        Power powerShotScript = power.GetComponent<Power>();

        if (powerShotScript != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                powerShotScript.direction = direction;
                powerShotScript.ActivateAndSetSprite();
            }
        }
    }
}
