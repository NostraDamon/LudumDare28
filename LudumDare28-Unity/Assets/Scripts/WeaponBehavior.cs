using UnityEngine;
using System.Collections;

public class WeaponBehavior : MonoBehaviour
{
	//public float autofire;
    //public float crosshair;
    //public float flashlight;
    //public float projectileSpeed;

    // Elements %
    public float acid;
    public float fire;
    public float ice;
    public float lightning;
    public float metal;

    // Main canon type
    public bool gatling;
    public bool liquid;
    public bool railgun;
    public bool rocketLauncher;
    public bool shotgun;

    // Secondary type
    public bool grenadeFrag;
    public bool grenade;
    public bool plasmaBall;
    public bool stickyMine;
    public bool mine;

    // Special
    public float highJump;
    public float health;
    public float moveSpeed;
    public float damage;
    public float fireRatePlus;
    public float lifeSteal;

    public string description;

    private _PartManager PartManager;
    private float fireTimer;
    private float fireTimerSecondary;

	// Use this for initialization
	void Start ()
    {
        PartManager = GameObject.Find("_PartManager").GetComponent<_PartManager>();

        description = string.Empty;
        fireTimer = 1;
        fireTimerSecondary = 1;
	}

    // Update is called once per frame
    void Update()
    {
        if (PartManager.inGame)
        {
            fireTimer -= Time.deltaTime;
            fireTimerSecondary -= Time.deltaTime;

            // Left CLick
            if (Input.GetMouseButton(0))
            {
                if (fireTimer <= 0)
                {
                    if (rocketLauncher)
                    {
                        GameObject rocket = Instantiate(Resources.Load("Prefabs/Bullet_Rocket"),
                                                        transform.position + transform.right*1,
                                                        Quaternion.Euler(transform.parent.transform.rotation.eulerAngles.x, transform.parent.transform.rotation.eulerAngles.y, transform.parent.transform.rotation.eulerAngles.z)) as GameObject;
                    }

                    fireTimer = 2 * (1 - fireRatePlus / 100);
                }
            }
            // Right Click
            else if (Input.GetMouseButtonDown(1))
            {
                if (fireTimerSecondary <= 0)
                {
                    if (grenade)
                    {
                        
                    }
                }

                fireTimerSecondary = 2;
            }
        }
    }

    public void UpdateProperties()
    {
        // Elements %
        acid = 0;
        fire = 0;
        ice = 0;
        lightning = 0;
        metal = 0;

        // Main canon type
        gatling = false;
        liquid = false;
        railgun = false;
        rocketLauncher = false;
        shotgun = false;

        // Secondary type
        grenadeFrag = false;
        grenade = false;
        plasmaBall = false;
        stickyMine = false;
        mine = false;

        // Special
        highJump = 0;
        health = 0;
        moveSpeed = 0;
        damage = 0;
        fireRatePlus = 0;
        lifeSteal = 0;

        description = string.Empty;

        for (int i = 0; i < transform.childCount; i++)
        {
            // Elements %
            acid += transform.GetChild(i).GetComponent<BuilderPartBehavior>().acid;
            fire += transform.GetChild(i).GetComponent<BuilderPartBehavior>().fire;
            ice += transform.GetChild(i).GetComponent<BuilderPartBehavior>().ice;
            lightning += transform.GetChild(i).GetComponent<BuilderPartBehavior>().lightning;
            metal += transform.GetChild(i).GetComponent<BuilderPartBehavior>().metal;

            // Main canon type
            if (!gatling) gatling = transform.GetChild(i).GetComponent<BuilderPartBehavior>().gatling;
            if (!liquid) liquid = transform.GetChild(i).GetComponent<BuilderPartBehavior>().liquid;
            if (!railgun) railgun = transform.GetChild(i).GetComponent<BuilderPartBehavior>().railgun;
            if (!rocketLauncher) rocketLauncher = transform.GetChild(i).GetComponent<BuilderPartBehavior>().rocketLauncher;
            if (!shotgun) shotgun = transform.GetChild(i).GetComponent<BuilderPartBehavior>().shotgun;

            // Secondary type
            if (!grenadeFrag) grenadeFrag = transform.GetChild(i).GetComponent<BuilderPartBehavior>().grenadeFrag;
            if (!grenade) grenade = transform.GetChild(i).GetComponent<BuilderPartBehavior>().grenade;
            if (!plasmaBall) plasmaBall = transform.GetChild(i).GetComponent<BuilderPartBehavior>().plasmaBall;
            if (!stickyMine) stickyMine = transform.GetChild(i).GetComponent<BuilderPartBehavior>().stickyMine;
            if (!mine) mine = transform.GetChild(i).GetComponent<BuilderPartBehavior>().mine;

            // Special
            highJump += transform.GetChild(i).GetComponent<BuilderPartBehavior>().highJump;
            health += transform.GetChild(i).GetComponent<BuilderPartBehavior>().health;
            moveSpeed += transform.GetChild(i).GetComponent<BuilderPartBehavior>().moveSpeed;
            damage += transform.GetChild(i).GetComponent<BuilderPartBehavior>().damage;
            fireRatePlus += transform.GetChild(i).GetComponent<BuilderPartBehavior>().fireRatePlus;
            lifeSteal += transform.GetChild(i).GetComponent<BuilderPartBehavior>().lifeSteal;
        }

        // Build description according to properties
        if (gatling)
        {
            description += "Gatling Gun\n";
        }
        if (liquid)
        {
            description += "Liquid Thrower\n";
        }
        if (railgun)
        {
            description += "Rail Gun\n";
        }
        if (rocketLauncher)
        {
            description += "Rocket Launcher\n";
        }
        if (shotgun)
        {
            description += "Shotgun\n";
        }

        if (grenade)
        {
            description += "Grenade Launcher\n";
        }
        if (grenadeFrag)
        {
            description += "Frag Grenade Launcher\n";
        }
        if (mine)
        {
            description += "Mine Gun\n";
        }
        if (stickyMine)
        {
            description += "Sticky Mine Gun \n";
        }
        if (plasmaBall)
        {
            description += "Plasma Ball Thrower\n";
        }

        if (damage > 0)
        {
            description += "\nDamage " + damage.ToString() + "\n";
        }
        if (fireRatePlus > 0)
        {
            description += "Fire Rate +" + fireRatePlus.ToString() + "%\n\n";
        }

        if (metal > 0)
        {
            description += "Damage +" + metal.ToString() + "%\n";
        }
        if (fire > 0)
        {
            description += "Fire +" + fire.ToString() + "%\n";
        }
        if (acid > 0)
        {
            description += "Acid +" + acid.ToString() + "%\n";
        }
        if (ice > 0)
        {
            description += "Ice +" + ice.ToString() + "%\n";
        }
        if (lightning > 0)
        {
            description += "Lightning +" + lightning.ToString() + "%\n";
        }

        if (health > 0)
        {
            description += "Health +" + health.ToString() + "\n";
        }
        if (highJump > 0)
        {
            description += "Jump Height +" + highJump.ToString() + "%\n";
        }
        if (moveSpeed > 0)
        {
            description += "Move Speed +" + moveSpeed.ToString() + "%\n";
        }
        if (lifeSteal > 0)
        {
            description += "Life Steal +" + lifeSteal.ToString() + "%\n";
        }
    }
}
