using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Composing a complete dialog out of DialogScreens.
/// </summary>
public class Dialog
{
    public enum DialogState { Idle, WaitForResponse }

    private DialogState state;

    /// <summary>
    /// Clears dialog text and response options and sets state to idle
    /// </summary>
    public void ReturnToIdle()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Shows given dialog text and response options related to it and sets state to wait
    /// </summary>
    public void ShowDialogScreen(DialogScreen dialogScreen)
    {
        throw new NotImplementedException();
    }

}
