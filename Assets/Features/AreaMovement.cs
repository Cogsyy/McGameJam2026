using UnityEngine;

public class AreaMovement : MonoBehaviour
{
    [SerializeField] private Transform interviewRoomTransform;
    [SerializeField] private Transform homeRoomTransform;
    [SerializeField] private Transform jobBoardTransform;
	[SerializeField] private Transform wardrobeTransform;

    [SerializeField] private Camera mainCamera;


	[SerializeField] private CanvasFader fadeCanvas;

	[SerializeField] private GameObject jobBoard;
	[SerializeField] private GameObject wardrobe;

	[SerializeField] private AudioClip _interviewMusic;
	[SerializeField] private AudioClip _homeMusic;
	[SerializeField] private AudioClip _doorSfx;

	public void GoToInterview()
    {
		fadeCanvas.FadeToAlpha(1, () =>
		{
			if(!interviewRoomTransform)
				return;
			PlaceCameraAtTransform(interviewRoomTransform);
			DeactivateAllSceneStuff();
			fadeCanvas.FadeToAlpha(0);
			FindAnyObjectByType<DialogueManager>()?.StartDialogue();
		});

		AudioManager.Instance.PlayMusic(_interviewMusic);
		AudioManager.Instance.PlaySFX(_doorSfx);
	}



    public void GoHome()
    {
		jobBoard.SetActive(false);
		
		fadeCanvas.FadeToAlpha(1, () =>
		{
			if(!homeRoomTransform)
				return;
			PlaceCameraAtTransform(homeRoomTransform);
			DeactivateAllSceneStuff();
			
			fadeCanvas.FadeToAlpha(0);
			FindAnyObjectByType<FirstPersonCamera>().SetMouseLookEnabled(true);
		});
		AudioManager.Instance.PlayMusic(_homeMusic);

	}

    public void GoToJobBoard()
    {
		fadeCanvas.FadeToAlpha(1, () =>
		{
			if(!jobBoardTransform)
				return;
			PlaceCameraAtTransform(jobBoardTransform);
			DeactivateAllSceneStuff();
			fadeCanvas.FadeToAlpha(0);
			jobBoard.SetActive(true);
		});
	}

	public void GoToWardrobe()
	{
		fadeCanvas.FadeToAlpha(1, () =>
		{
			if(!wardrobeTransform)
				return;
			PlaceCameraAtTransform(wardrobeTransform);
			DeactivateAllSceneStuff();
			fadeCanvas.FadeToAlpha(0);
			wardrobe.SetActive(true);
		});
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.F9))
		{
			GoToInterview();
		}
		else if(Input.GetKeyDown(KeyCode.F10))
		{
			GoHome();
		}
		else if(Input.GetKeyDown(KeyCode.F11))
		{
			GoToJobBoard();
		}
	}

	void PlaceCameraAtTransform(Transform transform)
    {
		mainCamera.transform.position = transform.position;
		mainCamera.transform.rotation = transform.rotation;
	}

	void DeactivateAllSceneStuff()
	{
		jobBoard.SetActive(false);
		wardrobe.SetActive(false);
	}
}
