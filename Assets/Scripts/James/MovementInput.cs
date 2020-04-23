
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementInput : MonoBehaviour
{

	private float Velocity;
	public float Jumpforce,gravity;
	public float dummy;
	public bool gather, candoublejump;
	private float time;

	public float InputX;
	public float InputZ;
	public Vector3 desiredMoveDirection;
	public bool blockRotationPlayer, Jump;
	private float gravity_actual;
	public float desiredRotationSpeed = 0.1f;
	public Animator anim;
	public float Speed,walkspeed,runspeed;
	public float allowPlayerRotation = 0.1f;
	public Camera cam;
	public CharacterController controller;
	public bool isGrounded, Sprinting, Flip, RunningFlip;

	[Header("Animation Smoothing")]
	[Range(0, 1f)]
	public float HorizontalAnimSmoothTime = 0.2f;
	[Range(0, 1f)]
	public float VerticalAnimTime = 0.2f;
	[Range(0, 1f)]
	public float StartAnimTime = 0.3f;
	[Range(0, 1f)]
	public float StopAnimTime = 0.15f;

	public float verticalVel;
	private Vector3 moveVector;

	// Use this for initialization
	void Start()
	{
		anim = this.GetComponent<Animator>();
		cam = Camera.main;
		controller = this.GetComponent<CharacterController>();
		Sprinting = false;
		time = 0f;
		gather = false;
        walkspeed = 3f;
        runspeed = 6f;
        Velocity = walkspeed;
	}

	// Update is called once per frame
	void Update()
	{
        if (Health.instance.dead == false)
        {
            isGrounded = controller.isGrounded;
            if (isGrounded)
            {
                verticalVel = -0.5f;
                Jump = false;
                Flip = false;
                RunningFlip = false;

            }
            else
            {

                verticalVel = verticalVel - gravity * Time.deltaTime;
                if (Jump || RunningFlip)
                    verticalVel -= gravity * Time.deltaTime;
                controller.Move(transform.up * verticalVel * Time.deltaTime);

            }
            InputMagnitude();

            time += Time.deltaTime;
           

            if (controller.isGrounded == false)
                controller.Move(new Vector3(0, verticalVel, 0));

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Sprinting = true;
                Velocity = runspeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Sprinting = false;
                Velocity = walkspeed;
            }
            if (Input.GetKeyDown(KeyCode.Q) && controller.isGrounded && time > 0.5f && !Sprinting)
            {
                time = 0f;
                Jump = true;
                verticalVel += Jumpforce;
                candoublejump = true;
                controller.Move(new Vector3(0, verticalVel, 0));
                if (controller.isGrounded)
                    Jump = false;

            }
            if (Input.GetKeyDown(KeyCode.Q) && controller.isGrounded && time > 0.5f && Sprinting)
            {
                time = 0f;
                RunningFlip = true;
                verticalVel += Jumpforce * 1.5f;
                controller.Move(new Vector3(0, verticalVel, 0));
            }
            //moveVector = new Vector3(0, verticalVel * .2f , 0);
            //	controller.Move(moveVector);

            /*if(Crouching.crouching)
            {
                gravity_actual = 0;
            }
            else
            {
                gravity_actual = 0;
            }
            */

            if (gather)
            {
                if (time > 0.2f)
                    gather = false;
            }
            anim.SetBool("Jump", Jump);
            anim.SetBool("Pickup", gather);
            anim.SetBool("Flip", Flip);
            anim.SetBool("RunningFlip", RunningFlip);
        }
	}

	void PlayerMoveAndRotation()
	{
		InputX = Input.GetAxis("Horizontal");
		InputZ = Input.GetAxis("Vertical");

		var camera = Camera.main;
		var forward = cam.transform.forward;
		var right = cam.transform.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize();
		right.Normalize();

		desiredMoveDirection = forward * InputZ + right * InputX;

		desiredMoveDirection.y -= gravity * 0.5f * Time.deltaTime;
		//	if (Jump)
		//	desiredMoveDirection.y += Jumpforce;

		if (blockRotationPlayer == false)
		{
			//if (controller.isGrounded)
			//	Jump = false;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
			controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);
		}
	}

	public void LookAt(Vector3 pos)
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), desiredRotationSpeed);
	}

	public void RotateToCamera(Transform t)
	{

		var camera = Camera.main;
		var forward = cam.transform.forward;
		var right = cam.transform.right;

		desiredMoveDirection = forward;

		t.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
	}

	void InputMagnitude()
	{
		//Calculate Input Vectors
		InputX = Input.GetAxis("Horizontal");
		InputZ = Input.GetAxis("Vertical");

		//anim.SetFloat ("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
		//anim.SetFloat ("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);


		Speed = new Vector2(InputX, InputZ).sqrMagnitude;
		dummy = Speed;
		if (!Sprinting)
			if (dummy > 0.5f)
				dummy = 0.5f;



		if (Speed > allowPlayerRotation)
		{
			anim.SetFloat("Blend", dummy, StartAnimTime, Time.deltaTime);
			PlayerMoveAndRotation();
		}
		else if (Speed < allowPlayerRotation)
		{
			anim.SetFloat("Blend", dummy, StopAnimTime, Time.deltaTime);
		}
	}
	public void OnControllerColliderHit(ControllerColliderHit hit)
	//public void OnColliderEnter(Collider collider)
	{
		if (hit.transform.tag == "Pickup")
		{
			
			if (Input.GetKeyDown(KeyCode.E))
			{
				time = 0f;
              //  Destroy(hit.gameObject.GetComponent<Item_Pickup>());
                
				hit.transform.SendMessage("Pickup", SendMessageOptions.DontRequireReceiver);
				gather = true;
				//if(Inventory.instance.Add(hit.gameObject.GetComponent<Item_Pickup>().item))
                {
                    hit.gameObject.GetComponent<BoxCollider>().enabled = false;
                    Destroy(hit.gameObject);
                }
            }
		}
	}

	public void Fall()
	{
		Jump = false;
	}
}
