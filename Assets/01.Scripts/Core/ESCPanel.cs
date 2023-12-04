using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCPanel : MonoBehaviour
{
    [SerializeField]
    Transform _escPanel;
    TextMeshProUGUI _countText;
    public bool _isPanelOn = false;
    bool _isCounting = false;
    public static ESCPanel Instance = null;
    private void Awake()
    {
        _escPanel = transform.Find("ESC");
        _countText = transform.Find("CountText").GetComponent<TextMeshProUGUI>();
        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (GameManager.Instance != null)
            if(!GameManager.Instance.isGameStart) return;
        if (Input.GetKeyDown(KeyCode.Escape) && !_isRunning)
        {
            if (_isPanelOn)
            {
                if (SettingManager.Instance.isSettingOn)
                {
                    if (SettingManager.Instance.isCoroutineRunning) return;
                    SettingManager.Instance.SettingClose();
                }
                else
                {
                    Resume();
                }
            }
            else
            {
                _isPanelOn = true;
                if (SceneManager.GetActiveScene().name == "SampleScene")
                {
                    GameStop();
                    Cursor.visible = true;
                    return;
                }
                _escPanel.gameObject.SetActive(true);
                AudioManager.Instance.audioSource.Pause();

            }
        }
    }

    public void Resume()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            GameStart();
            Cursor.visible = false;
            return;
        }
        _isPanelOn = false;
        _escPanel.gameObject.SetActive(false);
        AudioManager.Instance.audioSource.UnPause();
    }

    public void Exit()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            _escPanel.gameObject.SetActive(false);
            _isPanelOn = false;
            SceneManager.LoadScene(1);
        }
        else
        {
            Application.Quit();
        }
    }

    public void GameStop()
    {
        if (AudioManager.Instance.audioSource.isPlaying)
            AudioManager.Instance.audioSource.Pause();
        NoteGenerate.Instance.isGenerateEnd = false;
        GameManager.Instance.isGameStart = false;
        _escPanel.gameObject.SetActive(true);
    }

    public void GameStart()
    {
        if (_isRunning) return;
        StartCoroutine(IEGameStart());
    }

    bool _isRunning = false;
    IEnumerator IEGameStart()
    {
        _isRunning = true;
        _escPanel.gameObject.SetActive(false);
        _countText.gameObject.SetActive(true);
        for (int i = 3; i >= 1; i--)
        {
            _countText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        AudioManager.Instance.audioSource.UnPause();
        _isPanelOn = false;
        GameManager.Instance.isGameStart = true;
        _countText.gameObject.SetActive(false);
        NoteGenerate.Instance.isGenerateEnd = true;
        _isRunning = false;
    }
}
