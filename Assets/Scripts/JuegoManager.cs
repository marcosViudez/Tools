using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JuegoManager : MonoBehaviour {

	public GameObject tools;
	public GameObject frutas;
	public GameObject powerUPs;
	public GameObject powerUPCasilla;
	public GameObject andamio;

	public Text textTime;
	public Text textToolsCoger;
	public Text textscorePlayerUno;
	public Text textToolsStored;
	public Text infoEmpiezaJuego;
	public Text scoreCaida;
	public Text textoLevelStatus;
	public Text hiScoreTexto;

	public AudioClip cogerTools;
	public AudioClip newLevelSound;
	public AudioClip gameOverSound;

	private int tiempoEsperaPowerUpsCasilla = 6;
	private int vidasPlayerUno;
	public int numeroToolsCoger = 5;
	public int numeroToolsTiradas = 10;
	public int numeroToolsRecogidas;
	public int numeroToolsCreadas;
	public int numeroToolsPowerUP;

	private int levelGame = 1;
	private int tiempoGame;
	private int hiScore;
	private int scorePlayerUno;
	private int creditos;
	private int timeCreation = 2;

	public bool soundGameOver = false;
	public bool soundLevel = false;
	public bool playerMuerto = false;
	private bool juegoPausado = false;
	private int puntosPremio = 600;
	public int puntosFrutasConseguir = 0;

	private float minX = 59;
	private float maxX = 1117;
	private float alturaCreacion;

	// Use this for initialization
	void Start () 
	{
		// tiempoGame = 0;
		alturaCreacion = andamio.transform.position.y;
		numeroToolsRecogidas = 0;
		numeroToolsPowerUP = 0;
		leerDatosJuego ();
		StartCoroutine (creandoToolsLevel ());
		StartCoroutine (letraInicio ());
		// dificultadNiveles ();
	}

	void dificultadNiveles()
	{
		// configuracion de niveles
		if (levelGame >= 1) 
		{
			// aumentamos velocidad de caida tools para niveles altos
			PlayerPrefs.SetFloat ("gravedadReloj",4.0f);
		}
	}

	public void llamadaTiempoPowerUP()
	{
		StartCoroutine (tiempoPowerUpsCasilla ());
	}

	IEnumerator tiempoPowerUpsCasilla()
	{
		yield return new WaitForSeconds (tiempoEsperaPowerUpsCasilla);	
		GameObject.FindGameObjectWithTag ("Player").GetComponent<playerManager> ().numeroPowerUpCasilla = ""; 
		powerUPCasilla.GetComponent<Image> ().sprite = null;
		powerUPCasilla.GetComponent<Image> ().enabled = false;
		PlayerPrefs.SetFloat("gravedadReloj",3.0f);
		GameObject.FindGameObjectWithTag ("Player").GetComponent<playerManager> ().speedWalkPlayer = 200.0f;

	}


	void leerDatosJuego()
	{
		levelGame = PlayerPrefs.GetInt ("nivelJuego");
		hiScoreTexto.text = PlayerPrefs.GetInt ("scoreUno").ToString ();
		// vidasPlayerUno = PlayerPrefs.GetInt ("vidasPlayerUno");
		// numeroToolsCoger = PlayerPrefs.GetInt ("numeroToolsCoger");
		// numeroToolsTiradas = PlayerPrefs.GetInt ("numeroToolsTiradas");
		scorePlayerUno = PlayerPrefs.GetInt("scorePlayerUno");
		puntosFrutasConseguir = PlayerPrefs.GetInt("scoreFruit");
	}

	IEnumerator letraInicio()
	{
		infoEmpiezaJuego.GetComponent<Text> ().enabled = true;
		infoEmpiezaJuego.GetComponent<Text> ().text = "Level " + levelGame;		// mostramos texto level
		yield return new WaitForSeconds (timeCreation);							// tiempo de muestreo de texto
		infoEmpiezaJuego.GetComponent<Text> ().enabled = false;
	}

	public void mostrarTextoScoreCaida(int puntos, Vector3 posTools)
	{
		StartCoroutine (mostrarScoreCaida (puntos, posTools));		// mostrar puntos al recogerlos
	}

	IEnumerator mostrarScoreCaida(int puntos, Vector3 posTools)
	{
		scoreCaida.GetComponent<Text> ().enabled = true;
		scoreCaida.rectTransform.position = posTools;
		scoreCaida.GetComponent<Text> ().text = puntos.ToString();
		yield return new WaitForSeconds (1);	
		scoreCaida.GetComponent<Text> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		gamePause ();								// pausa de juego
		// tiempoGame = (int)Time.fixedTime;		// reloj de tiempo
		powerUPCrear();								// crea premios
		textosPantalla();							// textos mostrados en pantalla
		ExitGameOff ();								// salir de juego

		textoLevelStatus.text = levelGame.ToString ();

		if (Input.GetKeyDown (KeyCode.P)) 
		{
			juegoPausado = !juegoPausado;			// pausamos el juego
		}

		// cargamos un nuevo level al recoger las tools necesarias
		if (numeroToolsCreadas >= numeroToolsTiradas && numeroToolsRecogidas >= numeroToolsCoger) {
			StartCoroutine (newLevel ());
		} 

		if(numeroToolsCreadas >= numeroToolsTiradas && numeroToolsRecogidas < numeroToolsCoger) {
			StartCoroutine (gameOver ());
		}

		if (puntosFrutasConseguir >= puntosPremio) 
		{
			GameObject fruitCreate = Instantiate (frutas, new Vector3 (Random.Range (minX, maxX), alturaCreacion, 0), transform.rotation) as GameObject;
			fruitCreate.transform.SetParent (this.transform, true);
			puntosFrutasConseguir = 0;
		}
	}

	void gamePause()
	{
		if (juegoPausado) {
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}

	}

	void powerUPCrear()
	{
		// cada tools cogidas crea un premio
		if (numeroToolsPowerUP == 5) 
		{
			numeroToolsPowerUP = 0;
			GameObject powerUPCreate = Instantiate (powerUPs, new Vector3 (Random.Range (minX, maxX), alturaCreacion, 0), transform.rotation) as GameObject;
			powerUPCreate.transform.SetParent (this.transform, true);
		}
	}

	void nuevoLevelPlaySound()
	{
		if (!soundLevel) {
			soundLevel = true;
			GetComponent<AudioSource> ().PlayOneShot (newLevelSound);
		}
	}
		
	IEnumerator  newLevel()
	{
		nuevoLevelPlaySound ();	
		PlayerPrefs.SetInt ("scorePlayerUno", scorePlayerUno);
		PlayerPrefs.SetInt ("scoreFruit", puntosFrutasConseguir);
		PlayerPrefs.SetFloat ("gravedadReloj", 3.0f);
		infoEmpiezaJuego.GetComponent<Text> ().enabled = true;
		infoEmpiezaJuego.GetComponent<Text> ().text = "Congratulations !!! ";		// mostramos texto level
		yield return new WaitForSeconds (3);									// tiempo de muestreo de texto
		infoEmpiezaJuego.GetComponent<Text> ().enabled = false;
		SceneManager.LoadScene ("fase01");

		levelGame++;															// subimos de nivel
		soundLevel = false;
		PlayerPrefs.SetInt ("nivelJuego", levelGame);							// grabamos el nivel
	}

	public void llamadaGameOver()
	{
		StartCoroutine (gameOver ());
	}

	void soundGameOverPlay()
	{
		if (!soundGameOver) {
			soundGameOver = true;
			GetComponent<AudioSource> ().PlayOneShot (gameOverSound);
		}
	}

	IEnumerator gameOver()
	{
		soundGameOverPlay ();
		PlayerPrefs.SetInt ("scorePlayerUno", scorePlayerUno);
		infoEmpiezaJuego.GetComponent<Text> ().enabled = true;
		infoEmpiezaJuego.GetComponent<Text> ().text = "Game Over !!! ";		// mostramos texto level
		yield return new WaitForSeconds (3);									// tiempo de muestreo de texto
		infoEmpiezaJuego.GetComponent<Text> ().enabled = false;
		soundGameOver = false;
		SceneManager.LoadScene ("hiScore");
	}

	void textosPantalla()
	{
		// textTime.text = tiempoGame.ToString ();
		textToolsCoger.text = numeroToolsCoger.ToString ();
		textscorePlayerUno.text = scorePlayerUno.ToString ();
		textToolsStored.text = numeroToolsRecogidas.ToString ();
	}

	void crearPowerUPs()
	{
		GameObject powerUPCreate = Instantiate (powerUPs, new Vector3 (Random.Range (minX, maxX), alturaCreacion, 0), transform.rotation) as GameObject;
		powerUPCreate.transform.SetParent (this.transform, true);
	}

	IEnumerator creandoToolsLevel()
	{
		for (int i = 0; i < numeroToolsTiradas; i++) 
		{
			GameObject toolCreate = Instantiate (tools, new Vector3 (Random.Range (minX, maxX), alturaCreacion, 0), transform.rotation) as GameObject;
			toolCreate.transform.SetParent (this.transform, true);		// hacemos las tools hijas del panel juego
			// numeroToolsCreadas++;
			yield return new WaitForSeconds (timeCreation);				// esperamos un tiempo antes de crear otra
		}
	}

	public void sumarToolsRecogidas()
	{
		GetComponent<AudioSource> ().PlayOneShot (cogerTools);
		numeroToolsRecogidas++;
	}

	public void sumarScorePlayerOne(int ptosSumar)
	{
		scorePlayerUno += ptosSumar;		// suma ptos en el player uno
		puntosFrutasConseguir += ptosSumar;
	}

	public void sumarScoreFrutas(int fruitSumar)
	{
		scorePlayerUno += fruitSumar;		// suma ptos en el player uno
		puntosFrutasConseguir += fruitSumar;
	}

	void ExitGameOff()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			// Application.Quit ();		// salimos del juego pulsando teclaESC
			SceneManager.LoadScene("menu");
		}
	}
}
