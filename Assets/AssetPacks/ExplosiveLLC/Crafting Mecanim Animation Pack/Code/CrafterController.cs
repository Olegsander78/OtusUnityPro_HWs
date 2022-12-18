using UnityEngine;
using System.Collections;

namespace CraftingAnims
{
	public class CrafterController : MonoBehaviour
	{
		public enum CharacterState
		{
			Idle,
			Item,
			Box,
			Fishing,
			Hammer,
			Digging,
			Chopping,
			Food,
			Drink,
			Axe,
			Shovel,
			FishingPole,
			Saw,
			Sawing,
			PickAxe,
			PickAxing,
			Sickle,
			Rake,
			Spear,
			Spearfishing,
			Raking,
			Sit,
			Laydown,
			Climb,
			PushPull,
			Lumber,
			Overhead,
			Pray,
			Cart,
			Kneel,
			Painting,
			Use,
			Crawl
		};
		
		//Components.
		[HideInInspector] public Animator animator;
		[HideInInspector] public Rigidbody rb;
		[HideInInspector] public UnityEngine.AI.NavMeshAgent navMeshAgent;
		[HideInInspector] public GUIControls guiControls;

		public CharacterState charState;
		public float animationSpeed = 1;

		//Objects.
		private GameObject axe;
		private GameObject hammer;
		private GameObject fishingpole;
		private GameObject shovel;
		private GameObject box;
		private GameObject food;
		private GameObject drink;
		private GameObject saw;
		private GameObject pickaxe;
		private GameObject sickle;
		private GameObject rake;
		private GameObject chair;
		private GameObject ladder;
		private GameObject lumber;
		private GameObject pushpull;
		private GameObject sphere;
		private GameObject cart;
		private GameObject paintbrush;
		private GameObject spear;

		//Actions
		[HideInInspector] public bool isMoving;
		[HideInInspector] public bool isLocked;
		[HideInInspector] public bool isGrounded;
		[HideInInspector] public bool isSpearfishing;
		Coroutine coroutineLock = null;
		Vector3 newVelocity;
		bool isFacing;
		bool isRunning;
		float pushpullTime = 0f;
		bool usingItem;
		bool carryItem;

		//Input.
		bool allowedInput = true;
		Vector3 inputVec;
		float inputHorizontal = 0f;
		float inputVertical = 0f;
		float inputHorizontal2 = 0f;
		float inputVertical2 = 0f;
		bool inputFacing;
		bool inputRun;

		[Header("Movement")]
		public float rotationSpeed = 10f;
		public float runSpeed = 8f;
		public float walkSpeed = 4f;
		public float spearfishingSpeed = 1.25f;
		public float crawlSpeed = 1f;

		[Header("Navigation")]
		public bool useMeshNav;

		void Awake()
		{
			//Setup animator.
			animator = GetComponentInChildren<Animator>();
			if(animator)
			{
				animator.gameObject.AddComponent<AnimatorController>();
				animator.GetComponent<AnimatorController>().crafterController = this;
				animator.SetLayerWeight(1, 1f);
				animator.SetLayerWeight(2, 0f);
			}

			rb = GetComponent<Rigidbody>();
			guiControls = GetComponent<GUIControls>();

			axe = GameObject.Find("Axe");
			hammer = GameObject.Find("Hammer");
			fishingpole = GameObject.Find("FishingPole");
			shovel = GameObject.Find("Shovel");
			box = GameObject.Find("Carry");
			food = GameObject.Find("Food");
			drink = GameObject.Find("Drink");
			saw = GameObject.Find("Saw");
			pickaxe = GameObject.Find("PickAxe");
			sickle = GameObject.Find("Sickle");
			rake = GameObject.Find("Rake");
			chair = GameObject.Find("Chair");
			ladder = GameObject.Find("Ladder");
			lumber = GameObject.Find("Lumber");
			pushpull = GameObject.Find("PushPull");
			sphere = GameObject.Find("Sphere");
			cart = GameObject.Find("Cart");
			paintbrush = GameObject.Find("Paintbrush");
			spear = GameObject.Find("Spear");
		}
		
		void Start()
		{
			StartCoroutine(_ShowItem("none", 0f));
			charState = CharacterState.Idle;
		}
		
		//Input abstraction for easier asset updates using outside control schemes.
		void Inputs()
		{
			try
			{
				inputHorizontal = Input.GetAxisRaw("Horizontal");
				inputVertical = -(Input.GetAxisRaw("Vertical"));
				inputHorizontal2 = Input.GetAxisRaw("Horizontal2");
				inputVertical2 = -(Input.GetAxisRaw("Vertical2"));
				inputFacing = Input.GetButton("Aiming");
				inputRun = Input.GetButton("Fire3");
			}
			catch(System.Exception) { Debug.LogWarning("Inputs not found!"); }
		}
		
