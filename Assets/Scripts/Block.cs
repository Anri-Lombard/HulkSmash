using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Config params
    [SerializeField] AudioClip breakingSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    // Cached reference.
    Level level;
    GameStatus gameStatus;
    Rigidbody2D myRigidBody2d;

    // State Variables.
    [SerializeField] int timesHit; // Serialized for debugging.

    private void Start()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameStatus>();
        if (tag == "Breakable")
        { 
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlaySound();
        if (tag == "Breakable")
        {
            HandleHit();
            gameStatus.AddToScore();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            BreakBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError(gameObject.name + " devoid of any sprites.");
        }
    }

    private void PlaySound()
    {
        AudioSource.PlayClipAtPoint(breakingSound, Camera.main.transform.position);
    }

    private void BreakBlock()
    {
        Destroy(gameObject);
        level.RemoveBlock();
        MakeSparkles();
    }

    private void MakeSparkles()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
