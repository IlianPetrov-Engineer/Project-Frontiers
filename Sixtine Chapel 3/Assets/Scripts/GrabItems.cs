using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItems : MonoBehaviour
{
    private bool hasItem;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Either actually take the item or destroy it and instantiate another game object as a child to the player 
    private void OnTriggerEnter(Collider other)
    {
        if (!hasItem)
        {
            GameObject newObject = Instantiate(other.gameObject, transform.position + new Vector3(1, 1, 2), Quaternion.identity);
            newObject.transform.SetParent(transform, true);
            newObject.GetComponent<Collider>().enabled = false;
            newObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            other.gameObject.SetActive(false);
            hasItem = true;
        }
    }

}
