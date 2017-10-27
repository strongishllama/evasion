//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//  Author:         Emma Cameron & Taliesin Millhouse
//  Date Created:   22nd September 2016
//  Brief:          BaseGuard Class Controls The BaseGuard.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

public class BaseGuard : MonoBehaviour
{
    /// <summary>
    /// private int : Destination Point counts what point the BaseGuard needs to move towards.
    /// </summary>
    private int m_iDestinationPoint = 0;

    /// <summary>
    /// public float : How far the guard can see infont of him.
    /// </summary>
    public float m_fSightRange = 5.7f;

    /// <summary>
    /// private bool : Is a check to see tell the GameManager to transition a scene restart if the player is detected.
    /// </summary>
    private bool bIsTransitioning = false;

    /// <summary>
    /// public string : Checks the current scene and stores it in a string.
    /// </summary>
    public string m_strCurrentScene;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public Vector3 : The BaseGuards next destination to move towards.
    /// </summary>
    public Vector3 m_v3NextDestination;

    /// <summary>
    /// public Transform : BaseGuards eyes, where the detection ray cast will be cast from.
    /// </summary>
    public Transform m_Eyes;
    
    /// <summary>
    /// public Transform Array : Used to set the BaseGuards waypoints that it will travel towards.
    /// </summary>
    public Transform[] m_WayPoints;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    /// <summary>
    /// public Color : Stores the color changes to be applied to the materials that will be changing color.
    /// </summary>
    public Color m_Color;
    
    /// <summary>
    /// Material : Used to set the color changes in the BaseGuards, spotlights, wall lights and exit sign.
    /// </summary>
    Material m_Material;

    /// <summary>
    /// public NavMeshAgent : What dictates where the BaseGuard can move.
    /// </summary>
    public UnityEngine.AI.NavMeshAgent m_Agent;

    /// <summary>
    /// public AnimationManager : Initializing the AnimationManager for BASE Guard
    /// </summary>
    public Animator m_AnimatorGuard;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public Renderer : Is a reference to the Exit Sign to change its emission color.
    /// </summary>
    public Renderer m_ExitSign;

    /// <summary>
    /// public Renderer : Is a reference to the BaseGuard to change its emission color.
    /// </summary>
    public Renderer m_BaseGuardRenderer;

    /// <summary>
    /// public Renderer : Is a reference to the Walls to change their emission color.
    /// </summary>
    public Renderer[] m_WallRenderer;

    /// <summary>
    /// public Light Array : Stores the BaseGuard's lights  to create an offset while it is moving.
    /// </summary>
    public Light[] m_Lights;

	/// <summary>
	/// public GameObject : Stores the empty object that the lights are child objects of.
	/// </summary>
    public GameObject m_Lighting;

	/// <summary>
	/// public GameObject : Stores the furthest away from the BaseGuard to create a light offset.
	/// </summary>
    public GameObject m_LastLight;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public SoundManager : Initializing the SoundManager.
    /// </summary>
    public SoundManager m_SoundManager;

	/// <summary>
	/// public Player : Initalizing the Player.
	/// </summary>
    public Player m_Player;

	/// <summary>
	/// public FadeInOut : Initializing the FadeInOut.
	/// </summary>
    public FadeInOut m_FadeInOut;

