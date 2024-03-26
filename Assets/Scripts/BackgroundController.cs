using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public float transparentAlpha = 0.3f;
    public float opaqueAlpha = 1f;

    public VideoPlayer videoPlayer; // ������ �� VideoPlayer
    public VideoPlayer videoPlayer2;
    public VideoPlayer videoPlayer3;
    public VideoPlayer videoPlayer4;

    private bool videoPlayed2 = false;
    private bool videoPlayed3 = false;
    private bool videoPlayed4 = false;
    private bool videoPlayed = false; // ���� ��� ������������ ������������ �����

    private float alphaChangeSpeed = 1f / 3f; // �������� ��������� ������������ �� �������
    // private float currentAlpha1 = 1f; // ������� �������� ������������ ��� ������� ����
    // private float currentAlpha2 = 1f; // ������� �������� ������������ ��� ������� ����
    // private float currentAlpha3 = 1f; // ������� �������� ������������ ��� �������� ����

    public VideoClip videoClip; // ��� ���������
    public VideoClip videoClip2;
    public VideoClip videoClip3;
    public VideoClip videoClip4;

    public RawImage Screen; // ������ �� Raw Image ��� ����������� �����
    public RawImage Screen2;
    public RawImage Screen3;
    public RawImage Screen4;


    void Update()
    {
        int totalScore;
        if (int.TryParse(scoreText.text.Replace("Score: ", ""), out totalScore))
        {

            if (totalScore >= 100000 && !videoPlayed)
            {
                videoPlayer.Play();
                Time.timeScale = 0; // ����� ��� ���� ����
                Screen.enabled = true; // ���������� Raw Image ��� �����
                videoPlayed = true;
            }
            if (totalScore >= 100000 && !videoPlayed2)
            {
                videoPlayer2.Play();
                Time.timeScale = 0; // ����� ��� ���� ����
                Screen2.enabled = true; // ���������� Raw Image ��� ������� �����
                videoPlayed2 = true;
            }

            if (totalScore >= 150000 && !videoPlayed3)
            {
                videoPlayer3.Play();
                Time.timeScale = 0; // ����� ��� ���� ����
                Screen3.enabled = true; // ���������� Raw Image ��� �������� �����
                videoPlayed3 = true;
            }

            if (totalScore >= 200000 && !videoPlayed4)
            {
                videoPlayer4.Play();
                Time.timeScale = 0; // ����� ��� ���� ����
                Screen4.enabled = true; // ���������� Raw Image ��� ���������� �����
                videoPlayed4 = true;
            }
        }
    }

    void Start()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>(); // ��������� VideoPlayer � �������
        videoPlayer.playOnAwake = false;
        videoPlayer.clip = videoClip;
        Screen.texture = videoPlayer.texture; // ����������� �������� ����� � Raw Image
        Screen.enabled = false; // ���������, ��� Raw Image ��������

        videoPlayer.loopPointReached += OnVideoFinished; // ��������� ���������� ������� ���������� ������������ �����

        videoPlayer2 = gameObject.AddComponent<VideoPlayer>(); // ��������� VideoPlayer � �������
        videoPlayer2.playOnAwake = false;
        videoPlayer2.clip = videoClip2;
        Screen2.texture = videoPlayer2.texture; // ����������� �������� ����� � Raw Image
        Screen2.enabled = false; // ���������, ��� Raw Image ��������

        videoPlayer2.loopPointReached += OnVideoFinished; // ��������� ���������� ������� ���������� ������������ �����

        videoPlayer3 = gameObject.AddComponent<VideoPlayer>(); // ��������� VideoPlayer � �������
        videoPlayer3.playOnAwake = false;
        videoPlayer3.clip = videoClip3;
        Screen3.texture = videoPlayer3.texture; // ����������� �������� ����� � Raw Image
        Screen3.enabled = false; // ���������, ��� Raw Image ��������

        videoPlayer3.loopPointReached += OnVideoFinished; // ��������� ���������� ������� ���������� ������������ �����

        videoPlayer4 = gameObject.AddComponent<VideoPlayer>(); // ��������� VideoPlayer � �������
        videoPlayer4.playOnAwake = false;
        videoPlayer4.clip = videoClip4;
        Screen4.texture = videoPlayer4.texture; // ����������� �������� ����� � Raw Image
        Screen4.enabled = false; // ���������, ��� Raw Image ��������

        videoPlayer4.loopPointReached += OnVideoFinished; // ��������� ���������� ������� ���������� ������������ �����
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        vp.Stop(); // ������������� ������������ �����
        Screen.enabled = false; // �������� Raw Image ����� ���������� �����
        Time.timeScale = 1; // ������� ����� � ����
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
