using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sabrina Sampaio e Paulo Tozzo - projeto robotica 4
//Este script permite o player mover o alvo pelo mapa

public class Move : MonoBehaviour
{
    public GameObject wall;
    public float speed;
    // Inicialização
    void Start()
    {

    }

    // Update é chamado uma vez a cada frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
		if( Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel (0);
			Time.timeScale = 1;
		}
    }
    //proibindo colisao entre paredes e player
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cube") // or if(gameObject.CompareTag("YourWallTag"))
        {
            wall.SetActive(true);
            Time.timeScale = 0;
            /*se inibirmos a velocidade na direção que o vetor de movimento está apontando, podemos fazer o alvo não morrer no contato,
            e conseguir deslizar suavemente enquanto encosta no obstáculo (todo)*/
        }
    }
}
