using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilityHandler : MonoBehaviour
{
    public List<Hability> HabilityList = new List<Hability>();

    private void Start()
    {
        // Agregar habilidades a la lista desde el código.
        HabilityList.Add(new Hability("PowerUp", 15));
        HabilityList.Add(new Hability("ExplosiveMine", 35));
        HabilityList.Add(new Hability("HyperBeam", 75));
    }

    public class Hability
    {
        public string name;
        public int cost;

        public Hability(string name, int cost)
        {
            this.name = name;
            this.cost = cost;
        }
    }
}
