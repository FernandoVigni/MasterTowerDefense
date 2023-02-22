using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public EnemyManager enemyManager;

    public int level;
    public int wave;
    public float waveLimitTime;
    public int ammountOfWarriorsInWave;
    public int ammountOfMagesInWave;
    public int ammountOfGiantsInWave;

    // Start is called before the first frame update
    void Start()
    {
        StartLevelOne();
    }

    public void StartLevelOne()
    {
        enemyManager.listOfEnemiesToDefeatInThisWave.Clear();
        ammountOfWarriorsInWave = 10;
        ammountOfMagesInWave = 0;
        ammountOfGiantsInWave = 0;

        enemyManager.StartLevel(ammountOfWarriorsInWave, ammountOfMagesInWave, ammountOfGiantsInWave);
    }


//instanciar un enemigo 

/*
    - Sera el encargado de decir cuantos enemigos de cada tipo se deben instanciar antes de iniciar cada nivel. (2guerreros  4 magos    8gigantes)

- debe manejar las oleadas de como se van a dividir 
	(oleada1	2 guerreos	2 magos)
	(oleada2	2 magos	2 gigantes)
	(oleada3	4 gigantes)

-debe marcar al azar en que punto se va a instanciar el enemigo y pedirle al enemyManager que lo instancia ahi. trabajar en un circulo

- Debe tener un contador de tiempo entre oleadas
- ver si cuando se mueren todos los enemigos hacemos que el nivel empiece antes o aparece un boton de "Next Wave"

--------------------------------------
evaluar al instanciar poner un retardador de instanciado random en segundos para hacer que los enemigos no aparezcan todos juntos , poner de 0 a 2/3/5 segs de instanciado.
*/
}
