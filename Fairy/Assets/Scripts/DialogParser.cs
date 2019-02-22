using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogParser
{

    public struct DialogText
    {
        public int id;
        public string text;

        public DialogText(int id, string text)
        {
            this.id = id;
            this.text = text;
        }
    }

    public List<DialogScreen> ParseDialog(string filePath)
    {
        List<DialogScreen> dialogScreens = new List<DialogScreen>();

        string text = File.ReadAllText(filePath, Encoding.UTF8);
        dialogScreens.Add(new DialogScreen(1, text));

        return dialogScreens;
    }
    
    private DialogText GetNextDialogText()
    {
        string textPattern = @"(?<=<id = \d>).+?(?=<\/id>)";
        string idPattern = @"(?<=<id = )\d(?=>)";


        throw new NotImplementedException();
    }

    private Dictionary<int, string> GetNextResponses()
    {
        throw new NotImplementedException();
    }
}
