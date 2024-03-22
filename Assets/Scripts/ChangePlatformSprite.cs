using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class ChangePlatformSprite : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public Transform platformPrefab;
    public Transform platformMovePrefab;
    public Transform platformDestroyPrefab;

    public Sprite NewSprite1;
    public Sprite NewSprite2;
    public Sprite NewSprite3;

    void Update()
    {
        int totalScore;
        if (int.TryParse(scoreText.text.Replace("Score: ", ""), out totalScore))
        {
            if (totalScore >= 50000)
            {
                ChangeSprite(platformPrefab, NewSprite1);
                ChangeSprite(platformMovePrefab, NewSprite1);
                ChangeSprite(platformDestroyPrefab, NewSprite1);
            }
            if (totalScore >= 100000)
            {
                ChangeSprite(platformPrefab, NewSprite2);
                ChangeSprite(platformMovePrefab, NewSprite2);
                ChangeSprite(platformDestroyPrefab, NewSprite2);
            }
            if (totalScore >= 150000)
            {
                ChangeSprite(platformPrefab, NewSprite3);
                ChangeSprite(platformMovePrefab, NewSprite3);
                ChangeSprite(platformDestroyPrefab, NewSprite3);
            }
        }
    }

    void ChangeSprite(Transform platform, Sprite newSprite) // Updated parameter type to Transform
    {
        platform.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}