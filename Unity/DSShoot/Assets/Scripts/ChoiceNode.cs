using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceEntry
{
    // the text that will display on the choice's button
    public string text;
    // the next dialogue node this choice takes you to
    public DialogueNode next;
}

public class ChoiceNode : DialogueNode
{
    // all of the choices present at this node
    public ChoiceEntry[] choices;

    // TODO this might need some adjusting but will do this for now
    // makes sure there is even a next node
    public override bool HasNext(int index)
    {
        return choices[index].next != null;
    }

    // gives the requested choice result at the right index after selecting
    public override DialogueNode GetNext(int index)
    {
        return choices[index].next;
    }
}
