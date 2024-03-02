using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaController : MonoBehaviour
{
    //Creamos un RigidBody
    public Rigidbody rb;

    //Necesitamos un impulso para controlar cuánto bota la bola
    public float fuerzaImpulso = 3f;

    //Añadimos un boolean para controlar que bote una vez sólo sobre una pieza (un quesito), no sobre dos
    //queremos que cuando bote una vez, no pueda botar otra vez hasta tantos segundos
    public bool ignorarSiguienteColision;

    //Declaramos una variable para almacenar la posición inicial de la bola
    private Vector3 posicionInicial;

    //Añadimos las variables que necesitamos para la función de super velocidad
    //Primero declaramos la variable para saber cuántas veces hemos pasado por un stage sin tocar nada
	[HideInInspector]
    public int pasosPerfectos;

    //Declaramos una variable para saber cuánta velocidad añadirle a la bola cuando estemos en esa super velocidad
    //y lo inicializamos a 8 por ejemplo
    public float superVelocidad = 8;

    //Declaramos una variable para saber si estamos en esa super velocidad o no
    private bool esSuperVelocidad;

    //Declaramos una variable para saber a partir de qué número de pasos de discos perfectos activamos la super velocidad
    public int numeroPasosPerfectos = 3;

    //Almacenamos la posición inicial con el método Start
    //ya que este método se ejecuta al darle al Play y así almacenamos
    //la posición que tiene la bola al incio del juego
    private void Start()
    {
        posicionInicial = transform.position;
    }

    //¿Cuándo queremos que bote? Cuando toque el HollyTop o un HollyLevel
    //C# trae un método para esto: onCollisionEnter, cuando nuestra pelota entre en colisión con algo se ejecuta este método
    private void OnCollisionEnter(Collision colision)
    {

        //En el momento que bote una vez, salimos del método
        if (ignorarSiguienteColision)
        {
            return;
        }

        //Hacemos una comprobación para saber si estamos en super velocidad
        //y con lo que nos hemos chocado no es nuestro disco de Goal (el último disco)
        if (esSuperVelocidad && !colision.transform.GetComponent<GoalController>())
        {
            //si entra es que estamos en super velocidad y hemos chocado con un disco de por medio
            //destruimos el disco entero (nosotros chocamos con los quesitos así que tenemos que destruir el padre)
            //y ponemos que se destruya en 0.2 segundos
            Destroy(colision.transform.parent.gameObject, 0.2f);
        }
        else
        {
            //Vamos a crear un objeto DeathPart que sólo se va a asignar si la bola choca con un quesito rojo
            //es decir, si choca con un componente de tipo DeathPart
            DeathPart deathpart = colision.transform.GetComponent<DeathPart>();

            //Si chocamos con un quesito rojo
            if (deathpart)
            {
                //reseteamos el nivel
                GameManager.singleton.RestartNivel();
            }

        }
        //Cuando colisionemos con algo, le añadimos velocidad de Vector3 a cero para evitar problemas de velocidad al colisionar
        //Vector3 son las tres coordenadas en cuanto a posicion
        rb.velocity = Vector3.zero;

        //Cuando choque queremos que vaya hacia arriba, por lo que le añadimos fuerza en el eje de la Y queriendo que vaya para arriba
        //lo que hemos declarado en fuerzaImpulso
        //El ForceMode Impulse añade el impulso del rigibody usando su masa
        rb.AddForce(Vector3.up * fuerzaImpulso, ForceMode.Impulse);

        //cuando ya ha botado una vez ponemos el boolean a true
        ignorarSiguienteColision = true;

        //LLamamos al método de permitirSiguienteColision() para que vuelva a botar la bola pasados 0.2 ms
        Invoke("PermitirSiguienteColision", 0.2f);

        //Si choco con algo tenemos que poner el número de pasos perfectos a cero y la super velocidad a false
        numeroPasosPerfectos = 0;
        esSuperVelocidad = false;

    }

    //Utilizamos el método Update para activar la super velocidad en caso de que pasemos más de dos discos perfectos
    private void Update()
    {
        if (pasosPerfectos >= numeroPasosPerfectos && !esSuperVelocidad)
        {
            //si el número de pasos perfectos que hemos hecho es mayor o igual que el que hemos establecido
            //y no estamos en super velocidad, la activamos
            esSuperVelocidad = true;

            //y le sumamos a la velocidad actual, el extra que queremos para que tenga super velocidad
            //almacenado en la variable float superVelocidad
            //es decir, cuando vaya hacia abajo, poenmos el impulso del rigidBody a esa cantidad
            rb.AddForce(Vector3.down * superVelocidad, ForceMode.Impulse);
        }
    }

    //Para controlar que pueda volver a botar pasados unos cuantos segundos
    private void PermitirSiguienteColision()
    {
        ignorarSiguienteColision = false;
    }

    //Creamos un método para resetear la bola y que vuelva a la posición inicial
    public void ResetBola()
    {
        transform.position = posicionInicial;
    }
}