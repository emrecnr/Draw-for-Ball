using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPool : GenericPool<BallController>
{
    public static BallPool Instance { get; private set; }   
    protected override void SingletonObject()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);           

    }
}
