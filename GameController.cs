using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameController : MonoBehaviour {
	
	public Camera cam;
	public GameObject[] balls;
	public float timeLeft;
	public GUIText timerText;
	public GameObject WelldoneText;
	public GameObject restartButton;
	public GameObject splashScreen;
	public GameObject startButton;
	public GameObject RightButton;
	public GameObject LeftButton;
	public GameObject collectOrangeFace;
	public GameObject collectPurpleFace;
	public GameObject collectGreenFace;
	public FaceController faceController;
	private float maxWidth;
	private bool counting;
	public Timer calibration;
	public Score detect_collision;
	public Sprite_colour changeSprite;
	public enum GameState {Handed,Startcalibrate, Calibrate, detectOrange, Orange, prepPurple, Purple, prepGreen, Green}
	public GameState currentState;
	public GameObject AimforRightText;
	public GameObject AimforLeftText;


	public VideoClip videoToPlayL;
	public VideoClip videoToPlayR;
	private VideoPlayer videoPlayer;
	private VideoSource videoSource; 
	public RawImage rightImage;
	public RawImage leftImage;
	private AudioSource audioSource;
	public AudioClip audioClipLeft;
	public AudioClip audioClipRight;

	//public AudioClip audioToPlayL;
	//public AudioClip audioToPlayR;

	private float x;
	private float y;
	private float z;
	private int LastIndex;
	private float R;





	private SpriteRenderer spriteRenderer; 
	float lastStateChange =0.0f;

	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = balls[0].GetComponent<Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x - ballWidth;
		timerText.text = "TIME LEFT:\n" + Mathf.RoundToInt (timeLeft);

		collectOrangeFace.SetActive (false);
		collectPurpleFace.SetActive (false);
		collectGreenFace.SetActive (false);
		AimforRightText.SetActive (false);
		AimforLeftText.SetActive(false);
		rightImage.enabled=false;
		leftImage.enabled = false;

		Application.runInBackground = true;

		SetCurrentState (GameState.Handed);

	
	}

	public void SetCurrentState(GameState state)
	{
		currentState = state;
		lastStateChange = Time.time;
	}

	float GetStateElapsed()
	{
		return Time.time - lastStateChange;
	}


	void FixedUpdate () {
		if (counting) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				timeLeft = 0;
			}
			timerText.text = "TIME LEFT:\n" + Mathf.RoundToInt (timeLeft);
		}
	}



	void Update()
	{
		switch(currentState){

		case GameState.Handed:


		//	Debug.Log("Left or Right");
			if (Input.GetKeyDown (KeyCode.L)) { //left handed 
				leftImage.enabled=true;
				Vector3 lefthand = transform.position;
				lefthand.x = 7.5f;
				lefthand.y = 0.0f;
				lefthand.z = 0.0f;
				faceController.transform.position= lefthand; 
				//Debug.Log("Child is left handed");
				LeftButton.SetActive (false);
				RightButton.SetActive (false);
				AimforLeftText.SetActive (true);
				StartCoroutine (playVideoL ());
				faceController.rightHand = false;

				//SetCurrentState (GameState.Startcalibrate);

			}
			if (Input.GetKeyDown (KeyCode.R)) { //right handed
				rightImage.enabled=true;
				Vector3 righthand = transform.position;
				righthand.x = -7.5f;
				righthand.y = 0.0f;
				righthand.z = 0.0f;
				faceController.transform.position= righthand; 
			//	Debug.Log("Child is right handed");
				LeftButton.SetActive (false);
				RightButton.SetActive (false);
				AimforRightText.SetActive (true);
				StartCoroutine (playVideoR ());
				faceController.rightHand = true;


					//SetCurrentState (GameState.Startcalibrate);

			}

			break;

		case GameState.Startcalibrate:
			//Debug.Log("StartCalib");
			calibration.Startcal ();
			SetCurrentState (GameState.Calibrate);

			break;
			
		case GameState.Calibrate:
			//Debug.Log ("Calib");
			rightImage.enabled = false;

			leftImage.enabled = false;
		
			break;

		case GameState.detectOrange:
			



			changeSprite.StartChanging();
			detect_collision.StartScoring ();
			SetCurrentState (GameState.Orange);


			break;

		case GameState.Orange:

			changeSprite.UpdateSprite ();
			AimforRightText.SetActive (false);
			AimforLeftText.SetActive (false);

			//Debug.Log ("In Orange");

			break;

		case GameState.prepPurple:
			
			//collectOrangeFace.SetActive (false);
			collectPurpleFace.SetActive (true);
			//Debug.Log ("preparing purple");
			SetCurrentState (GameState.Purple);

			break;

		case GameState.Purple:

			changeSprite.UpdateSprite ();
		//	Debug.Log ("In purple");
			break;

		case GameState.prepGreen:
			collectGreenFace.SetActive (true);
			//Debug.Log ("Preparing green");
			SetCurrentState (GameState.Green);

			break;

		case GameState.Green:
			changeSprite.UpdateSprite ();
		//	Debug.Log ("Fuck Yes");
			break;
		}
	}

	public void StartGame () {
		splashScreen.SetActive (false);
		startButton.SetActive (false);
		StartCoroutine (Spawn ());
		SetCurrentState (GameState.Orange);
	}

	public IEnumerator Spawn () {
		yield return new WaitForSeconds (2.0f);
		counting = true;
		while (timeLeft > 0) {
//			GameObject ball = balls [Random.Range (0, balls.Length)];
//
////				Vector3 spawnPosition = new Vector3 (
////					                        -1.0f, 
////					                        transform.position.y, 
////					                        0.0f
////				                        );
//			
//			
//			R=Random.Range(0f,11f); 
//			if (R < 5.5) {
//				R -= 7.5f;
//			} else {
//				R -= 3.5f;
//			};
//
//				Vector3 spawnPositionB = new Vector3 (
//					                    //   transform.position.x + Random.Range (-maxWidth, maxWidth), 
//				transform.position.x + R, 
//					                       transform.position.y, 
//					                       0.0f
//				                       );
//
//
//		
//
//
//			Quaternion spawnRotation = Quaternion.identity;
//
//				Instantiate (ball, spawnPosition, spawnRotation);
//				yield return new WaitForSeconds (Random.Range (1.0f, 2.0f));
			GameObject ball = balls [Random.Range (0, balls.Length)];
		
		
			float[] positions = {-1.5f, 1.5f, -2.5f, 2.5f, -3.5f, 3.5f, -4.5f, 4.5f, -5.5f, 5.5f, -6.5f, 6.5f,-7.0f, 7.0f,-7.0f,7.0f};

			Quaternion spawnRotation = Quaternion.identity;

			Vector3 spawnPosition = new Vector3 (transform.position.x + positions[LastIndex], transform.position.y, 0.0f); 
			LastIndex++;

			if (currentState == GameState.Orange) {
				
				//Instantiate (balls [0], spawnPositionB, spawnRotation);
				Instantiate (balls [1], spawnPosition, spawnRotation);

			yield return new WaitForSeconds (3.0f);
			}
			yield return new WaitForSeconds (1.0f);
			if (currentState == GameState.Purple) {
				Instantiate (balls [5], spawnPosition, spawnRotation);
				yield return new WaitForSeconds (3.0f);
			}
			yield return new WaitForSeconds (1.0f);
			if (currentState == GameState.Green) {
				Instantiate (balls [2], spawnPosition, spawnRotation);
				yield return new WaitForSeconds (3.0f);
			}


		}
		yield return new WaitForSeconds (2.0f);
		collectGreenFace.SetActive (false);
		WelldoneText.SetActive (true);
		yield return new WaitForSeconds (3.0f);
		WelldoneText.SetActive (false);
		restartButton.SetActive (true);



	}
	IEnumerator playVideoR(){

		videoPlayer=gameObject.AddComponent<VideoPlayer>();
		audioSource=gameObject.AddComponent<AudioSource>();

		videoPlayer.playOnAwake=false;
		audioSource.playOnAwake=false;
		//audioSource.Pause();

		videoPlayer.source =VideoSource.VideoClip;
		audioSource.clip = audioClipRight;





		videoPlayer.audioOutputMode= VideoAudioOutputMode.None;

		videoPlayer.EnableAudioTrack(0, true);
		videoPlayer.SetTargetAudioSource(0,audioSource);

		videoPlayer.clip=videoToPlayR;
		videoPlayer.Prepare();

		while (!videoPlayer.isPrepared)
		{
			yield return null;
		}

		//Debug.Log("done prep video");

		rightImage.texture=videoPlayer.texture;

		videoPlayer.Play ();

		audioSource.Play();

	//	Debug.Log("playing video");
		while(videoPlayer.isPlaying)
		{
			//Debug.LogWarning("video time: " +Mathf.FloorToInt((float)videoPlayer.time));
			yield return null;
		}
		SetCurrentState (GameState.Startcalibrate);

		//Debug.Log("done playing Video");
	}

	IEnumerator playVideoL(){

		videoPlayer=gameObject.AddComponent<VideoPlayer>();
		audioSource=gameObject.AddComponent<AudioSource>();

		videoPlayer.playOnAwake=false;
		audioSource.playOnAwake=false;
		audioSource.Pause();

		videoPlayer.source =VideoSource.VideoClip;
		audioSource.clip = audioClipLeft;


		videoPlayer.audioOutputMode= VideoAudioOutputMode.None;

		videoPlayer.EnableAudioTrack(0, true);
		videoPlayer.SetTargetAudioSource(0,audioSource);

		videoPlayer.clip=videoToPlayL;
		videoPlayer.Prepare();

		while (!videoPlayer.isPrepared)
		{
			yield return null;
		}

	//	Debug.Log("done prep video");

		leftImage.texture=videoPlayer.texture;

		videoPlayer.Play ();

		audioSource.Play();

		//Debug.Log("playing video");
		while(videoPlayer.isPlaying)
		{
			//Debug.LogWarning("video time: " +Mathf.FloorToInt((float)videoPlayer.time));
			yield return null;
		}
		SetCurrentState (GameState.Startcalibrate);

		//Debug.Log("done playing Video");
	}


}
