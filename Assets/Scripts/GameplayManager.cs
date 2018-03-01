using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {
    public static int gridWidth = 11, gridHeight = 20;
	public static Transform[,]grid = new Transform[gridWidth, gridHeight];
	
	private void Start() {
		GenerateMino();
	}
	private string GetRandomMino(){
		int val = Random.Range(0,7);
		string minoName = "MinoT";
		switch (val)
		{
			
			case 0:
				minoName = "MinoL";
			break;
			case 1:
				minoName = "MinoJ";
			break;
			case 2:
				minoName = "MinoI";
			break;
			case 3:
				minoName = "MinoO";
			break;
			case 4:
				minoName = "MinoS";
			break;
			case 5:
				minoName = "MinoZ";
			break;
			case 6:
				minoName = "MinoT";
			break;
		}
		return "Prefabs/"+minoName;
	}
	public Transform GetTransformAtGridPosition(Vector3 pos){
		if (pos.y > gridHeight - 1)
			return null;
		else
			return grid[(int)pos.x, (int)pos.y];
	}
	public void UpdateGrid(SystemHandler termino){
		for (int y = 0; y < gridHeight; y++)
		{
			for (int x = 0; x < gridWidth; x++)
			{
				if(grid[x,y]!=null){
					if(grid[x,y].parent == termino.transform)
					grid[x,y] = null;
				}
			}
			
		}
		foreach (Transform mino in termino.transform)
		{
			Vector3 pos= Round(mino.position);
			if(pos.y < gridHeight)
				grid[(int)pos.x, (int)pos.y] =mino;
		}
	}
	public void GenerateMino(){
		GameObject termino = (GameObject)Instantiate(Resources.Load(GetRandomMino(),typeof(GameObject)), new Vector3(5.0f, 18.0f, 0.0f),Quaternion.identity);
	}

	
	public bool TermoInsideGrid(Vector3 pos){
		return(
			(int)pos.x >=0 &&
			(int)pos.x < gridWidth &&
			(int)pos.y >=0
		);
	}
	public Vector3 Round(Vector3 pos){
		return new Vector3(
			Mathf.Round(pos.x),
			Mathf.Round(pos.y),
			Mathf.Round(pos.z)
		);
	}
    private bool IsRowFullAt(int y)
    {
		for (int x = 0; x < gridWidth; x++)
		{
			if(grid[x,y]==null)
				return false;
		}
		return true;
    }
    private void DestroyRowAt(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        { 
			Destroy(grid[x,y].gameObject);
			grid[x,y] =null;
        }

    }
    private void MoveRowDown(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if (grid[x, y] != null){
				grid[x,y - 1] = grid[x,y];
				grid[x,y] = null;
				grid[x,y - 1].position += Vector3.down;
			}       
        }
    }
    private void MoveAllRowDowns(int y)
    {
		for (int i =y; i < gridHeight; i++)
			MoveRowDown(i);
    }
    public void DestroyRow()
    {
        for (int y = 0; y < gridHeight; y++)
        {
			if(IsRowFullAt(y)){
                DestroyRowAt(y);
				MoveAllRowDowns(y+1);
				y--;
			}
			
        }
    }
	public bool IsReachedLimitGrid(SystemHandler termino){
		for (int x = 0; x < gridWidth; x++)
		{
			foreach (Transform mino in termino.transform)
			{
				Vector3 pos =Round(mino.position);
				if(pos.y >= gridHeight-1 && !termino.isActiveAndEnabled){
					return true;
				}
			}
		}
		return false;
	}
	public void GameOver(SystemHandler termino){
		SceneManager.LoadScene("Tetris");
		enabled = true;
	}
}
