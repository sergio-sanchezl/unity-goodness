using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject arm;

	CharacterController cc;
	public Camera cam;
    Animator animator;

	public float movementSpeed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public float rotationSpeed = 2.0F;
	public float runSpeedMultiplier = 1.5F;

	private float pitch = 0F;
	private float yaw = 0F;

	private float jumpMultiplier = 1f;

	private Vector3 moveDirection = Vector3.zero;

    public Object[] weapons;
    private int currentWeaponIndex;

    private bool usingJetpack = false;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
		cc = GetComponent<CharacterController> ();
		//cam = gameObject.transform.GetChild (0).GetComponent<Camera>();
        EquipWeapon(0);
	}
	
	// Update is called once per frame
	void Update () {
        //avoid rescaling when having a parent.
        transform.localScale = transform.localScale;
        
        animator.SetFloat("Velocity", Input.GetAxis("Vertical"));
        
        if (cc.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Vertical"),0f,-Input.GetAxis("Horizontal"));
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= movementSpeed;
			if (Input.GetButton ("Jump")) {
				moveDirection.y = jumpSpeed * jumpMultiplier;
			}

			if (Input.GetButton ("Run")) {
				Debug.Log ("Running!");
				moveDirection.x *= runSpeedMultiplier;
			}

		} else if(usingJetpack)
        {
            moveDirection = new Vector3(Input.GetAxis("Vertical"), 0f, -Input.GetAxis("Horizontal"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= movementSpeed;
        }

		moveDirection.y -= gravity * Time.unscaledDeltaTime;
        if (usingJetpack)
            moveDirection.y = 0f;

        cc.Move (moveDirection * Time.unscaledDeltaTime);

		pitch += rotationSpeed * Input.GetAxis ("Mouse Y");
		yaw += rotationSpeed * Input.GetAxis ("Mouse X");

		pitch = Mathf.Clamp (pitch, -50f, 60f);

		while (yaw < 0F) {
			yaw += 360F;
		}
		while (yaw >= 360F) {
			yaw -= 360F;	
		}

		transform.eulerAngles = new Vector3 (0f, yaw, 0f);
		cam.transform.localEulerAngles = new Vector3 (-pitch, 90f, 0f);

        arm.transform.LookAt(cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 100)));
        arm.transform.Rotate(0f, -90f, 0f);

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1); 
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EquipWeapon(3);
        }
        if (Input.GetButton("Fire1"))
        {

            GameObject weapon = arm.transform.GetChild(0).gameObject;
            if(weapon != null)
            {
                weapon.GetComponent<WeaponScript>().PrimaryAttack();
            }
        }
        if (Input.GetButton("Fire2"))
        {

            GameObject weapon = arm.transform.GetChild(0).gameObject;
            if (weapon != null)
            {
                weapon.GetComponent<WeaponScript>().SecondaryAttack();
            }
        }
    }
    // Removes the weapon held in hand, and equips the specified one.
    void EquipWeapon(int index)
    {
        if(arm.transform.childCount != 0)
        {
            if (arm.transform.GetChild(0) != null)
            {
                Destroy(arm.transform.GetChild(0).gameObject);
            }
        }
        
        GameObject weapon = Instantiate(weapons[index]) as GameObject;
        Debug.Log("Equipped weapon: " + weapon.name);
        weapon.transform.parent = arm.transform;
        weapon.transform.position = arm.transform.position;
        weapon.transform.rotation = arm.transform.rotation;
        currentWeaponIndex = index;

    }
	void OnTriggerEnter(Collider col) {
		if (col.tag == "JumpPlatform") {
			jumpMultiplier = col.gameObject.GetComponent<JumpPlatformScript> ().getJumpMultiplier ();	
			//Debug.Log ("Touched a jump platform. Jump multiplier is now: " + jumpMultiplier);
		} else if (col.tag == "CharacterHolder")
        {
            gameObject.transform.parent = col.transform;
           // Debug.Log(gameObject.transform.parent + " " + col.transform + " " + "Enter");
        }
    }

	void OnTriggerExit(Collider col) {
		if (col.tag == "JumpPlatform") {
			jumpMultiplier = 1F;
			Debug.Log ("Left the jump platform. Jump multiplier is now: " + jumpMultiplier);
		} else if (col.tag == "CharacterHolder")
        {
            gameObject.transform.parent = null;
            Debug.Log(gameObject.transform.parent + " " + col.transform + " " + "Exit");
        }
    }

    public void SetUsingJetpack(bool usingJetpack) {
        this.usingJetpack = usingJetpack;
    }
    
}
