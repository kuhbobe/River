using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantAudio : MonoBehaviour
{
    private static ConstantAudio instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            // This is the first instance; mark it as the instance
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // An instance already exists, so destroy this one
            Destroy(gameObject);
        }
    }
}
