using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{


    public System.Action OnScore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
           OnScore?.Invoke();
            AudioManager.Instance.PlayScoreSFX();
        }
    }

}
