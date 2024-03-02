using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassScorePoint : MonoBehaviour
{
    //Utilizamos el método onTriggerEnter para que cuando se detecte que pasa por el Trigger añada 1 a la puntuación
    private void OnTriggerEnter(Collider other)
    {
        //Añadimos 1 a la puntuación
        GameManager.singleton.addPuntuacion(1);

        //Añadimos 1 al número de pasos perfectos de discos
        FindObjectOfType<BolaController>().pasosPerfectos++;
    }
}