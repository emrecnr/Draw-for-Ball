using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyArea"))
        {
            BackToPool();
            GameManager.Instance.canStart = false;
            UIManager.Instance.GameOver();
        }
    }
    public void BackToPool()
    {
        BallPool.Instance.Set(this);
    }
}
