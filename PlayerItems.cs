using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static PlayerInteraction;
using static UnityEditor.Progress;

/*
 * This script must be attached to an empty game object in the scene
 * it is in charge with grabbing the items and upgrading them
 */

public class PlayerItems : MonoBehaviour
{

    public IamJustAnItem[] collectables = new IamJustAnItem[11]; //the array with the elements that can be grabbed aka from the player's hand
    public bool[] isCollected = new bool[11]; //the array that checks if the items have been collected
    public FirstPersonController player;
    //[SerializeField] List<IamJustAnItem> grabbedItems = new List<IamJustAnItem>();
    void Start()
    {
        //player = GameObject.FindWithTag("Player");
    }


    void Update()
    {
        
    }

    public void GrabItem(GameObject item)
    {
        bool hasItem = false;
        if (!hasItem)
        {
            for (int i = 0; i < collectables.Length; i++)
            {
                if (item.name == collectables[i].itemName)
                {
                    item.SetActive(false);
                    collectables[i].gameObject.SetActive(true);
                    hasItem = true;
                    if (isCollected[6])
                    {
                        hasItem = false;
                    }
                    break;
                }
            }

            

            /*GameObject newObject = Instantiate(item, player.transform.position + new Vector3(1, 1, 2), Quaternion.identity);
            newObject.transform.SetParent(player.transform, true);
            newObject.GetComponent<Collider>().enabled = false;
            newObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            item.SetActive(false);
            hasItem = true;*/

            /**/
        }
    }

  
    public void DisableItem(IamJustAnItem item)
    {
        item.gameObject.SetActive(false);
    }

    //this method is for upgrading the grabbed item (eg empty glass -> full glass)
    //it will play the corresponded animation for each item
    public void UpgradeItem(IamJustAnItem item, string animationToPlay)
    {
        item.clip.Play(animationToPlay);
        
        
        
        /*foreach (var grabbed in collectables)
        {
            switch (item)
            {
                case "Glass":
                    item.gameObject.SetActive(false);
                    if (grabbed.itemName == "Full glass")
                    {
                        grabbed.gameObject.SetActive(true);
                    }
                    break;

            }
        }*/

    }
}
