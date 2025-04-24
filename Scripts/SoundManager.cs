using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    // COMPUTER
    public AudioSource computerSoundStart;
    public AudioSource computerSoundOn;

    // ORGANS
    public AudioSource organ1;
    public AudioSource organ2;
    public AudioSource organ3;

    // PILLS
    public AudioSource pillsClose1;
    public AudioSource pillsClose2;
    public AudioSource pillsOpen;
    public AudioSource pillsShake1;
    public AudioSource pillsShake2;
    public AudioSource pillsShake3;

    // SYRINGE
    public AudioSource syringeBreak1;
    public AudioSource syringeBreak2;
    public AudioSource syringeBreak3;

    // WHEELS
    public AudioSource wheels1;
    public AudioSource wheels2;

    // BODY
    public AudioSource body;

    // LIST OF SOUNDS
    List<AudioSource> soundsList = new List<AudioSource>();

    private void Start() {
        soundsList.Add(computerSoundStart);
        soundsList.Add(computerSoundOn);

        soundsList.Add(organ1);
        soundsList.Add(organ2);
        soundsList.Add(organ3);

        soundsList.Add(pillsClose1);
        soundsList.Add(pillsClose2);
        soundsList.Add(pillsOpen);
        soundsList.Add(pillsShake1);
        soundsList.Add(pillsShake2);
        soundsList.Add(pillsShake3);

        soundsList.Add(syringeBreak1);
        soundsList.Add(syringeBreak2);
        soundsList.Add(syringeBreak3);

        soundsList.Add(wheels1);
        soundsList.Add(wheels2);

        soundsList.Add(body);
    }


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void playAudio(AudioSource audio) {
        if(audio.isPlaying == false) {
            audio.Play();
        }
    }

    public void stopAllSounds() {
        foreach (AudioSource audio in soundsList) {
            if (audio.isPlaying) {
                audio.Stop();
            }
        }
    }
}
