using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _forceSpeed = 500f;
    private float _angle = 45;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();   
    }
    private void Start()
    {
        StartForce();
    }
    private void StartForce()
    {
       float randomAngleValue = Random.Range(_angle, -_angle);
        Vector2 newAngle = new Vector2(randomAngleValue, randomAngleValue);
        _rigidbody.AddForce(newAngle * _forceSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyArea"))
        {
            BackToPool();
        }
    }
    public void BackToPool()
    {
        BallPool.Instance.Set(this);
    }
}
