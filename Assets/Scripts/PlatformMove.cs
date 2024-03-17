using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
  public float speed; // скорость движения
  public float minX, maxX; // диапазон движения платформы по X
  private bool right; // движение платформы вправо (true) или влево (false)

  void Update()
  {
    if (right && transform.position.x < maxX) // платформа двигается вправо и позиция платформы < maxX
    {
      transform.position += Vector3.right*speed*Time.deltaTime; // Vector3.right=(1,0,0)
    }
    else if (right) // платформа двигается вправо, но достигла границы maxX
    {
      right = false; // задаём движение платформы влево
    }
    else if (!right && transform.position.x > minX) // платформа двигается влево и позиция платформы > minX
    {
      transform.position += Vector3.left*speed*Time.deltaTime; // Vector3.left=(-1,0,0)
    }
    else if (!right) // платформа двигается влево, но достигла границы minX
    {
      right = true; // задаём движение платформы вправо
    }
  }

}