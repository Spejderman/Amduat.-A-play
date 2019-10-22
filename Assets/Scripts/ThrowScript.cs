﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{

    private IEnumerator throwKnifeMovement;
    private IEnumerator bounceKnife;

    private Vector3 target;

    [SerializeField]
    private float speedScalar;


    //flags
    bool isThrown = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Snake"))
        {
            StopCoroutine(throwKnifeMovement);
            KnifeHit();
            this.transform.parent = other.transform;
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(this,2);
        }
        else if (other.CompareTag("Knife"))
        {
            print("Bounce knife");
            SoundManager.Instance.knifeClangInstance.start();
        }

    }

    public void ThrowKnife(Vector3 target)
    {
        if (!isThrown)
        {
            SoundManager.Instance.knifeThrowInstance.start();
            SoundManager.Instance.showdownMuInstance.setParameterByName("Intensity", 1);

            print("throw!");
            target -= this.transform.position;
            // target override. Change later
            target = Vector3.down;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, target);

            throwKnifeMovement = ThrowKnifeMovement(target.normalized);
            StartCoroutine(throwKnifeMovement);
            isThrown = true;
        }
    }

    IEnumerator ThrowKnifeMovement(Vector3 target)
    {
        while (true)
        {
            this.transform.position += target * speedScalar * Time.deltaTime;
            yield return null;
        }
    }

    public void KnifeHit()
    {
        SoundManager.Instance.knifeHitInstance.start();
        EventManager.KnifeHitEvent();
        // transform.root.GetComponent<KnifeSpawner>().SpawnKnife();
    }
}
