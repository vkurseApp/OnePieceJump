using UnityEngine;
using TMPro;

public class Background : MonoBehaviour
{
    public Sprite[] backgroundsTransform;
    private SpriteRenderer backgroundRenderer; // ��� ��������� �������
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        backgroundRenderer = GetComponent<SpriteRenderer>();
        backgroundRenderer.enabled = false; // ���������� ������ ���������
    }

    void Update()
    {
        backgroundRenderer.enabled = true;
        int index = DetermineSpriteIndex();

        if (index != -1)
        {
            backgroundRenderer.sprite = backgroundsTransform[index];
        }
        else
        {
            backgroundRenderer.enabled = false;
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