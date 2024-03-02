using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellixController : MonoBehaviour
{
    //Declaramos una variable para almacenar nuestra �ltima posici�n
    //As�, si la primera posici�n es 0 y la �ltima es 2 en el eje x, sabremos que nos hemos movido hacia la derecha
    //y si es -2 sabremos que nos hemos movido hacia la izquierda
    //Creamos un Vector2 poque solo tenemos dos ejes (x e y), hacia la z no se mueve
    private Vector2 ultimaTapPosicion;


    //Declaramos una variable con la rotaci�n inicial que s� que tendr� tres coordenadas porque es la rotaci�n del Helix
    private Vector3 rotacionIncial;

    // Start is called before the first frame update
    void Start()
    {
        //para saber la rotaci�n inicial del Hellix y dependiendo de esa rotaci�n inicial
        //luego rotarlo hacia la izquierda o hacia la derecha
        rotacionIncial = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //Tenemos que saber cu�ndo estamos pulsando la pantalla
        //para esto Unity tiene un m�todo que se llama GetMouseButton
        //con el par�metro 0 obtenemos cuando se pulsa el bot�n izquierdo del rat�n
        //tambi�n si pulsamos con el dedo
        if (Input.GetMouseButton(0))
        {
            //Declaramos un Vector2 con coordenadas x e y instanci�ndolo para saber la posici�n de neustro dedo o rat�n
            Vector2 actualTapPosicion = Input.mousePosition;

            //Si no hemos tocado la pantalla, es decir, si nuestra �ltima posici�n es cero
            if (ultimaTapPosicion == Vector2.zero)
            {
                ultimaTapPosicion = actualTapPosicion;
            }

            //Ahora necesitamos saber qu� distancia nos hemos movido para saber si hemos ido a la derecha o a la izquierda
            //para ello le restamos a nuestra �ltima posici�n, la actual posici�n
            //lo hacemos s�lo con el eje x porque nos interesa saber si nos hemos movido izquierda o derecha
            float distancia = ultimaTapPosicion.x - actualTapPosicion.x;
            //actualizamos la posici�n
            ultimaTapPosicion = actualTapPosicion;

            //Rotamos sobre el eje Y de la Hellix la distancia
            //si la distancia es positiva girar� a la derecha y si es negativa hacia la izquierda
            transform.Rotate(Vector3.up * distancia);

            //Tenemos que gestionar cu�ndo dejamos de pulsar
            //con getmousebuttonup sabemos cu�ndo levantamos el dedo o el rat�n de la pantalla
            if (Input.GetMouseButtonUp(0))
            {
                //cuando dejemos de pulsar se vuelve  ala posici�n cero
                ultimaTapPosicion = Vector2.zero;

            }
        }

    }
}