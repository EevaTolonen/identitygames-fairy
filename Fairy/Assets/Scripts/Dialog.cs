using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
    public struct DialogText
    {
        public int id;
        public string text;

        public DialogText(int i, string t)
        {
            id = i;
            text = t;
        }
    }

    public struct Choice
    {
        int nextDialogId;
        string text;

        public Choice(int i, string t)
        {
            nextDialogId = i;
            text = t;
        }
    }

    private List<DialogText> dialogTexts = new List<DialogText>();
    private List<Choice> choices = new List<Choice>();

    public void AddDialogText(int id, string txt)
    {
        DialogText dialog = new DialogText(id, txt);
        dialogTexts.Add(dialog);
    }

    public void AddChoice(int idToNextDialog, string txt)
    {
        Choice choice = new Choice(idToNextDialog, txt);
        choices.Add(choice);
    }

    public DialogText GetDialogTextById(int id)
    {
        foreach(DialogText dText in dialogTexts)
        {
            if (dText.id == id) return dText;
        }

        throw new ArgumentException("Dialog text with given id not found");
    }
}
