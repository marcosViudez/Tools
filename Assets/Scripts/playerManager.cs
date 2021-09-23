using UnityEngine;
using System.Collections;

public class playerManager : MonoBehaviour {

	private Rigidbody2D playerRigidBody2D;
	private Animator anim;
	public GameObject sueloFail;

	private float movePlayerVector;

	public float speedWalkPlayer = 200.0f;			// velocidad de movimiento Horizontal																		
	public string numeroPowerUpCasilla;

	public bool facingRight;
	public bool cajaLevantada;

	public bool estasMuerto;

	// Use this for initialization
	void Start () 
	{
		// Debug.Log (transform.position);
		anim = GetComponent<Animator>();	
		playerRigidBody2D = (Rigidbody2D)GetComponent (typeof(Rigidbody2D));		// inciamos nuestra componente rigidbody2D
											
	}	
	
	// Update is called once per frame
	void Update () 
	{
		powerUPsPlayer ();

		if(!GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().playerMuerto)
		{
			movePlayerVector = Input.GetAxis ("Horizontal");																	// teclas mover cursores en la horizontal
		}

		playerRigidBody2D.velocity = new Vector2 (movePlayerVector * speedWalkPlayer, playerRigidBody2D.velocity.y);		// movemos el jugador

		if (Mathf.Abs (movePlayerVector) > 0) {
			anim.SetBool ("andar", true);
			anim.SetBool ("upCaja", false);
		} else {
			anim.SetBool ("andar", false);
		}

		if (movePlayerVector > 0 && !facingRight) 
		{
			Flip ();

		} else if (movePlayerVector < 0 && facingRight) 
		{
			Flip ();
		}

		if (Input.GetKey (KeyCode.Space) && Mathf.Abs (movePlayerVector) == 0) {
			// levantamos caja de herramientas
			anim.SetBool ("upCaja", true);
			cajaLevantada = true;
		} else {
			anim.SetBool ("upCaja", false);
			cajaLevantada = false;
		}

		if (GameObject.FindGameObjectWithTag ("panelJuego").GetComponent<JuegoManager> ().playerMuerto) {
			anim.SetBool ("muerto", true);
			// GameObject.FindGameObjectWithTag ("Suelo").GetComponent<Collider2D> ().enabled = false; 
			// sueloFail.GetComponent<Collider2D> ().enabled = false;
			// this.GetComponent<Rigidbody2D> ().gravityScale = 10;
		}
	}

	void powerUPsPlayer()
	{
		if (numeroPowerUpCasilla == "velocidad") {
			speedWalkPlayer = 300.0f;
		}
	}

	void Flip()
	{
		// giramos el sprite 
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
