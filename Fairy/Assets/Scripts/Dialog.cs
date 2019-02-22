using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Composing a complete dialog out of DialogScreens.
/// </summary>
public class Dialog : MonoBehaviour
{
    public TextAsset dialogFile;

    public GameObject dialogContainer;
    public GameObject responseContainer;
    
    public enum DialogState { Idle, WaitForResponse }

    private DialogState state;
    private List<GameObject> responseTextReferences = new List<GameObject>();

    //TEST SCRIPT
    public void Awake()
    {
        DialogParser parser = new DialogParser();

        string test = parser.ParseDialog("Assets/Scripts/ExampleDialog.txt")[0].Text;
        Debug.Log(test);
    }
    //END OF TEST

    public List<GameObject> CreateResponseFields(int count)
    {
        if (count < 1) throw new ArgumentException("Response count must be equal or greater than 1");

        List<GameObject> fieldList = new List<GameObject>();
        
        //TODO: Needs refactoring: Create fields 1 .. n
        // Duplicate code...
        if(count == 1)
        {
            GameObject responseFieldObject = CreateResponseField();

            fieldList.Add(responseFieldObject);
        }
        else if (count == 2)
        {
            GameObject responseFieldObject = CreateResponseField();

            float oldWidth = responseFieldObject.GetComponent<RectTransform>().rect.width;
            float oldHeight = responseFieldObject.GetComponent<RectTransform>().rect.height;
            float oldY = responseFieldObject.GetComponent<RectTransform>().anchoredPosition.y;

            responseFieldObject.GetComponent<RectTransform>().sizeDelta = new Vector2(oldWidth / 2, oldHeight);
            responseFieldObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-oldWidth / 4, oldY);

            GameObject responseFieldObject1 = CreateResponseField();
            
            responseFieldObject1.GetComponent<RectTransform>().sizeDelta = new Vector2(oldWidth / 2, oldHeight);
            responseFieldObject1.GetComponent<RectTransform>().anchoredPosition = new Vector2(oldWidth / 4, oldY);


            fieldList.Add(responseFieldObject);
            fieldList.Add(responseFieldObject1);
        }
        else if (count == 3)
        {
            GameObject responseFieldObject = CreateResponseField();

            float oldWidth = responseFieldObject.GetComponent<RectTransform>().rect.width;
            float oldHeight = responseFieldObject.GetComponent<RectTransform>().rect.height;
            float oldY = responseFieldObject.GetComponent<RectTransform>().anchoredPosition.y;
            
            responseFieldObject.GetComponent<RectTransform>().sizeDelta = new Vector2(oldWidth / 3, oldHeight);
            
            GameObject responseFieldObject1 = CreateResponseField();
            responseFieldObject1.GetComponent<RectTransform>().sizeDelta = new Vector2(oldWidth / 3, oldHeight);
            responseFieldObject1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-oldWidth / 3, oldY);

            GameObject responseFieldObject2 = CreateResponseField();
            responseFieldObject2.GetComponent<RectTransform>().sizeDelta = new Vector2(oldWidth / 3, oldHeight);
            responseFieldObject2.GetComponent<RectTransform>().anchoredPosition = new Vector2(oldWidth / 3, oldY);

            fieldList.Add(responseFieldObject);
            fieldList.Add(responseFieldObject1);
            fieldList.Add(responseFieldObject2);
        }

        return fieldList;
    }

    private GameObject CreateResponseField()
    {
        GameObject responseFieldObject = new GameObject("ResponseFieldObject");
        TextMeshProUGUI responseFieldScriptRef = responseFieldObject.AddComponent<TextMeshProUGUI>();
        responseFieldObject.transform.SetParent(transform, false);
        CopyRectTransform(ref responseContainer, ref responseFieldObject);
        return responseFieldObject;
    }

    /// <summary>
    /// Clears dialog text and response options and sets state to idle
    /// </summary>
    public void ReturnToIdle()
    {
        foreach(GameObject responseTextField in responseTextReferences)
        {
            Destroy(responseTextField);
        }

        dialogContainer.GetComponent<TextMeshProUGUI>().text = "";
        state = DialogState.Idle;
    }

    /// <summary>
    /// Shows given dialog text and response options related to it and sets state to wait
    /// </summary>
    public void ShowDialogScreen(DialogScreen dialogScreen)
    {

        state = DialogState.WaitForResponse;
        throw new NotImplementedException();
    }

    /// <summary>
    /// Copies RectTransform values from object to another
    /// </summary>
    public void CopyRectTransform(ref GameObject from, ref GameObject to)
    {
        RectTransform rectTransform = from.GetComponent<RectTransform>();
        RectTransform objRectTransform = to.GetComponent<RectTransform>();

        objRectTransform.anchorMin = rectTransform.anchorMin;
        objRectTransform.anchorMax = rectTransform.anchorMax;
        objRectTransform.anchoredPosition = rectTransform.anchoredPosition;
        objRectTransform.sizeDelta = rectTransform.sizeDelta;
    }
}
