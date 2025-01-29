
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float raycastingDistance = 1f;
    [SerializeField] private LayerMask layer;

    [System.Serializable]
    public class Interactible
    {
        public string name; // Name of the interactible (e.g., "Glass", "Plant")
        public GameObject gameObject; // Reference to the interactible GameObject
        public bool isInRange; // Whether the player is in range
        public bool isInteracted; // Whether the interactible has been interacted with
        
    }

    [SerializeField] private List<Interactible> interactibles = new List<Interactible>();

    public PlayerItems playerItems; // Link to the PlayerItems script
    public PlayerChores playerQuest; // Link to the PlayerQuest script

    void Update()
    {
        // Perform raycast to detect interactibles
        Raycast();
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.forward * raycastingDistance, Color.red);

        // Handle interactions when the player presses the "E" key
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleInteractions();         
        }
    }

    /*public void GrabItem(GameObject item)
    {
        bool hasItem = false;
        if (!hasItem)
        {
            GameObject newObject = Instantiate(item, transform.position + new Vector3(1, 1, 2), Quaternion.identity);
            newObject.transform.SetParent(transform, true);
            newObject.GetComponent<Collider>().enabled = false;
            newObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            item.SetActive(false);
            hasItem = true;
        }
    }*/

    private void Raycast()
    {
        RaycastHit hit;

        // Check if the raycast hits an object
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out hit, raycastingDistance, layer))
        {
            string hitName = hit.collider.tag;

            // Update the state of interactibles based on the raycast
            foreach (var interactible in interactibles)
            {
                if (interactible.name == hitName)
                {
                    interactible.isInRange = true;
                }

                else
                {
                    interactible.isInRange = false;
                }
            }
        }
        else
        {
            // Reset all interactibles when no object is hit
            foreach (var interactible in interactibles)
            {
                interactible.isInRange = false;
            }
        }
    }

    private void HandleInteractions()
    {
        foreach (var interactible in interactibles)
        {
            if (interactible.isInRange)
            {
                switch (interactible.name)
                {
                    // Glass interaction
                    case "Glass":
                        playerItems.isCollected[0] = true; // Player picks up the glass
                        Debug.Log("Picked up Glass");
                        playerItems.GrabItem(interactible.gameObject);
                        //playerQuest.ChangeTask();
                        break;

                    // Sink interaction
                    case "Sink":
                        if (playerItems.isCollected[0]) // Player has an empty glass
                        {
                            playerItems.isCollected[1] = true; // Fill the glass with water
                            Debug.Log("Filled Glass with Water");
                            //playerQuest.ChangeTask();
                            playerItems.UpgradeItem(playerItems.collectables[0], "Fill Glass");
                        }
                        break;

                    // Plant interaction
                    case "Plant":
                        if (playerItems.isCollected[1]) // Player has a full glass
                        {
                            interactible.isInteracted = true; // Mark the plant as watered
                            Debug.Log("Watered the Plant");
                            //playerQuest.ChangeTask();
                            playerItems.DisableItem(playerItems.collectables[0]);
                        }
                        break;

                    // Trash interaction
                    case "Trash":
                        interactible.isInteracted = true; // Trash is picked up
                        Debug.Log("Picked up Trash");
                        playerItems.GrabItem(interactible.gameObject);
                        
                        break;

                    // Paintings interaction
                    case "Painting1":
                        playerQuest.quest_bool[2] = true; // Mark Painting 1 as fixed
                        Debug.Log("Fixed Painting 1");
                        break;

                    case "Painting2":
                        playerQuest.quest_bool[3] = true; // Mark Painting 2 as fixed
                        Debug.Log("Fixed Painting 2");
                        break;

                    case "Painting3":
                        playerQuest.quest_bool[4] = true; // Mark Painting 3 as fixed
                        Debug.Log("Fixed Painting 3");
                        break;

                    // Bed interaction
                    case "Bed":
                        if (playerQuest.quest_bool[6]) // Go to bed quest is active
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                            Debug.Log("Going to bed and loading next scene...");
                        }
                        break;

                    // Acid interaction
                    case "Acid":
                        //playerItems.isCollected[2] = true; // Player picks up acid
                        Debug.Log("Picked up Acid");
                        if (playerItems.isCollected[1])
                        {
                            interactible.isInteracted = true;
                            playerItems.DisableItem(playerItems.collectables[1]);
                        }
                        break;

                    // Bathroom key interaction
                    case "KeyBathroom":
                        playerItems.isCollected[3] = true; // Player picks up the key for the kitchen cupboard
                        Debug.Log("Picked up Key for Kitchen Cupboard");
                        playerItems.GrabItem(interactible.gameObject);
                        break;

                    // Kitchen cupboard interaction
                    case "KitchenCupboard":
                        if (playerItems.isCollected[3]) // Player has the key
                        {
                            Debug.Log("Opened Kitchen Cupboard");
                            playerItems.DisableItem(playerItems.collectables[3]);                         
                        }
                        break;

                    case "Lighter fluid":
                        Interactible kitchenCupboard = interactibles.Find(i => i.name == "KitchenCupboard");
                        if ((kitchenCupboard!=null) && kitchenCupboard.isInteracted)
                        {
                            playerItems.isCollected[7] = true;
                            playerItems.GrabItem(interactible.gameObject);
                            Debug.Log("Lighter fluid is picked up");
                        }
                        break;

                    case "Lighter":
                        Interactible laundryCabinet = interactibles.Find(i => i.name == "Laundry Cabinet");
                        if ((laundryCabinet != null) && laundryCabinet.isInteracted)
                        {
                            playerItems.isCollected[6] = true;
                            playerItems.GrabItem(interactible.gameObject);
                            Debug.Log("Lighter is picked up");
                        }
                        break;

                    // Bathroom key interaction
                    case "KeyBedroom":
                        playerItems.isCollected[4] = true; // Player picks up the key for the laundry cabinet
                        Debug.Log("Picked up Key for Laundry Cabinet");
                        playerItems.GrabItem(interactible.gameObject);
                        break;

                    // Kitchen cupboard interaction
                    case "Laundry Cabinet":
                        if (playerItems.isCollected[4]) // Player has the key
                        {
                            Debug.Log("Opened Kitchen Cupboard");
                            playerItems.DisableItem(playerItems.collectables[4]);
                        }
                        break;

                    // Fireplace interaction
                    case "Fireplace":
                        if (playerItems.isCollected[6] && playerItems.isCollected[7]) // Player has lighter and lighter fluid
                        {
                            interactible.isInteracted = true; // Fire is started
                            Debug.Log("Started a Fire in the Fireplace");
                            playerItems.DisableItem(playerItems.collectables[6]);
                            playerItems.DisableItem(playerItems.collectables[7]);
                        }
                        break;

                    // Shovel interaction
                    case "Shovel":
                        playerItems.isCollected[10] = true; // Player picks up the shovel
                        Debug.Log("Picked up Shovel");
                        playerItems.GrabItem(interactible.gameObject);
                        break;

                    // Shed interaction
                    case "Shed":
                        if (playerItems.isCollected[8]) // Player has bolt cutters
                        {
                            playerItems.DisableItem(playerItems.collectables[8]);
                            if (playerItems.isCollected[9]) // Player has shed key
                            {
                                interactible.isInteracted = true; // Shed is unlocked
                                playerItems.DisableItem(playerItems.collectables[9]);
                            }

                            Debug.Log("Cut chains on the Shed");
                        }
                        break;

                    case "Shed key":
                        playerItems.isCollected[9] = true; // Player picks up the Shed key
                        Debug.Log("Picked up the Shed key");
                        playerItems.GrabItem(interactible.gameObject);
                        break;

                    // Hole interaction
                    case "Hole":
                        if (playerItems.isCollected[10]) // Player has the shovel
                        {
                            Debug.Log("Dug a Hole");
                            interactible.isInteracted = true; // Hole is dug
                            playerItems.DisableItem(playerItems.collectables[10]);
                        }
                        break;

                    // Bolt cutters interaction
                    case "BoltCutters":
                        playerItems.isCollected[8] = true; // Player picks up the bolt cutters
                        Debug.Log("Picked up Bolt Cutters");
                        playerItems.GrabItem(interactible.gameObject);
                        break;

                    // Default case for unrecognized interactibles
                    default:
                        Debug.Log("Unrecognized interactible: " + interactible.name);
                        break;
                }
            }
        }
    }
}

