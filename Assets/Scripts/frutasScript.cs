using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class frutasScript : MonoBehaviour {

	public float speedDownfruit = 3.0f;		// velocidad de bajada de fruta
	private int numeroFruit;
	private int puntosFrutas;

	public AudioClip crearFruta;

	public Sprite[] frutasImages = new Sprite[5];
	public string[] typeFrutas = new string[5];
	public int[] pointsFrutas = new int[5];

	// Use this for initialization
	void Start () 
	{
		rellenarPtosFrutas ();
		rellenarNombresFrutas ();
		escogerFruit ();
		gameObject.name = typeFrutas [numeroFruit];			
		puntosFrutas = pointsFrutas[numeroFruit];				
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.GetComponent<Rigidbody2D> ().gravityScale = speedDownfruit;
	}

	void escogerFruit()
	{
		numeroFruit = Random.Range (0, 4);
		GetComponent<Image> ().sprite = frutasImages [numeroFruit];
		GetComponent<AudioSource> ().PlayOneShot (crearFruta);
	}

	void rellenarNombresFrutas()
	{
		typeFrutas [0] = "manzana";
		typeFrutas [1] = "naranja";
		typeFrutas [2] = "platanos";
		typeFrutas [3] = "pastel";
		typeFrutas [4] = "bocata";
	}

	void rellenarPtosFrutas()
	{
		pointsFrutas [0] = 100;
		pointsFrutas [1] = 200;
		pointsFrutas [2] = 250;
		pointsFrutas [3] = 300;
		pointsFrutas [4] = 500;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			// Debug.Log ("fruta cogida");
			GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().sumarScorePlayerOne (puntosFrutas);	// sumamos ptos metodo otro script
			Destroy (gameObject);				// destruimos el gameobject fruta cogida
		} 

		if (other.gameObject.tag == "Suelo") 
		{
			// Debug.Log ("fruta Perdida");
			Destroy (gameObject,5);				// destruimos el gameobject fruta perdida
		}
	}
}
