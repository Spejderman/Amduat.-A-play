﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollManager;

public class Hour12_SceneManager : Timeline_BaseClass
{

    //character actions
    public GameObject moveCharacters;
    private Vector3 charactersStartPos = new Vector3(-3.5f, 0,0);
    private Vector3 charactersEndPos = new Vector3 (5, 0, 0);

    //cam Actions
    public GameObject Cam;
    private Vector3 camPosStart = new Vector3(0, 0, -10);
    private Vector3 camPosEnd = new Vector3(10, 0, -10);

    public SpriteRenderer IsisSprite;

    public BackGround_Object_Movement_Script dustballMovement;
    public IsisAnimationScript IsisAnim;

    public Animator anim;

    float maxRotateSpeed = -0.4f;
    // Start is called before the first frame update
    void Start()
    {

        anim.speed = 0f;

        AddTimelineEvent(0.7f, IsisAction);
        AddTimelineEvent(0.99f, IsisAnim.startAnim);
        //sidste ting i start()
        HandleKeys();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Scroll.scrollValueAccelerated();


        if (input < 0)
        {
            anim.speed = 0.7f;
            float speed = Mathf.Abs(Scroll.scrollValueAccelerated(0.99999f)) * 0.8f * Time.deltaTime;
            speed = Mathf.Clamp(speed, 0, 0.001f);
            Timeline += speed;
            Timeline = Mathf.Clamp(Timeline, 0, 1);
            dustballMovement.rotateSpeed -= ((Time.deltaTime * speed)*150);
            if (dustballMovement.rotateSpeed <= maxRotateSpeed)
            {
                dustballMovement.rotateSpeed = maxRotateSpeed;
            }


            CharacterActions();
            CamActions();
        }
        if (input == 0)
        {
            anim.speed = 0f;
            if (dustballMovement.rotateSpeed <= 0)
            {
                dustballMovement.rotateSpeed = 0;

            }
        }

    }


    void CharacterActions()
    {

        moveCharacters.transform.position = Vector3.Lerp(charactersStartPos, charactersEndPos, Timeline);

    }

    void CamActions()
    {

        Cam.transform.position = Vector3.Lerp(camPosStart, camPosEnd, Timeline);


    }

    void IsisAction()
    {

        StartCoroutine(FadeIsis(1, 3));

    }


    IEnumerator FadeIsis(float value, float time)
    {

        float startValue = 0;
        float startTime = Time.time;

        while (IsisSprite.color.a < value)
        {
            float t = (Time.time - startTime) / time;
            Color newColor = new Color(IsisSprite.color.r, IsisSprite.color.g, IsisSprite.color.b, Mathf.Lerp(startValue, value, t));
            IsisSprite.color = newColor;
            yield return null;
        }

    }


}