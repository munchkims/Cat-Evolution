using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Combination
{
    public Mammals mammal;
    public Anthropods anthropod;
    public Plants plant;
    public Alchemy element;

    // this is a constructor
    public Combination(Mammals mammal, Anthropods anthropod, Plants plant, Alchemy element)
    {
        this.mammal = mammal;
        this.anthropod = anthropod;
        this.plant = plant;
        this.element = element;
    }

    // comparing two combinations
    public bool Matches(Combination other)
    {
        return mammal == other.mammal && anthropod == other.anthropod && plant == other.plant && element == other.element;
    }
}

[Serializable]
public class Combo
{
    public Combination combination;
    public string name;
    public bool isFound;
}
