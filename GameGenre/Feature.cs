using UnityEngine;
using System.Collections;

public class Feature
{

    string description;

    int lvl;

    bool incremented;

    public Feature(string description, int lvl, bool incremented)
    {
        this.description = description;
        this.lvl = lvl;
        this.incremented = incremented;
    }

    public string getDescription()
    {
        return description;
    }

    public int getLvl()
    {
        return lvl;
    }

    public bool getIncremented()
    {
        return incremented;
    }
 }

