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
    public VideoPlayer videoPlayer2;
    public VideoPlayer videoPlayer3;
    public VideoPlayer videoPlayer4;
    private bool videoPlayed2 = false;
    private bool videoPlayed3 = false;
    private bool videoPlayed4 = false;
    private bool videoPlayed = false; // Флаг для отслеживания проигрывания видео

    private float alphaChangeSpeed = 1f / 3f; // Скорость изменения прозрачности за секунду
    private float currentAlpha1 = 1f; // Текущее значение прозрачности для первого фона
    private float currentAlpha2 = 1f; // Текущее значение прозрачности для второго фона
    private float currentAlpha3 = 1f; // Текущее значение прозрачности для третьего фона

    public VideoClip videoClip; // Ваш видеофайл
    public VideoClip videoClip2; // Ваш видеофайл
    public VideoClip videoClip3; // Ваш видеофайл
    public VideoClip videoClip4; // Ваш видеофайл
    public RawImage Screen; // Ссылка на Raw Image для отображения видео
    public RawImage Screen2;
    public RawImage Screen3;
    public RawImage Screen4;


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

            if (totalScore >= 50000 && !videoPlayed)
            {
                videoPlayer.Play();
                Time.timeScale = 0; // Пауза для всей игры
                Screen.enabled = true; // Показываем Raw Image для видео
                videoPlayed = true;
            }
            if (totalScore >= 100000 && !videoPlayed2)
            {
                videoPlayer2.Play();
                Time.timeScale = 0; // Пауза для всей игры
                Screen2.enabled = true; // Показываем Raw Image для второго видео
                videoPlayed2 = true;
            }

            if (totalScore >= 150000 && !videoPlayed3)
            {
                videoPlayer3.Play();
                Time.timeScale = 0; // Пауза для всей игры
                Screen3.enabled = true; // Показываем Raw Image для третьего видео
                videoPlayed3 = true;
            }

            if (totalScore >= 200000 && !videoPlayed4)
            {
                videoPlayer4.Play();
                Time.timeScale = 0; // Пауза для всей игры
                Screen4.enabled = true; // Показываем Raw Image для четвертого видео
                videoPlayed4 = true;
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

        videoPlayer2 = gameObject.AddComponent<VideoPlayer>(); // Добавляем VideoPlayer к объекту
        videoPlayer2.playOnAwake = false;
        videoPlayer2.clip = videoClip2;
        Screen2.texture = videoPlayer2.texture; // Привязываем текстуру видео к Raw Image
        Screen2.enabled = false; // Убедитесь, что Raw Image невидимо

        videoPlayer2.loopPointReached += OnVideoFinished; // Добавляем обработчик события завершения проигрывания видео

        videoPlayer3 = gameObject.AddComponent<VideoPlayer>(); // Добавляем VideoPlayer к объекту
        videoPlayer3.playOnAwake = false;
        videoPlayer3.clip = videoClip3;
        Screen3.texture = videoPlayer3.texture; // Привязываем текстуру видео к Raw Image
        Screen3.enabled = false; // Убедитесь, что Raw Image невидимо

        videoPlayer3.loopPointReached += OnVideoFinished; // Добавляем обработчик события завершения проигрывания видео

        videoPlayer4 = gameObject.AddComponent<VideoPlayer>(); // Добавляем VideoPlayer к объекту
        videoPlayer4.playOnAwake = false;
        videoPlayer4.clip = videoClip4;
        Screen4.texture = videoPlayer4.texture; // Привязываем текстуру видео к Raw Image
        Screen4.enabled = false; // Убедитесь, что Raw Image невидимо

        videoPlayer4.loopPointReached += OnVideoFinished; // Добавляем обработчик события завершения проигрывания видео
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
