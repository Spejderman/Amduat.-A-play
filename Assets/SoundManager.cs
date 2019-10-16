﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ScrollManager;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    FMOD.Studio.System system;

    [SerializeField]
    private int hour;

    //AMBIENCE
    private string oceanAmbPath = "event:/HOUR 7/Ocean";
    public FMOD.Studio.EventInstance oceanAmbInstance;

    //MUSIC
    private string showdownMuPath = "event:/MUSIC/Showdown";
    public FMOD.Studio.EventInstance showdownMuInstance;

    //Hour 7 Sounds
    private string apopisIdlePath = "event:/HOUR 7/ApopisTiredIdle";
    public FMOD.Studio.EventInstance apopisIdleInstance;

    private string spearReadyPath = "event:/HOUR 7/SpearReady";
    public FMOD.Studio.EventInstance spearReadyInstance;

    private string spearHitPath = "event:/HOUR 7/SpearHit";
    public FMOD.Studio.EventInstance spearHitInstance;

    private string spearMissPath = "event:/HOUR 7/SpearMiss";
    public FMOD.Studio.EventInstance spearMissInstance;
    FMOD.Studio.PLAYBACK_STATE spearMissPlaybackState;
    bool spearIsNotPlaying;

    private string spearChargePath = "event:/HOUR 7/SpearCharge";
    public FMOD.Studio.EventInstance spearChargeInstance;
    

    private void Awake()
    {
        CheckInstance();
    }

    private void Start()
    {
        system = FMODUnity.RuntimeManager.StudioSystem;

        CreateSoundInstances();

        SetHour(6);

        if(hour == 6) {
            oceanAmbInstance.start();
            apopisIdleInstance.start();
            spearChargeInstance.start();
        }
    }

    private void Update()
    {
        //INPUTS FOR TESTING
        if (Input.GetKeyDown(KeyCode.A)) {
        }

        if (Input.GetKeyDown(KeyCode.S)) {
        }


        //UPDATES FOR SPECIFIC HOURS

        //HOUR 7
        if(GetHour() == 6) {
            spearMissInstance.getPlaybackState(out spearMissPlaybackState);
            spearIsNotPlaying = spearMissPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING;
            print(spearIsNotPlaying + " is ");
        }
    }

    public int GetHour() {
        return hour;
    }

    public void SetHour(int currentHour) {
        hour = currentHour;
    }

    void CreateSoundInstances() { 
        //AMBIENCE INSTANCES
        oceanAmbInstance = FMODUnity.RuntimeManager.CreateInstance(oceanAmbPath);

        //MUSIC INSTANCES
        showdownMuInstance = FMODUnity.RuntimeManager.CreateInstance(showdownMuPath);

        //HOUR 7 SFX INSTANCES
        apopisIdleInstance = FMODUnity.RuntimeManager.CreateInstance(apopisIdlePath);
        spearReadyInstance = FMODUnity.RuntimeManager.CreateInstance(spearReadyPath);
        spearHitInstance = FMODUnity.RuntimeManager.CreateInstance(spearHitPath);
        spearMissInstance = FMODUnity.RuntimeManager.CreateInstance(spearMissPath);
        spearChargeInstance = FMODUnity.RuntimeManager.CreateInstance(spearChargePath);
    }

    void CheckInstance()
    {
        //Checking that only one instance exists
        if (Instance == null)
        {
            //Instance = FindObjectOfType<SoundManager>();

            if (Instance == null)
            {
                Instance = this;
            }
            /*else
            {
                Destroy(this);
            }*/
        }
    }

    public void PlaySpearMiss(float _charge) {
        if (spearIsNotPlaying)
        {
            spearMissInstance.setParameterByName("Pitch", _charge);
            spearMissInstance.start();
        }
    }
}
