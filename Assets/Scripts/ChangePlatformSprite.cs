using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class ChangePlatformSprite : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public Transform platformPrefab, platformMovePrefab, platformDestroyPrefab;

    public Sprite ArabastaPlatform, SkypiaPlatform, DressrosePlatform, KaidouPlatform;

    void Update()
    {
        int totalScore;
        if (int.TryParse(scoreText.text.Replace("Score: ", ""), out totalScore))
        {
            if (totalScore == 0)
            {
                ChangeSprite(ArabastaPlatform);
            }
            if (totalScore >= 50000)
            {
                ChangeSprite(SkypiaPlatform);
            }
            if (totalScore >= 100000)
            {
                ChangeSprite(DressrosePlatform);
            }
            if (totalScore >= 150000)
            {
                ChangeSprite(KaidouPlatform);
            }
        }
    }

    void ChangeSprite(Sprite newSprite) // Updated parameter type to Transform
    {
        platformPrefab.GetComponent<SpriteRenderer>().sprite = newSprite;
        platformMovePrefab.GetComponent<SpriteRenderer>().sprite = newSprite;
        platformDestroyPrefab.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}