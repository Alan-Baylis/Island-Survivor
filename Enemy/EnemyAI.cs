using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public GameObject character;
	public Rigidbody attackBox;
	public GameObject aim;
    public GameObject lifeBar;

    public GameObject redBar;

    public GameObject killCounter;

    private static float maxHealthPoints = 100;
    public float currentHealthPoints;
    float groundDistance = 20;

    bool isAlive = true;
    
	float size;
	float throwForce;

	float distance = 25;

	bool isFlying;
	bool isUp;
	
	bool moveX;
	bool moveZ;

	bool addZ;
	bool addX;
	
	bool playerFar;
	
	RaycastHit ray;
	
	float attackTimer;

    System.Random rand;

    bool showLifeBar = false;
    float lifeBarTimer = 0;

    float damageTimer;

    // Use this for initialization
    void Start ()
    {
        EnableSprite(false);

        rand = new System.Random((int)transform.position.x + (int)transform.position.z);

        currentHealthPoints = maxHealthPoints;

        size = 0.1f * rand.Next(20) + 0.5f;
        throwForce = 50 + rand.Next(60);

        transform.localScale *= size;

	}
	
	// Update is called once per frame
	void Update () 
	{

        damageTimer += Time.deltaTime;

        if(showLifeBar)
        {
            lifeBarTimer += Time.deltaTime;

            if(lifeBarTimer > 4)
            {
                EnableSprite(false);
            }
        }

        if(isAlive)
        {
            if (Physics.Raycast(transform.position + new Vector3(0, -10, 0), transform.position + new Vector3(0, -20, 0), out ray, 5))
            {
                isFlying = true;
            }
            else
            {
                isFlying = false;
            }

            if(!isFlying)
            {
                if (Physics.Raycast(transform.position + new Vector3(0, -10, 0), transform.position + new Vector3(0, -100, 0), out ray, 100))
                {
                    isUp = true;
                }
                else
                {
                    isUp = false;
                }
            }

            // if the player is close
            if (character.transform.position.x < transform.position.x - distance ||
                character.transform.position.x > transform.position.x + distance)
            {
                if (character.transform.position.z < transform.position.z - distance ||
                character.transform.position.z > transform.position.z + distance)
                {
                    playerFar = true;
                }
                else
                {
                    playerFar = false;
                }
            }

            if (!playerFar)
            {

                transform.LookAt(character.transform.position);
                transform.Rotate(270, 90, 0);

                if (character.transform.position.x < transform.position.x - 10 ||
                    character.transform.position.x > transform.position.x + 10)
                {
                    moveX = true;
                }
                else
                {
                    moveX = false;
                }

                if (character.transform.position.z < transform.position.z - 10 ||
                character.transform.position.z > transform.position.z + 10)
                {
                    moveZ = true;
                }
                else
                {
                    moveZ = false;
                }

                if (moveZ)
                {
                    if (character.transform.position.z > transform.position.z)
                    {
                        addZ = true;
                    }
                    else
                    {
                        addZ = false;
                    }
                }

                if (moveX)
                {
                    if (character.transform.position.x > transform.position.x)
                    {
                        addX = true;
                    }
                    else
                    {
                        addX = false;
                    }
                }

                if (moveX || moveZ)
                    MoveEnemy();

                if (!playerFar)
                    Attack();
            }
            SetGroundDistance();
        }
        else
        {
            //GetComponent<Rigidbody>().useGravity = true;
            Destroy(gameObject, 5);
        }

	}
	
	void SetGroundDistance()
	{
		// y axis
		if (!isFlying)
		{
			if(isUp)
			{
				transform.position += new Vector3(0, -0.1f, 0);
			}
			else
			{
				transform.position += new Vector3(0, 0.1f, 0);
			}
		}
		
	}
	
	void MoveEnemy()
	{
		// z and x axis
		if(addZ)
		{
			transform.position += new Vector3(0, 0, 0.1f);
		}
		else
		{
			transform.position += new Vector3(0, 0, -0.1f);
		}
		
		if(addX)
		{
			transform.position += new Vector3(0.1f, 0, 0);
		}
		else
		{
			transform.position += new Vector3(-0.1f, 0, 0);
		}
		
	}
	
	void Attack()
	{
		attackTimer += Time.deltaTime;

		if(attackTimer > 2f)
		{
			attackTimer = 0;
			
			Rigidbody newBox = (Rigidbody)Instantiate (attackBox, aim.transform.position, aim.transform.rotation);
            newBox.transform.localScale *= size;
			newBox.name = "AttackBox";
			newBox.GetComponent<Rigidbody>().velocity = transform.transform.TransformDirection(-throwForce,0,0);
            //Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), newBox.GetComponent<Collider>(),true);

            GetComponent<AudioSource>().Play();
		}
	}

    public void DealDamage(float damage)
    {
        if(damageTimer > 0.5f)
        {
            currentHealthPoints -= damage;

            EnableSprite(true);

            damageTimer = 0;
        }
    }

    void EnableSprite(bool enb)
    {
        foreach(SpriteRenderer spr in lifeBar.GetComponentsInChildren<SpriteRenderer>())
        {
            spr.enabled = enb;
        }

        if(!enb)
        {
            lifeBar.GetComponentInChildren<TextMesh>().text = "";
        }
        else
        {
            lifeBar.GetComponentInChildren<TextMesh>().text = "Enemy";

            // dead
            if (currentHealthPoints <= 0)
            {
                currentHealthPoints = 0;
                GetComponent<Rigidbody>().useGravity = true;

                if(isAlive)
                    killCounter.GetComponent<EnemyCounter>().deadEnemies++;

                isAlive = false;
            }

            redBar.transform.localScale = new Vector2(currentHealthPoints * 0.01f, 5.8f);
        }

        showLifeBar = enb;
    }

}
