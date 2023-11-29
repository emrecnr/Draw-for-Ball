using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] LineController lineController;
    [SerializeField] private TMP_Text _currentDrawLine;
    [Header("--- START PANEL ---")]
    [SerializeField] Button startButton;
    [SerializeField] Button leaderButton;
    [SerializeField] Button quitButton;
    public TMP_Text bestScoreText;
    [Header("--- GAME PANEL---")]
    public TMP_Text _currentScoreText;
    [Header("--- BUTTONS GAME OVER PANEL ---")]
    public Button adButton;
    [SerializeField] Button retryButton;
    [SerializeField] Button gameOverQuitButton;

    [Header("--- PANELS ---")]
    [SerializeField] GameObject _startPanel;
    [SerializeField] GameObject _gamePanel;
    [SerializeField] GameObject _gameOverPanel;
    [Header("--- HELPERS ---")]
    [SerializeField] TMP_Text _startCountDown;

    
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        bestScoreText.text = "BEST SCORE\n" + GameManager.Instance.bestScore.ToString();
    }
    private void OnEnable()
    {
        lineController.OnDrawLine += HandleOnDrawLine;
        startButton.onClick.AddListener(StartGame);
        retryButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
        gameOverQuitButton.onClick.AddListener(QuitGame);
    }
   
    public void StartGame()
    {
        StartCoroutine(StartCountdown());
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ContinueGameWithAds()
    {
        _gameOverPanel.SetActive(false);
        adButton.gameObject.SetActive(false);
        StartCoroutine(StartCountdown());
    }
    private void QuitGame()
    {
        Application.Quit();
    }
    private IEnumerator StartCountdown()
    {
        _startPanel.SetActive(false);
        int startCount = 3;

        while (startCount > 0)
        {
            _startCountDown.gameObject.SetActive(true);
            _startCountDown.text = startCount.ToString();
            yield return new WaitForSeconds(1f);
            startCount--;
        }
        GameManager.Instance.canStart = true;
        _startCountDown.text = "Draw!";
        yield return new WaitForSeconds(.5f);
        _gamePanel.SetActive(true);
        _startCountDown.gameObject.SetActive(false);
    }
    private void HandleOnDrawLine(int current)
    {
        if (current < 0) return;
        _currentDrawLine.text = current.ToString();
    }
    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
        _gamePanel.SetActive(false);
    }
}
