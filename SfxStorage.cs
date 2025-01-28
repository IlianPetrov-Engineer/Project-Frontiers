using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Represents a sound effects component that holds configuration for individual sound effects.
/// </summary>
[System.Serializable]
public class SfxComponent
{
    /// <summary>
    /// Represents the name of the sound effect (SFX).
    /// This variable is used as an identifier to link and retrieve corresponding audio clips from the SfxStorage.
    /// </summary>
    public string sfxName;

    /// <summary>
    /// A list of audio clips associated with a specific sound effect.
    /// These clips can be randomly selected and played by SfxSource to provide variation in audio playback.
    /// </summary>
    public List<AudioClip> clips;

    /// <summary>
    /// Indicates whether the sound effect should play in a continuous loop.
    /// </summary>
    public bool loop;

    /// <summary>
    /// Represents the volume level for the corresponding sound effect component.
    /// Determines how loud the sound will play, within the range of 0 (muted) to 1 (full volume).
    /// </summary>
    [Range(0, 1)]
    public float volume;
}

/// <summary>
/// A class used to store and organize audio data for sound effects (SFX) in a Unity project.
/// </summary>
/// <remarks>
/// This class holds a collection of <see cref="SfxComponent"/> objects, each of which represents
/// a configuration for a specific sound effect. Each configuration includes properties for
/// the sound effect name, associated audio clips, volume levels, and looping behavior.
/// </remarks>
public class SfxStorage : MonoBehaviour
{
    /// <summary>
    /// A collection of SfxComponent objects, representing the storage for sound effects.
    /// Each SfxComponent contains information about the sound effect name, associated audio clips,
    /// looping behavior, and volume settings. This list is used to organize and access
    /// sound effects for playback in other components.
    /// </summary>
    public List<SfxComponent> allSfx;
}
