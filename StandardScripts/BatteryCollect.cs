using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BatteryCollect : MonoBehaviour {

    // how many batterys were collected already
    public int charge = 0;

    // this image component
    private Image imageComponent;

    // sprites that will be shown to indicate how many batterys were collected
    public Sprite[] chargetex;

    void Awake()
    {
        enabled = true;
    }

    // Use this for initialization
    void Start () {

        imageComponent = GetComponent<Image>();
        imageComponent.enabled = false;

        charge = 0;
	
	}
	
	// Update is called once per frame
	void Update () {


        for(int i = 0; i < chargetex.Length; i++)
        {
            if(charge > 0 && charge == i)
            {
                imageComponent.color += new Color(0, 0, 0, 1);

                imageComponent.sprite = chargetex[i];

                imageComponent.enabled = true;
            }
        }
	}
}
