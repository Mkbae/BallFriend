using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
				_instance = FindObjectOfType(typeof(GameManager)) as GameManager;
			return _instance;
		}
	}

	public TextMeshPro Text_Stage;

	private int WaveNumber;

	public event System.Action<int> OnNewWave;

	private void Start()
	{
		New_Game();
	}

	public void New_Game()
	{
		WaveNumber++;
		StartCoroutine(NewGameEffect());
	}

	private IEnumerator NewGameEffect()
	{
		Text_Stage.color = Color.clear;
		Text_Stage.text = string.Format("Stage {0}", WaveNumber);
		float percent = 0;
		while (percent < 1)
		{
			percent += Time.deltaTime;

			Text_Stage.color = Color.white * percent;
			yield return null;
		}

		yield return new WaitForSeconds(2);

		Text_Stage.color = Color.clear;

		Player.Instance.controller.StateReset();
		if (OnNewWave != null)
            OnNewWave(WaveNumber);
	}

	public void GameClear()
	{
		Debug.Log("Stage Clear : " + WaveNumber);
		New_Game();
	}
}