		void Update()
		{
			if(allowedInput) { Inputs(); }
			if(charState != CharacterState.PushPull) { CameraRelativeInput(); } 
			else { PushPull(); }
			if(Input.GetKey(KeyCode.R)) { gameObject.transform.position = new Vector3(0,0,0); }

			//Facing switch.
			if(inputFacing) { isFacing = true; }
			else { isFacing = false; }

			//Slow time.
			if(Input.GetKeyDown(KeyCode.T))
			{
				if(Time.timeScale != 1) { Time.timeScale = 1; }
				else { Time.timeScale = 0.15f; }
			}

			//Pause.
			if(Input.GetKeyDown(KeyCode.P))
			{
				if(Time.timeScale != 1) { Time.timeScale = 1; }
				else { Time.timeScale = 0f; }
			}
		}
		
		void FixedUpdate(){
			CheckForGrounded();

			//If locked, apply Root motion.
			if(!isLocked)
			{
				if(charState == CharacterState.Climb || charState == CharacterState.PushPull || charState == CharacterState.Laydown || charState == CharacterState.Use)
				{
					animator.applyRootMotion = true;
					isMoving = false;
					rb.useGravity = false;
				} 
				else
				{
					animator.applyRootMotion = false;
					rb.useGravity = true;
				}
			}

			//Change animator Animation Speed.
			animator.SetFloat("AnimationSpeed", animationSpeed);

		}
		
		void LateUpdate()
		{
			//Running.
			if(inputRun)
			{
				//Don't run with Box, Cart, Lumber, etc.
				if(charState != CharacterState.Box 
					&& charState != CharacterState.Cart 
					&& charState != CharacterState.Overhead 
					&& charState != CharacterState.PushPull 
					&& charState != CharacterState.Lumber 
					&& charState != CharacterState.Use)
				{
					isRunning = true;
					isFacing = false;
				}
			}
			else { isRunning = false; }

			//If using Navmesh nagivation, update Animator values.
			if(useMeshNav)
			{
				if(navMeshAgent.velocity.sqrMagnitude > 0)
				{
					animator.SetBool("Moving", true);
					animator.SetFloat("VelocityY", navMeshAgent.velocity.magnitude);
				}
			}

			//Crafter is moving.
			if(UpdateMovement() > 0)
			{
				isMoving = true;
				animator.SetBool("Moving", true);
			}
			else
			{
				isMoving = false;
				animator.SetBool("Moving", false);
			}

			//Get local velocity of charcter and update animator with values.
			float velocityXel = transform.InverseTransformDirection(rb.velocity).x;
			float velocityZel = transform.InverseTransformDirection(rb.velocity).z;

			//Set animator values if not pushpull.
			if(charState != CharacterState.PushPull)
			{
				animator.SetFloat("VelocityX", velocityXel / runSpeed);
				animator.SetFloat("VelocityY", velocityZel / runSpeed);
			}
		}
		
		//Moves the character.
		float UpdateMovement()
		{
			Vector3 motion = inputVec;

			//reduce input for diagonal movement.
			if(motion.magnitude > 1) { motion.Normalize(); }
		
			if(!isLocked && !useMeshNav 
				&& charState != CharacterState.PushPull 
				&& charState != CharacterState.Laydown 
				&& charState != CharacterState.Crawl)
			{
				//set speed by walking / running.
				if(isRunning) { newVelocity = motion * runSpeed; } 
				else if(isSpearfishing) { newVelocity = motion * spearfishingSpeed; }
				else { newVelocity = motion * walkSpeed; }
			} 
			else if(charState == CharacterState.Crawl) { newVelocity = motion * crawlSpeed; }

			//Aiming or rotate towards movement direction.
			if(isFacing 
				&& charState != CharacterState.Box
				&& charState != CharacterState.Lumber
				&& charState != CharacterState.Overhead)
			{
				Facing();
			}
			else
			{
				if(!isLocked && charState != CharacterState.PushPull 
					&& charState != CharacterState.Laydown 
					&& charState != CharacterState.Use)
				{
					RotateTowardsMovementDir();
				}
			}

			//if character is falling use momentum.
			newVelocity.y = rb.velocity.y;
			rb.velocity = newVelocity;

			//return a movement value for the animator.
			return inputVec.magnitude;
		}
		
		//checks if character is within a certain distance from the ground, and markes it IsGrounded.
		void CheckForGrounded()
		{
			float distanceToGround;
			float threshold = .45f;
			RaycastHit hit;
			Vector3 offset = new Vector3(0, 0.4f, 0);
			if(Physics.Raycast((transform.position + offset), -Vector3.up, out hit, 100f))
			{
				distanceToGround = hit.distance;
				if(distanceToGround < threshold) { isGrounded = true; }
				else { isGrounded = false; }
			}
		}
		
