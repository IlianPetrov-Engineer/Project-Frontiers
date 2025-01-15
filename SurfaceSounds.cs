using UnityEngine;
using System.Collections.Generic;
using StarterAssets;
using System.Linq;

/// <summary>
/// Manages the surface tag beneath the player to dynamically assign the appropriate sound list to the Footsteps component's active sound list.
/// </summary>

[RequireComponent(typeof(FirstPersonController))]
[RequireComponent(typeof(Footsteps))]

[System.Serializable]
public class SoundElement
{
    [Tooltip("Surface tag associated with the sound list below.")]
    public string surfaceType;
    [Tooltip("A list of footsteps sounds for the selected surface.")]
    public List<AudioClip> surfaceSounds;
}

public class SurfaceSounds : MonoBehaviour
{
    [Header("Sounds settings")]
    [Space(7)]
    [Tooltip("A sound list contains SoundElement class objects for setting up different tegs with different sounds.")]
    public List<SoundElement> sounds; // A sound list contains SoundElement class objects for setting up different tegs with different sounds
    [Space(7)]
    [Tooltip("A sound list contains default footsteps sound that will be used if the surface doesn't have a surface tag.")]
    public List<AudioClip> defaultSounds; // A sound list contains default footsteps sound that will be used if the surface doesn't have a surface tag
    [Space(14)]
    [Header("Global settings")]
    [Space(7)]
    [Tooltip("The layer mask of the terrain.")]
    public LayerMask layerMask;


    private Footsteps footsteps;

    private void Start()
    {
        footsteps = GetComponent<Footsteps>();
        if (sounds.First(item => item.surfaceType == "__") is null) // If the SoundElements list doesn't have a default value, create it
        {
            SoundElement tempElement = new()
            {
                surfaceSounds = defaultSounds,
                surfaceType = "__"
            };
            sounds.Add(tempElement);
        }
    }

    private void Update()
    {
        if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hitInfo, Mathf.Infinity, layerMask)) // Performs downward raycasting to identify the surface tag beneath the player
        {
            if (hitInfo.collider.TryGetComponent<SurfaceTag>(out SurfaceTag surfaceTag))
            {
                // If the surface has a surface tag, use the corresponding sound list
                ReplaceSounds(sounds.First(item => item.surfaceType == surfaceTag.surfaceTag).surfaceSounds);
            }
            else
            {
                // If the surface doesn't have a surface tag, use the default sound list
                ReplaceSounds(sounds.First(item => item.surfaceType == "__").surfaceSounds);
            }
        }
    }

    private void ReplaceSounds(List<AudioClip> newSounds)
    {
        footsteps.footstepsSounds = newSounds;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, -transform.up * 100);
    }
}
