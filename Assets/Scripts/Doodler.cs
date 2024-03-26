using System.Collections;
using TMPro;
using UnityEngine;

public class Doodler : MonoBehaviour
{
    public float MoveSpeed; // скорость движения/перемещения по X
    public float JumpForce; // сила прыжка по Y
    public float MoveDecreaser; // замедление движения по X
    private Rigidbody2D rb;
    private Animator anim;
    public TextMeshProUGUI ScoreTxt;
    public GameObject Panel, Player; // ссылка на Canvas.Panel
    public RectTransform ScoreTxtEnd; // ссылка на Canvas.Text(TMP)
    public int score = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // получаем ссылку на компонент "Rigidbody2D"
        anim = GetComponent<Animator>();
        PlayOdaAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (rb.velocity.y <= 0.5f)
            {
                Vector2 Velocity = rb.velocity;
                Velocity.y = JumpForce;
                rb.velocity = Velocity;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GomuGomuFruit"))
        {
            StartCoroutine(ApplyAbilityGomuGomu());
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("GashaGashaFruit"))
        {
            StartCoroutine(ApplyAbilityGashaGasha());
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("MokuMokuFruit"))
        {
            StartCoroutine(ApplyAbilityMokuMoku());
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("HitoHitoFruit"))
        {
            StartCoroutine(ApplyAbilityHitoHito());
            Destroy(other.gameObject);
        }
        if (other.tag == "BossPower" || other.tag == "Sea")
        {
            Panel.SetActive(true); // активируем Canvas.Panel
            ScoreTxtEnd.localPosition = new Vector3(20, 150, 0);
            Destroy(Player);
        }
    }

    IEnumerator ApplyAbilityGomuGomu()
    {
        float originalJumpForce = 25; // Сохраняем оригинальное значение JumpForce
        JumpForce += 10; // Увеличиваем JumpForce на 2

        yield return new WaitForSeconds(5f); // Устанавливаем длительность увеличенного JumpForce

        JumpForce = originalJumpForce; // Возвращаем оригинальное значение JumpForce
    }

    IEnumerator ApplyAbilityGashaGasha()
    {
        float originalMoveSpeed = 3; // Сохраняем оригинальное значение MoveSpeed
        MoveSpeed = 10; // Устанавливаем новое значение MoveSpeed

        yield return new WaitForSeconds(5f); // Устанавливаем длительность увеличенной скорости

        MoveSpeed = originalMoveSpeed; // Возвращаем оригинальное значение MoveSpeed
    }

    IEnumerator ApplyAbilityMokuMoku()
    {
        float originalGravityScale = 2.5f; // Устанавливаем значение гравитации для полета
        rb.gravityScale = 0; // Устанавливаем гравитацию в 0, чтобы персонаж мог летать

        yield return new WaitForSeconds(5f); // Устанавливаем длительность полета

        rb.gravityScale = originalGravityScale; // Возвращаем значение гравитации для персонажа
    }

    IEnumerator ApplyAbilityHitoHito()
    {
        score += 5000; // Увеличиваем количество очков на 5000
        ScoreTxt.text = "Score: " + score.ToString(); // Обновляем текст с количеством очков на канвасе

        yield return null; // Длительность не требуется
    }

    void PlayOdaAnimation()
    {
        anim.Play("oda");
    }

    void Update()
    {
        Vector2 Velocity = rb.velocity;

        if (Input.GetButton("Fire1"))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - transform.position;

            if (Mathf.Abs(direction.x) > 0.1f)
            {
                if (direction.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    Velocity.x = -MoveSpeed;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    Velocity.x = MoveSpeed;
                }
                rb.velocity = Velocity;
            }
            else
            {
                Velocity.x = 0; // Остановка движения по оси X при малом отклонении курсора от центра
                rb.velocity = Velocity;
            }
        }
        else
        {
            Velocity.x *= MoveDecreaser;
            rb.velocity = Velocity;
        }
    }
}
