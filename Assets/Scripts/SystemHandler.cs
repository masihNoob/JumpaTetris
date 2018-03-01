using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemHandler : MonoBehaviour {

	
	private GameplayManager gameplaymanager;
	[SerializeField]
	private float fallSpeed = 1;
	[SerializeField]
	private bool allowRotation = true;
	[SerializeField]
	private bool limitRotation = false;
	private float fall = 0;
	// Use this for initialization
	void Start () {
		gameplaymanager = FindObjectOfType<GameplayManager>();
	}
	
	// Update is called once per frame
	void Update () {
		InputKeyHandler();
		UpdateMino();
	}

	private void InputKeyHandler(){
		if(Input.GetKeyDown(KeyCode.RightArrow))
			Handler("Right");
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
            Handler("Left");
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            Handler("Down");
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            Handler("Action");
	}
	private void UpdateMino(){
		if(Time.time - fall >= fallSpeed){
			Handler("Down");
			fall = Time.time;
		}
	}
	private void Handler(string command)
	{
		switch (command)
		{
			case "Right":
				MoveHorizontal(Vector3.right);
			break;
			case "Left":
            	MoveHorizontal(Vector3.left);
			break;
			case "Down":
				MoveVertical();
			break;
			case "Action":
				if(allowRotation){
					ActionLimitRotataion(1);
					if(!IsValidPosition())
						ActionLimitRotataion(-1);
					else
						gameplaymanager.UpdateGrid(this);
				}
			break;
		}
	}
	private void ActionLimitRotataion(int modifier){
		if(limitRotation){
			if(transform.rotation.eulerAngles.z >=90)
				transform.Rotate(Vector3.forward* -90);
			else
                transform.Rotate(Vector3.forward * 90 );
		}else
		{
            transform.Rotate(Vector3.forward * 90 * modifier);
		}
	}
	private void MoveVertical(){
		transform.position += Vector3.down;
		if(!IsValidPosition()){
            transform.position += Vector3.up;
			gameplaymanager.DestroyRow();
			enabled = false;
			if (gameplaymanager.IsReachedLimitGrid(this))
				gameplaymanager.GameOver(this);
			else
				gameplaymanager.GenerateMino();

		}else
            gameplaymanager.UpdateGrid(this);
	}
	private void MoveHorizontal(Vector3 direction){
		transform.position += direction;
		if(!IsValidPosition())
            transform.position += direction * -1;
        else
            gameplaymanager.UpdateGrid(this);
	}
	private bool IsValidPosition(){
		foreach (Transform mino in transform){
			Vector3 pos = gameplaymanager.Round(mino.position);
			if(!gameplaymanager.TermoInsideGrid(pos))
			return false;
			if (gameplaymanager.GetTransformAtGridPosition(pos)!=null && gameplaymanager.GetTransformAtGridPosition(pos).parent != transform){
				return false;
			}
		}return true;
		
	}

	
}
