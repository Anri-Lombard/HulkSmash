using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    // Config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float yPush = 10f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // State
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchBallOnMouseClick();
        }
    }

    private void LaunchBallOnMouseClick()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            hasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5f, 5f), yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
            (
            Random.Range(0f, randomFactor),
            Random.Range(0f, randomFactor)
            );
        if (hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
    }
}
