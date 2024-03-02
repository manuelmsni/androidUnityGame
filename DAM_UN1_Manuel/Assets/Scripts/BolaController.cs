using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaController : MonoBehaviour
{
    //Creamos un RigidBody
    public Rigidbody rb;

    //Necesitamos un impulso para controlar cu�nto bota la bola
    public float fuerzaImpulso = 3f;

    //A�adimos un boolean para controlar que bote una vez s�lo sobre una pieza (un quesito), no sobre dos
    //queremos que cuando bote una vez, no pueda botar otra vez hasta tantos segundos
    public bool ignorarSiguienteColision;

    //Declaramos una variable para almacenar la posici�n inicial de la bola
    private Vector3 posicionInicial;

    //Almacenamos la posici�n inicial con el m�todo Start
    //ya que este m�todo se ejecuta al darle al Play y as� almacenamos
    //la posici�n que tiene la bola al incio del juego
    private void Start()
    {
        posicionInicial = transform.position;
    }

    //�Cu�ndo queremos que bote? Cuando toque el HollyTop o un HollyLevel
    //C# trae un m�todo para esto: onCollisionEnter, cuando nuestra pelota entre en colisi�n con algo se ejecuta este m�todo
    private void OnCollisionEnter(Collision colision)
    {
        //En el momento que bote una vez, salimos del m�todo
        if (ignorarSiguienteColision)
        {
            return;
        }

        //Vamos a crear un objeto DeathPart que s�lo se va a asignar si la bola choca con un quesito rojo
        //es decir, si choca con un componente de tipo DeathPart
        DeathPart deathpart = colision.transform.GetComponent<DeathPart>();

        //Si chocamos con un quesito rojo
        if (deathpart)
        {
            //reseteamos el nivel
            GameManager.singleton.RestartNivel();
        }

        //Cuando colisionemos con algo, le a�adimos velocidad de Vector3 a cero para evitar problemas de velocidad al colisionar
        //Vector3 son las tres coordenadas en cuanto a posicion
        rb.velocity = Vector3.zero;

        //Cuando choque queremos que vaya hacia arriba, por lo que le a�adimos fuerza en el eje de la Y queriendo que vaya para arriba
        //lo que hemos declarado en fuerzaImpulso
        //El ForceMode Impulse a�ade el impulso del rigibody usando su masa
        rb.AddForce(Vector3.up * fuerzaImpulso, ForceMode.Impulse);

        //cuando ya ha botado una vez ponemos el boolean a true
        ignorarSiguienteColision = true;

        //Llamamos al m�todo de permitirSiguienteColision() para que vuelva a botar la bola pasados 0.2 ms
        Invoke("PermitirSiguienteColision", 0.2f);

    }

    //Para controlar que pueda volver a botar pasados unos cuantos segundos
    private void PermitirSiguienteColision()
    {
        ignorarSiguienteColision = false;
    }

    //Creamos un m�todo para resetear la bola y que vuelva a la posici�n inicial
    public void ResetBola()
    {
        transform.position = posicionInicial;
    }
}