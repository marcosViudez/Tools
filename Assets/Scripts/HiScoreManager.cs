using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HiScoreManager : MonoBehaviour {

	public Text textHiScoreUno;
	public Text textHiScoreDos;
	public Text textHiScoreTres;
	public Text textHiScoreCuatro;

	public int scoreConseguido;

	public int[] listaScores = new int[4];

	private int tempScore;

	public bool guardarScore;

	// Use this for initialization
	void Start () 
	{
		leerDatosScores ();
		scoreConseguido = PlayerPrefs.GetInt ("scorePlayerUno");
		// Debug.Log (scoreConseguido);
		guardarScore = false;
		grabarScore ();
	}

	public void resetScores()
	{
		PlayerPrefs.SetInt ("scorePlayerUno", 0);
		PlayerPrefs.SetInt ("scoreUno", 0);
		PlayerPrefs.SetInt ("scoreDos", 0);
		PlayerPrefs.SetInt ("scoreTres", 0);
		PlayerPrefs.SetInt ("scoreCuatro", 0);
		leerDatosScores ();
	}

	void leerDatosScores()
	{
		listaScores[0] = PlayerPrefs.GetInt ("scoreUno");
		listaScores[1] = PlayerPrefs.GetInt ("scoreDos");
		listaScores[2] = PlayerPrefs.GetInt ("scoreTres");
		listaScores[3] = PlayerPrefs.GetInt ("scoreCuatro");
	}
	
	// Update is called once per frame
	void Update () 
	{
		textHiScoreUno.GetComponent<Text> ().text = listaScores[0].ToString ();
		textHiScoreDos.GetComponent<Text> ().text = listaScores[1].ToString ();
		textHiScoreTres.GetComponent<Text> ().text = listaScores[2].ToString ();
		textHiScoreCuatro.GetComponent<Text> ().text = listaScores[3].ToString ();
	}

	void guardarTablaScores()
	{
		PlayerPrefs.SetInt ("scoreUno", listaScores[0]);
		PlayerPrefs.SetInt ("scoreDos", listaScores[1]);
		PlayerPrefs.SetInt ("scoreTres", listaScores[2]);
		PlayerPrefs.SetInt ("scoreCuatro", listaScores[3]);
	}

	void grabarScore()
	{
		// movemos la lista para grabar scores
		for (int i = 0; i < 4; i++) 
		{
			if (scoreConseguido > listaScores [i]) 
			{
				tempScore = listaScores [i];
				listaScores [i] = scoreConseguido;
				scoreConseguido = tempScore;
			}
			guardarScore = true;
		}

		if (guardarScore) 
		{
			// guardo valores y reseteo
			guardarTablaScores ();
			PlayerPrefs.SetInt ("scorePlayerUno", 0);
			scoreConseguido = PlayerPrefs.GetInt ("scorePlayerUno");
		}

	}
}
