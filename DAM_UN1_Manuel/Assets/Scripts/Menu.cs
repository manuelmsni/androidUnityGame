using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Añadimos librería para que nos deje pasar de una escena a otra
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class Menu : MonoBehaviour
{
    //Creamos dos métodos para asociarlos al hacer click al botón de Play y al de Exit

    //Método de jugar al juego
    public void EmpezarJugar()
    {
        //Tenemos que cambiar de escena para que nos lleve a donde tenemos el juego
        //Estas escenas las podemos coger de Scenes in Build, para eso las hemos metido

        //Para pasar de escena, decimos que cargue la escena actual y que vaya a la siguiente
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Método de salir del juego
    public void SalirJuego()
    {
        //Salimos del juego (funcionará cuando tengamos el instalable, en Unity no)
        UnityEngine.Application.Quit();
    }
}