using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            ChangePauseState();
        }
    }

    private void ChangePauseState()
    {
        paused = !paused;
        _pauseMenu.enabled = paused;

        if (_fpCamera != null)
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
