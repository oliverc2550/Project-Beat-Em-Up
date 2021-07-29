using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Audio Manager Script from <https://www.youtube.com/watch?v=6OT43pvUyfY&ab_channel=Brackeys> timestamp ~ 4:00
public class AudioManager : MonoBehaviour
{
    public SoundClass[] Sounds;
    public static AudioManager Instance;

    void Awake()
    {
        if(Instance == null) //Null check
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); //If there are multiple in the scene destroy the duplicate
            return;
        }
        DontDestroyOnLoad(gameObject); //Set the audioManager to not be destroyed between scenes

        foreach(SoundClass s in Sounds) //Iterate over each entry in the Sounds array
        {
            //Set the AudioSource properties based on the properties set in the SoundClass
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.loop = s.Loop;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.spatialBlend = s.SpatialBlend;
        }
    }
    private void Start()
    {
        Play("BackgroundNoise"); //Play the theme song after the game starts
        Play("ThemePlaceholder");
    }
    //Method used to play audio clip that corresponds to the string name passed in. Audio clip string name is set in the inspector.
    public void Play(string name)
    {
        SoundClass s = Array.Find(Sounds, sound => sound.Name == name); //Finds the specific audio clip to play
        if(s == null) //Null check
        {
            Debug.Log("That sound doesn't exist");
            return;
        }
        s.Source.Play(); //Plays the audio clip
    }
}
