using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Añadimos una librería
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Necesitamos la referencia a la puntuación actual para mostrarlo y a la mejor puntuación para ver
    //si la supera y mostrarla también
    //Para poder hacer referencia añadimos la librería arriba de UnityEngine.UI

    //Declaramos un Texto de puntuación actual para posteriormente recoger ahí dicha puntuación
    public Text puntuacionActual;

    //Declaramos un Texto de mejor puntuación para posteriormente recoger ahí dicha puntuación
    public Text mejorPuntuacion;

    // Update is called once per frame
    void Update()
    {
        //Cambiamos el texto de puntuación actual para que muestre la puntuación actual real que se almacena en GameManager
        puntuacionActual.text = "Puntuación: " + GameManager.singleton.puntuacionActual;

        //Hacemos lo mismo con mejor puntuación
        mejorPuntuacion.text = "Mejor: " + GameManager.singleton.mejorPuntuacion;
    }
}

