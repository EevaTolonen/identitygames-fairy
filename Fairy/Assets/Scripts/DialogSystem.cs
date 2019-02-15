using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{

    private TextMeshProUGUI dialogContainer;
    private TextMeshProUGUI[] dialogChoiceContainers;

    /// <summary>
    /// Shows given text at dialog window.
    /// </summary>
    public void SetText(string txt) {
        dialogContainer.text = txt;
    }

    /// <summary>
    /// Clears dialog window.
    /// </summary>
    public void Clear() {
        dialogContainer.text = "";
    }

    /// <summary>
    /// Shows given dialog choices in dialog window and returns index of player made choice
    /// </summary>
    public int GetChoiceIndex(string[] txtArr) {
        throw new NotImplementedException();
    }
}
