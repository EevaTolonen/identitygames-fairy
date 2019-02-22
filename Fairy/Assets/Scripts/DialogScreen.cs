//@author Olli Paakkunainen

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Part of whole dialog. Contains all infomation what is shown at once.
/// A dialog text and player responses to it.
/// </summary>
public class DialogScreen
{
    //text shown
    private string text;

    public string Text {
        get {
            return text;
        }
    }

    //identifier
    private int id;

    //int -> identifier of next DialogScreen | string -> text shown
    private Dictionary<int, string> responses;

    public DialogScreen(int id, string text, Dictionary<int, string> choices)
    {
        this.id = id;
        this.text = text;
        this.responses = choices;
    }

    public DialogScreen(int id, string text)
    {
        this.id = id;
        this.text = text;
        responses = null;
    }

    public DialogScreen(int id)
    {
        this.id = id;
        this.text = "";
        responses = null;
    }
    
}
