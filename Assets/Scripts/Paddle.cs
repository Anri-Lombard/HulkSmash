using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Configuration parameters.
    [SerializeField] float screenWidth;
    [SerializeField] float minx;
    [SerializeField] float maxx;

    // Cached reference.
    GameStatus gameStatus;
    Ball theBall;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(Mathf.Clamp(GetXPos(), minx + 1, maxx - 1), transform.position.y);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (gameStatus.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidth;
        }
    }
}
