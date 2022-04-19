using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNode : DialogueNode
{
    // the next node in the dialogue graph
    public DialogueNode next;

    // return if has a next or not
    public override bool HasNext(int index)
    {
        return next != null;
    }

    // simply returns the next node in graph
    public override DialogueNode GetNext(int index)
    {
        return next;
    }
}
