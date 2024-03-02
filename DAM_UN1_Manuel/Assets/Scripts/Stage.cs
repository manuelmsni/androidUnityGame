using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A�adimos una librer�a
using System;

//Creamos una clase llamada Nivel
//se va a encargar de las partes que tenemos desde arriba del Hellix
//hasta abajo, es decir de las partes de los discos
//para que nos salga en el inspector tiene que ser serializable
[Serializable]
public class Nivel
{
    //Lo primero le vamos a especificar cu�ntas partes visibles queremos en nuestro disco
    //por ejemplo del 1 al 11 (si hay 12 no puede caer al de abajo), y lo hacemos d�ndole un rango
    [Range(1, 11)]
    //Este n�mero de partes lo almacenamos en la variable numeroPartesVisibles
    public int numeroPartesVisibles = 11;

    //Establecemos tambi�n el n�mero de quesitos rojos que pueda haber
    //es decir, el n�mero de deathpart, por ejemplo de 0 a 11
    [Range(0, 11)]
    //y lo almacenamos en la variable numeroDeathPart
    public int numeroDeathPart = 1;

}


//Nivel tiene que heredar de ScriptableObject
//porque queremos tener un objeto de tipo Nivel que en Unity es un Stage
//Para poder crear este tipo de objeto tenemos que poner un CreateAssetMenu
[CreateAssetMenu(fileName = "Nuevo Stage")]
public class Stage : ScriptableObject
{
    //Aqu� podemos modificar el color del HellixPart, el HellixController y nuestra Bola
    //Declaramos una variable para poder modificar el color de fondo
    //y lo ponemos a blanco por ejemplo
    public Color colorFondoStage = Color.white;

    //Declaramos otra variable para modificar el color de cada parte en el nivel
    //y lo ponemos tambi�n a blanco por ejemplo
    public Color colorParteNivelStage = Color.white;

    //Declaramos otra variable para el color de la bola
    //y lo ponemos tambi�n a blanco por ejemplo
    public Color colorBolaStage = Color.white;

    //Tambi�n necesitamos una lista donde ir�n nuestros niveles
    //para ello declaramos una variable List de tipo Nivel
    //para saber cu�ntos niveles tenemos
    public List<Nivel> niveles = new List<Nivel>();
}