using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class PauseLogic : MonoBehaviour
{
    [SerializeField]
    GameObject root;
    public UnityEvent PauseSwap;
    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        PauseSwap.AddListener(TogglePause);
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            isPaused = false;
            Unpause();
        }
        else
        {
            isPaused = true;
            Pause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseSwap.Invoke();
        }
    }

    void Pause()
    {
        Time.timeScale = 0f;
        root.SetActive(true);
    }
    void Unpause()
    {
        Time.timeScale= 1; 
        root.SetActive(false);
    }
}
