using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Declaramos una variable para almacenar la mejor puntuación
    public int mejorPuntuacion;

    //Declaramos otra variable para almacenar la puntuación actual
    public int puntuacionActual;

    //Declaramos otra variable para almacenar el nivel actual y lo inicializamos con el nivel 0
    public int nivelActual = 0;

    //Declaramos una instancia de GameManager
    public static GameManager singleton;

    // Llamamos al método Awake, porque queremos que antes de que comience con Start, lo primero que haga
    //sea mirar si no hay GameManager y en ese caso crearlo
    //o mirar si hay más de un GameManager y en este caso borrar todos menos uno
    void Awake()
    {
        //si no tenemos ningún GameManager
        if (singleton == null)
        {
            //nuestro GameManager será éste
            singleton = this;
        }
        //si el GameManager no es este
        else if (singleton != this)
        {
            //Lo destruimos
            Destroy(gameObject);
        }

        //Cogemos la variable de mejor puntuacion almacenada con PlayerPrefs en el método AddPuntuacion
        mejorPuntuacion = PlayerPrefs.GetInt("MejorPuntuacion");
    }

    //Para gestionar cuándo pasamos de nivel
    public void PasarNivel()
    {
        //sumamos uno a nuestro nivel actual
        nivelActual++;

        //Ponemos la bola en la posición inicial
        FindObjectOfType<BolaController>().ResetBola();

        //Cargamos el nuevo nivel
        FindObjectOfType<HellixController>().CargarStage(nivelActual);

        UnityEngine.Debug.Log("Pasamos de nivel");
    }

    //Para resetear el nivel por si damos con algún obstáculo
    public void RestartNivel()
    {
        //lo primero que tenemos que hacer es resetear la puntuacion
        singleton.puntuacionActual = 0;

        //y poner la bola en la posición inicial
        //para ello tenemos que llamar al método ResetBola() que acabamos de crear en BolaController
        //utilizando FindObjectOfType que nos encuentra el objeto del tipo que le indicamos
        //lo hace buscando por todos los objetos y en el momento que encuentra un componente
        //al que esté asociado, en este caso, BolaController, como es nuestra bola, lo coge
        FindObjectOfType<BolaController>().ResetBola();

        // Al restart nivel, cargamos el nuevo nivel
        FindObjectOfType<HellixController>().CargarStage(nivelActual);
    }

    //Para añadir puntuación pasándole la puntuación que tenemos que añadir
    public void addPuntuacion(int puntuacionToAdd)
    {
        //sumamos a nuestra puntuación actual la que le tenemos que añadir
        puntuacionActual += puntuacionToAdd;

        //comprobamos si nuestra puntuación actual es mejor que nuestra mejor puntuación
        if (puntuacionActual > mejorPuntuacion)
        {
            //en este caso nuestra mejor puntuación pasaría a ser la actual
            mejorPuntuacion = puntuacionActual;

            //utilizamos la clase PlayersPrefs para almacenar la mejor puntuación
            //se pone una clave y su valor
            PlayerPrefs.SetInt("MejorPuntuacion", mejorPuntuacion);
        }
    }

}