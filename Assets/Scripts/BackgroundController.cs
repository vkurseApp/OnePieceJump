using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public Transform backgroundTransform1;
    public Transform backgroundTransform2;
    public Transform backgroundTransform3;
    public Transform backgroundTransform4;
    public Transform backgroundTransform5;
    public Transform backgroundTransform6;
    public TextMeshProUGUI scoreText;
    public float transparentAlpha = 0.3f; // Прозрачность для значения больше 50000
    public float opaqueAlpha = 1f; // Непрозрачность для значения меньше или равно 50000
    public VideoPlayer videoPlayer; // Ссылка на VideoPlayer
    private bool videoPlayed = false; // Флаг для отслеживания проигрывания видео

    private float alphaChangeSpeed = 1f / 3f; // Скорость изменения прозрачности за секунду
    private float currentAlpha1 = 1f; // Текущее значение прозрачности для первого фона
    private float currentAlpha2 = 1f; // Текущее значение прозрачности для второго фона
    private float currentAlpha3 = 1f; // Текущее значение прозрачности для третьего фона

    public VideoClip videoClip; // Ваш видеофайл
    public RawImage Screen; // Ссылка на Raw Image для отображения видео

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

            if (totalScore >= 100000 && !videoPlayed)
            {
                videoPlayer.Play();
                Time.timeScale = 0; // Пауза для всей игры
                Screen.enabled = true; // Показываем Raw Image для видео
                videoPlayed = true;
            }
        }
    }

    void Start()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>(); // Добавляем VideoPlayer к объекту
        videoPlayer.playOnAwake = false;
        videoPlayer.clip = videoClip;
        Screen.texture = videoPlayer.texture; // Привязываем текстуру видео к Raw Image
        Screen.enabled = false; // Убедитесь, что Raw Image невидимо

        videoPlayer.loopPointReached += OnVideoFinished; // Добавляем обработчик события завершения проигрывания видео
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        vp.Stop(); // Останавливаем проигрывание видео
        Screen.enabled = false; // Скрываем Raw Image после завершения видео
        Time.timeScale = 1; // Снимаем паузу с игры
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
                    currentAlpha -= alphaChangeSpeed * Time.deltaTime; // Уменьшаем прозрачность постепенно
                    currentAlpha = Mathf.Max(currentAlpha, transparentAlpha); // Ограничиваем минимальное значение

                    backgroundColor.a = currentAlpha; // Устанавливаем текущее значение прозрачности
                }
                else
                {
                    backgroundColor.a = opaqueAlpha; // Устанавливаем непрозрачность для значения меньше или равно пороговому значению
                }

                backgroundMaterial.color = backgroundColor; // Применяем новое значение цвета к фоновому объекту
            }
        }
    }
}
