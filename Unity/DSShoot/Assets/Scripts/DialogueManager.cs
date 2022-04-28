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
    public Image charImage;
    // always empty, gets start coords for when displaying buttons
    public GameObject buttonAnchor;
    // how far apart each button should be placed
    public float buttonSpacing = 30f;
    // store buttons to be deleted later
    private List<GameObject> buttonList;

    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
        buttonList = new List<GameObject>();
    }

    void Update()
    {
        // only click for next entry if basic node
        if(currEntry is BasicNode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DisplayNext(0);
            }
        }
    }

    // called when game is ready to begin dialogue
    public void StartDialogue()
    {
        textName.SetText(currEntry.line.character.displayName);
        textWindow.SetText(currEntry.line.text);
        charImage.enabled = true;
        charImage.sprite = currEntry.line.character.displayImage;
    }

    // fetch the info from the next node in graph and display
    public void DisplayNext(int index)
    {
        // destroy any live buttons from previous node
        buttonList.ForEach(Destroy);
        // make new list to ensure it's empty
        buttonList = new List<GameObject>();

        if (!currEntry.HasNext(index))
        {
            EndDialogue();
            return;
        }
        else
        {
            // TODO account for choice nodes instead of const arg
            currEntry = currEntry.GetNext(index);

            // update texts with new current node info
            textName.SetText(currEntry.line.character.displayName);
            textWindow.SetText(currEntry.line.text);
            // if image sprite is null disable image display
            if (currEntry.line.character.displayImage == null) charImage.enabled = false;
            else
            {
                charImage.enabled = true;
                charImage.sprite = currEntry.line.character.displayImage;
            }

            // generate buttons if it's a choice node
            if (currEntry is ChoiceNode)
            {
                // for every possible choice in this node, generate a button
                int currButtons = 0;
                foreach (ChoiceEntry choice in ((ChoiceNode)currEntry).choices)
                {
                    // generate a new button
                    GameObject newButton = Instantiate(buttonAnchor, FindObjectOfType<Canvas>().transform);
                    // space the buttons at constant distance vertically
                    newButton.GetComponent<RectTransform>().position = new Vector3(newButton.GetComponent<RectTransform>().position.x,
                        newButton.GetComponent<RectTransform>().position.y + currButtons * buttonSpacing,
                        newButton.GetComponent<RectTransform>().position.z);

                    // add the event to trigger to the button
                    // weird behavior needs new int allocation each iteration, just copy the index
                    int choiceIndex = currButtons;
                    newButton.GetComponent<Button>().onClick.AddListener(delegate { DisplayNext(choiceIndex); });
                    // set the button text to display per choice
                    newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(((ChoiceNode)currEntry).choices[choiceIndex].text);

                    // add button to this manager's list to track for deletion later
                    buttonList.Add(newButton);

                    // incr track what button being looked at
                    currButtons++;
                }
            }
        }
    }

    public void EndDialogue()
    {
        Debug.Log("no more nodes");
    }
}
