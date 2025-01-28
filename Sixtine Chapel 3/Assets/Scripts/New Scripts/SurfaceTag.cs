using StarterAssets;
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
            Debug.LogError(error);
        }
    }

    private bool CheckSoundAvailability(out string error)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Is there an object with tag Player ni the scene
        if (player != null)
        {
            if (player.GetComponent<FirstPersonController>().footstepsSounds.Any(item => item.surfaceType == surfaceTag)) // Does SurfaceSound list contains the selected tag
            {
                error = null;
                return true;
            }
            else
            {
                error = $"There is no surface type created that matches the selected surface tag - \"{surfaceTag}\" for the object {gameObject}.";
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
