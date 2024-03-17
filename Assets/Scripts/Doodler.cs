using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodler : MonoBehaviour
{
    public float MoveSpeed; // скорость движения/перемещения по X
    public float JumpForce; // сила прыжка по Y
    public float MoveDecreaser; // замедление движения по X
    private Rigidbody2D rb;

    public Transform fruct1, fruct2, fruct3, fruct4; // префабы фруктов


    private Animator anim;

    IEnumerator ApplyAbility1()
    {
        // Логика применения способности 1 (Гому-Гому Фрукт)
        // Применяем способность
        yield return new WaitForSeconds(1f); // Пример времени действия способности
                                             // Возвращаем персонажу обычные характеристики
    }

    IEnumerator ApplyAbility2()
    {
        // Логика применения способности 2 (Вапу но Ми)
        // Применяем способность
        yield return new WaitForSeconds(1f); // Пример времени действия способности
                                             // Возвращаем персонажу обычные характеристики
    }

    IEnumerator ApplyAbility3()
    {
        // Логика применения способности 3 (Фува но Ми)
        // Применяем способность
        yield return new WaitForSeconds(1f); // Пример времени действия способности
                                             // Возвращаем персонажу обычные характеристики
    }

    IEnumerator ApplyAbility4()
    {
        // Логика применения способности 4 (Ой ой но Ми)
        // Применяем способность
        yield return new WaitForSeconds(1f); // Пример времени действия способности
                                             // Возвращаем персонажу обычные характеристики
    }




    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // получаем ссылку на компонент "Rigidbody2D"
        anim = GetComponent<Animator>();
        PlayOdaAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) // Проверяем тэг объекта, чтобы определить, что это платформа
        {
            if (rb.velocity.y <= 0.5f) // Проверка отрицательной скорости по Y (вертикальной скорости) объекта, т.е. падает ли объект
            {
                Vector2 Velocity = rb.velocity; // Velocity = вектор текущей скорости объекта
                Velocity.y = JumpForce; // составляющая Y Velocity = сила прыжка
                rb.velocity = Velocity; // Задаём вектор текущей скорости объекта
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fruct")) // Проверяем тэг объекта, чтобы определить, что это фрукт
        {
            Vector2 currentVelocity = rb.velocity; // Сохраняем текущую скорость перед изменением

            Transform fruct = other.transform; // Объявляем переменную fruct и присваиваем ей значение other.transform

            if (fruct == fruct1) // Проверяем, с каким фруктом столкнулся персонаж
            {
                StartCoroutine(ApplyAbility1()); // Применяем способность 1 (Гому-Гому Фрукт)
            }
            else if (fruct == fruct2)
            {
                StartCoroutine(ApplyAbility2()); // Применяем способность 2 (Вапу но Ми)
            }
            else if (fruct == fruct3)
            {
                StartCoroutine(ApplyAbility3()); // Применяем способность 3 (Фува но Ми)
            }
            else if (fruct == fruct4)
            {
                StartCoroutine(ApplyAbility4()); // Применяем способность 4 (Ой ой но Ми)
            }

            Destroy(other.gameObject); // Уничтожаем фрукт после столкновения

            rb.velocity = currentVelocity; // Восстанавливаем сохраненную скорость после уничтожения фрукта
        }
    }





    void PlayOdaAnimation()
    {
        anim.Play("oda"); // Проигрываем анимацию "oda"
    }

    void Update()
    {
        Vector2 Velocity = rb.velocity;

        if (Input.GetButton("Fire1"))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - transform.position;

            if (Mathf.Abs(direction.x) > 0.1f) // Проверка на значительное отклонение позиции мыши по X от центра персонажа
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