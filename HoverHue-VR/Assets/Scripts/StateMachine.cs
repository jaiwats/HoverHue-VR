using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private Enum states;
    private Enum events;
    private Enum CurrentState;

    class Outcomes
    {
        public Dictionary<Enum, Enum> outcomeMap;
        public Outcomes()
        {
            outcomeMap = new Dictionary<Enum, Enum>();
        }
    }

    Dictionary<Enum, Outcomes> transitions;

    public StateMachine(Enum states, Enum events, Enum initailStates)
    {
        this.states = states;
        this.events = events;
        this.CurrentState = initailStates;

        Debug.Log("Number of staes = " + Enum.GetValues(states.GetType()).Length + " Number of events = " + Enum.GetValues(events.GetType()).Length + " Innital stae is " + initailStates);

        transitions = new Dictionary<Enum, Outcomes>();
    }

    public void AddEvent(Enum instate, Enum outstate, Enum TriggerEvent)
    {
        if (!transitions.ContainsKey(instate))
        {
            transitions.Add(instate, new Outcomes());
        }
        Outcomes outcomes = transitions[instate];

        if (!outcomes.outcomeMap.ContainsKey(TriggerEvent))
        {
            outcomes.outcomeMap.Add(TriggerEvent, outstate);
        }

        else
        {
            Debug.Log("Wrong" + TriggerEvent + "Out" + outstate);
            outcomes.outcomeMap[TriggerEvent] = outstate;
        }

    }

    public void HandleEvent(Enum ev)
    {
        if (transitions.ContainsKey(CurrentState))
        {
            Outcomes outcomes = transitions[CurrentState];
            if (outcomes.outcomeMap.ContainsKey(ev))
            {
                CurrentState = outcomes.outcomeMap[ev];
                Debug.Log("Now in " + CurrentState);
                //To make the state appera reagarless of state.
                StaticData.StatesOfWhisps = CurrentState.ToString();
            }
        }
        else
        {
            Debug.Log("No Event for " + CurrentState);
        }

    }

    public Enum getState()
    {
        return CurrentState;
    }
}
