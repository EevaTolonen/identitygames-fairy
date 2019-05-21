// @author Olli Paakkunainen

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogParser
{
    public string textToParse;

    public void ReadFile(string filePath)
    {
        string text = File.ReadAllText(filePath, Encoding.UTF8);
        textToParse = text;
    }
    
    public List<DialogText> GetDialogTexts()
    {
        List<DialogText> dialogs = new List<DialogText>();
        string textPattern = @"(?<=<id = \d>).+?(?=<\/id>)";
        string idPattern = @"(?<=<id = )\d(?=>)";
        string responsePattern = @"(?=\[).+?(?<=\](?!\[))";

        MatchCollection textMatch = Regex.Matches(textToParse, textPattern, RegexOptions.Singleline);
        MatchCollection idMatch = Regex.Matches(textToParse, idPattern, RegexOptions.Singleline);
        MatchCollection responseMatch = Regex.Matches(textToParse, responsePattern, RegexOptions.Singleline);
        
        for (int i = 0; i < textMatch.Count; i++)
        {
            List<DialogText.Response> responses = SplitResponses(responseMatch[i]);

            DialogText dialog = new DialogText(int.Parse(idMatch[i].Value), textMatch[i].Value, responses);
            dialogs.Add(dialog);
        }

        return dialogs;
    }

    private List<DialogText.Response> SplitResponses(Match responseMatch)
    {
        List<DialogText.Response> responseList = new List<DialogText.Response>();
        string pattern = @"(?<=\[).+?(?=])";
        MatchCollection responseMatches = Regex.Matches(responseMatch.Value, pattern);
        
        for(int i = 0; i < responseMatches.Count; i++)
        {
            string[] idTextSplit = responseMatches[i].Value.Split(':');
            int id = int.Parse(idTextSplit[0]);
            string text = idTextSplit[1];
            
            responseList.Add(new DialogText.Response(id, text));
        }

        return responseList;
    }
}
