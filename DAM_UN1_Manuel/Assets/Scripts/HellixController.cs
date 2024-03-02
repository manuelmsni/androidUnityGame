using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class HellixController : MonoBehaviour
{
    //Declaramos una variable para almacenar nuestra última posición
    //Así, si la primera posición es 0 y la última es 2 en el eje x, sabremos que nos hemos movido hacia la derecha
    //y si es -2 sabremos que nos hemos movido hacia la izquierda
    //Creamos un Vector2 poque solo tenemos dos ejes (x e y), hacia la z no se mueve
    private Vector2 ultimaTapPosicion;


    //Declaramos una variable con la rotación inicial que sí que tendrá tres coordenadas porque es la rotación del Helix
    private Vector3 rotacionIncial;

    //Necesitamos saber dónde está el HellixTop y el HellixGoal 
    //también necesitamos el HelixPartPrefab porque es lo que vamos a establecer para poner tantas
    //como necesitamos y como hayamos especificado dentro de nuestros stages
    //dependiendo del número de partes que hayamos puesto tendremos que instanciar más o menos HelixPartPrefabs
    //Necesitamos una lista de todos los niveles
    //Y también necesitamos la distancia entre el HellixTop y el HellixGoal para saber cuántos discos puedo poner
    //Declaramos las variables donde almacenar esto
    //Declaramos la variable para almacenar la posición del HellixTop
    public Transform topTransform;

    //Declaramos la variable para almacenar la posición del HellixGoal
    public Transform goalTransform;

    //Declaramos la referencia al HelixPartPrefab
    public GameObject helixLevelPrefab;

    //Declaramos una lista de todos los niveles
    public List<Stage> allStages = new List<Stage>();

    //Declaramos la variable para almacenar la distancia entre HellixTop y HellixGoal
    public float distanciaHelix;

    //Declaramos una lista para saber cuántos niveles de los discos hemos spawneado
    private List<GameObject> nivelesSpawned = new List<GameObject>();

    //Vamos a utilizar el método Awake que se llama antes que al Start
    private void Awake()
    {
        //para saber la rotación inicial del Hellix y dependiendo de esa rotación inicial
        //luego rotarlo hacia la izquierda o hacia la derecha
        rotacionIncial = transform.localEulerAngles;

        //Calculamos la distancia entre el HellixTop y el HellixGoal añadiéndole 0.1 de margen 
        distanciaHelix = topTransform.localPosition.y - (goalTransform.localPosition.y + .1f);

        //Cargamos el nivel con el método creado
        CargarStage(0);
    }

    // Update is called once per frame
    void Update()
    {
        //Tenemos que saber cuándo estamos pulsando la pantalla
        //para esto Unity tiene un método que se llama GetMouseButton
        //con el parámetro 0 obtenemos cuando se pulsa el botón izquierdo del ratón
        //también si pulsamos con el dedo
        if (Input.GetMouseButton(0))
        {
            //Declaramos un Vector2 con coordenadas x e y instanciándolo para saber la posición de neustro dedo o ratón
            Vector2 actualTapPosicion = Input.mousePosition;

            //Si no hemos tocado la pantalla, es decir, si nuestra última posición es cero
            if (ultimaTapPosicion == Vector2.zero)
            {
                ultimaTapPosicion = actualTapPosicion;
            }

            //Ahora necesitamos saber qué distancia nos hemos movido para saber si hemos ido a la derecha o a la izquierda
            //para ello le restamos a nuestra última posición, la actual posición
            //lo hacemos sólo con el eje x porque nos interesa saber si nos hemos movido izquierda o derecha
            float distancia = ultimaTapPosicion.x - actualTapPosicion.x;
            //actualizamos la posición
            ultimaTapPosicion = actualTapPosicion;

            //Rotamos sobre el eje Y de la Hellix la distancia
            //si la distancia es positiva girará a la derecha y si es negativa hacia la izquierda
            transform.Rotate(Vector3.up * distancia);

            //Tenemos que gestionar cuándo dejamos de pulsar
            //con getmousebuttonup sabemos cuándo levantamos el dedo o el ratón de la pantalla
            if (Input.GetMouseButtonUp(0))
            {
                //cuando dejemos de pulsar se vuelve  ala posición cero
                ultimaTapPosicion = Vector2.zero;

            }
        }

    }

    //Añadimos un método para cargar un stage, pasándole el número de nivel que queremos que nos cargue
    public void CargarStage(int numeroStage)
    {
        //Lo primero vamos a establecer los niveles que tenemos y los que vamos a cargar en el videojuego
        //Tenemos que tener en cuenta que no podemos tener 0 stages y que tampoco podemos pasarnos de los stages que hemos puesto
        //Para ello creamos un Stage gestionando que esté dentro del tamaño de allStages que es la variable en la que se almacenan todos los niveles
        Stage stage = allStages[Mathf.Clamp(numeroStage, 0, allStages.Count - 1)];

        //Gestionamos que no haya cero niveles
        if (stage == null)
        {
            //si no tenemos ningún nivel hacemos un Debug para saber nosotros que no hay
            UnityEngine.Debug.Log("No Stages");
            //hacemos un return para que salga 
            return;

        }

        //Cuando cargue el Stage, dependiendo cuál sea, queremos gestionar que cambie el color de fondo, de la bola...
        //para ello teníamos unas variables reservadas en Stage.cs

        //Gestionamos el color de fondo
        Camera.main.backgroundColor = allStages[numeroStage].colorFondoStage;

        //Gestionamos el color de la bola
        FindObjectOfType<BolaController>().GetComponent<Renderer>().material.color = allStages[numeroStage].colorBolaStage;

        //Cada vez que se cargue un nivel, tenemos que resetear la rotación, para ello volvemos a la inicial
        transform.localEulerAngles = rotacionIncial;

        //Vamos a destruir niveles antiguos que se han podido crear mal y quedarse en la escena
        //para ello recorremos todos los objetos que haya en los niveles spawneados y los destruimos
        foreach (GameObject gameObject in nivelesSpawned)
        {
            Destroy(gameObject);
        }

        //Para poner los niveles entre el HellixTop y el HellixGoal tenemos que tener en cuenta la distancia 
        //entre los niveles intermedios y poder ir poniéndolos
        //Para calcular esa distancia creamos un float almacenarla que será igual a la distancia calculada en Awake
        //que es la distancia entre el HellixTop y el HellixGoal, entre el número de niveles que hay
        float distanciaNivel = distanciaHelix / stage.niveles.Count;

        //Cogemos la altura del HellixTop para tener un punto de referencia desde el que ir bajando e ir añadiendo los niveles
        float spawnPosY = topTransform.localPosition.y;

        //Bajando de esta altura vamos añadiendo los niveles
        for (int i = 0; i < stage.niveles.Count; i++)
        {
            //Le restamos a la spawnedPosY la distancia del Nivel para poder colocar la siguiente ahí
            spawnPosY -= distanciaNivel;

            //Con Instantiate clonamos un objeto de ese tipo
            //así que estamos clonando un objeto helixLevelPrefab
            GameObject nivel = Instantiate(helixLevelPrefab, transform);

            //para que no haya errores de posiciones de los discos, lo gestionamos
            nivel.transform.localPosition = new Vector3(0, spawnPosY, 0);

            //añadimos el nivel a la lista
            nivelesSpawned.Add(nivel);

            //Creamos una variable donde guardamos las partes (los quesitos) de los discos que están desabilitadas
            //para calcular dichas partes, como sabemos que un disco está formado por 12 quesitos, a 12 le restamos las partes visibles
            //que están almacenadas en Stage.cs en la clase Nivel
            int partesToDisable = 12 - stage.niveles[i].numeroPartesVisibles;

            //Creamos una lista para almacenar las partes deshabilitadas
            List<GameObject> partesDeshabilitadas = new List<GameObject>();

            //La parte que se deshabilita de cada disco, tendrá que ser aleatoria
            //No tiene gracia que siempre sea la misma, porque si no la bola caerá por todos los huecos sin hacer nada
            //Para gestionar la aleatoriedad hacemos lo siguiente
            while (partesDeshabilitadas.Count < partesToDisable)
            {
                //Almacenamos en randomPart aleatoriamente un hijo, es decir, un quesito, del nivel 
                //puede ser desde la primera parte (la 0) hasta la última
                GameObject randomPart = nivel.transform.GetChild(UnityEngine.Random.Range(0, nivel.transform.childCount)).gameObject;

                //Si la lista de partes deshabilitadas no contiene la parte randomPart
                if (!partesDeshabilitadas.Contains(randomPart))
                {
                    //Primero deshabilitamos esa parte
                    randomPart.SetActive(false);

                    //Y después añadimos esta parte deshabilitada aleatoria la lista de partes deshabilitadas
                    partesDeshabilitadas.Add(randomPart);

                }


            }

            //Para gestionar las partes donde podemos morir creamos una lista de GameObject para almacenar las partes habilitadas de cada nivel
            List<GameObject> partesHabilitadas = new List<GameObject>();

            //vamos ahora a cambiar el color de todas las partes habilitadas de los discos, para ello hacemos un foreach para ir cambiando el color de todos los componentes del nivel
            foreach (Transform t in nivel.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[numeroStage].colorParteNivelStage;

                //si dicho objeto está activo en nuestra jerarquía
                if (t.gameObject.activeInHierarchy)
                {
                    //lo añado a la lista de partes habilitadas
                    partesHabilitadas.Add(t.gameObject);
                }
            }

            //Creamos una lista de partes en las que podemos morir
            List<GameObject> deathParts = new List<GameObject>();

            //La parte en la que podemos morir de cada disco, tendrá que ser aleatoria
            //Para gestionar la aleatoriedad hacemos lo siguiente
            while (deathParts.Count < stage.niveles[i].numeroDeathPart)
            {
                //Almacenamos en randomPart aleatoriamente una parte habilitada del nivel, es decir, un quesito activo 
                //puede ser desde la primera parte (la 0) hasta la última
                //que será nuestra death part
                GameObject randomPart = partesHabilitadas[(UnityEngine.Random.Range(0, partesHabilitadas.Count))];

                //Si la lista de death parts no contiene la parte randomPart
                if (!deathParts.Contains(randomPart))
                {
                    //Primero tendremos que hacer la randomPart del tipo componente DeathPart (creado en DeathPart.cs)
                    randomPart.gameObject.AddComponent<DeathPart>();

                    //Y después añadimos esta death part aleatoria a la lista de death parts
                    deathParts.Add(randomPart);

                }


            }

        }

    }
}