using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    //Creamos una referencia a BolaController
    public BolaController bola;

    //Creamos un float que nos indique la altura a la que mantenernos de la pelota
    public float altura;

    // Start is called before the first frame update
    void Start()
    {
        //al darle al play instanciamos la altura que queremos mantener la pelota
        //Esta altura ser� la altura que tengo en la c�mara menos la altura que tiene la pelota
        altura = transform.position.y - bola.transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        //Creamos un Vector3 con las coordenadas de mi posici�n actual
        Vector3 posicionActual = transform.position;

        //En todo momento, cuando se actualice, tenemos que respetar que en la posici�n en la que me encuentre, se deje la altura establecida
        //para mantener la pelota, por lo que a la altura de la posicionActual tenemos que sumarle a la altura de la pelota, 
        //la altura que mantenemos sobre �sta
        posicionActual.y = bola.transform.position.y + altura;

        //Asignamos ahora que nuestra altura sea �sta
        transform.position = posicionActual;

    }
}