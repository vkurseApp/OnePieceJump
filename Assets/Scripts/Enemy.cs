using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Sprite[] bossesSprites;
    public GameObject powerObject;
    private SpriteRenderer bossRenderer; // ��� ��������� �������
    // public float shootingInterval = 2f; // �������� ����� ����������
    public TextMeshProUGUI scoreText;
    // private float shootingTimer;

    void Awake()
    {
        bossRenderer = GetComponent<SpriteRenderer>();
        bossRenderer.enabled = false; // ���������� ������ ���������
        // powerObject = powerObject.GetComponent<GameObject>();
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
