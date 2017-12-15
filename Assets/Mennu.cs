using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Mennu : MonoBehaviour {
	public GameObject pointingame;
	public GameObject score,menu;
	public Text best;
	public Text last;
	public GameObject opcje;
	public GameObject customize;

	public static Mennu instance = null;

	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	
	}
	
	// Update is called once per frame
	void Update () {
		best.text = PlayerPrefs.GetInt ("Najlepszy wynik").ToString ();
		last.text = GameMenager.instance.punkty.ToString ();

		if (GameMenager.instance.firstClick == true) {
			menu.SetActive (true);
			pointingame.SetActive (false);
		} else {
			score.SetActive (false);
			menu.SetActive (false);
			pointingame.SetActive (true);
		}
	
	}

	public void Opcje_On(){
		opcje.SetActive (true);
	}

	public void Opcje_Off(){
		opcje.SetActive (false);
	}


	public void Customize_on(){
		GameMenager.instance.Lost ();
		customize.SetActive (true);
	}

	public void Customize_off(){
		customize.SetActive (false);
	}
}
