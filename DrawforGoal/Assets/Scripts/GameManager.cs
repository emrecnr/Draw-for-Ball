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
    public int _currentScore;
    public int bestScore;

    public bool canStart,canWatchAd=true;
    



    private void Awake()
    {
       Instance = this;
        screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
        screenHeight = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height, 0f)).y;
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
        }
        else
        {
            bestScore = 0;
        }
        
    }
    private void OnEnable()
    {
        scoreArea.OnScore += HandleOnScore;
        UIManager.Instance.adButton.onClick.AddListener(RewardedAdContinue);
    }
    private void OnDisable()
    {
        scoreArea.OnScore -= HandleOnScore;
        UIManager.Instance.adButton.onClick.RemoveListener(RewardedAdContinue);
    }
    public void HandleOnScore()
    {
        _currentScore += _scorePoint;
        UIManager.Instance._currentScoreText.text = "Score : "+_currentScore.ToString();
        ballSpawner.Continue();
        lineController.Continue();
    }
    public void RewardedAdContinue()
    {

        if (!AdSource.Instance.GetAdProvider().IsRewardedAdReady)
        {
            AdSource.Instance.GetAdProvider().LoadAd();
            return;
        }
        AdSource.Instance.GetAdProvider().ShowRewarded(() =>
        {
            Debug.Log("Rewarded Ad Continue");
            UIManager.Instance.ContinueGameWithAds();
            ballSpawner.Continue();
            lineController.Continue();
            canWatchAd = false;

        },
        () =>
        {
            Debug.LogWarning("---");
        });

    }
    public void GameOver()
    {
        if (_currentScore > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore",_currentScore);            
            Debug.Log("New Score!");
        }
    }

}
