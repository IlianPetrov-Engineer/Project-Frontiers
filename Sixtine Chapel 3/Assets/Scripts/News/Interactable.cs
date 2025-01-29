using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public enum DeactivationMode
{
    HoldingMode,
    
    ToggleMode,
    
    OnetimeActivation
}

[System.Serializable]
public class OnCollisionEnterEvent
{
    public List<string> conditionsToMatch;
    public string tag;
    public UnityEvent onCollisionEnter;
}

public class Interactable : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;
    public List<OnCollisionEnterEvent> onCollisionEnterEvents;
    
    [Header("Settings")]
    public DeactivationMode deactivationMode;  // Enum variable
    public float activateHoldingDuration;
    public float deactivateHoldingDuration;
    public bool active;
    
    private bool _outlineEnabled;
    private Outline _outline;
    private Conditions _conditions;
    
    public void Activate()
    {
        onActivate.Invoke();

        switch (deactivationMode)
        {
            case DeactivationMode.OnetimeActivation:
                Destroy(this);
                break;
            case DeactivationMode.ToggleMode or DeactivationMode.HoldingMode:
                active = true;
                break;
        }
    }
    
    public void Deactivate()
    {
        onDeactivate.Invoke();
        active = false;
    }
    
    public void Hover()
    {
        SetOutlineState(true);
    }
    
    private void Start()
    {
        if (active)
        {
            Activate();
        }

        InitializeOutline();
        InitializeConditions();
    }
    
    private void Update()
    {
        // Update the outline state based on its current state
        SetOutlineState(_outlineEnabled);
        _outlineEnabled = false;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        var collisionEvent = onCollisionEnterEvents.Find(
            evt => collision.gameObject.CompareTag(evt.tag));
        
        if (collisionEvent == null) { return; }

        if (collisionEvent.conditionsToMatch.Any())
        {
            if (!collisionEvent.conditionsToMatch.All(condition => _conditions.GetCondition(condition)))
            {
                return;
            }
        }

        collisionEvent?.onCollisionEnter.Invoke();
    }
    
    private void InitializeOutline()
    {
        if (TryGetComponent<Outline>(out var outline))
        {
            _outline = outline;
        }
    }
    
    private void SetOutlineState(bool isEnabled)
    {
        if (_outline)
        {
            _outline.enabled = isEnabled;
        }
    }

    private void InitializeConditions()
    {
        var definedConditions = onCollisionEnterEvents
            .SelectMany(item => item.conditionsToMatch)
            .ToList();
        if (TryGetComponent<Conditions>(out var conditions) && definedConditions.Any())
        {
            _conditions = conditions;
            var allConditions = conditions.GetConditionNames();

            foreach (var condition in definedConditions.Where(condition => !allConditions.Contains(condition)))
            {
                Debug.LogError($"Could not find defined condition {condition} in all conditions.");
            }
        }
    }
}