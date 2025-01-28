using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;


[System.Serializable]
public class ConditionElement
{
    public string conditionName;
    public bool state;
}

public class Conditions : MonoBehaviour
{
    public List<ConditionElement> allConditions;

    public void SetConditionTrue(string conditionName)
    {
        try
        {
            allConditions.First(item => item.conditionName == conditionName).state = true;
        }
        catch
        {
            Debug.LogError("Condition " + conditionName + " could not be found");
        }
    }

    public void SetConditionFalse(string conditionName)
    {
        try
        {
            allConditions.First(item => item.conditionName == conditionName).state = false;
        }
        catch
        {
            Debug.LogError("Condition " + conditionName + " could not be found");
        }
    }

    public bool GetCondition(string conditionName)
    {
        return allConditions.First(item => item.conditionName == conditionName).state;
    }

    public List<string> GetConditionNames()
    {
        return allConditions.Select(item => item.conditionName).ToList();
    }
}
