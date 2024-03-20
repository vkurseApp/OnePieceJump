using UnityEngine;
using TMPro;

public class BackgroundController : MonoBehaviour
{
    public Transform backgroundTransform1;
    public Transform backgroundTransform2;
    public Transform backgroundTransform3;
    public Transform backgroundTransform4;
    public Transform backgroundTransform5;
    public Transform backgroundTransform6;
    public TextMeshProUGUI scoreText;
    public float transparentAlpha = 1f; // ������������ ��� �������� ������ 50000
    public float opaqueAlpha = 1f; // �������������� ��� �������� ������ ��� ����� 50000

    private float alphaChangeSpeed = 1f / 3f; // �������� ��������� ������������ �� �������
    private float currentAlpha1 = 1f; // ������� �������� ������������ ��� ������� ����
    private float currentAlpha2 = 1f; // ������� �������� ������������ ��� ������� ����
    private float currentAlpha3 = 1f; // ������� �������� ������������ ��� �������� ����

    void Update()
    {
        int totalScore;
        if (int.TryParse(scoreText.text.Replace("Score: ", ""), out totalScore))
        {
            UpdateBackgroundAlpha(backgroundTransform1, totalScore, 50000, ref currentAlpha1);
            UpdateBackgroundAlpha(backgroundTransform2, totalScore, 100000, ref currentAlpha2);
            UpdateBackgroundAlpha(backgroundTransform3, totalScore, 150000, ref currentAlpha3);
            UpdateBackgroundAlpha(backgroundTransform4, totalScore, 50000, ref currentAlpha1);
            UpdateBackgroundAlpha(backgroundTransform5, totalScore, 100000, ref currentAlpha2);
            UpdateBackgroundAlpha(backgroundTransform6, totalScore, 150000, ref currentAlpha3);
        }
    }

    void UpdateBackgroundAlpha(Transform backgroundTransform, int totalScore, int scoreThreshold, ref float currentAlpha)
    {
        Renderer backgroundRenderer = backgroundTransform.GetComponent<Renderer>();
        if (backgroundRenderer != null)
        {
            Material backgroundMaterial = backgroundRenderer.material;
            if (backgroundMaterial != null)
            {
                Color backgroundColor = backgroundMaterial.color;

                if (totalScore > scoreThreshold)
                {
                    currentAlpha -= alphaChangeSpeed * Time.deltaTime; // ��������� ������������ ����������
                    currentAlpha = Mathf.Max(currentAlpha, transparentAlpha); // ������������ ����������� ��������

                    backgroundColor.a = currentAlpha; // ������������� ������� �������� ������������
                }
                else
                {
                    backgroundColor.a = opaqueAlpha; // ������������� �������������� ��� �������� ������ ��� ����� ���������� ��������
                }

                backgroundMaterial.color = backgroundColor; // ��������� ����� �������� ����� � �������� �������
            }
        }
    }
}
