using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundFXCat { DragonHit, ArrowShot, BowPull, DragonDeath, AirSweep,  PickupCoin, GoldReturn }
    public GameObject audioObject;
    public AudioClip[] dragonHit;
    public AudioClip[] arrowShot;
    public AudioClip[] bowPull;
    public AudioClip[] dragonDeath;
    public AudioClip[] airSweep;
    public AudioClip[] coinClips;
    public AudioClip[] goldReturn;




    public void AudioTrigger(SoundFXCat audioType, Vector3 audioPosition, float volume)
    {
        GameObject newAudio = GameObject.Instantiate(audioObject, audioPosition, Quaternion.identity);
        ControlAudio ca = newAudio.GetComponent<ControlAudio>();
        switch (audioType)
        {
            case (SoundFXCat.DragonDeath):
                ca.myClip = dragonDeath[Random.Range(0, dragonDeath.Length)];
                break;
            case (SoundFXCat.DragonHit):
                ca.myClip = dragonHit[Random.Range(0, dragonHit.Length)];
                break;
            case (SoundFXCat.ArrowShot):
                ca.myClip = arrowShot[Random.Range(0, arrowShot.Length)];
                break;
           
            case (SoundFXCat.AirSweep):
                ca.myClip = airSweep[Random.Range(0, airSweep.Length)];
                break;
            case (SoundFXCat.BowPull):
                ca.myClip = bowPull[Random.Range(0, bowPull.Length)];
                break;
            case (SoundFXCat.PickupCoin):
                ca.myClip = coinClips[Random.Range(0, coinClips.Length)];
                break;
            case (SoundFXCat.GoldReturn):
                ca.myClip = goldReturn[Random.Range(0, goldReturn.Length)];
                break;
        }
        ca.volume = volume;
        ca.StartAudio();
    }
}
