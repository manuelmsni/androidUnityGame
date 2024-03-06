using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Añadimos una librería
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class UIManager : MonoBehaviour
{
    //Necesitamos la referencia a la puntuación actual para mostrarlo y a la mejor puntuación para ver
    //si la supera y mostrarla también
    //Para poder hacer referencia añadimos la librería arriba de UnityEngine.UI

    //Declaramos un Texto de puntuación actual para posteriormente recoger ahí dicha puntuación
    public UnityEngine.UI.Text puntuacionActual;

    //Declaramos un Texto de mejor puntuación para posteriormente recoger ahí dicha puntuación
    public UnityEngine.UI.Text mejorPuntuacion;

    //Declaramos las variables para gestionar nuestro Slider
    //Desclaramos una para el Slider
    public Slider slider;

    //Declaramos variables para los textos del nivel actual y del siguiente nivel que aparecene en el slider
    public UnityEngine.UI.Text nivelActual;
    public UnityEngine.UI.Text siguienteNivel;

    //Declaramos dos variables para saber el transform de la plataforma de arriba (HelixTop) y de la de abajo (HelixGoal)
    public Transform topTransform;
    public Transform goalTransform;

    //Declaramos la referencia a nuestra bola
    public Transform bola;

    //Método para cambiar el nivel y el progreso del Slider
    public void CambiarNivelYProgresoSlider()
    {
        //Queremos que en el texto del primer círculo nos salga el nivel actual
        //para ello lo cambiamos y lo cogemos de la referencia que tenemos en GameManager
        //como la variable de nivelActual la tenemos inicializada en GameManager a 0, le sumamos 1 para que muestre un 1 la primera vez
        nivelActual.text = "" + (GameManager.singleton.nivelActual + 1);

        //Hacemos lo mismo con el texto del círculo del final para el siguiente nivel
        //en este caso sumándole 2 
        siguienteNivel.text = "" + (GameManager.singleton.nivelActual + 2);

        //Creamos una variable para saber la distancia en altura (eje Y) entre el HelixTop y el HelixGoal
        float distanciaTotal = (topTransform.position.y - goalTransform.position.y);

        //Creamos otra variable para saber la distancia en altura (eje Y) que hemos recorrido
        //es decir, desde el HelixTop hasta donde estamos
        float distanciaRecorrida = (distanciaTotal - (bola.position.y - goalTransform.position.y));

        //como quiero que se muestre esto en el slider, tengo que modificar el Value de éste que es donde se especifica
        //creo una variable llamada valor donde calculo la proporción de lo que avanzo
        float valor = (distanciaRecorrida / distanciaTotal);

        //esta proporción es en la que queremos que se amplíe el slider, por lo que lo modificamos en value
        //además para que el cambio entre los valores no sea tan brusco utilizamos Math.Lerp que hace una interpolación lineal entre ellos
        //de tal manera que el cambio es más progresivo
        slider.value = Mathf.Lerp(slider.value, valor, 5);
    }

    // Update is called once per frame
    void Update()
    {
        //Cambiamos el texto de puntuación actual para que muestre la puntuación actual real que se almacena en GameManager
        puntuacionActual.text = "Puntuación: " + GameManager.singleton.puntuacionActual;

        //Hacemos lo mismo con mejor puntuación
        mejorPuntuacion.text = "Mejor: " + GameManager.singleton.mejorPuntuacion;

        //Llamamos al método para que el Slider se vaya acutalizando
        CambiarNivelYProgresoSlider();
    }
}

