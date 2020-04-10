using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    // number of "minutes" elapsed
    int elapsedTimeUnits;
    // number of seconds that corresponds to one "minute"
    float timeScaleFactor = 0.01f;
    //float timeScaleFactor = Time.deltaTime;
    // Base decision making rate of the AI. After the first choice, time to complete task takes over as rate
    int decisionMakingRate = 10;
    // Value to keep track of when the current time unit started
    float lastTimeStamp;
    // This factor amplifies the postive effects of a lower score over the negative effects of waiting
    float prioritizationFactor = 1.1f;

    List<Action> actions;
    List<Goal> goals;
    //float discontent;
    void Start()
    {
        // Initialize Goals
        goals = new List<Goal>();
        Goal eat = new Goal("Eat", 1f, 0.05f);
        Goal bathroom = new Goal("Bathroom", 1f, 0.05f);
        Goal tiredness = new Goal("Tired", 1f, 0.05f);
        Goal getWorkDone = new Goal("Work To Do", 1f, 0.05f);
        goals.Add(eat);
        goals.Add(bathroom);
        goals.Add(tiredness);
        goals.Add(getWorkDone);

        // Initialize Actions
        actions = new List<Action>();
        Action getMeal = new Action("Get Meal", "Eat", 10f, 25f);
        Action getSnack = new Action("Get Snack", "Eat", 1f, 10f);
        Action useRestroom = new Action("Use Restroom", "Bathroom", 8f, 10f);
        Action powerNap = new Action("Power Nap", "Tired", 7f, 15f);
        Action goToSleep = new Action("Go To Sleep", "Tired", 30f, 90f);
        Action stareAtAssignment = new Action("Stare At Assignment", "Work To Do", 2f, 10f);
        Action actuallyDoWork = new Action("Actually Do Work", "Work To Do", 20f, 30f);
        actions.Add(getMeal);
        actions.Add(getSnack);
        actions.Add(useRestroom);
        actions.Add(powerNap);
        actions.Add(goToSleep);
        actions.Add(stareAtAssignment);
        actions.Add(actuallyDoWork);
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)Time.timeSinceLevelLoad >= lastTimeStamp + timeScaleFactor)
        {

            lastTimeStamp = Time.timeSinceLevelLoad;
            elapsedTimeUnits++;
            // increment the incentive to complete each goal
            foreach (Goal g in goals)
            {
                g.IncreaseNeed();
                Debug.Log("Goal: " + g.goalName + " Current Need: " + g.value.ToString("F2"));
            }
            // Every 10 "minutes" make a decision
            Action bestAction;
            if (elapsedTimeUnits % decisionMakingRate == 0)
            {
                bestAction = ChooseAction(actions, goals);
                Debug.Log("Action Chosen: " + bestAction.actionName);
                decisionMakingRate = (bestAction.duration > 10) ? (int)bestAction.duration : 10;
                // Once a decision has been chosen, the AI cannot make another decision until the task is complete or after 10 minutes for short tasks
                Debug.Log(bestAction.actionName + " will take " + bestAction.duration + " minutes");
                // Once the action has been chosen, be sure that values are modified
                bestAction.PerformAction(goals);
            }
        }
    }

    public Action ChooseAction(List<Action> actions, List<Goal> goals)
    {
        Action bestAction = null;
        float bestValue = Mathf.Infinity;

        Debug.Log("Making Decision: ");
        foreach (Action action in actions)
        {
            Debug.Log("Checking Action: " + action.actionName);
            float currentValue = Discontentment(action, goals);
            if (currentValue < bestValue)
            {
                bestValue = currentValue;
                bestAction = action;
            }
        }
        return bestAction;
    }
    public float Discontentment(Action action, List<Goal> goals)
    {
        foreach (Goal goal in goals)
        {
            //goalValue = Mathf.sqrt()
            float newValue = Mathf.Max(goal.value - action.GetGoalChange(goal) * prioritizationFactor, 0);
            newValue += action.GetDuration() * goal.GetChange();
            discontent += goal.GetDiscontentment(newValue);
        }
        Debug.Log("Discontent from " + action.actionName + ": " + discontent);
        return discontent;
    }
}