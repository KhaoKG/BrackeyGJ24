using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingListener : MonoBehaviour
{
    public static MovingListener instance;
    private GameObject firstCamera;
    private GameObject newCamera;
    private AkAudioListener listener;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("remove new instance");
            Destroy(this.gameObject);
        }
        else instance = this;
        //firstCamera = GameObject.FindWithTag("MainCamera");
        //this.transform.parent = firstCamera.transform;
        //Debug.Log("MovingListener Awake");
        AkAudioListener.DefaultListeners.Add(listener);
    }

    public void RemoveListener()
    {
        this.transform.parent = null;
        DontDestroyOnLoad(this);
        Debug.Log("Unparent Listener");
    }

    public void MoveListener()
    {
        newCamera = GameObject.FindWithTag("MainCamera");
        this.transform.parent = newCamera.transform;
        Debug.Log("Attach Listener To New Camera");
    }
}
