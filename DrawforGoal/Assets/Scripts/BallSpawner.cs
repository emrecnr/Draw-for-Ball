using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bucket;
    [SerializeField] private Transform[] bucketSpawnPoints;
    [SerializeField] private Bucket _scoreArea;
    private float _spawnDelay = 1;
    private bool _isContinue;
    [SerializeField] private BallController ball = null;

    private Rigidbody2D _rigidbody;
    private float _maxForceSpeed = 750f;
    private float _minForceSpeed = 600f;
    private float _angle;

    private float _timer = 2.5f;
    private void Start()
    {
        this.gameObject.transform.position = new Vector3(0, -GameManager.Instance.screenHeight, 0);
        StartCoroutine(BallSpawn());
    }

    private void Update()
    {
        
    }
    private void Spawn()
    {
        ball = BallPool.Instance.Get();
        _rigidbody = ball.GetComponent<Rigidbody2D>();
        ball.transform.position = transform.position;
        ball.transform.rotation = Quaternion.identity;
        ball.gameObject.SetActive(true);
        _angle = Random.Range(70f, 110f);
        float force = Random.Range(_maxForceSpeed, _minForceSpeed);
        Vector2 position = Quaternion.AngleAxis(_angle, Vector3.forward) * Vector2.right;
        _rigidbody.AddForce(position * force);

    }
    private void CheckVelocity()
    {
        if (_rigidbody!=null&&_rigidbody.velocity == Vector2.zero)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                GameManager.Instance.canStart = false;
                UIManager.Instance.GameOver();
                GameManager.Instance.GameOver();
            }
        }
    }
    private void ReturnToPool()
    {

        BallPool.Instance.Set(ball);
    }
    private IEnumerator BallSpawn()
    {
        while (true)
        {
            if (!_isContinue && GameManager.Instance.canStart)
            {
                yield return new WaitForSeconds(_spawnDelay);
                Spawn();
                yield return new WaitForSeconds(_spawnDelay);
                Transform randomTransform = bucketSpawnPoints[Random.Range(0, bucketSpawnPoints.Length)];
                bucket.transform.position = randomTransform.position;
                bucket.SetActive(true);
                _isContinue = true;
            }
            else
            {
                CheckVelocity();
                yield return null;
            }
        }
    }
    public void Continue()
    {
        ReturnToPool();
        bucket.SetActive(false);
        _isContinue = false;
        Debug.Log("Restart");
    }
}
