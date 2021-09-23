using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class toolsManager : MonoBehaviour {

	public float speedDownTool = 3.0f;		// velocidad de bajada de tools
	private int numeroTool;
	public int puntosTool;

	public AudioClip crearTools;

	public int[] pointsTools = new int[9];			// ptos que vale cada herramienta
	public string[] typeTools = new string[9];		// nombre de cada herramienta
	public Sprite[] toolsImages = new Sprite[9];	// imagenes de las tools

	// Use this for initialization
	void Start () 
	{
		speedDownTool = PlayerPrefs.GetFloat ("gravedadReloj");
		rellenarVectorTools ();								// rellenamos el array de herramientas
		listaPuntosTools();									// rellenamos el array de ptos
		escogeTool ();										// escogemos herramienta
		gameObject.name = typeTools [numeroTool];			// le cambiamos nombre al gameobject
		puntosTool = pointsTools[numeroTool];				// numero de ptos que vale la herramienta}
	}

	void escogeTool()
	{
		numeroTool = Random.Range (0, 8);
		GetComponent<Image> ().sprite = toolsImages [numeroTool];	// escoge la foto de la imagen de tools
		GetComponent<AudioSource> ().PlayOneShot (crearTools);
	}

	void listaPuntosTools()
	{
		pointsTools [0] = 35;
		pointsTools [1] = 20;
		pointsTools [2] = 40;
		pointsTools [3] = 30;
		pointsTools [4] = 50;
		pointsTools [5] = 50;
		pointsTools [6] = 10;
		pointsTools [7] = 90;
		pointsTools [8] = 100;
	}

	void rellenarVectorTools()
	{
		typeTools [0] = "llave";
		typeTools [1] = "martillo";
		typeTools [2] = "destornillador";
		typeTools [3] = "inglesa";
		typeTools [4] = "alicate";
		typeTools [5] = "cortante";
		typeTools [6] = "sierra";
		typeTools [7] = "metro";
		typeTools [8] = "cinta";
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.GetComponent<Rigidbody2D> ().gravityScale = speedDownTool;
	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player" && GameObject.FindGameObjectWithTag ("Player").GetComponent<playerManager> ().cajaLevantada) {
			//Debug.Log ("tools cogida");
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().sumarScorePlayerOne (puntosTool);	// sumamos ptos metodo otro script
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().sumarToolsRecogidas ();
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().numeroToolsCreadas++;
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().numeroToolsPowerUP++;
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().mostrarTextoScoreCaida (puntosTool, transform.position);
			Destroy (gameObject);				// destruimos el gameobject tool cogida

		} else if (other.gameObject.tag == "Player" && GameObject.FindGameObjectWithTag ("Player").GetComponent<playerManager> ().numeroPowerUpCasilla != "casco"){
			Destroy (gameObject);				// destruimos el gameobject tool cogida GAMEOVER
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().llamadaGameOver();
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().playerMuerto = true;

		}

		if (other.gameObject.tag == "Player" && GameObject.FindGameObjectWithTag ("Player").GetComponent<playerManager> ().numeroPowerUpCasilla == "casco" && GameObject.FindGameObjectWithTag ("Player").GetComponent<playerManager> ().cajaLevantada) {
			//Debug.Log ("tools cogida");
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().sumarScorePlayerOne (puntosTool);	// sumamos ptos metodo otro script
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().sumarToolsRecogidas ();
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().numeroToolsCreadas++;
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().numeroToolsPowerUP++;
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().mostrarTextoScoreCaida (puntosTool, transform.position);
			Destroy (gameObject);				// destruimos el gameobject tool cogida

		}

		if (other.gameObject.tag == "Player" && GameObject.FindGameObjectWithTag ("Player").GetComponent<playerManager> ().numeroPowerUpCasilla == "casco" && !GameObject.FindGameObjectWithTag ("Player").GetComponent<playerManager> ().cajaLevantada) 
		{
			// si tenemos casco y nos da una llave se destruye el gameobject
			Destroy (gameObject);				// destruimos el gameobject tool perdida

			GameObject.FindGameObjectWithTag ("Player").GetComponent<playerManager> ().numeroPowerUpCasilla = ""; 
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().powerUPCasilla.GetComponent<Image> ().sprite = null;
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().powerUPCasilla.GetComponent<Image> ().enabled = false;
		}

		if (other.gameObject.tag == "Suelo") 
		{
			// Debug.Log ("tools Perdida");
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().numeroToolsCreadas++;
			Destroy (gameObject);				// destruimos el gameobject tool perdida
		}
	}
}
