using System.Collections.Generic;
using UnityEngine;

public class Troop
{
    List<GameObject> enemies = new List<GameObject>();

    public GameObject this[int index] {get => enemies[index];}
}
