using UnityEngine;
using System.Linq;


/// <summary>
/// SfxSource is responsible for managing the playback of sound effects associated with a given SfxStorage.
/// This component requires an AudioSource and works in conjunction with SfxStorage to play, loop, or stop audio clips.
/// </summary>
/// <remarks>
/// The SfxSource component serves as a bridge between an SfxStorage instance and Unity's AudioSource.
/// It allows playback of sound effects based on their names, defined in the associated SfxStorage.
/// Ensure that the SfxStorage is properly set and contains valid data.
/// </remarks>
/// <example>
/// SfxSource should be attached to a GameObject that also has an AudioSource component.
/// Assign an appropriate SfxStorage to configure sound effects. Use methods such as Play and Stop to manage the audio playback.
/// </example>
[RequireComponent(typeof(AudioSource))]

public class SfxSource : MonoBehaviour
{
    /// <summary>
    /// Holds a reference to an instance of <see cref="SfxStorage"/> which contains
    /// a collection of sound effects (SFX) configurations.
    /// </summary>
    /// <remarks>
    /// This variable is utilized by <see cref="SfxSource"/> to access sound effect components
    /// for playback functionality, including properties such as audio clips, volume, and looping settings.
    /// If not explicitly assigned, the component will attempt to find the first available
    /// instance of <see cref="SfxStorage"/> in the scene at runtime.
    /// </remarks>
    public SfxStorage sfxStorage = null;

    /// <summary>
    /// Represents the AudioSource component attached to the GameObject.
    /// This variable is used to manage and play audio clips using settings like volume, looping, and clip selection.
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// Initializes the SfxSource component. If the SfxStorage is not assigned,
    /// it attempts to find the first object of type SfxStorage in the scene.
    /// Additionally, it retrieves the AudioSource component attached to this
    /// GameObject.
    /// </summary>
    private void Start()
    {
        if (sfxStorage == null)
        {
            sfxStorage = FindFirstObjectByType<SfxStorage>();
        }
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays an audio effect corresponding to the specified sound effect name.
    /// </summary>
    /// <param name="sfxName">The name of the sound effect to play. Must match an entry in the SfxStorage.</param>
    public void Play(string sfxName)
    {
        try
        {
            var activeSfxComponent = sfxStorage.allSfx.First(item => item.sfxName == sfxName);
            audioSource.volume = activeSfxComponent.volume;
            if (activeSfxComponent.loop)
            {
                audioSource.loop = true;
                audioSource.clip = activeSfxComponent.clips[Random.Range(0, activeSfxComponent.clips.Count)];
                audioSource.Play();
                return;
            }

            audioSource.loop = false;
            audioSource.PlayOneShot(activeSfxComponent.clips[Random.Range(0, activeSfxComponent.clips.Count)]);
        }
        catch
        {
            Debug.LogError("Selected SFX Storage does not contain the selected SFX Name. Object: " + gameObject.name);
        }
    }

    /// <summary>
    /// Stops playback of the current audio being played by the AudioSource component.
    /// </summary>
    public void Stop() { audioSource.Stop(); }
}