		//All movement is based off camera facing.
		void CameraRelativeInput()
		{
			//Camera relative movement
			Transform cameraTransform = Camera.main.transform;

			//Forward vector relative to the camera along the x-z plane.
			Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
			forward.y = 0;
			forward = forward.normalized;

			//Right vector relative to the camera always orthogonal to the forward vector.
			Vector3 right = new Vector3(forward.z, 0, -forward.x);

			//directional inputs.
			inputVec = inputHorizontal * right + -inputVertical * forward;
		}
		
		void PushPull()
		{
			if(inputHorizontal == 0 && inputVertical == 0) { pushpullTime = 0; }
			if(inputHorizontal != 0) { inputVertical = 0; }
			if(inputVertical != 0) { inputHorizontal = 0; }
			pushpullTime += 0.5f * Time.deltaTime;
			float h = Mathf.Lerp(0, inputHorizontal, pushpullTime);
			float v = Mathf.Lerp(0, inputVertical, pushpullTime);
			animator.SetFloat("VelocityX", h);
			animator.SetFloat("VelocityY", v);
		}
		
		//Face character along input direction.
		void RotateTowardsMovementDir()
		{
			if(inputVec != Vector3.zero) { transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputVec), Time.deltaTime * rotationSpeed); }
		}

		void Facing()
		{
			//Has joystick conneccted.
			for(int i = 0; i < Input.GetJoystickNames().Length; i++)
			{
				//if the right joystick is moved, use that for facing.
				if(Mathf.Abs(inputHorizontal2) > 0.1 || Mathf.Abs(inputVertical2) < -0.1)
				{
					Vector3 joyDirection = new Vector3(inputHorizontal2, 0, -inputVertical2);
					joyDirection = joyDirection.normalized;
					Quaternion joyRotation = Quaternion.LookRotation(joyDirection);
					transform.rotation = joyRotation;
				}
			}

			//no joysticks, use mouse aim.
			if(Input.GetJoystickNames().Length == 0)
			{
				Plane characterPlane = new Plane(Vector3.up, transform.position);
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Vector3 mousePosition = new Vector3(0, 0, 0);
				float hitdist = 0.0f;
				if(characterPlane.Raycast(ray, out hitdist)) { mousePosition = ray.GetPoint(hitdist); }
				mousePosition = new Vector3(mousePosition.x, transform.position.y, mousePosition.z);
				Vector3 relativePos = transform.position - mousePosition;
				Quaternion rotation = Quaternion.LookRotation(-relativePos);
				transform.rotation = rotation;
			}
		}

		void LockMovement(float locktime)
		{
			if(coroutineLock != null) { StopCoroutine(coroutineLock); }
			coroutineLock = StartCoroutine(_LockMovement(locktime));
		}

		IEnumerator _LockMovement(float locktime)
		{
			allowedInput = false;
			isLocked = true;
			animator.applyRootMotion = true;
			if(locktime != -1f)
			{
				yield return new WaitForSeconds(locktime);
				isLocked = false;
				animator.applyRootMotion = false;
				allowedInput = true;
			}
		}
		
		IEnumerator _ChangeCharacterState(float waitTime, CharacterState state)
		{
			yield return new WaitForSeconds(waitTime);
			charState = state;
		}

		#region AnimationLayerBlending

		void RightHandBlend(bool use)
		{
			StartCoroutine(_RightHandBlend(use));
		}

		IEnumerator _RightHandBlend(bool use)
		{
			if(use)
			{
				float counter = 0f;
				while(counter < 1)
				{
					counter += 0.05f;
					yield return new WaitForEndOfFrame();
					animator.SetLayerWeight(3, counter);
				}
				animator.SetLayerWeight(3, 1);
				usingItem = true;
			}
			else
			{
				float counter = 1f;
				while(counter > 0)
				{
					counter -= 0.05f;
					yield return new WaitForEndOfFrame();
					animator.SetLayerWeight(3, counter);
				}
				animator.SetLayerWeight(3, 0);
				usingItem = false;
			}
		}

		IEnumerator _RightHandBlendOff(float time)
		{
			yield return new WaitForSeconds(time);
			StartCoroutine(_RightHandBlend(false));
		}

		IEnumerator _RightArmBlendOff(float time)
		{
			if(carryItem)
			{
				yield return new WaitForSeconds(time);
				StartCoroutine(_RightArmBlend(false));
			}
		}

		void RightArmBlend(bool use)
		{
			StartCoroutine(_RightArmBlend(use));
		}

		IEnumerator _RightArmBlend(bool use)
		{
			if(use)
			{
				float counter = 0f;
				while(counter < 1)
				{
					counter += 0.05f;
					yield return new WaitForEndOfFrame();
					animator.SetLayerWeight(2, counter);
				}
				animator.SetLayerWeight(2, 1);
				carryItem = true;
			}
			else
			{
				float counter = 1f;
				while(counter > 0)
				{
					counter -= 0.05f;
					yield return new WaitForEndOfFrame();
					animator.SetLayerWeight(2, counter);
				}
				animator.SetLayerWeight(2, 0);
				carryItem = false;
			}
		}

		//Blend Right Arm Carry animation on/off.
		public void CarryItem(bool carry)
		{
			if(carry)
			{
				carryItem = true;
				RightArmBlend(true);
			}
			else
			{
				carryItem = false;
				RightArmBlend(false);
			}
		}

		void BlendOff(float time)
		{
			guiControls.ResetCarry();
			StartCoroutine(_RightArmBlendOff(time));
			StartCoroutine(_RightHandBlendOff(time));
		}

		#endregion

		IEnumerator _ShowItem(string item, float waittime)
		{
			yield return new WaitForSeconds (waittime);
			if(item == "none")
			{
				axe.SetActive(false);
				hammer.SetActive(false);
				fishingpole.SetActive(false);
				shovel.SetActive(false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "axe")
			{
				axe.SetActive(true);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "hammer")
			{
				axe.SetActive(false);
				hammer.SetActive (true);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "fishingpole")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (true);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "shovel")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (true);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "box")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(true);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "food")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(true);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "drink")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(true);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "saw")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(true);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "pickaxe")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(true);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "sickle")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(true);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "rake")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(true);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "chair")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(true);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "chaireat")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(true);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(true);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "chairdrink")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(true);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(true);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "ladder")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(true);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "pushpull")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(true);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "lumber")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(true);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "sphere")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(true);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "cart")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(true);
				paintbrush.SetActive(false);
				spear.SetActive(false);
			}
			else if(item == "paintbrush")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(true);
				spear.SetActive(false);
			}
			else if(item == "spear")
			{
				axe.SetActive(false);
				hammer.SetActive (false);
				fishingpole.SetActive (false);
				shovel.SetActive (false);
				box.SetActive(false);
				food.SetActive(false);
				drink.SetActive(false);
				saw.SetActive(false);
				pickaxe.SetActive(false);
				sickle.SetActive(false);
				rake.SetActive(false);
				chair.SetActive(false);
				ladder.SetActive(false);
				pushpull.SetActive(false);
				lumber.SetActive(false);
				sphere.SetActive(false);
				cart.SetActive(false);
				paintbrush.SetActive(false);
				spear.SetActive(true);
			}
			yield return null;
		}

		public void RecieveAction(string action)
		{
			#region LoseItems

			if(action == "Give Item")
			{
				animator.SetTrigger("ItemHandoffTrigger");
				StartCoroutine(_ShowItem("none", 0.4f));
				StartCoroutine(_ChangeCharacterState(0.4f, CrafterController.CharacterState.Idle));
				LockMovement(1f);
				BlendOff(0f);
			}
			if(action == "Put Away Item")
			{
				animator.SetTrigger("ItemBeltAwayTrigger");
				StartCoroutine(_ShowItem("none", 0.5f));
				StartCoroutine(_ChangeCharacterState(0.4f, CrafterController.CharacterState.Idle));
				LockMovement(1f);
				BlendOff(0f);
			}
			if(action == "Put Down Item")
			{
				animator.SetTrigger("ItemPutdownTrigger");
				StartCoroutine(_ShowItem("none", 0.7f));
				charState = CrafterController.CharacterState.Idle;
				LockMovement(1.2f);
				BlendOff(0f);
			}
			if(action == "Drop Item")
			{
				animator.SetTrigger("ItemDropTrigger");
				StartCoroutine(_ShowItem("none", 0.4f));
				charState = CrafterController.CharacterState.Idle;
				LockMovement(1.2f);
				BlendOff(0f);
			}
			if(action == "Plant Item")
			{
				animator.SetTrigger("ItemPlantTrigger");
				StartCoroutine(_ShowItem("none", 0.4f));
				charState = CrafterController.CharacterState.Idle;
				LockMovement(1.8f);
				BlendOff(0f);
			}

			#endregion

			if(charState == CrafterController.CharacterState.Idle)
			{
				#region GainItems

				if(action == "Get Hammer")
				{
					animator.SetTrigger("ItemBeltTrigger");
					StartCoroutine(_ShowItem("hammer", 0.5f));
					charState = CrafterController.CharacterState.Hammer;
					LockMovement(1f);
					RightHandBlend(true);
				}
				if(action == "Get Paintbrush")
				{
					animator.SetTrigger("ItemBeltTrigger");
					StartCoroutine(_ShowItem("paintbrush", 0.5f));
					charState = CrafterController.CharacterState.Painting;
					LockMovement(1f);
					RightHandBlend(true);
				}
				if(action == "Get Axe")
				{
					animator.SetTrigger("ItemBackTrigger");
					StartCoroutine(_ShowItem("axe", 0.5f));
					charState = CrafterController.CharacterState.Axe;
					LockMovement(1.2f);
					RightHandBlend(true);
				}
				if(action == "Get Spear")
				{
					animator.SetTrigger("ItemBackTrigger");
					StartCoroutine(_ShowItem("spear", 0.5f));
					charState = CrafterController.CharacterState.Spear;
					isSpearfishing = true;
					LockMovement(1.2f);
					RightHandBlend(true);
				}
				if(action == "Get PickAxe")
				{
					animator.SetTrigger("ItemBackTrigger");
					StartCoroutine(_ShowItem("pickaxe", 0.5f));
					charState = CrafterController.CharacterState.PickAxe;
					LockMovement(1.2f);
					RightHandBlend(true);
				}
				if(action == "Pickup Shovel")
				{
					animator.SetTrigger("ItemPickupTrigger");
					StartCoroutine(_ShowItem("shovel", 0.3f));
					charState = CrafterController.CharacterState.Shovel;
					LockMovement(1.2f);
					RightHandBlend(true);
				}
				if(action == "PullUp Fishing Pole")
				{
					animator.SetTrigger("ItemPullUpTrigger");
					StartCoroutine(_ShowItem("fishingpole", 0.5f));
					charState = CrafterController.CharacterState.FishingPole;
					LockMovement(1.7f);
					RightHandBlend(true);
				}
				if(action == "Take Food")
				{
					animator.SetTrigger("ItemTakeTrigger");
					StartCoroutine(_ShowItem("food", 0.3f));
					charState = CrafterController.CharacterState.Food;
					LockMovement(1.2f);
					RightHandBlend(true);
				}
				if(action == "Recieve Drink")
				{
					animator.SetTrigger("ItemRecieveTrigger");
					StartCoroutine(_ShowItem("drink", 0.5f));
					charState = CrafterController.CharacterState.Drink;
					LockMovement(1.2f);
					RightHandBlend(true);
				}
				if(action == "Pickup Box")
				{
					animator.SetTrigger("CarryPickupTrigger");
					StartCoroutine(_ShowItem("box", 0.1f));
					charState = CrafterController.CharacterState.Box;
					LockMovement(1.2f);
				}
				if(action == "Pickup Lumber")
				{
					animator.SetTrigger("LumberPickupTrigger");
					StartCoroutine(_ShowItem("lumber", 0.5f));
					charState = CrafterController.CharacterState.Lumber;
					LockMovement(1.6f);
				}
				if(action == "Pickup Overhead")
				{
					animator.SetTrigger("CarryOverheadPickupTrigger");
					StartCoroutine(_ShowItem("sphere", 0.5f));
					charState = CrafterController.CharacterState.Overhead;
					LockMovement(1.2f);
				}
				if(action == "Recieve Box")
				{
					animator.SetTrigger("CarryRecieveTrigger");
					StartCoroutine(_ShowItem("box", 0.5f));
					charState = CrafterController.CharacterState.Box;
					LockMovement(1.2f);
				}
				if(action == "Get Saw")
				{
					animator.SetTrigger("ItemBeltTrigger");
					StartCoroutine(_ShowItem("saw", 0.5f));
					charState = CrafterController.CharacterState.Saw;
					LockMovement(1.2f);
					RightHandBlend(true);
				}
				if(action == "Get Sickle")
				{
					animator.SetTrigger("ItemBeltTrigger");
					StartCoroutine(_ShowItem("sickle", 0.5f));
					charState = CrafterController.CharacterState.Sickle;
					LockMovement(1.2f);
					RightHandBlend(true);
				}
				if(action == "Get Rake")
				{
					animator.SetTrigger("ItemBackTrigger");
					StartCoroutine(_ShowItem("rake", 0.5f));
					charState = CrafterController.CharacterState.Rake;
					LockMovement(1.2f);
					RightHandBlend(true);
				}

				#endregion

				#region Actions

				if(action == "Use")
				{
					animator.SetBool("Use", true);
					charState = CrafterController.CharacterState.Use;
					LockMovement(-1);
				}
				if(action == "Crawl")
				{
					animator.SetTrigger("CrawlStartTrigger");
					charState = CrafterController.CharacterState.Crawl;
					LockMovement(1f);
				}
				if(action == "Sit")
				{
					animator.SetTrigger("ChairSitTrigger");
					StartCoroutine(_ShowItem("chair", 0.3f));
					charState = CrafterController.CharacterState.Sit;
					LockMovement(-1f);
				}
				if(action == "Push Cart")
				{
					animator.SetTrigger("CartPullGrabTrigger");
					StartCoroutine(_ShowItem("cart", 0.25f));
					charState = CrafterController.CharacterState.Cart;
					LockMovement(1.2f);
				}
				if(action == "Laydown")
				{
					animator.SetTrigger("LaydownLaydownTrigger");
					charState = CrafterController.CharacterState.Laydown;
					LockMovement(-1f);
				}
				if(action == "Gather")
				{
					animator.SetTrigger("GatherTrigger");
					LockMovement(2.2f);
				}
				if(action == "Gather Kneeling")
				{
					animator.SetTrigger("GatherKneelingTrigger");
					LockMovement(2.2f);
				}
				if(action == "Wave1")
				{
					animator.SetTrigger("Wave1Trigger");
					LockMovement(2.2f);
				}
				if(action == "Scratch Head")
				{
					animator.SetTrigger("Bored1Trigger");
					LockMovement(2.5f);
				}
				if(action == "Cheer1")
				{
					animator.SetTrigger("Cheer1Trigger");
					LockMovement(2.7f);
				}
				if(action == "Cheer2")
				{
					animator.SetTrigger("Cheer2Trigger");
					LockMovement(3f);
				}
				if(action == "Cheer3")
				{
					animator.SetTrigger("Cheer3Trigger");
					LockMovement(2.4f);
				}
				if(action == "Fear")
				{
					animator.SetTrigger("FearTrigger");
					LockMovement(4f);
				}
				if(action == "Climb")
				{
					animator.SetTrigger("ClimbStartTrigger");
					StartCoroutine(_ShowItem("ladder", 0.3f));
					charState = CrafterController.CharacterState.Climb;
					LockMovement(-1f);
				}
				if(action == "Climb Top")
				{
					this.gameObject.transform.position += new Vector3(0, 3, 0);
					animator.SetTrigger("ClimbOnTopTrigger");
					StartCoroutine(_ShowItem("ladder", 0.3f));
					charState = CrafterController.CharacterState.Climb;
					LockMovement(-1f);
				}
				if(action == "Pray")
				{
					animator.SetTrigger("Pray-DownTrigger");
					charState = CrafterController.CharacterState.Pray;
					LockMovement(-1f);
				}
				if(action == "Push Pull")
				{
					animator.SetTrigger("PushPullStartTrigger");
					StartCoroutine(_ShowItem("pushpull", 0.3f));
					charState = CrafterController.CharacterState.PushPull;
					LockMovement(1.2f);
				}
			}

			#endregion

			#region EnterStates

			if(charState == CrafterController.CharacterState.Shovel)
			{
				if(action == "Start Digging")
				{
					animator.SetTrigger("DiggingStartTrigger");
					charState = CrafterController.CharacterState.Digging;
					LockMovement(-1f);
				}
			}
			if(charState == CrafterController.CharacterState.Rake)
			{
				if(action == "Start Raking")
				{
					animator.SetTrigger("DiggingStartTrigger");
					charState = CrafterController.CharacterState.Raking;
					LockMovement(-1f);
				}
			}
			if(charState == CrafterController.CharacterState.Axe)
			{
				if(action == "Start Chopping")
				{
					animator.SetTrigger("ChoppingStartTrigger");
					charState = CrafterController.CharacterState.Chopping;
					LockMovement(-1f);
				}
			}
			if(charState == CrafterController.CharacterState.FishingPole)
			{
				if(action == "Cast Reel")
				{
					animator.SetTrigger("FishingCastTrigger");
					charState = CrafterController.CharacterState.Fishing;
					LockMovement(-1f);
				}
			}
			if(charState == CrafterController.CharacterState.Spear)
			{
				if(action == "Start Spearfishing")
				{
					animator.SetTrigger("SpearfishStartTrigger");
					charState = CrafterController.CharacterState.Spearfishing;
					LockMovement(1.2f);
				}
			}

			#endregion

			#region States

			if(charState == CrafterController.CharacterState.Cart)
			{
				if(action == "Release Cart")
				{
					animator.SetTrigger("CartPullReleaseTrigger");
					StartCoroutine(_ShowItem("none", 0.75f));
					charState = CrafterController.CharacterState.Idle;
					LockMovement(1.2f);
				}
			}
			if(charState == CrafterController.CharacterState.Hammer)
			{
				if(action == "Hammer Wall")
				{
					animator.SetTrigger("HammerWallTrigger");
					LockMovement(1.9f);
				}
				if(action == "Hammer Table")
				{
					animator.SetTrigger("HammerTableTrigger");
					LockMovement(1.9f);
				}
				if(action == "Kneel")
				{
					animator.SetTrigger("ItemKneelDownTrigger");
					charState = CrafterController.CharacterState.Kneel;
					LockMovement(-1f);
				}
				if(action == "Chisel")
				{
					animator.SetTrigger("ItemChiselTrigger");
					LockMovement(1.2f);
				}
			}
			if(charState == CrafterController.CharacterState.Painting)
			{
				if(action == "Paint Wall")
				{
					animator.SetTrigger("ItemPaintTrigger");
					LockMovement(1.9f);
				}
				if(action == "Fill Brush")
				{
					animator.SetTrigger("ItemPaintRefillTrigger");
					LockMovement(1.9f);
				}
			}
			if(charState == CrafterController.CharacterState.PickAxe)
			{
				if(action == "Start PickAxing")
				{
					animator.SetTrigger("ChoppingStartTrigger");
					charState = CrafterController.CharacterState.PickAxing;
					LockMovement(-1f);
				}
			}
			if(charState == CrafterController.CharacterState.Saw)
			{
				if(action == "Start Sawing")
				{
					animator.SetTrigger("SawStartTrigger");
					charState = CrafterController.CharacterState.Sawing;
					LockMovement(-1f);
				}
			}
			if(charState == CrafterController.CharacterState.Drink)
			{
				if(action == "Drink")
				{
					animator.SetTrigger("ItemDrinkTrigger");
					LockMovement(1.4f);
				}
				if(action == "Water")
				{
					animator.SetTrigger("ItemWaterTrigger");
					LockMovement(2f);
				}
			}
			if(charState == CrafterController.CharacterState.Food)
			{
				if(action == "Eat Food")
				{
					animator.SetTrigger("ItemEatTrigger");
					LockMovement(1.4f);
				}
				if(action == "Plant Food")
				{
					animator.SetTrigger("ItemPlantTrigger");
					StartCoroutine(_ShowItem("none", 0.6f));
					charState = CrafterController.CharacterState.Idle;
					LockMovement(1.2f);
				}
			}
			if(charState == CrafterController.CharacterState.Sickle)
			{
				if(action == "Use Sickle")
				{
					animator.SetTrigger("ItemSickleUse");
					LockMovement(1.6f);
				}
			}
			if(charState == CrafterController.CharacterState.Box)
			{
				if(action == "Put Down Box")
				{
					animator.SetTrigger("CarryPutdownTrigger");
					StartCoroutine(_ShowItem("none", 0.9f));
					charState = CrafterController.CharacterState.Idle;
					LockMovement(1.2f);
				}
				if(action == "Throw Box")
				{
					animator.SetTrigger("CarryThrowTrigger");
					StartCoroutine(_ShowItem("none", 0.5f));
					charState = CrafterController.CharacterState.Idle;
					LockMovement(1.2f);
				}
				if(action == "Give Box")
				{
					animator.SetTrigger("CarryHandoffTrigger");
					StartCoroutine(_ShowItem("none", 0.6f));
					charState = CrafterController.CharacterState.Idle;
					LockMovement(1.2f);
				}
			}
			if(charState == CrafterController.CharacterState.Lumber)
			{
				if(action == "Put Down Lumber")
				{
					animator.SetTrigger("CarryPutdownTrigger");
					StartCoroutine(_ShowItem("none", 1f));
					charState = CrafterController.CharacterState.Idle;
					LockMovement(1.2f);
				}
			}
			if(charState == CrafterController.CharacterState.Overhead)
			{
				if(action == "Throw Sphere")
				{
					animator.SetTrigger("CarryOverheadThrowTrigger");
					StartCoroutine(_ShowItem("none", 0.5f));
					charState = CrafterController.CharacterState.Idle;
					LockMovement(1.2f);
				}
			}
			if(charState == CrafterController.CharacterState.PushPull)
			{
				if(action == "Release")
				{
					animator.SetTrigger("PushPullReleaseTrigger");
					StartCoroutine(_ShowItem("none", 0.5f));
					StartCoroutine(_ChangeCharacterState(0.5f, CrafterController.CharacterState.Idle));
					LockMovement(1f);
				}
			}
			if(charState == CrafterController.CharacterState.Crawl)
			{
				if(action == "Getup")
				{
					animator.SetTrigger("CrawlGetupTrigger");
					charState = CrafterController.CharacterState.Idle;
					LockMovement(1f);
				}
			}
			if(charState == CrafterController.CharacterState.Spear)
			{
				if(action == "Start Spearfishing")
				{
					animator.SetTrigger("SpearfishAttackTrigger");
					LockMovement(0.6f);
				}
				if(action == "Finish Spearfishing")
				{
					animator.SetTrigger("SpearfishEndTrigger");
					LockMovement(1f);
				}
			}

			if(charState == CrafterController.CharacterState.Spearfishing)
			{
				if(action == "Spear")
				{
					animator.SetTrigger("SpearfishAttackTrigger");
					LockMovement(1.6f);
				}
				if(action == "Finish Spearfishing")
				{
					animator.SetTrigger("SpearfishEndTrigger");
					charState = CrafterController.CharacterState.Spear;
					LockMovement(0.6f);
				}
			}

			#endregion

			#region LockedStates

			if(charState == CrafterController.CharacterState.Pray)
			{
				if(action == "Stand")
				{
					animator.SetTrigger("Pray-StandTrigger");
					charState = CrafterController.CharacterState.Idle;
					LockMovement(1.4f);
				}
			}
			if(charState == CrafterController.CharacterState.Kneel)
			{
				if(action == "Hammer") { animator.SetTrigger("ItemKneelHammerTrigger"); }
				if(action == "Stand")
				{
					animator.SetTrigger("ItemKneelStandTrigger");
					charState = CrafterController.CharacterState.Hammer;
					LockMovement(1f);
				}
			}
			if(charState == CrafterController.CharacterState.Chopping)
			{
				if(action == "Chop Vertical") { animator.SetTrigger("ChopVerticalTrigger"); }
				if(action == "Chop Horizontal") { animator.SetTrigger("ChopHorizontalTrigger"); }
				if(action == "Chop Diagonal") { animator.SetTrigger("ChopDiagonalTrigger"); }
				if(action == "Chop Ground") { animator.SetTrigger("ChopGroundTrigger"); }
				if(action == "Finish Chopping")
				{
					animator.SetTrigger("ChopFinishTrigger");
					charState = CrafterController.CharacterState.Axe;
					LockMovement(1.4f);
				}
			}
			if(charState == CrafterController.CharacterState.PickAxing)
			{
				if(action == "Swing Vertical") { animator.SetTrigger("ChopVerticalTrigger"); }
				if(action == "Swing Horizontal") { animator.SetTrigger("ChopHorizontalTrigger"); }
				if(action == "Swing Ground") { animator.SetTrigger("ChopGroundTrigger"); }
				if(action == "Swing Ceiling") { animator.SetTrigger("ChopCeilingTrigger"); }
				if(action == "Swing Diagonal") { animator.SetTrigger("ChopDiagonalTrigger"); }
				if(action == "Finish PickAxing")
				{
					animator.SetTrigger("ChopFinishTrigger");
					charState = CrafterController.CharacterState.PickAxe;
					LockMovement(1.4f);
				}
			}
			if(charState == CrafterController.CharacterState.Raking)
			{
				if(action == "Rake") { animator.SetTrigger("ItemRakeUse"); }
				if(action == "Finish Raking")
				{
					animator.SetTrigger("DiggingFinishTrigger");
					charState = CrafterController.CharacterState.Rake;
					LockMovement(1f);
				}
			}
			if(charState == CrafterController.CharacterState.Digging)
			{
				if(action == "Dig") { animator.SetTrigger("DiggingScoopTrigger"); }
				if(action == "Finish Digging")
				{
					animator.SetTrigger("DiggingFinishTrigger");
					charState = CrafterController.CharacterState.Shovel;
					LockMovement(1f);
				}
			}
			if(charState == CrafterController.CharacterState.Sawing)
			{
				if(action == "Finish Sawing")
				{
					animator.SetTrigger("SawFinishTrigger");
					charState = CrafterController.CharacterState.Saw;
					LockMovement(1f);
				}
			}
			if(charState == CrafterController.CharacterState.Sit)
			{
				if(action == "Talk1") { animator.SetTrigger("ChairTalk1Trigger"); }
				if(action == "Eat")
				{
					animator.SetTrigger("ChairEatTrigger");
					StartCoroutine(_ShowItem("chaireat", 0.2f));
					StartCoroutine(_ShowItem("chair", 1.1f));
				}
				if(action == "Drink")
				{
					animator.SetTrigger("ChairDrinkTrigger");
					StartCoroutine(_ShowItem("chairdrink", 0.2f));
					StartCoroutine(_ShowItem("chair", 1.1f));
				}
				if(action == "Stand")
				{
					animator.SetTrigger("ChairStandTrigger");
					StartCoroutine(_ShowItem("none", 0.5f));
					charState = CrafterController.CharacterState.Idle;
					LockMovement(1.3f);
				}
			}
			if(charState == CrafterController.CharacterState.Fishing)
			{
				if(action == "Reel In") { animator.SetTrigger("FishingReelTrigger"); }
				if(action == "Finish Fishing")
				{
					animator.SetTrigger("FishingFinishTrigger");
					charState = CrafterController.CharacterState.FishingPole;
					LockMovement(0.7f);
				}
			}
			if(charState == CrafterController.CharacterState.Climb)
			{
				if(action == "Climb Off Bottom")
				{
					animator.SetTrigger("ClimbOffBottomTrigger");
					StartCoroutine(_ShowItem("none", 0.9f));
					StartCoroutine(_ChangeCharacterState(0.9f, CrafterController.CharacterState.Idle));
				}
				if(action == "Climb Up") { animator.SetTrigger("ClimbUpTrigger"); }
				if(action == "Climb Down") { animator.SetTrigger("ClimbDownTrigger"); }
				if(action == "Climb Off Top")
				{
					Vector3 posPivot = animator.pivotPosition;
					animator.SetTrigger("ClimbOffTopTrigger");
					StartCoroutine(_ShowItem("none", 2f));
					StartCoroutine(_ChangeCharacterState(2f, CrafterController.CharacterState.Idle));
					animator.stabilizeFeet = true;
				}
			}
			if(charState == CrafterController.CharacterState.Laydown)
			{
				if(action == "Getup")
				{
					animator.SetTrigger("LaydownGetupTrigger");
					charState = CrafterController.CharacterState.Idle;
					LockMovement(2f);
				}
			}
			if(charState == CrafterController.CharacterState.Use)
			{
				if(action == "Stop Use")
				{
					animator.SetBool("Use", false);
					charState = CrafterController.CharacterState.Idle;
					LockMovement(0.3f);
				}
			}

			#endregion
		}
	}
}