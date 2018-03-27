using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public GameController gameController;
	//	public StateMachine gameController;




	void OnMouseDown () {

		gameController.StartGame ();

	}
}