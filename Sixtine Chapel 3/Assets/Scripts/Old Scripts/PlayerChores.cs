using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



/*
 * 
 */

public class PlayerChores : MonoBehaviour
{
    
    [TextArea(1, 5)]
    [SerializeField] string info = "This script must be attached to an empty game object, for each day there is a different scene and you need to fill in the tasks of each day in the inspector each, day will start with the first task from the array";
    private int taskTracker; //this is a variable to keep track of the number of the tasks

    private int[] taskChecker = new int[3]; //this is a 1D array to keep track if the tasks are done: 0 for false, 1 for true

    [SerializeField] string[] playerChores = new string[3]; //list the chores in order in the inspector

    [SerializeField] TMP_Text quest; //the text shown in the UI, the task basically

    public bool[] quest_bool;
    void Start()
    {
        quest.text = playerChores[0]; //shows the first task on the screen
    }


    void Update()
    {
        
    }

    public void TaskIsDone()
    {
        taskChecker[taskTracker] = 1;
    }

    public void ChangeTask(int taskNumber)
    {
        quest.text = playerChores[taskNumber];
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
