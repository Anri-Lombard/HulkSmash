using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loseCollider : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("GameOver");
        //Want to avoid strings.
    }
}