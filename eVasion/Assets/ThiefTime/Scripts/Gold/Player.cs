//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//  Author:         Taliesin Millhouse & Emma Cameron
//  Date Created:   22nd September 2016
//  Brief:          Player Class Controls Games Main Character.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

public class Player : MonoBehaviour
{
    /// <summary>
    /// public int : The distance the player will move in a turn.
    /// </summary>
    public float m_fMoveDistance = 2.0f;
    
    /// <summary>
    /// public bool : A check to see if the player is moving.
    /// </summary>
    public bool m_bIsMoving = false;

    /// <summary>
    /// public bool : A check to see if the player's dash ability is availiable.
    /// </summary>
    public bool m_bDashIsAvailiable = true;

    /// <summary>
    /// public bool : A check to see if the player's next move is a dash.
    /// </summary>
    public bool m_bDashNextMove = false;

	/// <summary>
	/// public bool : A check to see if the player's move is complete.
	/// </summary>
	public bool m_bMoveComplete = true;

	/// <summary>
	/// public bool : A check to see if the player's dash has been used.
	/// </summary>
	public bool m_bDashUsed = false;

	/// <summary>
	/// public bool : A check to see if the player is detected.
	/// </summary>
    public bool m_bPlayerIsDetected = false;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// public Vector3 : Stores the player's end position to move towards.
	/// </summary>
	public Vector3 m_v3TargetPosition;

    /// <summary>
    /// public Vector3 : Stores a temporary Vector3 for calculating whether the player's move is valid.
    /// </summary>
    public Vector3 m_v3Temporary;

    /// <summary>
    /// public Vector3 : Stores a temporary Vector3 for calculating whether the player's dash move is valid.
    /// </summary>
    public Vector3 m_v3DashTemorary;

	/// <summary>
	/// public AnimationManager : Initializing the AnimationManager for PLAYER
	/// </summary>
	public Animator m_AnimatorPlayer;

	//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// public GameManger: Initializing an instance of the GameManager.
	/// </summary>
	public GameManager m_GameManager;

    /// <summary>
    /// public SoundManager : Initializing the SoundManager.
    /// </summary>
    public SoundManager m_SoundManager;

