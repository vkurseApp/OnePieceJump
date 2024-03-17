using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // работа с менеджером сцены

public class Restart : MonoBehaviour
{
  public void RestartGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // перезапуск сцены, SceneManager.LoadScene(0)
  }

//  public void ClearScore()
//  {
//    PlayerPrefs.SetInt("score", 1);
//    PlayerPrefs.Save();
//  }

}