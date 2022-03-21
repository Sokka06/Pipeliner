// Used with Bolt/Visual Scripting
/*
using System.Collections;
using System.Collections.Generic;
using Bolt;
using Ludiq;
using UnityEngine;

[UnitTitle("On Button Event")]//Custom EventUnit to receive the event. Adding On to the unit title as an event naming convention.
[UnitCategory("Events/UI")]//Setting the path to find the unit in the fuzzy finder in Events > My Events.
public class ButtonEventUnit : EventUnit<string>
{
    [DoNotSerialize] // No need to serialize ports
    public ValueInput Key; // Adding the ValueInput variable for myValueA
        
    [DoNotSerialize]// No need to serialize ports.
    public ValueOutput Result { get; private set; }// The event output data to return when the event is triggered.
        
    protected override bool register => true;

    // Adding an EventHook with the name of the event to the list of visual scripting events.
    public override EventHook GetHook(GraphReference reference)
    {
        return new EventHook(UIEvents.ButtonEvent);
    }
    protected override void Definition()
    {
        base.Definition();
            
        Key = ValueInput<string>("Key", "");
            
        // Setting the value on our port.
        Result = ValueOutput<string>(nameof(Result));
            
    }
    // Setting the value on our port.
    protected override void AssignArguments(Flow flow, string data)
    {
        flow.SetValue(Result, data);
    }

    protected override bool ShouldTrigger(Flow flow, string args)
    {
        return string.Equals(flow.GetValue<string>(Key), args);
    }
}
*/
