using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas _pauseMenu;
    [SerializeField] private FirstPersonCamera _fpCamera;
    private bool paused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pauseMenu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(WaitThenPause());
        }
    }

    private IEnumerator WaitThenPause()
    {
        yield return null;
        if (!paused && !_fpCamera._isFocused)
        {
            ChangePauseState();
        }
    }

    private void ChangePauseState()
    {
        paused = !paused;
        _pauseMenu.enabled = paused;
        if(paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

		if(_fpCamera != null)
        {
            _fpCamera.SetMouseLookEnabled(!paused);
        }
    }

    public void OnClickResumeButton()
    {
        ChangePauseState();
    }

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
