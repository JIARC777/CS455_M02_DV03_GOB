using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Action
{
    public string actionName;
    public string effectingGoal;
    public float goalEffect;
    public float duration;
    public GameObject taskTarget;
    float timeSinceActionStarted;
    
    public Action(string actionName,string effectingGoal, float goalEffect, float duration, GameObject taskTarget)
    {
        this.actionName = actionName;
        this.effectingGoal = effectingGoal;
        this.goalEffect = goalEffect;
        this.duration = duration;
        this.taskTarget = taskTarget;
    }
    public virtual float GetGoalChange(Goal goal) {
        return ((effectingGoal == goal.goalName) ? goalEffect : 0); 
    }
    public float GetDuration() { return duration;}

    public void PerformAction(List<Goal> goals, AI character)
    {
        foreach (Goal g in goals)
        {
            // Check if the action has an effect on the given goal
            if (g.goalName == effectingGoal)
            {
                 g.SatisfyGoal(this);
                 MoveToTarget(character);
            }
        }
    }
    public void MoveToTarget(AI character)
    {
        character.transform.position = (taskTarget.transform.position - new Vector3(0, -0.5f, 0));
    }
}
*/
public class Action
{
    public string actionName;
    public string effectingGoal;
    public float goalEffect;
    public float duration;

    public Action(string actionName, string effectingGoal, float goalEffect, float duration)
    {
        this.actionName = actionName;
        this.effectingGoal = effectingGoal;
        this.goalEffect = goalEffect;
        this.duration = duration;
    }
    public virtual float GetGoalChange(Goal goal)
    {
        return ((effectingGoal == goal.goalName) ? goalEffect : 0);
    }
    public float GetDuration() { return duration; }

    public void PerformAction(List<Goal> goals)
    {
        foreach (Goal g in goals)
        {
            // Check if the action has an effect on the given goal
            if (g.goalName == effectingGoal)
            {
                g.SatisfyGoal(this);
            }
        }
    }
}