	public OtherSounds m_OtherSounds;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// private void : Used to initialise variables at the start of the script.
    /// </summary>
    void Start()
    {
        // Reference to the Animator instance.
        m_AnimatorPlayer = GetComponent<Animator>();

		// Start the player idling.
		IdleAnimation();
        
		// By default the player's target position is it's position.
        m_v3TargetPosition = transform.position;
	}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public void : Sets the forward end position.
    /// </summary>
    public void MoveUp()
    {
        // Creating new Vector3 position infront of the player at the correct Move Distance.
        m_v3Temporary = transform.position + Vector3.forward * m_fMoveDistance;
        // Creating new Vector3 position infront of the player at the correct Dash Distance.
        m_v3DashTemorary = transform.position + Vector3.forward * (m_fMoveDistance / 2);

        // Creating new NavMeshHit.
        UnityEngine.AI.NavMeshHit m_NavMeshHit = new UnityEngine.AI.NavMeshHit();

        if (UnityEngine.AI.NavMesh.SamplePosition(m_v3DashTemorary, out m_NavMeshHit, 1.3f, 1 << UnityEngine.AI.NavMesh.GetAreaFromName("Walkable")))
        {
            //If the NavMeshHit(taken fron the Temporary Vector3 created above) is inside the Walkable NavMesh perform the player move.
            if (UnityEngine.AI.NavMesh.SamplePosition(m_v3Temporary, out m_NavMeshHit, 1.3f, 1 << UnityEngine.AI.NavMesh.GetAreaFromName("Walkable")))
            {
                // Player is moving.
                m_bIsMoving = true;

                // Setting the Target Positio infront of the player at the correct Move Distance.
                m_v3TargetPosition = transform.position + Vector3.forward * m_fMoveDistance;

                // Setting the Relative Position. (The direction the player should be looking).
                Vector3 m_v3RelativePosition = m_v3TargetPosition - transform.position;
                // Creating Quaternion Rotation to look at the Relative Positon.
                Quaternion m_Rotation = Quaternion.LookRotation(m_v3RelativePosition);
                // Performing rotation at the calculated Quaternion Rotation.
                transform.rotation = m_Rotation;

                // Performing enemy movement.
                m_GameManager.EnemyMovement();

				// If player's move distance is not equal to 4. Play walk animation.
				if (m_fMoveDistance != 4.0f)
				{
					WalkAnimation();
				}
				// Else play dash animation.
				else
				{
					DashAnimation();
				}

				// Player & Enemies movement sound plays.
				m_OtherSounds.MovementSounds();
            }
            else if ((SceneManager.GetActiveScene().name == "Level_06") ||
                     (SceneManager.GetActiveScene().name == "Level_07") ||
                     (SceneManager.GetActiveScene().name == "Level_08"))
            {
                if (!m_bDashUsed)
                {
                    // Base Portrait.
                    m_GameManager.m_UIImages[0].gameObject.SetActive(false);
                    // Base Dash or Dash Not Availiable/Used.
                    m_GameManager.m_UIImages[1].gameObject.SetActive(true);
                    // Dash Availiable.
                    m_GameManager.m_UIImages[2].gameObject.SetActive(true);
                    // Dash Primed.
                    m_GameManager.m_UIImages[3].gameObject.SetActive(false);

                    DashNotValid();
                }
            }
        }
        else if ((SceneManager.GetActiveScene().name == "Level_06") ||
                 (SceneManager.GetActiveScene().name == "Level_07") ||
                 (SceneManager.GetActiveScene().name == "Level_08"))
        {
            if (!m_bDashUsed)
            {
                // Base Portrait.
                m_GameManager.m_UIImages[0].gameObject.SetActive(false);
                // Base Dash or Dash Not Availiable/Used.
                m_GameManager.m_UIImages[1].gameObject.SetActive(true);
                // Dash Availiable.
                m_GameManager.m_UIImages[2].gameObject.SetActive(true);
                // Dash Primed.
                m_GameManager.m_UIImages[3].gameObject.SetActive(false);

                DashNotValid();
            }
        }
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// public void : Sets the left end position.
        /// </summary>
    public void MoveLeft()
    {
        // Creating new Vector3 position infront of the player at the correct Move Distance.
        m_v3Temporary = transform.position - Vector3.right * m_fMoveDistance;
        // Creating new Vector3 position infront of the player at the correct Dash Distance.
        m_v3DashTemorary = transform.position - Vector3.right * (m_fMoveDistance / 2);

        // Creating new NavMeshHit.
        UnityEngine.AI.NavMeshHit m_NavMeshHit = new UnityEngine.AI.NavMeshHit();

        if (UnityEngine.AI.NavMesh.SamplePosition(m_v3DashTemorary, out m_NavMeshHit, 1.3f, 1 << UnityEngine.AI.NavMesh.GetAreaFromName("Walkable")))
        {
            // If the NavMeshHit (taken fron the Temporary Vector3 created above) is inside the Walkable NavMesh perform the player move.
            if (UnityEngine.AI.NavMesh.SamplePosition(m_v3Temporary, out m_NavMeshHit, 1.3f, 1 << UnityEngine.AI.NavMesh.GetAreaFromName("Walkable")))
            {
                // Player is moving.
                m_bIsMoving = true;

                // Setting the Target Positio infront of the player at the correct Move Distance.
                m_v3TargetPosition = transform.position - Vector3.right * m_fMoveDistance;

                // Setting the Relative Position. (The direction the player should be looking).
                Vector3 m_v3RelativePosition = m_v3TargetPosition - transform.position;
                // Creating Quaternion Rotation to look at the Relative Positon.
                Quaternion m_Rotation = Quaternion.LookRotation(m_v3RelativePosition);
                // Performing rotation at the calculated Quaternion Rotation.
                transform.rotation = m_Rotation;

                // Performing enemy movement.
                m_GameManager.EnemyMovement();

				// If player's move distance is not equal to 4. Play walk animation.
				if (m_fMoveDistance != 4.0f)
				{
					WalkAnimation();
				}
				// Else play dash animation.
				else
				{
					DashAnimation();
				}

				// Player & Enemies movement sound plays.
				m_OtherSounds.MovementSounds();
            }
            else if ((SceneManager.GetActiveScene().name == "Level_06") ||
                     (SceneManager.GetActiveScene().name == "Level_07") ||
                     (SceneManager.GetActiveScene().name == "Level_08"))
            {
                if (!m_bDashUsed)
                {
                    // Base Portrait.
                    m_GameManager.m_UIImages[0].gameObject.SetActive(false);
                    // Base Dash or Dash Not Availiable/Used.
                    m_GameManager.m_UIImages[1].gameObject.SetActive(true);
                    // Dash Availiable.
                    m_GameManager.m_UIImages[2].gameObject.SetActive(true);
                    // Dash Primed.
                    m_GameManager.m_UIImages[3].gameObject.SetActive(false);

                    DashNotValid();
                }
            }
        }
        else if ((SceneManager.GetActiveScene().name == "Level_06") ||
                 (SceneManager.GetActiveScene().name == "Level_07") ||
                 (SceneManager.GetActiveScene().name == "Level_08"))
        {
            if (!m_bDashUsed)
            {
                // Base Portrait.
                m_GameManager.m_UIImages[0].gameObject.SetActive(false);
                // Base Dash or Dash Not Availiable/Used.
                m_GameManager.m_UIImages[1].gameObject.SetActive(true);
                // Dash Availiable.
                m_GameManager.m_UIImages[2].gameObject.SetActive(true);
                // Dash Primed.
                m_GameManager.m_UIImages[3].gameObject.SetActive(false);

                DashNotValid();
            }
        }
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public void : Sets the backward end position.
    /// </summary>
    public void MoveDown()
    {
        // Creating new Vector3 position infront of the player at the correct Move Distance.
        m_v3Temporary = transform.position - Vector3.forward * m_fMoveDistance;
        // Creating new Vector3 position infront of the player at the correct Dash Distance.
        m_v3DashTemorary = transform.position - Vector3.forward * (m_fMoveDistance / 2);

        // Creating new NavMeshHit.
        UnityEngine.AI.NavMeshHit m_NavMeshHit = new UnityEngine.AI.NavMeshHit();

        if (UnityEngine.AI.NavMesh.SamplePosition(m_v3DashTemorary, out m_NavMeshHit, 1.3f, 1 << UnityEngine.AI.NavMesh.GetAreaFromName("Walkable")))
        {
            // If the NavMeshHit (taken fron the Temporary Vector3 created above) is inside the Walkable NavMesh perform the player move.
            if (UnityEngine.AI.NavMesh.SamplePosition(m_v3Temporary, out m_NavMeshHit, 1.3f, 1 << UnityEngine.AI.NavMesh.GetAreaFromName("Walkable")))
            {
                // Player is moving.
                m_bIsMoving = true;

                // Setting the Target Positio infront of the player at the correct Move Distance.
                m_v3TargetPosition = transform.position - Vector3.forward * m_fMoveDistance;

                // Setting the Relative Position. (The direction the player should be looking).
                Vector3 m_v3RelativePosition = m_v3TargetPosition - transform.position;
                // Creating Quaternion Rotation to look at the Relative Positon.
                Quaternion m_Rotation = Quaternion.LookRotation(m_v3RelativePosition);
                // Performing rotation at the calculated Quaternion Rotation.
                transform.rotation = m_Rotation;

                // Performing enemy movement.
                m_GameManager.EnemyMovement();

				// If player's move distance is not equal to 4. Play walk animation.
				if (m_fMoveDistance != 4.0f)
				{
					WalkAnimation();
				}
				// Else play dash animation.
				else
				{
					DashAnimation();
				}

				// Player & Enemies movement sound plays.
				m_OtherSounds.MovementSounds();
            }
            else if ((SceneManager.GetActiveScene().name == "Level_06") ||
                     (SceneManager.GetActiveScene().name == "Level_07") ||
                     (SceneManager.GetActiveScene().name == "Level_08"))
            {
                if (!m_bDashUsed)
                {
                    // Base Portrait.
                    m_GameManager.m_UIImages[0].gameObject.SetActive(false);
                    // Base Dash or Dash Not Availiable/Used.
                    m_GameManager.m_UIImages[1].gameObject.SetActive(true);
                    // Dash Availiable.
                    m_GameManager.m_UIImages[2].gameObject.SetActive(true);
                    // Dash Primed.
                    m_GameManager.m_UIImages[3].gameObject.SetActive(false);

                    DashNotValid();
                }
            }
        }
        else if ((SceneManager.GetActiveScene().name == "Level_06") ||
                 (SceneManager.GetActiveScene().name == "Level_07") ||
                 (SceneManager.GetActiveScene().name == "Level_08"))
        {
            if (!m_bDashUsed)
            {
                // Base Portrait.
                m_GameManager.m_UIImages[0].gameObject.SetActive(false);
                // Base Dash or Dash Not Availiable/Used.
                m_GameManager.m_UIImages[1].gameObject.SetActive(true);
                // Dash Availiable.
                m_GameManager.m_UIImages[2].gameObject.SetActive(true);
                // Dash Primed.
                m_GameManager.m_UIImages[3].gameObject.SetActive(false);

                DashNotValid();
            }
        }
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public void : Sets the right end position.
    /// </summary>
    public void MoveRight()
    {

        // Creating new Vector3 position infront of the player at the correct Move Distance.
        m_v3Temporary = transform.position + Vector3.right * m_fMoveDistance;
        // Creating new Vector3 position infront of the player at the correct Dash Distance.
        m_v3DashTemorary = transform.position + Vector3.right * (m_fMoveDistance / 2);

        // Creating new NavMeshHit.
        UnityEngine.AI.NavMeshHit m_NavMeshHit = new UnityEngine.AI.NavMeshHit();

        if(UnityEngine.AI.NavMesh.SamplePosition(m_v3DashTemorary, out m_NavMeshHit, 1.3f, 1 << UnityEngine.AI.NavMesh.GetAreaFromName("Walkable")))
        {
            // If the NavMeshHit (taken fron the Temporary Vector3 created above) is inside the Walkable NavMesh perform the player move.
            if (UnityEngine.AI.NavMesh.SamplePosition(m_v3Temporary, out m_NavMeshHit, 1.3f, 1 << UnityEngine.AI.NavMesh.GetAreaFromName("Walkable")))
            {
                // Player is moving.
                m_bIsMoving = true;

                // Setting the Target Positio infront of the player at the correct Move Distance.
                m_v3TargetPosition = transform.position + Vector3.right * m_fMoveDistance;

                // Setting the Relative Position. (The direction the player should be looking).
                Vector3 m_v3RelativePosition = m_v3TargetPosition - transform.position;
                // Creating Quaternion Rotation to look at the Relative Positon.
                Quaternion m_Rotation = Quaternion.LookRotation(m_v3RelativePosition);
                // Performing rotation at the calculated Quaternion Rotation.
                transform.rotation = m_Rotation;

                // Performing enemy movement.
                m_GameManager.EnemyMovement();

				// If player's move distance is not equal to 4. Play walk animation.
				if (m_fMoveDistance != 4.0f)
				{
					WalkAnimation();
				}
				// Else play dash animation.
				else
				{
					DashAnimation();
				}

				// Player & Enemies movement sound plays.
				m_OtherSounds.MovementSounds();
            }
            else if ((SceneManager.GetActiveScene().name == "Level_06") ||
                     (SceneManager.GetActiveScene().name == "Level_07") ||
                     (SceneManager.GetActiveScene().name == "Level_08"))
            {
                if (!m_bDashUsed)
                {
                    // Base Portrait.
                    m_GameManager.m_UIImages[0].gameObject.SetActive(false);
                    // Base Dash or Dash Not Availiable/Used.
                    m_GameManager.m_UIImages[1].gameObject.SetActive(true);
                    // Dash Availiable.
                    m_GameManager.m_UIImages[2].gameObject.SetActive(true);
                    // Dash Primed.
                    m_GameManager.m_UIImages[3].gameObject.SetActive(false);

                    DashNotValid();
                }
            }
        }
        else if ((SceneManager.GetActiveScene().name == "Level_06") ||
                 (SceneManager.GetActiveScene().name == "Level_07") ||
                 (SceneManager.GetActiveScene().name == "Level_08"))
        {
            if (!m_bDashUsed)
            {
                // Base Portrait.
                m_GameManager.m_UIImages[0].gameObject.SetActive(false);
                // Base Dash or Dash Not Availiable/Used.
                m_GameManager.m_UIImages[1].gameObject.SetActive(true);
                // Dash Availiable.
                m_GameManager.m_UIImages[2].gameObject.SetActive(true);
                // Dash Primed.
                m_GameManager.m_UIImages[3].gameObject.SetActive(false);

                DashNotValid();
            }
        }
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public void : Dash is called so the player can move twice as far in one turn.
    /// </summary>
    public void DashOn()
    {
        m_fMoveDistance = 4.0f;
        m_bDashNextMove = true;
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// public void : Dash is not called so the player will move its normal turn.
	/// </summary>
	public void DashOff()
    {
        m_fMoveDistance = 2.0f;
        m_bDashNextMove = false;

    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// public void : Dash has been used and the player cannot use dash again.
	/// </summary>
	public void DashCompleted()
    {
        m_fMoveDistance = 2.0f;
        m_bDashIsAvailiable = false;
        m_bDashNextMove = false;
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// public void : The dash the player has tried to do is not valid and is not counted.
	/// </summary>
	public void DashNotValid()
    {
        if (m_bDashUsed)
        {
            m_fMoveDistance = 2.0f;
            m_bDashNextMove = false;
            m_bDashIsAvailiable = false;
        }
        else
        {
            m_fMoveDistance = 2.0f;
            m_bDashNextMove = false;
            m_bDashIsAvailiable = true;
        }
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// public void : The player has completed its move.
	/// </summary>
	public void MoveComplete()
    {
        m_bMoveComplete = true;
        m_AnimatorPlayer.SetBool("Idle", true);
        m_AnimatorPlayer.SetBool("Walking", false);
        m_AnimatorPlayer.SetBool("Dash", false);
		m_AnimatorPlayer.SetBool("Detect", false);
	}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// public void : Player's idle animation.
	/// </summary>
	public void IdleAnimation()
    {
		m_AnimatorPlayer.SetBool("Idle", true);
		m_AnimatorPlayer.SetBool("Walking", false);
		m_AnimatorPlayer.SetBool("Dash", false);
		m_AnimatorPlayer.SetBool("Detect", false);
	}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// public void : Player's walk animation.
	/// </summary>
	public void WalkAnimation()
	{
		m_AnimatorPlayer.SetBool("Idle", false);
		m_AnimatorPlayer.SetBool("Walking", true);
		m_AnimatorPlayer.SetBool("Dash", false);
		m_AnimatorPlayer.SetBool("Detect", false);
	}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// public void : Player's dash animation.
	/// </summary>
	public void DashAnimation()
	{
		m_AnimatorPlayer.SetBool("Idle", false);
		m_AnimatorPlayer.SetBool("Walking", false);
		m_AnimatorPlayer.SetBool("Dash", true);
		m_AnimatorPlayer.SetBool("Detect", false);
	}

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// public void : Player's detect animation.
	/// </summary>
	public void DetectAnimation()
    {
        m_AnimatorPlayer.SetBool("Idle", false);
        m_AnimatorPlayer.SetBool("Walking", false);
        m_AnimatorPlayer.SetBool("Dash", false);
        m_AnimatorPlayer.SetBool("Detect", true);
    }
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}