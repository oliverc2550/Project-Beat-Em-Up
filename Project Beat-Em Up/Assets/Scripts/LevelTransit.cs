using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (other.gameObject.CompareTag("Player"))
        {
            bl_SceneLoader.GetActiveLoader().LoadLevel("Level" + (SceneManager.GetActiveScene().buildIndex + 1) + "Test");
        }
    }
}
