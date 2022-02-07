using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DiolougeManager : MonoBehaviour
{
    //public string[] diolouge;
    //public string[] name;
  
    //public bool[] isPlayer;
   
 

    public DialogueScriptableObject Dialogues;
    public Sprite defaultIcon;
    [Space]
    public Text talkArea;
    public Text nameArea;
    [Space]
    public Image characterOne;
    public GameObject characterOneRoot;
    [Space]
    public Image characterTwo;
    public GameObject characterTwoRoot;
    [Space]
    public Image background;
    public GameObject backgroundRoot;
    [Space]
    public int diolougeCurrentlyOn;
    
    [Space]
    public AudioSource aud;

    [Space]
    public GameObject decisions;
    public Text choiceOne;
    public Text choiceTwo;
    [Space]
    public GameObject dialogueManager;

    bool deciding = false;
    // Start is called before the first frame update
    void Awake()
    {
         diolougeCurrentlyOn = 0;
     
        
        nameArea.text = Dialogues.dialogues[diolougeCurrentlyOn].name;
        if(Dialogues.dialogues[diolougeCurrentlyOn].isPlayer){
            characterOneRoot.SetActive(true);
            characterTwoRoot.SetActive(false);
            characterOne.sprite = Dialogues.dialogues[diolougeCurrentlyOn].icon;
        }else{
            characterOneRoot.SetActive(false);
            characterTwoRoot.SetActive(true);
            characterTwo.sprite = Dialogues.dialogues[diolougeCurrentlyOn].icon;
        }
     StopAllCoroutines();
      StartCoroutine(TypeSentence(Dialogues.dialogues[diolougeCurrentlyOn].diolouge));
    }

    public void NextTalk(){
        if(diolougeCurrentlyOn >= (Dialogues.dialogues.Length - 1)){
            //if out of lines go onto another scene
            DialogueEnded();
        }else{
            diolougeCurrentlyOn++;
                  //check who's image to change
        if(Dialogues.dialogues[diolougeCurrentlyOn].isPlayer){
            //turns players on or off
            if(!Dialogues.dialogues[diolougeCurrentlyOn].playerOneActive){
                characterOneRoot.SetActive(false);
            }else{
                characterOneRoot.SetActive(true);
            }
            
            if(!Dialogues.dialogues[diolougeCurrentlyOn].playerTwoActive){
            characterTwoRoot.SetActive(false);
            }else{
            characterTwoRoot.SetActive(true);
            }
            
            characterOne.sprite = Dialogues.dialogues[diolougeCurrentlyOn].icon;

            //background
            if(Dialogues.dialogues[diolougeCurrentlyOn].hasBackground){
                backgroundRoot.SetActive(true);
                background.sprite = Dialogues.dialogues[diolougeCurrentlyOn].backgroundImage;
            }else{
                backgroundRoot.SetActive(false);
            }
        }else{
            //turns players on or off
                       if(!Dialogues.dialogues[diolougeCurrentlyOn].playerOneActive){
                characterOneRoot.SetActive(false);
            }else{
                characterOneRoot.SetActive(true);
            }
            
                        //background
            if(Dialogues.dialogues[diolougeCurrentlyOn].hasBackground){
                backgroundRoot.SetActive(true);
                background.sprite = Dialogues.dialogues[diolougeCurrentlyOn].backgroundImage;
            }else{
                backgroundRoot.SetActive(false);
            }

            if(!Dialogues.dialogues[diolougeCurrentlyOn].playerTwoActive){
            characterTwoRoot.SetActive(false);
            }else{
            characterTwoRoot.SetActive(true);
            }


            characterTwo.sprite = Dialogues.dialogues[diolougeCurrentlyOn].icon;
            }
            StopAllCoroutines();
            StartCoroutine(TypeSentence(Dialogues.dialogues[diolougeCurrentlyOn].diolouge));
        }
    }
    // Update is called once per frame
    void Update()
    {
          if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit"))
        {
            if(deciding == false){
                NextTalk();
            }
            
        }
    }

    //typing effect
    IEnumerator TypeSentence (string sentence)
	{
        nameArea.text = Dialogues.dialogues[diolougeCurrentlyOn].name;
		talkArea.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			talkArea.text += letter;
            aud.PlayOneShot(Dialogues.dialogues[diolougeCurrentlyOn].talkSound, 0.7f);
			yield return null;
		}
	}

    //choices
    public void ChoiceOne(){
        diolougeCurrentlyOn = -1;
        Dialogues = Dialogues.choices[0].associatedDialogue;

        decisions.SetActive(false);
        NextTalk();

        deciding = false;
    }

    public void ChoiceTwo(){
        diolougeCurrentlyOn = -1;
        Dialogues = Dialogues.choices[1].associatedDialogue;

        decisions.SetActive(false);
        NextTalk();

        deciding = false;
    }

    //deals with the different ends
    public void DialogueEnded(){
        if(Dialogues.end == dialogueEndings.GoToScene){
                SceneManager.LoadScene(Dialogues.sceneToChangeTo, LoadSceneMode.Single);
        }else if(Dialogues.end == dialogueEndings.DialogueChoice) {
                decisions.SetActive(true); 
                deciding = true;
                choiceOne.text = Dialogues.choices[0].choiceName;
                choiceTwo.text = Dialogues.choices[1].choiceName;

        }else if(Dialogues.end == dialogueEndings.Leave) {
                dialogueManager.SetActive(false);
                Dialogues = null;
                
            }
    }
}
