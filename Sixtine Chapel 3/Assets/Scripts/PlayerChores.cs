using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerChores : MonoBehaviour
{
    private int taskTracker; //this is a variable to keep track of the number of the tasks

    private int[] taskChecker = new int[3]; //this is a 1D array to keep track if the tasks are done: 0 for false, 1 for true

    [SerializeField] string[] playerChores = new string[3]; //list the chores in order in the inspector

    [SerializeField] TMP_Text quest; //the text shown in the UI, the task basically

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

    public void ChangeTask()
    {
        quest.text = playerChores[++taskTracker];
    }
}
