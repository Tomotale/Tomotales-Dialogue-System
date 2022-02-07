using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialouge/DialogueScriptableObject", order = 1)]
public class DialogueScriptableObject : ScriptableObject
{   
[System.Serializable]
 public class Dialogue
 {
     
      public string name;
      [TextArea(3, 10)]
      public string diolouge;
      [Space]
      public Sprite icon;
      public Sprite backgroundImage;
      [Space]
      public bool isPlayer;
      public bool hasBackground;
      [Space]
      public bool playerOneActive;
      public bool playerTwoActive;
      [Space]
      public AudioClip talkSound;

 }

[System.Serializable]
 public class Choice
 {   
    public string choiceName;
    public DialogueScriptableObject associatedDialogue;

 }
   

    public Dialogue[] dialogues;
    [Space]
    public dialogueEndings end;
    public string sceneToChangeTo;
    [Space]
    public Choice[] choices;
}

 
  public enum dialogueEndings // your custom enumeration
    {
        GoToScene, 
        DialogueChoice, 
        Leave
    };