using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour {

	public float speedDownPowerUP = 3.0f;		// velocidad de bajada de fruta
	private int numeroPowerUP;

	public AudioClip crearPowerUP;

	public Sprite[] PowerUPImages = new Sprite[3];
	public string[] typePowerUP = new string[3];

	// Use this for initialization
	void Start ()
	{
		rellenarNombresPowerUP ();
		escogerPowerUP();
		gameObject.name = typePowerUP [numeroPowerUP];	
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.GetComponent<Rigidbody2D> ().gravityScale = speedDownPowerUP;

		if (numeroPowerUP != 1) 
		{
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().llamadaTiempoPowerUP ();
		}

		if(numeroPowerUP == 1)
		{
			// relentizamos la caida de objetos durante un tiempo determinado
			PlayerPrefs.SetFloat("gravedadReloj",1.0f);
		}

	}

	void escogerPowerUP()
	{
		numeroPowerUP = Random.Range (0, 3);
		GetComponent<Image> ().sprite = PowerUPImages [numeroPowerUP];
		GetComponent<AudioSource> ().PlayOneShot (crearPowerUP);

	}

	void rellenarNombresPowerUP()
	{
		typePowerUP [0] = "casco";
		typePowerUP [1] = "reloj";
		typePowerUP [2] = "velocidad";
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			// Debug.Log ("powerUP cogida");
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().powerUPCasilla.SetActive(true);
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().powerUPCasilla.GetComponent<Image> ().sprite = PowerUPImages [numeroPowerUP];
			GameObject.FindGameObjectWithTag ("Player").GetComponent<playerManager> ().numeroPowerUpCasilla = typePowerUP [numeroPowerUP];
			Destroy (gameObject);				// destruimos el gameobject powerUP cogida
		} 

		if (other.gameObject.tag == "Suelo") 
		{
			// Debug.Log ("powerUP Perdida");
			Destroy (gameObject,5);				// destruimos el gameobject powerUP perdida
		}
	}
}
