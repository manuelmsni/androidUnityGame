using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellixController : MonoBehaviour
{
    //Declaramos una variable para almacenar nuestra última posición
    //Así, si la primera posición es 0 y la última es 2 en el eje x, sabremos que nos hemos movido hacia la derecha
    //y si es -2 sabremos que nos hemos movido hacia la izquierda
    //Creamos un Vector2 poque solo tenemos dos ejes (x e y), hacia la z no se mueve
    private Vector2 ultimaTapPosicion;


    //Declaramos una variable con la rotación inicial que sí que tendrá tres coordenadas porque es la rotación del Helix
    private Vector3 rotacionIncial;

    // Start is called before the first frame update
    void Start()
    {
        //para saber la rotación inicial del Hellix y dependiendo de esa rotación inicial
        //luego rotarlo hacia la izquierda o hacia la derecha
        rotacionIncial = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //Tenemos que saber cuándo estamos pulsando la pantalla
        //para esto Unity tiene un método que se llama GetMouseButton
        //con el parámetro 0 obtenemos cuando se pulsa el botón izquierdo del ratón
        //también si pulsamos con el dedo
        if (Input.GetMouseButton(0))
        {
            //Declaramos un Vector2 con coordenadas x e y instanciándolo para saber la posición de neustro dedo o ratón
            Vector2 actualTapPosicion = Input.mousePosition;

            //Si no hemos tocado la pantalla, es decir, si nuestra última posición es cero
            if (ultimaTapPosicion == Vector2.zero)
            {
                ultimaTapPosicion = actualTapPosicion;
            }

            //Ahora necesitamos saber qué distancia nos hemos movido para saber si hemos ido a la derecha o a la izquierda
            //para ello le restamos a nuestra última posición, la actual posición
            //lo hacemos sólo con el eje x porque nos interesa saber si nos hemos movido izquierda o derecha
            float distancia = ultimaTapPosicion.x - actualTapPosicion.x;
            //actualizamos la posición
            ultimaTapPosicion = actualTapPosicion;

            //Rotamos sobre el eje Y de la Hellix la distancia
            //si la distancia es positiva girará a la derecha y si es negativa hacia la izquierda
            transform.Rotate(Vector3.up * distancia);

            //Tenemos que gestionar cuándo dejamos de pulsar
            //con getmousebuttonup sabemos cuándo levantamos el dedo o el ratón de la pantalla
            if (Input.GetMouseButtonUp(0))
            {
                //cuando dejemos de pulsar se vuelve  ala posición cero
                ultimaTapPosicion = Vector2.zero;

            }
        }

    }
}