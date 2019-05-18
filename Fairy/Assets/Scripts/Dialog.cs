using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityStandardAssets._2D;

/// <summary>
/// Composing a complete dialog out of DialogScreens.
/// </summary>
public class Dialog : MonoBehaviour
{
    public string dialogFilePath;
    public AudioClip[] dialogAudioClips;

    public TextMeshProUGUI dialogTextbox;
    public TextMeshProUGUI responseTextbox1;
    public TextMeshProUGUI responseTextbox2;
    public TextMeshProUGUI responseTextbox3;
    public TextMeshProUGUI responseTextbox4;
    
    public bool dialogActive = false;
    public int selectedResponse = 0;

    private AudioSource audioSource;
    private List<DialogText> dialogTexts;
    private DialogText currentDialogText;
    public bool isActive = true;

    private int idToNextDialog = -1;


    public void Awake()
    {
        DialogParser parser = new DialogParser();
        parser.ReadFile(dialogFilePath);
        dialogTexts = parser.GetDialogTexts();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        responseTextbox1 = GameObject.FindGameObjectWithTag("Response1").GetComponent<TextMeshProUGUI>();
        responseTextbox2 = GameObject.FindGameObjectWithTag("Response2").GetComponent<TextMeshProUGUI>();
        responseTextbox3 = GameObject.FindGameObjectWithTag("Response3").GetComponent<TextMeshProUGUI>();
        responseTextbox4 = GameObject.FindGameObjectWithTag("Response4").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive)
        {
            if (other.gameObject.tag == "PlayerBody")
            {
                isActive = false;
                StopPlayerMovement(true);
                StartCoroutine(StartDialogEvent());
            } 
        }
    }

    private static void StopPlayerMovement(bool status)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Platformer2DUserControl userControl = player.GetComponent<Platformer2DUserControl>();
        userControl.stopMovement = status;
    }

    public void ToNextDialog()
    {
        switch (selectedResponse)
        {
            case 1:
                if (currentDialogText.responses.Count >= 1)
                    idToNextDialog = currentDialogText.responses[0].Id;
                break;
            case 2:
                if (currentDialogText.responses.Count >= 2)
                    idToNextDialog = currentDialogText.responses[1].Id;
                break;
            case 3:
                if (currentDialogText.responses.Count >= 3)
                    idToNextDialog = currentDialogText.responses[2].Id;
                break;
            case 4:
                if (currentDialogText.responses.Count >= 4)
                    idToNextDialog = currentDialogText.responses[3].Id;
                break;
            default:
                break;
        }
        if(idToNextDialog == 0)
        {
            audioSource.Stop();
            ClearAll();
            dialogActive = false;
            StopPlayerMovement(false);
        } else if(idToNextDialog != -1)
        {
            ClearAll();
            currentDialogText = GetDialogTextWithId(idToNextDialog);
            ShowText(currentDialogText);
            ShowResponses(currentDialogText);

            if(idToNextDialog-1 <= dialogAudioClips.Length - 1)
            {
                audioSource.Stop();
                audioSource.clip = dialogAudioClips[idToNextDialog-1];
                audioSource.Play();
            }
        }
    }

    private DialogText GetDialogTextWithId(int idToNextDialog)
    {
        foreach(DialogText text in dialogTexts)
        {
            if(text.Id == idToNextDialog)
            {
                return text;
            }
        }

        return new DialogText();
    }

    private IEnumerator StartDialogEvent()
    {
        dialogActive = true;
        currentDialogText = dialogTexts[0];
        ShowText(currentDialogText);
        ShowResponses(currentDialogText);
        if (dialogAudioClips.Length >= 1)
        {
            audioSource.Stop();
            audioSource.clip = dialogAudioClips[0];
            audioSource.Play(); 
        }

        yield return new WaitUntil(() => selectedResponse != 0);
    }

    private void ShowResponses(DialogText dialogText)
    {
        switch(dialogText.responses.Count)
        {
            case 1:
                responseTextbox1.text = dialogText.responses[0].Text;
                break;
            case 2:
                responseTextbox1.text = dialogText.responses[0].Text;
                responseTextbox2.text = dialogText.responses[1].Text;
                break;
            case 3:
                responseTextbox1.text = dialogText.responses[0].Text;
                responseTextbox2.text = dialogText.responses[1].Text;
                responseTextbox3.text = dialogText.responses[2].Text;
                break;
            case 4:
                responseTextbox1.text = dialogText.responses[0].Text;
                responseTextbox2.text = dialogText.responses[1].Text;
                responseTextbox3.text = dialogText.responses[2].Text;
                responseTextbox4.text = dialogText.responses[3].Text;
                break;
            default:
                break;
        }
    }

    private void ShowText(DialogText dialogText)
    {
        dialogTextbox.text = dialogText.Text;
    }

    private void ClearAll()
    {
        dialogTextbox.text = "";
        responseTextbox1.text = "";
        responseTextbox2.text = "";
        responseTextbox3.text = "";
        responseTextbox4.text = "";
    }

    private IEnumerator WaitForKeyDown(KeyCode keyCode)
    {
        while (!Input.GetKeyDown(keyCode))
            yield return null;
    }
}
