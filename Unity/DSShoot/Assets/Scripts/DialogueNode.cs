using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a single node in a dialogue graph
// two types, basic node and choice node
public abstract class DialogueNode : MonoBehaviour
{
    // the dialogue and character speaking is stored in this one data type
    public DialogueLine line;

    // check if there is actually a subsequent node
    public abstract bool HasNext(int index);
    // get a better implement of this with the arg type since ChoiceNode needs an index arg for choice
    public abstract DialogueNode GetNext(int index);
}
