using System.Collections.Generic;
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
    public PlayerQuest playerQuest; // Link to the PlayerQuest script

    private void Start()
    {
        playerItems = GameObject.FindObjectOfType<PlayerItems>(); //Finds the playerItems script
        playerQuest = GameObject.FindObjectOfType<PlayerQuest>(); //Finds the playerQuest script
    }

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
                        playerItems.item_bool[0] = true; // Player picks up the glass
                        Debug.Log("Picked up Glass");
                        break;

                    // Sink interaction
                    case "Sink":
                        if (playerItems.item_bool[0]) // Player has an empty glass
                        {
                            playerItems.item_bool[1] = true; // Fill the glass with water
                            Debug.Log("Filled Glass with Water");
                        }
                        break;

                    // Plant interaction
                    case "Plant":
                        if (playerItems.item_bool[1]) // Player has a full glass
                        {
                            interactible.isInteracted = true; // Mark the plant as watered
                            Debug.Log("Watered the Plant");
                        }
                        break;

                    // Trash interaction
                    case "Trash":
                        interactible.isInteracted = true; // Trash is picked up
                        Debug.Log("Picked up Trash");
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
                        playerItems.item_bool[2] = true; // Player picks up acid
                        Debug.Log("Picked up Acid");
                        break;

                    // Bathroom key interaction
                    case "KeyBathroom":
                        playerItems.item_bool[3] = true; // Player picks up the key for the kitchen cupboard
                        Debug.Log("Picked up Key for Kitchen Cupboard");
                        break;

                    // Kitchen cupboard interaction
                    case "KitchenCupboard":
                        if (playerItems.item_bool[3]) // Player has the key
                        {
                            playerItems.item_bool[7] = true; // Player picks up the ligher fluid
                            Debug.Log("Opened Kitchen Cupboard");
                        }
                        break;
                        
                        // Bathroom key interaction
                    case "KeyBedroom":
                        playerItems.item_bool[4] = true; // Player picks up the key for the laundry cabinet
                        Debug.Log("Picked up Key for Laundry Cabinet");
                        break;

                    // Kitchen cupboard interaction
                    case "Laundry Cabinet":
                        if (playerItems.item_bool[4]) // Player has the key
                        {
                            playerItems.item_bool[6] = true; // Player picks up the ligher
                            Debug.Log("Opened Kitchen Cupboard");
                        }
                        break;

                    // Fireplace interaction
                    case "Fireplace":
                        if (playerItems.item_bool[6] && playerItems.item_bool[7]) // Player has lighter and lighter fluid
                        {
                            interactible.isInteracted = true; // Fire is started
                            Debug.Log("Started a Fire in the Fireplace");
                        }
                        break;

                    // Shovel interaction
                    case "Shovel":
                        playerItems.item_bool[10] = true; // Player picks up the shovel
                        Debug.Log("Picked up Shovel");
                        break;

                    // Shed interaction
                    case "Shed":
                        if (playerItems.item_bool[8]) // Player has bolt cutters
                        {
                            if(playerItems.item_bool[9]) // Player has shed key
                            {
                                interactible.isInteracted = true; // Shed is unlocked
                            }

                            Debug.Log("Cut chains on the Shed");
                        }
                        break;

                    // Hole interaction
                    case "Hole":
                        if (playerItems.item_bool[10]) // Player has the shovel
                        {
                            Debug.Log("Dug a Hole");
                            interactible.isInteracted = true; // Hole is dug
                        }
                        break;

                    // Bolt cutters interaction
                    case "BoltCutters":
                        playerItems.item_bool[8] = true; // Player picks up the bolt cutters
                        Debug.Log("Picked up Bolt Cutters");
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

