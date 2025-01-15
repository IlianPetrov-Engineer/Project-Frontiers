using System.Linq;
using UnityEngine;

/// <summary>
/// A simple property script for defining the surface tag associated with the parent game object.
/// </summary>

public class SurfaceTag : MonoBehaviour
{
    [Tooltip("The tag of the surface that will be used for choosing the footsteps sound list.")]
    public string surfaceTag;

    private void Start()
    {
        if (!CheckSoundAvailability(out string error))
        {
            Debug.Log(error);
        }
    }

    private bool CheckSoundAvailability(out string error)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Is there an object with tag Player ni the scene
        if (player != null)
        {
            if (player.TryGetComponent<SurfaceSounds>(out SurfaceSounds surfaceSounds)) // Does the player object contains SurfaceSound
            {
                if (surfaceSounds.sounds.Any(item => item.surfaceType == surfaceTag)) // Does SurfaceSound list contains the selected tag
                {
                    error = null;
                    return true;
                }
                else
                {
                    error = "There is no surface type created that matches the selected surface tag.";
                    return false;
                }
            }
            else
            {
                error = "There is no SurfaceSounds component attached to the Player.";
                return false;
            }
        }
        else
        {
            error = "There is no Player type in the scene.";
            return false;
        }
    }
}
