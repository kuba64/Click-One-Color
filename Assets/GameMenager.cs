using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class GameMenager : MonoBehaviour {
	public Color FirstColor; 
	public Color a,b;
	public Color hightscore_color;
	public bool firstClick = true;
	public Text punkty_ui;
	public GameObject[] poziomy_trudnosci;
	public int[] progi_punktowe;
	public float[] czas_zmiany;
	public int level;

	public int punkty;
	public float current_time;
	public static GameMenager instance = null;

	public GameObject background;


	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);


		//PlayerPrefs.DeleteAll ();
	}
	
	// Update is called once per frame
	void Update () {


		if (punkty > PlayerPrefs.GetInt ("Najlepszy wynik")) {
			punkty_ui.color = hightscore_color;
		}else 
			punkty_ui.color = Color.black;
		
		if (firstClick == true) {


		}
		else if(firstClick==false){
		//punkty
		punkty_ui.text = punkty.ToString ();

		
		if (progi_punktowe [level+1] == punkty) {
			poziomy_trudnosci [level].SetActive (false);
			level++;
			poziomy_trudnosci [level].SetActive (true);
			current_time = czas_zmiany [level];
		}

		current_time -=Time.deltaTime;


		if (current_time <= 0) {
			for (int i = 0; i < poziomy_trudnosci [level].transform.childCount ; i++) {
				Transform child = poziomy_trudnosci [level].transform.GetChild (i);

				if (child.GetComponent<Image> ().color == a)
					child.GetComponent<Image> ().color = b;
				
				else 
					child.GetComponent<Image> ().color = a;
			
			}
			current_time = czas_zmiany [level];

		}


		//rotacja
		if (punkty >= progi_punktowe [1] - 5) {
			poziomy_trudnosci [level].transform.Rotate ( Vector3.forward, Time.deltaTime * 150);

				//nierchuome małe kulki
				for (int i = 0; i < poziomy_trudnosci [level].transform.childCount ; i++) {
					Transform child = poziomy_trudnosci [level].transform.GetChild (i);
					child.transform.Rotate ( -Vector3.forward, Time.deltaTime * 150);
				}
		}


		}
	




	
}


	public void ClickButton(){
		Color a = EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().color;
		if (firstClick == true) {
			punkty = 0;
			FirstColor = a;
			firstClick = false;
		}

		if (FirstColor == a)
			punkty++;
		else {
			Lost ();
		}

		
	}

	public void Lost()
	{
		if (punkty > PlayerPrefs.GetInt ("Najlepszy wynik")) {
			PlayerPrefs.SetInt ("Najlepszy wynik", punkty);
			
		}
		Mennu.instance.score.SetActive (true);
		poziomy_trudnosci [level].SetActive (false);
		level = 0;
		poziomy_trudnosci [level].SetActive (true);
		current_time = czas_zmiany [level];
		poziomy_trudnosci [0].transform.localRotation = new Quaternion (0, 0, 0, 0);
		firstClick = true;
	}



	public void Zmiana_kolor(){
		for (int j = 0; j < poziomy_trudnosci.Length; j++) {
			for (int i = 0; i < poziomy_trudnosci [j].transform.childCount; i++) {
				Transform child = poziomy_trudnosci [j].transform.GetChild (i);
				if (child.GetComponent<Image> ().color == a) {
					child.GetComponent<Image> ().color = EventSystem.current.currentSelectedGameObject.GetComponent<Kolor_Button> ().a;

				}else
					child.GetComponent<Image> ().color = EventSystem.current.currentSelectedGameObject.GetComponent<Kolor_Button> ().b;
				

			}

		
		}

		a = EventSystem.current.currentSelectedGameObject.GetComponent<Kolor_Button> ().a;
		b = EventSystem.current.currentSelectedGameObject.GetComponent<Kolor_Button> ().b;
		background.GetComponent<Image>().color = EventSystem.current.currentSelectedGameObject.GetComponent<Kolor_Button> ().back;

	


	}


}
