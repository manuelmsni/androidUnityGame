using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPart : MonoBehaviour
{
    //Lo primero que vamos a hacer es asignar el color rojo al queso, para que se sepa que si botamos ahí, perdemos puntos
    //Para ello Unity trae un método llamado onEnable, que se ejecuta cuando se activa el objeto
    private void OnEnable()
    {
        //Cambiamos el color a rojo
        //Para ello cogemos el componente renderer y lo cambiamos a rojo
        //Cada objeto tiene su Mesh Renderer donde tenemos un material que le da el color
        //lo que hacemos es acceder a ese material del Mesh Renderer
        var renderer = GetComponent<Renderer>();
        if(renderer!=null) renderer.material.color = Color.red;

    }
}