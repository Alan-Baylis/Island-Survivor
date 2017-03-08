using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour {

    //[SerializeField]
    //private GameObject gun;
    [SerializeField]
    private GameObject outpost;
    [SerializeField]
    private GameObject launcher;
    [SerializeField]
    private GameObject batteryGUI;
    [SerializeField]
    private GameObject narrator;

    public GameObject rayCaster;

    public AudioClip hitMarker;

    private BatteryCollect Bscript;
    private CoconutThrow Throwscript;
    private DoorManager DoorManager;
    private PlayerAnimations playerAnim;
    private PlayerInteractions playerInteractions;
    private SubtitleManager narratorLines;

    // raycast to open the door
    RaycastHit ray;

    // sfx
    public AudioClip catchBattery;

    private float shotDistance = 15;

    private Vector3 ajust = new Vector3(0, 0, -0.5f);

	// Use this for initialization
	void Start () {

        Bscript = batteryGUI.GetComponent<BatteryCollect>();

        Throwscript = launcher.GetComponent<CoconutThrow>();

        DoorManager = outpost.GetComponent<DoorManager>();

        playerAnim = GetComponent<PlayerAnimations>();

        playerInteractions = GetComponent<PlayerInteractions>();

        narratorLines = narrator.GetComponent<SubtitleManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
        // testing raycast
        if(Physics.Raycast(transform.position, transform.forward, out ray, 5))
        {
            // open the first door
            if(ray.collider.gameObject.name == "door" && !DoorManager.open && Bscript.charge == 4)
            {
                PlayerInteractions.firstDoor = true;
                DoorManager.OpenDoor();
            }
        }

        // destroy enemy
       if (MapGenerator.mapGameGenre == 9)
        {

            if (Physics.Raycast(rayCaster.transform.position, rayCaster.transform.forward * 30, out ray, 50) && playerAnim.isShooting)
            {
                if (ray.collider.gameObject.tag == "enemy" && playerAnim.isShooting)
                {
                    ray.collider.gameObject.GetComponent<EnemyAI>().currentHealthPoints -= 10;
                    ray.collider.gameObject.GetComponent<EnemyAI>().DealDamage(1);
                    playerAnim.aud2.Play();
                }
            }

            Debug.DrawLine(rayCaster.transform.position, rayCaster.transform.forward * 30);
        }
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
		if (MapGenerator.mapGameGenre == 8) {
			// the player can shot coconuts in the platform
			if (hit.gameObject.name == "mat") {
				Throwscript.canThrow = true;

				if (!playerInteractions.firstCoconutTrow) {
					narratorLines.UpdateSubtitle ("Aqui você pode atirar cocos nesses alvos para liberar mais uma bateria.", 0, 2);
					playerInteractions.firstCoconutTrow = true;
				}
			} else {
				Throwscript.canThrow = false;
			}
		}

    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        // collect battery
        if (collisionInfo.gameObject.tag == "battery")
        {
            GetComponent<AudioSource>().PlayOneShot(catchBattery);

            if(Bscript.charge == 3)
            {
                narratorLines.UpdateSubtitle("Boa, agora você já tem todas baterias para abrir a porta", 0, 1);
            }

            Bscript.charge++;

            Destroy(collisionInfo.gameObject);
        }
    }
}