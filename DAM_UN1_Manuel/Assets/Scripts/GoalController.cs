using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    //Usamos el método onCollisionEnter para que cuando se entre en colisión con un quesito, se pase de nivel
    private void OnCollisionEnter(Collision collision)
    {
        //Llamamos al método de GameManager que nos permite pasar de nivel
        GameManager.singleton.PasarNivel();
    }
}