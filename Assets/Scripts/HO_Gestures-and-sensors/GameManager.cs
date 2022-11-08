using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // Instead of using serialize field, a cleaner approach would be to use event broadcasters.
    // GameManager would not then be tied to game objects in a scene, which would then allow its usage on other scenes.
    [SerializeField] private SliceableSpawner spawner;
    [SerializeField] private TextMeshProUGUI highScoreTxt;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private GameObject pausePanel;

    private float highScore = 0f;
    private float score = 0f;

    private float prevTime;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
    }

    private void Start()
    {
        highScoreTxt.SetText($"High Score: None");
        scoreTxt.SetText($"Score: N/A");
    }

    public void IncrementScore(int amount = 1)
    {
        score += amount;
        scoreTxt.SetText($"Score: {score}");
        if (score > highScore)
        {
            highScore = score;
            highScoreTxt.SetText($"High Score: {highScore}");
        }
    }

    public void PlayGame()
    {
        spawner.StartSpawning();
        score = 0f;
        scoreTxt.SetText($"Score: 0");
    }

    public void ResetGame()
    {
        spawner.StopSpawning();
    }

    public void ResetAll()
    {
        spawner.StopSpawning();
        highScoreTxt.SetText($"High Score: None");
        scoreTxt.SetText($"Score: N/A");
    }

    public void Pause()
    {
        prevTime = Time.timeScale;
        Time.timeScale = 0f;
        GestureManager.Instance.isEnabled = false;
    }

    public void Resume()
    {
        Time.timeScale = prevTime;
        GestureManager.Instance.isEnabled = true;
    }

    public void OnApplicationPause(bool pause)
    {
        Debug.Log($"Application Pause: {pause}");
        if (pause)
        {
            pausePanel.SetActive(true);
            Pause();
        }
    }
}
