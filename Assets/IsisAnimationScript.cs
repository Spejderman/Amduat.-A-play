﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsisAnimationScript : MonoBehaviour
{

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0f;

    }

    public void startAnim()
    {

        anim.speed = 0.2f;

    }
}
