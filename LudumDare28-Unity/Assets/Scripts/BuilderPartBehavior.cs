using UnityEngine;
using System.Collections;

public class BuilderPartBehavior : MonoBehaviour
{
	public Material matBasic;
    public Material matLight;

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

	// Use this for initialization
	void Start ()
    {
        description = string.Empty;

        // Build description according to properties
        if (gatling){
            description += "Gatling Gun\n";
        }
        if (liquid){
            description += "Liquid Thrower\n";
        }
        if (railgun){
            description += "Rail Gun\n";
        }
        if (rocketLauncher){
            description += "Rocket Launcher\n";
        }
        if (shotgun){
            description += "Shotgun\n";
        }

        if (grenade){
            description += "Grenade Launcher\n";
        }
        if (grenadeFrag){
            description += "Frag Grenade Launcher\n";
        }
        if (mine){
            description += "Mine Gun\n";
        }
        if (stickyMine){
            description += "Sticky Mine Gun \n";
        }
        if (plasmaBall){
            description += "Plasma Ball Thrower\n";
        }

        if (fire > 0){
            description += "Fire +" + fire.ToString() + "%\n";
        }
        if (acid > 0){
            description += "Acid +" + acid.ToString() + "%\n";
        }
        if (ice > 0){
            description += "Ice +" + ice.ToString() + "%\n";
        }
        if (lightning > 0){
            description += "Lightning +" + lightning.ToString() + "%\n";
        }
        if (metal > 0){
            description += "Damage +" + metal.ToString() + "%\n";
        }

        if (highJump > 0){
            description += "Jump Height +" + highJump.ToString() + "%\n";
        }
        if (health > 0){
            description += "Health +" + health.ToString() + "\n";
        }
        if (moveSpeed > 0){
            description += "Move Speed +" + moveSpeed.ToString() + "%\n";
        }
        if (damage > 0){
            description += "Damage " + damage.ToString() + "\n";
        }
        if (fireRatePlus > 0){
            description += "Fire Rate +" + fireRatePlus.ToString() + "%\n";
        }
        if (lifeSteal > 0){
            description += "Life Steal +" + lifeSteal.ToString() + "%\n";
        }
	}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
