using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public Map[] maps;
	public int mapIndex;

	public GameObject obstaclePrefab;
	public GameObject rotateObstaclePrefab;
	public GameObject destinationPrefab;

	private Map currentMap;

	private void Awake()
	{
		FindObjectOfType<GameManager>().OnNewWave += OnNewWave;
	}

	void OnNewWave(int waveNumber)
	{
		mapIndex = waveNumber - 1;
		if (mapIndex >= maps.Length)
			return;
		GeneratorMap();
	}

	public void GeneratorMap()
	{
		//Create map holder object
		string holderName = "Generated Map";
		if (transform.Find (holderName)) {
			DestroyImmediate(transform.Find (holderName).gameObject);
		}

		Transform mapHolder = new GameObject(holderName).transform;
		mapHolder.parent = transform;

		Camera mainCam = Camera.main;

		//set left wall.
		GameObject leftWall = new GameObject();
		Wall left_wall = leftWall.AddComponent<Wall>();
		left_wall.SettingToWall(Wall.WallState.Left);
		leftWall.name = "left_wall";
		leftWall.transform.position = new Vector2(-mainCam.orthographicSize * 1.78f, 0);
		BoxCollider2D left_collider = leftWall.AddComponent<BoxCollider2D>();
		left_collider.size = new Vector2(1, mainCam.orthographicSize * 2);
		left_collider.offset = new Vector2(-0.5f, 0);
		leftWall.transform.parent = mapHolder;

		//set right wall.
		GameObject rightWall = new GameObject();
		Wall right_wall = rightWall.AddComponent<Wall>();
		right_wall.SettingToWall(Wall.WallState.Right);
		rightWall.name = "right_wall";
		rightWall.transform.position = new Vector2(mainCam.orthographicSize* 1.78f, 0);
		BoxCollider2D right_collider = rightWall.AddComponent<BoxCollider2D>();
		right_collider.size = new Vector2(1, mainCam.orthographicSize* 2);
		right_collider.offset = new Vector2(0.5f, 0);
		right_collider.transform.parent = mapHolder;

		//set upper wall.
		GameObject upperWall = new GameObject();
		Wall upper_wall = upperWall.AddComponent<Wall>();
		upper_wall.SettingToWall(Wall.WallState.Upper);
		upperWall.name = "upper_wall";
		upperWall.transform.position = new Vector2(0,mainCam.orthographicSize);
		BoxCollider2D upper_collider = upperWall.AddComponent<BoxCollider2D>();
		upper_collider.size = new Vector2(mainCam.orthographicSize * 2 * 1.78f, 1);
		upper_collider.offset = new Vector2(0,0.5f);
		upper_collider.transform.parent = mapHolder;

		//set bottom wall. = floor
		GameObject bottomWall = new GameObject();
		Wall bottom_wall = bottomWall.AddComponent<Wall>();
		bottom_wall.SettingToWall(Wall.WallState.Bottom);
		bottomWall.name = "bottom_wall";
		bottomWall.layer = 31;//floor
		bottomWall.transform.position = new Vector2(0,-mainCam.orthographicSize);
		BoxCollider2D bottom_collider = bottomWall.AddComponent<BoxCollider2D>();
		bottom_collider.size = new Vector2(mainCam.orthographicSize* 2 * 1.78f, 1);
		bottom_collider.offset = new Vector2(0,-0.5f);
		bottom_collider.transform.parent = mapHolder;



		//map setting
		if (mapIndex >= maps.Length)
			return;
		
		GameObject go;
		currentMap = maps[mapIndex];

		//set obstacle
		for (int i = 0; i < currentMap.obstaclePos.Length; i++)
		{
			go = Instantiate(obstaclePrefab, currentMap.obstaclePos[i], Quaternion.identity);
			SpriteRenderer customrenderer = go.GetComponent<SpriteRenderer>();
			if (customrenderer != null)
				customrenderer.color = GTColor.blueviolet;
			go.transform.parent = mapHolder;
		}

		//set rotate obstacle
		for (int i = 0; i<currentMap.rotateObstacles.Length; i++)
		{
			go = Instantiate(rotateObstaclePrefab, currentMap.rotateObstacles[i].rotateObstaclePos, Quaternion.identity);
			SpriteRenderer customrenderer = go.GetComponent<SpriteRenderer>();
			if (customrenderer != null)
				customrenderer.color = GTColor.blueviolet;

			RotateObstacle ro = go.GetComponent<RotateObstacle>();
			if (ro != null)
				ro.Setting(currentMap.rotateObstacles[i].rotateObstacleSpeed,currentMap.rotateObstacles[i].IsRotateToClockDir);

			go.transform.parent = mapHolder;
		}
			
		//set destination
		go = Instantiate(destinationPrefab, currentMap.destinationPos, Quaternion.identity);
		SpriteRenderer customrenderer2 = go.GetComponent<SpriteRenderer>();
		if (customrenderer2 != null)
			customrenderer2.color = GTColor.brown;
		go.transform.parent = mapHolder;
	}

	[System.Serializable]
	public class Map
	{
		public Vector2 destinationPos;
		public Vector2[] obstaclePos;
		public SetRotateObstacle[] rotateObstacles;
	}

	[System.Serializable]
	public class SetRotateObstacle
	{
		public Vector2 rotateObstaclePos;
		public bool IsRotateToClockDir;
		public float rotateObstacleSpeed;
	}
}
