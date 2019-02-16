using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handles dialog system implemented by canvas. 
/// Methods are intended to be called by triggers on game level.
/// </summary>
public class DialogSystem : MonoBehaviour
{
    /// <summary>
    /// Images are merely used for sizing and positioning of textboxes
    /// and can be disabled after wake if needed.
    /// </summary>
    [Header("Containers:")]
    public Image dialogContainer;
    public Image choiceContainer;

    private TextMeshProUGUI dialogTextbox;
    private TextMeshProUGUI[] choiceTextboxArray;

    /// <summary>
    /// Shows given text at dialog window.
    /// </summary>
    public void SetText(string txt) {
        dialogTextbox.text = txt;
    }

    /// <summary>
    /// Clears dialog window.
    /// </summary>
    public void Clear() {
        dialogTextbox.text = "";
    }

    /// <summary>
    /// Shows given dialog choices in dialog window 
    /// and returns index of player made choice.
    /// </summary>
    public int GetChoiceIndex(string[] txtArr) {
        throw new NotImplementedException();
    }
}
