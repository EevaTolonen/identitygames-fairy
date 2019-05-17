using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogButtonHandler : MonoBehaviour
{
    public int responseButtonNumber;

    private void Start()
    {
        Debug.Log(GameManager.instance);

        switch(responseButtonNumber)
        {
            case 1:
                gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.Response1Handler);
                break;
            case 2:
                gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.Response2Handler);
                break;
            case 3:
                gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.Response3Handler);
                break;
            case 4:
                gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.Response4Handler);
                break;
            default:
                break;
        }
    }
}
