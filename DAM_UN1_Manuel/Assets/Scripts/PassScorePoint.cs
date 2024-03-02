using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassScorePoint : MonoBehaviour
{
    //Utilizamos el m�todo onTriggerEnter para que cuando se detecte que pasa por el Trigger a�ada 1 a la puntuaci�n
    private void OnTriggerEnter(Collider other)
    {
        //A�adimos 1 a la puntuaci�n
        GameManager.singleton.addPuntuacion(1);
    }
}