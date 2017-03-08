 using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerAnimations : MonoBehaviour {

    [SerializeField]
    private Rigidbody laser;

    public GameObject mapGenerator;

    public AudioClip startShot;

    public GunEnergy gunScript;
    public GunAnimation gunAnim;

    public static bool playerAlive = true;
    public float reloadTime = 3;

    public float maxHealthPoints = 100;
    public float healthPoints = 100;    

    public bool isMoving = false;
    public bool isShooting = false;
    public bool isRunning = false;
    public bool isAiming = false;

    private float shotTimer = 0;

    private float lifeTimer = 0;

    AudioSource aud1;
    public AudioSource aud2;

	// Use this for initialization
	void Start () {

        AudioSource[] ad = new AudioSource[2];
        ad = GetComponents<AudioSource>();

        aud1 = ad[1];
        aud2 = ad[2];

        GetComponentInChildren<AudioSource>().volume = 0.35f;
        
	}
	    
	// Update is called once per frame
	void Update () {

        // if the player is alive, he can move
        if (playerAlive)
        {

            // if the current genre is the shooter one
            if (MapGenerator.mapGameGenre == 9)
            {

                lifeTimer += Time.deltaTime;

                if(lifeTimer > 6 && healthPoints < 100)
                {
                    healthPoints += 10;
                    lifeTimer = 0;
                }

                // test if the player is moving
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    isMoving = true;
                }
                else
                {
                    isMoving = false;
                }

				if (Input.GetKey("left shift") && isMoving)
                {
                    isRunning = true;
                }
                else
                {
                    isRunning = false;
                }

                if (Input.GetMouseButton(1))
                {
                    isAiming = true;
                }
                else
                {
                    isAiming = false;
                }

                TestPlayerShooting();
            }
        }
        else // any key will reload de scene
        {
            isAiming = false;
            isRunning = false;
            isShooting = false;
            isMoving = false;

            if (Input.anyKeyDown && reloadTime <= 0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                mapGenerator.GetComponent<MapGenerator>().RegenerateMap(MapGenerator.mapGameGenre, 8);

                playerAlive = true;
            }

            reloadTime -= Time.deltaTime;
        }
    }

    void TestPlayerShooting()
    {
        // test if the player is shootingx
        if (Input.GetMouseButtonDown(0) && gunScript.currentEnergy > 0)
        {
            aud1.Play();

            isShooting = true;

            GetComponentInChildren<ParticleSystem>().Play();

            shotTimer += Time.deltaTime;
        }
        else if (Input.GetMouseButton(0) && gunScript.currentEnergy > 0)
        {
            if(shotTimer > 0.2f)
            {
                shotTimer = 0;
                aud1.Play();
            }

            isShooting = true;

            GetComponentInChildren<ParticleSystem>().Play();

            shotTimer += Time.deltaTime;
        }
        else
        {
            shotTimer = 0;
            isShooting = false;

            GetComponentInChildren<ParticleSystem>().Stop();

        }

    }
}
