using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }
    }
    private void Spawn()
    {
        BallController ball = BallPool.Instance.Get();
        ball.transform.parent = transform;
        ball.transform.position = transform.position;
        ball.gameObject.SetActive(true);
    }
}
