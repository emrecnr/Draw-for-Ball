using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Bucket scoreArea;
    [SerializeField] BallSpawner ballSpawner;
    [SerializeField] LineController lineController;
    public float screenWidth;
    public float screenHeight;
    private int _scorePoint = 1;
    private int _currentScore;

    public bool canStart;



    private void Awake()
    {
       Instance = this;
        screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
        screenHeight = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height, 0f)).y;
        
    }
    private void OnEnable()
    {
        scoreArea.OnScore += HandleOnScore;
        UIManager.Instance.adButton.onClick.AddListener(RewardedAdContinue);
    }
    private void OnDisable()
    {
        scoreArea.OnScore -= HandleOnScore;
    }
    public void HandleOnScore()
    {
        _currentScore += _scorePoint;
        Debug.Log($"Score: {_currentScore}");
        ballSpawner.Continue();
        lineController.Continue();
    }
    public void RewardedAdContinue()
    {
        AdSource.Instance.GetAdProvider().ShowRewarded(() =>
        {
            Debug.Log("Rewarded Ad Continue");
            UIManager.Instance.ContinueGameWithAds();
            ballSpawner.Continue();
            lineController.Continue();
        },
        () =>
        {
            Debug.LogWarning("---");
        });

    }

}
