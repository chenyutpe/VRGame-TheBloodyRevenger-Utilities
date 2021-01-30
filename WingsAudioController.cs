using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsAudioController : MonoBehaviour
{
   [SerializeField] AudioClip[] clips;
   [SerializeField] AudioSource source;

   public void Play()
   {
       source.clip = clips[Random.Range(0, 6)];
       source.Play();
   }
}
