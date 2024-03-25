using UnityEngine;
using TMPro;

public class Background : MonoBehaviour
{
    public Sprite[] backgroundsTransform;
    private SpriteRenderer backgroundRenderer; // ��� ��������� �������
    public TextMeshProUGUI scoreText;
    // public float transparentAlpha = 0.3f;
    // public float opaqueAlpha = 1f;
    // private float alphaChangeSpeed = 1f / 3f; // �������� ��������� ������������ �� �������
    // private float currentAlpha1 = 1f; // ������� �������� ������������ ��� ������� ����
    // private float currentAlpha2 = 1f; // ������� �������� ������������ ��� ������� ����
    // private float currentAlpha3 = 1f; // ������� �������� ������������ ��� �������� ����

    void Awake()
    {
        backgroundRenderer = GetComponent<SpriteRenderer>();
        backgroundRenderer.enabled = false; // ���������� ������ ���������
    }

    void Update()
    {
        // int totalScore;
        backgroundRenderer.enabled = true;
        int index = DetermineSpriteIndex();
        // if (int.TryParse(scoreText.text.Replace("Score: ", ""), out totalScore))
        // {
        //     UpdateBackgroundAlpha(backgroundTransform1, totalScore, 50000, ref currentAlpha1);
        //     UpdateBackgroundAlpha(backgroundTransform2, totalScore, 100000, ref currentAlpha2);
        //     UpdateBackgroundAlpha(backgroundTransform3, totalScore, 150000, ref currentAlpha3);
        // }

        if (index != -1)
        {
            backgroundRenderer.sprite = backgroundsTransform[index];
        }
        else
        {
            backgroundRenderer.enabled = false;
        }

        // void UpdateBackgroundAlpha(Transform backgroundTransform, int totalScore, int scoreThreshold, ref float currentAlpha)
        // {
        //     Renderer backgroundRenderer = backgroundTransform.GetComponent<Renderer>();
        //     if (backgroundRenderer != null)
        //     {
        //         Material backgroundMaterial = backgroundRenderer.material;
        //         if (backgroundMaterial != null)
        //         {
        //             Color backgroundColor = backgroundMaterial.color;

        //             if (totalScore > scoreThreshold)
        //             {
        //                 currentAlpha -= alphaChangeSpeed * Time.deltaTime; // ��������� ������������ ����������
        //                 currentAlpha = Mathf.Max(currentAlpha, transparentAlpha); // ������������ ����������� ��������

        //                 backgroundColor.a = currentAlpha; // ������������� ������� �������� ������������
        //             }
        //             else
        //             {
        //                 backgroundColor.a = opaqueAlpha; // ������������� �������������� ��� �������� ������ ��� ����� ���������� ��������
        //             }

        //             backgroundMaterial.color = backgroundColor; // ��������� ����� �������� ����� � �������� �������
        //         }
        //     }
        // }

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