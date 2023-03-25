using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    [Tooltip("The layers that cause the sound to play")]
    public LayerMask collisionTriggers = ~0;
    [Tooltip("Source to play sound from")]
    public AudioSource source;
    //[Tooltip("Source to play sound from")]
    //public AudioClip clip;
    [Space]
    [Tooltip("Source to play sound from")]
    public AnimationCurve velocityVolumeCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [SerializeField] private Collider[] sortedColliderArray;
    [SerializeField] private AudioClip[] sortedAudioclipArray;

    public float volumeAmp = 0.8f;
    public float velocityAmp = 0.5f;
    public float soundRepeatDelay = 0.2f;

    bool canPlaySound = true;
    Coroutine playSoundRoutine;

    private void Start()
    {
        //So the sound doesn't play when falling in place on start
        StartCoroutine(SoundPlayBuffer(1f));
    }

    private void OnDisable()
    {
        if (playSoundRoutine != null)
            StopCoroutine(playSoundRoutine);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (canPlaySound && collisionTriggers == (collisionTriggers | (1 << collision.gameObject.layer)))
        {
            if (source != null && source.enabled)
            {
                foreach(Collider collider in sortedColliderArray)
                {
                    if(collision.contacts[0].thisCollider == collider)
                    {
                        int soundIndex = Array.IndexOf(sortedColliderArray, collider);
                        AudioClip clip = sortedAudioclipArray[soundIndex];

                        if (clip != null || source.clip != null)
                            source.PlayOneShot(clip == null ? source.clip : clip, velocityVolumeCurve.Evaluate(collision.relativeVelocity.magnitude * velocityAmp) * volumeAmp);
                        if (playSoundRoutine != null)
                            StopCoroutine(playSoundRoutine);
                        playSoundRoutine = StartCoroutine(SoundPlayBuffer());
                    }
                }

                //foreach (ContactPoint c in collision.contacts)
                //{
                //    if(c.thisCollider == )
                //}
            }
        }
    }

    IEnumerator SoundPlayBuffer()
    {
        canPlaySound = false;
        yield return new WaitForSeconds(soundRepeatDelay);
        canPlaySound = true;
        playSoundRoutine = null;
    }

    IEnumerator SoundPlayBuffer(float time)
    {
        canPlaySound = false;
        yield return new WaitForSeconds(time);
        canPlaySound = true;
        playSoundRoutine = null;
    }
}
