using UnityEngine;
using System.Collections;

public class MaxScoreScript : MonoBehaviour {

    public UnityEngine.UI.Text bestScore;
	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

        if (PlayerPrefs.HasKey("maxScore"))
        {
            bestScore.text = PlayerPrefs.GetInt("maxScore").ToString();
        }
        else
        {
            bestScore.text = "0";
        }
	}
}
