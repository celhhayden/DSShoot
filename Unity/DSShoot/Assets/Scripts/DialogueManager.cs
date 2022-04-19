using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // the current entry being looked at in the graph
    public DialogueNode currEntry;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textWindow;

    // Start is called before the first frame update
    void Start()
    {
        // FOR TESTING
        StartDialogue();
        //DisplayNext();
    }

    void Update()
    {
        // only click for next entry if basic node
        if(currEntry is BasicNode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DisplayNext();
            }
        }
    }

    // called when game is ready to being dialogue
    public void StartDialogue()
    {
        textName.SetText(currEntry.line.character.displayName);
        textWindow.SetText(currEntry.line.text);
        //Debug.Log("start sequence with " + currEntry.line.character.displayName);
        //Debug.Log("saying: " + currEntry.line.text);
    }

    // fetch the info from the next node in graph and display
    public void DisplayNext()
    {
        // TODO add choice button indexing instead of const index
        if (!currEntry.HasNext(0))
        {
            EndDialogue();
            return;
        }
        else
        {
            // TODO account for choice nodes instead of const arg
            currEntry = currEntry.GetNext(0);

            textName.SetText(currEntry.line.character.displayName);
            textWindow.SetText(currEntry.line.text);

            //Debug.Log("speaker: " + currEntry.line.character.displayName);
            //Debug.Log("saying: " + currEntry.line.text);
        }
    }

    public void EndDialogue()
    {
        Debug.Log("no more nodes");
    }
}
