using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Parameters.
    [SerializeField] int breakableBlock; //Serialized for debugging.

    // Cached referance.
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        breakableBlock++;
    }

    public void RemoveBlock()
    {
        breakableBlock--;
        if (breakableBlock <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
