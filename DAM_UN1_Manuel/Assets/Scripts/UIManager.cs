using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A�adimos una librer�a
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class UIManager : MonoBehaviour
{
    //Necesitamos la referencia a la puntuaci�n actual para mostrarlo y a la mejor puntuaci�n para ver
    //si la supera y mostrarla tambi�n
    //Para poder hacer referencia a�adimos la librer�a arriba de UnityEngine.UI

    //Declaramos un Texto de puntuaci�n actual para posteriormente recoger ah� dicha puntuaci�n
    public UnityEngine.UI.Text puntuacionActual;

    //Declaramos un Texto de mejor puntuaci�n para posteriormente recoger ah� dicha puntuaci�n
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

    //M�todo para cambiar el nivel y el progreso del Slider
    public void CambiarNivelYProgresoSlider()
    {
        //Queremos que en el texto del primer c�rculo nos salga el nivel actual
        //para ello lo cambiamos y lo cogemos de la referencia que tenemos en GameManager
        //como la variable de nivelActual la tenemos inicializada en GameManager a 0, le sumamos 1 para que muestre un 1 la primera vez
        nivelActual.text = "" + (GameManager.singleton.nivelActual + 1);

        //Hacemos lo mismo con el texto del c�rculo del final para el siguiente nivel
        //en este caso sum�ndole 2 
        siguienteNivel.text = "" + (GameManager.singleton.nivelActual + 2);

        //Creamos una variable para saber la distancia en altura (eje Y) entre el HelixTop y el HelixGoal
        float distanciaTotal = (topTransform.position.y - goalTransform.position.y);

        //Creamos otra variable para saber la distancia en altura (eje Y) que hemos recorrido
        //es decir, desde el HelixTop hasta donde estamos
        float distanciaRecorrida = (distanciaTotal - (bola.position.y - goalTransform.position.y));

        //como quiero que se muestre esto en el slider, tengo que modificar el Value de �ste que es donde se especifica
        //creo una variable llamada valor donde calculo la proporci�n de lo que avanzo
        float valor = (distanciaRecorrida / distanciaTotal);

        //esta proporci�n es en la que queremos que se ampl�e el slider, por lo que lo modificamos en value
        //adem�s para que el cambio entre los valores no sea tan brusco utilizamos Math.Lerp que hace una interpolaci�n lineal entre ellos
        //de tal manera que el cambio es m�s progresivo
        slider.value = Mathf.Lerp(slider.value, valor, 5);
    }

    // Update is called once per frame
    void Update()
    {
        //Cambiamos el texto de puntuaci�n actual para que muestre la puntuaci�n actual real que se almacena en GameManager
        puntuacionActual.text = "Puntuaci�n: " + GameManager.singleton.puntuacionActual;

        //Hacemos lo mismo con mejor puntuaci�n
        mejorPuntuacion.text = "Mejor: " + GameManager.singleton.mejorPuntuacion;

        //Llamamos al m�todo para que el Slider se vaya acutalizando
        CambiarNivelYProgresoSlider();
    }
}

