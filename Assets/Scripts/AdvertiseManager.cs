using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class AdvertiseManager : MonoBehaviour
{
	private static AdvertiseManager _instance;
	public static AdvertiseManager Instance
	{
		get
		{
			if (_instance == null)
				_instance = FindObjectOfType(typeof(AdvertiseManager)) as AdvertiseManager;
			return _instance;
		}
	}

	private const string appID_googldPlay = "1610100";
	private const string appID_iOS = "1610101";

#if UNITY_ADS
	private void Start()
	{
		Advertisement.Initialize(Application.platform == RuntimePlatform.Android?appID_googldPlay:appID_iOS);
	}

	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show(options);
		}
	}

	void HandleShowResult(ShowResult result)
	{
		if (result == ShowResult.Finished || result == ShowResult.Skipped)
		{
			//get reward.

		}
		else if (result == ShowResult.Failed)
		{
			Debug.LogError("Video failed to show");
		}
	}
#endif
}