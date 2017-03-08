using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunEnergy : MonoBehaviour {

    [SerializeField]
    private GameObject character;
    [SerializeField]
    private GameObject energy;

    private PlayerAnimations playerAnim;

    private Image energyImage;

    public static float maxEnergy = 100;
    public float currentEnergy;

    float timerToRecharge;

	// Use this for initialization
	void Start () {

        playerAnim = GameObject.Find("Character").GetComponent<PlayerAnimations>();

        currentEnergy = 100;

        timerToRecharge = 0;

        energyImage = energy.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        if (playerAnim.isShooting && currentEnergy > 0)
        {
            currentEnergy -= 1f;
            timerToRecharge = 0;
        }
        else
        {
            timerToRecharge += Time.deltaTime;

            if (timerToRecharge > 0.5f && currentEnergy < 100)
            {
                currentEnergy += 1f;
            }
        }

        energy.transform.localScale = new Vector3((currentEnergy * 0.0017f), 0.11f);
        
	}
}
