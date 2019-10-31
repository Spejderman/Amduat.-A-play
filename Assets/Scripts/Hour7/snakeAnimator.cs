﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeAnimator : MonoBehaviour
{

    public SpearAnimation SA;
    public AnimationClip SnakeIdle;
    public AnimationClip SnakeDie;
    float animSpeed = 0.5f;
    float animSpeedDead = 0.7f;

    Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        SA = FindObjectOfType<SpearAnimation>();

        anim = GetComponent<Animation>();
        
        
        anim.clip = SnakeIdle;
        anim["SnakeIdle"].speed = animSpeed;
        anim.Play();

    }

    private void Update()
    {
        if (SA.stapDone)
        {
            anim["SnakeDead"].speed = animSpeedDead;
            anim.CrossFade("SnakeDead", 5F, PlayMode.StopSameLayer);
        }
    }


}