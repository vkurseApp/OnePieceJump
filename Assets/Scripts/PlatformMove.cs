using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float speed; // скорость движения
    public float minX, maxX; // диапазон движения платформы по X
    private bool right; // движение платформы вправо (true) или влево (false)
    private bool landed; // флаг для отслеживания приземления на платформу

    void Update()
    {
        if (right && transform.position.x < maxX) // платформа двигается вправо и позиция платформы < maxX
        {
            transform.position += Vector3.right * speed * Time.deltaTime; // Vector3.right=(1,0,0)
        }
        else if (right) // платформа двигается вправо, но достигла границы maxX
        {
            right = false; // задаём движение платформы влево
        }
        else if (!right && transform.position.x > minX) // платформа двигается влево и позиция платформы > minX
        {
            transform.position += Vector3.left * speed * Time.deltaTime; // Vector3.left=(-1,0,0)
        }
        else if (!right) // платформа двигается влево, но достигла границы minX
        {
            right = true; // задаём движение платформы вправо
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !landed) // Проверяем, что на платформу приземлился игрок и платформа еще не исчезла
        {
            landed = true; // Устанавливаем флаг приземления
            StartCoroutine(DisappearPlatform()); // Запускаем корутину для исчезновения платформы
        }
    }

    IEnumerator DisappearPlatform()
    {
        yield return new WaitForSeconds(1f); // Ждем 1 секунду
        gameObject.SetActive(false); // Выключаем платформу
    }
}
