using UnityEngine;
using UnityEngine.Audio;

//Changelog
/*Inital Script created by Oliver for use on Module 4 Project "Surviving the Edge"
 * Audio Manager Script from <https://www.youtube.com/watch?v=6OT43pvUyfY&ab_channel=Brackeys> timestamp ~ 5:30
 */
[System.Serializable] //Allows the script to be used in the Unity Editor
public class SoundClass
{
    public string Name; //Name of the Audio Clip
    public AudioClip Clip; //Audio Clip
    //Audio Clip Properties
    public bool Loop;
    [Range(0f, 1f)] public float Volume;
    [Range(0.1f, 3f)] public float Pitch;
    [Range(0f, 1f)] public float SpatialBlend;
    [HideInInspector] public AudioSource Source;

}
