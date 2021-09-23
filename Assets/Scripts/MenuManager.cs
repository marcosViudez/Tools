using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public Text textScorePlayerUno;
	public AudioClip soundBotonesCLick;


	// Use this for initialization
	void Start () 
	{

		textScorePlayerUno.GetComponent<Text> ().enabled = false;	// ocultamos el score player 01 ya que no hay monedas
	}

	// Update is called once per frame
	void Update () 
	{
	
	}

	public void sonidoBotonesPlay ()
	{
		GetComponent<AudioSource> ().PlayOneShot (soundBotonesCLick);

	}

	public void menu()
	{
		SceneManager.LoadScene ("menu");
	}

	public void jugar()
	{
		PlayerPrefs.SetInt ("nivelJuego", 1);
		PlayerPrefs.SetInt ("scorePlayerUno", 0);
		PlayerPrefs.SetInt ("scoreFruit", 0);
		PlayerPrefs.SetFloat ("gravedadReloj", 3.0f);
		// PlayerPrefs.SetInt ("vidasPlayerUno", 3);
		// PlayerPrefs.SetInt ("numeroToolsCoger", 5);
		// PlayerPrefs.SetInt ("numeroToolsTiradas", 10);
		SceneManager.LoadScene ("fase01");
	}

	public void opcionesJuego()
	{
		SceneManager.LoadScene ("opciones");
	}

	public void hiScoreJuego()
	{
		SceneManager.LoadScene ("hiScore");
	}

	public void salirJuego()
	{
		Application.Quit ();
	}
}
