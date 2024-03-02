using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    //Usamos el m�todo onCollisionEnter para que cuando se entre en colisi�n con un quesito, se pase de nivel
    private void OnCollisionEnter(Collision collision)
    {
        //Llamamos al m�todo de GameManager que nos permite pasar de nivel
        GameManager.singleton.PasarNivel();
    }
}