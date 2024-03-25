using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Power : MonoBehaviour
{
    public float speed = 5f;
    public Sprite[] powerSprites; // ������ �������� ��� ����
    public TextMeshProUGUI scoreText;
    public Vector2 direction;
    private SpriteRenderer spriteRenderer; // ��� ��������� �������



    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // ���������� ������ ���������
    }

    void Update()
    {
        if (spriteRenderer.enabled)
        {
            // �������� � �������� �����������, ���� ���� �������
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    public void ActivateAndSetSprite()
    {
        // ����� ��� ��������� � ��������� ������� � ����������� �� �����
        spriteRenderer.enabled = true; // ���������� ������

        // ����������� ������� ������� �� ������ �������� �����
        int index = DetermineSpriteIndex();
        if (index != -1)
        {
            spriteRenderer.sprite = powerSprites[index];
        }
        else
        {
            // ���� �� ����� ���������� ������, ���� ������� ����������
            spriteRenderer.enabled = false;
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

}
