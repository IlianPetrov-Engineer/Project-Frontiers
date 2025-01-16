using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{

    [SerializeField] GameObject[] collectables = new GameObject[10]; //the array for the collectables
    private bool[] isCollected = new bool[10]; //the array that checks if the items has been collected

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void HasBeenCollected(int itemNo)
    {
        isCollected[itemNo] = true;
    }
}
