using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_colour : MonoBehaviour {

	public Sprite spriteY;
	public Sprite spriteO; // Drag your first sprite here
	public Sprite spriteP; // Drag your second sprite here
	public Sprite spriteG;
	public GameController stateMachine; 

	public SpriteRenderer spriteRenderer; 

	public void StartChanging ()
	{
		//spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
		if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
			spriteRenderer.sprite = spriteY ; // set the sprite to sprite1
	}

	public void UpdateSprite ()
	{
		if (stateMachine.currentState == GameController.GameState.Orange) {

			spriteRenderer.sprite = spriteO;
		}

		if (stateMachine.currentState == GameController.GameState.Purple) {

			spriteRenderer.sprite = spriteP;
		}

		if (stateMachine.currentState == GameController.GameState.Green) {

			spriteRenderer.sprite = spriteG;
		}


		}
	}
//
//	void ChangeTheDamnSprite ()
//	{
//		if (spriteRenderer.sprite == spriteY) // if the spriteRenderer sprite = sprite1 then change to sprite2
//		{
//			spriteRenderer.sprite = spriteO;
//		}
//		if(spriteRenderer.sprite == spriteO)
//		{
//			spriteRenderer.sprite = spriteP ; // otherwise change it back to sprite1
//		}
//		if(spriteRenderer.sprite == spriteP)
//		{
//			spriteRenderer.sprite = spriteG ; // otherwise change it back to sprite1
//		}
	
//}