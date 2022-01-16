using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tpControl : MonoBehaviour
{
	//only allow specific tag object? if left empty, any object can teleport
	public string objectTag = "if empty, any object will tp";
	//one or more destination pads
	public Transform[] destinationPad;
	private bool inside;
	//check to wait for arrived object to leave before enabling teleporation again
	[HideInInspector]
	//gameobjects inside the pad
	private Transform subject;
	//add a sound component if you want the teleport playing a sound when teleporting
	public AudioSource teleportSound;
	//add a sound component for the teleport pad, vibrating for example, or music if you want :D
	//also to make it more apparent when the pad is off, stop this component playing with "teleportPadSound.Stop();"
	//PS the distance is set to 10 units, so you only hear it standing close, not from the other side of the map
	public AudioSource teleportPadSound;
	public GameObject playerCamera;

	// newly added variables by Ziyang
	public GameObject tpCanvas;
	// The destination text to be changed accordingly by pressing left or right arrow
	public GameObject desPadText;
	// Distance between center of the pad and canvas
	public int canvasOffset = 20;
	public int tpCanvasHeight = 20;
	private Transform parent_tran;
	private int padIndex = 0;
	private Text desPad;
	private bool rotating = true;
	private bool running = true;
	private GameObject player;
	private float prevSpeed;
	private float prevSen;
	private bool disableMovement = false;
	private Transform cur_DesPad;
	private ParticleSystem ps;
	private float timeCount = 1f;
	private bool isFading = false;
	private Graphic blackColor;
	private float fadeSpeed;
	private bool fadingWhite = false;

	void Start()
	{
		//Set the countdown ready to the time you chose
		desPadText = GameObject.Find("Des");
		parent_tran = gameObject.transform.parent;
		desPad = desPadText.GetComponent<Text>();
		tpCanvas = GameObject.Find("TPcanvas");
		player = GameObject.Find("Player");
		ps = GetComponent<ParticleSystem>();
		blackColor = GameObject.Find("blkColor").GetComponent<Graphic>();
		fadeSpeed = 1 / timeCount;

		if (destinationPad.Length != 0)
		{
			cur_DesPad = destinationPad[0];
		}

		if (playerCamera == null)
		{
			playerCamera = GameObject.Find("Camera");
		}
	}
	private float nfmod(float a, float b)
	{
		return a - b * Mathf.Floor(a / b);
	}


	void Update()
	{
		if (fadingWhite)
		{
			timeCount -= Time.deltaTime;
			Color blkColor = blackColor.color;
			blackColor.color = new Color(0, 0, 0, blkColor.a - fadeSpeed * Time.deltaTime);

			if (timeCount <= 0)
			{
				blackColor.color = new Color(0, 0, 0, 0);
				fadingWhite = false;
				timeCount = 1f;
				disableMovement = false;
				player.GetComponent<MovePlayer>().speed = prevSpeed;
				playerCamera.GetComponent<LookMouse>().mouseSensitivity = prevSen;
			}
		}

		//check if theres something/someone inside
		if (inside)
		{
			var em = ps.emission;
			if (Input.GetKeyDown(KeyCode.E))
			{
				tpCanvas.transform.position = new Vector3(transform.position.x, tpCanvasHeight, transform.position.z + canvasOffset);
				disableMovement = true;
				tpCanvas.SetActive(true);
				desPad.text = cur_DesPad.name.Substring(4);
				rotating = true;
				running = true;
				prevSpeed = player.GetComponent<MovePlayer>().speed;
				prevSen = playerCamera.GetComponent<LookMouse>().mouseSensitivity;
				playerCamera.GetComponent<LookMouse>().mouseSensitivity = 0;
				player.GetComponent<MovePlayer>().speed = 0;
				em.enabled = false;
			}

			if (Input.GetKeyDown(KeyCode.X))
			{
				disableMovement = false;
				tpCanvas.SetActive(false);
				player.GetComponent<MovePlayer>().speed = prevSpeed;
				playerCamera.GetComponent<LookMouse>().mouseSensitivity = prevSen;
				em.enabled = true;
			}

			if (disableMovement)
			{
				if (running)
				{
					Vector3 to = new Vector3(transform.position.x, subject.position.y, transform.position.z);
					if (Vector3.Distance(subject.position, to) > 0.1f)
					{
						subject.position = Vector3.Lerp(subject.position, to, Time.deltaTime * prevSpeed);
					}
					else
					{
						subject.position = to;
						running = false;
					}
				}

				if (rotating)
				{

					Vector3 to = parent_tran.eulerAngles;
					if (Vector3.Distance(subject.eulerAngles, to) > 0.1f)
					{
						subject.eulerAngles = Vector3.Lerp(subject.eulerAngles, to, Time.deltaTime*5);
					}
					else
					{
						subject.eulerAngles = to;
						rotating = false;
						playerCamera.GetComponent<LookMouse>().mouseSensitivity = prevSen;
					}
				}

				if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
				{
					padIndex = (int) nfmod(padIndex - 1, destinationPad.Length);
					cur_DesPad = destinationPad[padIndex];
					desPad.text = cur_DesPad.name.Substring(4);
				}

				if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
				{
					padIndex = (padIndex + 1) % destinationPad.Length;
					cur_DesPad = destinationPad[padIndex];
					desPad.text = cur_DesPad.name.Substring(4);
				}

				if (Input.GetKeyDown(KeyCode.Return))
				{

					isFading = true;
				}

				if (isFading)
				{
					timeCount -= Time.deltaTime;
					Color blkColor = blackColor.color;
					blackColor.color = new Color(0, 0, 0, blkColor.a + fadeSpeed * Time.deltaTime);

					if (timeCount <= 0)
					{
						blackColor.color = Color.black;
						isFading = false;
						Teleport();
					}
				}
			}

			Physics.SyncTransforms();
		}
	}

	void Teleport()
	{
		var em = ps.emission;
		tpCanvas.SetActive(false);
		em.enabled = true;
		subject.position = new Vector3(cur_DesPad.position.x, subject.position.y, cur_DesPad.position.z);
		//subject.position = cur_DesPad.transform.position;
		teleportSound.Play();
		timeCount = 2f;
		fadingWhite = true;
	}

	void OnTriggerEnter(Collider trig)
	{
		//when an object enters the trigger
		//if you set a tag in the inspector, check if an object has that tag
		if (objectTag != "")
		{
			//if the objects tag is the same as the one allowed in the inspector
			if (trig.gameObject.tag == objectTag)
			{

				//set the subject to be the entered object
				subject = trig.transform;
				//and check inside, ready for teleport
				inside = true;
			}
		}
	}

	void OnTriggerExit(Collider trig)
	{
		//////////////if you set a tag for the entering pad, you should also set it for the exiting pad////////
		//when an object exists the trigger
		//if you set a tag in the inspector, check if an object has that tag
		//otherwise the pad will register any object as the one leaving 
		if (objectTag != "")
		{
			//if the objects tag is the same as the one allowed in the inspector
			if (trig.gameObject.tag == objectTag)
			{
				//set that the object left
				inside = false;
				//remove the subject from the pads memory
				subject = null;
			}
		}
	}
}
