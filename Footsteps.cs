using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays footstep sounds dynamically based on the active sound list.
/// </summary>

public class Footsteps : MonoBehaviour
{
    [Tooltip("The delay between the footsteps sounds.")]
    public float footstepsDelay = 1;

    [HideInInspector]
    public bool walking = false;
    [HideInInspector]
    public float sprinting = 1;
    [HideInInspector]
    public List<AudioClip> footstepsSounds;

    private AudioSource audioSource;
    private Coroutine activeCoroutine;

    private void Start()
    {
        if (TryGetComponent<AudioSource>(out AudioSource _audioSource))
        {
            _audioSource.loop = false;
            _audioSource.playOnAwake = false;
            audioSource = _audioSource;
        }
        else
        {
            Debug.LogError("There is no audio source attached to the Player");
        }
    }

    private void Update()
    {
        // Starting the sound loop coroutine if the player moves and there is no active coroutine

        if (walking && activeCoroutine is null)
        {
            activeCoroutine = StartCoroutine(PlayFootstepsSounds());
        }
        else if (!walking && activeCoroutine is not null) // Stopping the coroutine when the player stops
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }
    }

    IEnumerator PlayFootstepsSounds()
    {
        while (true)
        {
            audioSource.Stop();
            audioSource.clip = footstepsSounds[Random.Range(0, footstepsSounds.Count)]; // Pick the random sound from the active sound list
            audioSource.Play();
            yield return new WaitForSeconds(footstepsDelay / sprinting); // Choosing the appropriate delay based on the player movement speed 
        }
    }
}
