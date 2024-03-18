using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Doodler : MonoBehaviour
{
    public float MoveSpeed; // скорость движения/перемещения по X
    public float JumpForce; // сила прыжка по Y
    public float MoveDecreaser; // замедление движения по X
    private Rigidbody2D rb;
    private Animator anim;
    public Transform GomuGomuFruit, GashaGashaFruit, MokuMokuFruit, HitoHitoFruit; // префабы фруктов
    public TextMeshProUGUI ScoreTxt;
    public int score;


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
    }

    IEnumerator ApplyAbilityGomuGomu()
    {
        float originalJumpForce = 25; // Сохраняем оригинальное значение JumpForce
        JumpForce += 2; // Увеличиваем JumpForce на 2
        ScoreTxt.text = "Score: " + score.ToString();

        yield return new WaitForSeconds(5f); // Устанавливаем длительность увеличенного JumpForce

        JumpForce = originalJumpForce; // Возвращаем оригинальное значение JumpForce
    }

    IEnumerator ApplyAbilityGashaGasha()
    {
        yield return new WaitForSeconds(1f);// Устанавливаем длительность
    }

    IEnumerator ApplyAbilityMokuMoku()
    {
        yield return new WaitForSeconds(1f);// Устанавливаем длительность
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