	public OtherSounds m_OtherSounds;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// private void : Used to initialise variables at the start of the script.
    /// </summary>
    void Start()
    {
		// Reference to the Animator instance.
		m_AnimatorGuard = GetComponent<Animator>();

		// Setting the default start colors.
        SetStartColors();

        // Getting NavMeshAgent.
        m_Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // Auto braking is enabled to allow a smooth stop.
        m_Agent.autoBraking = true;
        // Setting the BaseGuard's stopping distance from the target position.
        m_Agent.stoppingDistance = 1.0f;

        // If BaseGuard has no waypoints.
        if (m_WayPoints.Length == 0)
        {
            // Stop the game and send Debug LogError to tell the user that waypoints for the BaseGuard need to be set.
            Debug.LogError("No Waypoints set for Guard " + gameObject.name);
        }
        // Else if BaseGuard does have waypoints.
        else
        {
            // For each waypoint, check if it's a valid position to travel towards.
            for (int i = 0; i < m_WayPoints.Length; ++i)
            {
                // If current waypoint is equal to null (is not a valid position to travel towards).
                if (m_WayPoints[i] == null)
                {
                    // Sto[ the game and send Debug LogError to tell the user what waypoint is not a valid position.
                    Debug.LogError("Waypoin " + i + " null for guard " + gameObject.name);
                }
            }
        }
        
        // Go to next waypoint.
        GotoNextPoint();

		// Start the BaseGuard idling.
        IdleAnimation();
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public void : Moves the BaseGuard to its next waypoint.
    /// </summary>
    public void GotoNextPoint()
    {
		// Play walk animation.
        WalkAnimation();

		// Turn of the last light.
        m_LastLight.SetActive(false);

        // If no waypoints have been setup.
        if (m_WayPoints.Length == 0)
        {
            // Exit the function.
            return;
        }
        
        // Setting BaseGuard's destination to be the next waypoint.
        m_Agent.destination = m_WayPoints[m_iDestinationPoint].position;
        
        // Setting BaseGuard to look at it's destination (the next waypoint).
        transform.LookAt(m_Agent.destination);

        // Setting BaseGuard to iterate through it's waypoints, selects each one and progresses towards it.
        m_iDestinationPoint = (m_iDestinationPoint + 1) % m_WayPoints.Length;

		// Start the BaseGuard's light offset.
        if (m_Lighting)
		{
			StartCoroutine(LightingOffset());
		}
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public void : Makes BaseGuard look at it's next desination.
    /// </summary>
    public void LookAtNextPoint()
    {
        // Once it reaches it's current destination it will look at it's next one (so it's not looking at a wall).
        transform.LookAt(m_WayPoints[m_iDestinationPoint], Vector3.up);
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public void : Makes BaseGuard attempt to detect the Player.
    /// </summary>
    public void Detect()
    {
		// Turn on the last light.
        m_LastLight.SetActive(true);

		// Play idle animation.
        IdleAnimation();

		// Stop the BaseGuard's light offset.
        StopCoroutine(LightingOffset());

        // Makes BaseGuard look at it's next destination.
        LookAtNextPoint();

        // Creating temporary RaycastHit.
        RaycastHit m_Hit;
        // If a RayCast is sent out forwards from BaseGuard's Eyes and hit's a box collider with the tag Player (Player has been detected causing scene restart).
        if (Physics.Raycast(m_Eyes.transform.position, m_Eyes.transform.forward, out m_Hit, m_fSightRange) && m_Hit.collider.CompareTag("Player"))
        {
			// Set player to be detected.
            m_Player.m_bPlayerIsDetected = true;
			// Play player's detect animation.
            m_Player.DetectAnimation();
			// Play BaseGuard's detect animation.
            DetectAnimation();

            // If the Player has already been detected and the scene restart has already begun.
            if (bIsTransitioning)
            {
                // Exit the funcion.
                return;
            }

			// Set the detect colors.
            SetDetectColors();

            // Play the detection sound.
            m_OtherSounds.DetectionSound();
            // Start fading out of the scene.

            // Setting the active scene as the current scene.
            m_strCurrentScene = SceneManager.GetActiveScene().name;
            // Load the current scene created above and fade into it.
            StartCoroutine(PlayerIsDetected(m_strCurrentScene));
        }

		// If player's position is the game position as the BaseGuard's.
        if ((m_Player.transform.position - transform.position).sqrMagnitude < 1)
        {
			// Set player to be detected.
            m_Player.m_bPlayerIsDetected = true;
			// Play player's detect animation.
			m_Player.DetectAnimation();
			// Play BaseGuard's detect animation.
			DetectAnimation();

			// Set the detect colors.
            SetDetectColors();

			// Play the detection sound.
			m_OtherSounds.DetectionSound();
            // Start fading out of the scene.

            // Setting the active scene as the current scene.
            m_strCurrentScene = SceneManager.GetActiveScene().name;
            // Load the current scene created above and fade into it.
            StartCoroutine(PlayerIsDetected(m_strCurrentScene));
        }
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// private void : Set blue color.
	/// </summary>
    private void SetBlueColor()
    {
        m_Color.r = 0.1323529f;
        m_Color.g = 3.0f;
        m_Color.b = 3.0f;
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void SetStartColors()
    {
		SetBlueColor();
		m_BaseGuardRenderer.sharedMaterial.SetColor("_EmissionColor", m_Color);
		m_ExitSign.sharedMaterial.SetColor("_EmissionColor", m_Color);

		for (int iCount = 0; iCount < m_Lights.Length; ++iCount)
		{
		    m_Lights[iCount].color = m_Color;
		}
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// private void : Is called when the player is detected. Sets all the emission colors to red.
	/// </summary>
    public void SetDetectColors()
	{ 
        m_BaseGuardRenderer.sharedMaterial.SetColor("_EmissionColor", Color.red);
        m_ExitSign.sharedMaterial.SetColor("_EmissionColor", Color.red);

        for (int iCount = 0; iCount < m_WallRenderer.Length; ++iCount)
        {
            m_Material = m_WallRenderer[iCount].material;
            m_Material.SetColor("_EmissionColor", Color.red);
        }

        for (int iCount = 0; iCount < m_Lights.Length; ++iCount)
        {
            m_Lights[iCount].color = Color.red;
        }
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// private void : BaseGuard's idle animation.
	/// </summary>
    private void IdleAnimation()
    {
        m_AnimatorGuard.SetBool("Idle", true);
        m_AnimatorGuard.SetBool("Walking", false);
        m_AnimatorGuard.SetBool("Detect", false);
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// private void : BaseGuard's walk animation.
	/// </summary>
    private void WalkAnimation()
    {
        m_AnimatorGuard.SetBool("Idle", false);
        m_AnimatorGuard.SetBool("Walking", true);
        m_AnimatorGuard.SetBool("Detect", false);
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	///  private void : BaseGuard's detect animation.
	/// </summary>
    private void DetectAnimation()
    {
        m_AnimatorGuard.SetBool("Idle", false);
        m_AnimatorGuard.SetBool("Walking", false);
        m_AnimatorGuard.SetBool("Detect", true);
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// private IEnumerator : BaseGuard's lighting offset.
	/// </summary>
	/// <returns></returns>
    IEnumerator LightingOffset()
    {
        bool bRunning = true;
        while (bRunning)
        {
            m_Lighting.transform.position = m_Agent.destination;
            yield return new WaitForEndOfFrame();
        }
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// private IEnumerator : Controls the fade in/out when the Player is detected and the scene restart is triggered.
    /// </summary>
    /// <param name="a_CurrentScene"></param>
    /// <param name="a_fFadeTime"></param>
    /// <returns></returns>
    IEnumerator PlayerIsDetected(string a_strCurrentScene)
    {
        bIsTransitioning = true;
        yield return new WaitForSeconds(1.0f);
        m_FadeInOut.EndScene(a_strCurrentScene);
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
