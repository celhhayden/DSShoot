using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stores a line of dialogue as well as its source speaker
public class DialogueLine : MonoBehaviour
{
    // character saying this line
    public CharacterProfile character;

    // what's being said
    // will change this if localizing (obj with line per lang)
    [TextArea(2, 5)]
    public string text;
}
