using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A�adimos una librer�a
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Necesitamos la referencia a la puntuaci�n actual para mostrarlo y a la mejor puntuaci�n para ver
    //si la supera y mostrarla tambi�n
    //Para poder hacer referencia a�adimos la librer�a arriba de UnityEngine.UI

    //Declaramos un Texto de puntuaci�n actual para posteriormente recoger ah� dicha puntuaci�n
    public Text puntuacionActual;

    //Declaramos un Texto de mejor puntuaci�n para posteriormente recoger ah� dicha puntuaci�n
    public Text mejorPuntuacion;

    // Update is called once per frame
    void Update()
    {
        //Cambiamos el texto de puntuaci�n actual para que muestre la puntuaci�n actual real que se almacena en GameManager
        puntuacionActual.text = "Puntuaci�n: " + GameManager.singleton.puntuacionActual;

        //Hacemos lo mismo con mejor puntuaci�n
        mejorPuntuacion.text = "Mejor: " + GameManager.singleton.mejorPuntuacion;
    }
}

