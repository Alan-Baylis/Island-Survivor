using UnityEngine;
using System.Collections;

public class TargetManager : MonoBehaviour {

    [SerializeField]
    GameObject battery;
    [SerializeField]
    GameObject narrator;

    private SubtitleManager narratorLines;

    // count how hits
    public int hits = 0;

    // end game
    public bool isPlayable = true;

    public AudioClip hitSound;
    public AudioClip resetSound;

    void Start()
    {
        narratorLines = narrator.GetComponent<SubtitleManager>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(hits == 3)
        {
            battery.transform.position = transform.position + new Vector3(0, 2, -20);
            Instantiate(battery);

            narratorLines.UpdateSubtitle("Pois é. Acho que esse mini game está muito fácil...", 0, 2);

            hits = 0;
            isPlayable = false;
        }
	}
}
