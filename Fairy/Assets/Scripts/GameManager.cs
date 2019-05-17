using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Vector2 lastCheckPointPos = Vector2.zero;
    public Texture2D cursorTexture;

    public List<Dialog> dialogs;
    private List<GameObject> dialogObjects;
    private Transform startingPoint;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        dialogObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Dialog"));
    }

    // GameManager that manages the state of our game
    // Start is called before the first frame update
    void Awake()
    {
        if(cursorTexture != null)
        {
            Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width / 2, cursorTexture.height / 2), CursorMode.Auto);
        }

        startingPoint = GameObject.FindGameObjectWithTag("StartingPoint").transform;
        if (lastCheckPointPos == Vector2.zero) lastCheckPointPos = startingPoint.position;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.M))
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-737, -725, 0); 
        }
    }

    private void UpdateDialogs(int response)
    {
        foreach(GameObject obj in dialogObjects)
        {
            Dialog dialog = obj.GetComponent<Dialog>();
            if (dialog.dialogActive)
            {
                dialog.selectedResponse = response;
                dialog.ToNextDialog(); 
            }
        }
    }

    public void Response1Handler()
    {
        Debug.Log("Button 1");
        UpdateDialogs(1);
    }


    public void Response2Handler()
    {
        Debug.Log("Button 2");
        UpdateDialogs(2);
    }

    public void Response3Handler()
    {
        Debug.Log("Button 3");
        UpdateDialogs(3);
    }

    public void Response4Handler()
    {
        Debug.Log("Button 4");
        UpdateDialogs(4);
    }
}

