using UnityEngine;
using System.Collections;

public class GameGenre {

    private int id;

    public string genreName;

    private Color genreColor;

    private Feature[] features;

    public GameGenre(int id, string genreName, Color genreColor, Feature[] features)
    {
        this.id = id;
        this.genreName = genreName;
        this.genreColor = genreColor;
        this.features = features;
    }

    public string getName()
    {
        return genreName;
    }

    public Color getColor()
    {
        return genreColor;
    }

    public int getInt()
    {
        return id;
    }

    public Feature[] getFeatures()
    {
        return features;
    }
}
