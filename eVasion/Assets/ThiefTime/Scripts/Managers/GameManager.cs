//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//  Author:         Taliesin Millhouse & Emma Cameron
//  Date Created:   15th October 2016
//  Brief:          GameManager Class Managers Player & Enemy classes as well as turn based movment.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// public bool : Checks whether certain functionality is available for testing (Unlimited Dash, skipping to any scene at any time).
    /// </summary>
    public bool m_bIsDeveloperModeActive = false;
    
    /// <summary>
    /// public bool : Checks whether the game is paused or not.
    /// </summary>
    public bool m_bIsPaused = false;
    
    /// <summary>
    /// public Player : Initializing the Player.
    /// </summary>
    public Player m_Player;

    /// <summary>
    /// public BaseGuard[] : Initializing the array of BaseGuards.
    /// </summary>
    public BaseGuard[] m_BaseGuard;

	public BaseGuard_90[] m_BaseGuard_90;

    /// <summary>
    /// public GameObject : Initializing the PauseMenu.
    /// </summary>
    public GameObject m_PauseMenu;

    public GameObject m_DashPromt;

    public Image[] m_UIImages;

    public Image m_FadeBackground;

    public SoundManager m_SoundManager;

	public OtherSounds m_OtherSounds;

    public bool m_bSoundIsMuted;

	public float m_fTimer = 0.0f;

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// void Awake: Is run once at the start of when the script is first called (will only run once).
    /// </summary>
    void Awake()
    {
        // Setting the Pause Menu to not be active (visable) by default.
        m_PauseMenu.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Level_06")
        {
            //StartCoroutine("DashPrompt");
        }

        m_Player.m_bDashIsAvailiable = true;
        m_Player.m_fMoveDistance = 2.0f;

        if ((SceneManager.GetActiveScene().name == "Level_06") ||
            (SceneManager.GetActiveScene().name == "Level_07") ||
            (SceneManager.GetActiveScene().name == "Level_08"))
        {
            // Base Portrait.
            m_UIImages[0].gameObject.SetActive(false);
            // Base Dash or Dash Not Availiable/Used.
            m_UIImages[1].gameObject.SetActive(true);
            // Dash Availiable.
            m_UIImages[2].gameObject.SetActive(true);
            // Dash Primed.
            m_UIImages[3].gameObject.SetActive(false);
        }
        else
        {
            // Base Portrait.
            m_UIImages[0].gameObject.SetActive(false);
            // Base Dash or Dash Not Availiable/Used.
            m_UIImages[1].gameObject.SetActive(false);
            // Dash Availiable.
            m_UIImages[2].gameObject.SetActive(false);
            // Dash Primed.
            m_UIImages[3].gameObject.SetActive(false);
        }
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// void Update : Is called once per frame.
    /// </summary>
    void Update ()
    {
        //Any functions that should only be enabled when developing the game.
        DeveloperFunctions();

		m_fTimer += Time.deltaTime;
		
		//m_SoundManager.BackgroundMusic();
		//
		//if (SceneManager.GetActiveScene().name == "MainMenu")
		//{
		//	return;
		//}

		// If Escape key is pressed. The Pause Menu is set to be active.
		if (Input.GetKeyDown(KeyCode.Escape) && m_FadeBackground.color.a == 0.0f)
        {
            m_PauseMenu.SetActive(!m_PauseMenu.activeInHierarchy);
        }

        // If R is pressed. Reset Scene.
        if (Input.GetKey(KeyCode.R))
        {
            RestartScene();
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
			//m_OtherSounds.SoundOnOffToggle();
        }

        // If the Pause Menu is active.
        if (m_PauseMenu.activeInHierarchy)
        {
            // Set TimeScale to equal zero (stop time within the game).
            Time.timeScale = 0;
            // Return used to exit the update loop.
            return;
        }

        // If the Pause Menu is not active, set TimeScale to equal one (time at it's normal rate).
        Time.timeScale = 1;

        // Set the player's speed.
        SetPlayerSpeed();

        // If player is not moving.
        if (!m_Player.m_bIsMoving && !m_Player.m_bPlayerIsDetected && m_fTimer > 0.9f)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if ((SceneManager.GetActiveScene().name == "Level_004") ||
                    (SceneManager.GetActiveScene().name == "Level_005") ||
                    (SceneManager.GetActiveScene().name == "Level_06") ||
                    (SceneManager.GetActiveScene().name == "Level_07") ||
                    (SceneManager.GetActiveScene().name == "Level_08"))
                {
                    DashToggle();
                }

            }

            if (m_Player.m_bMoveComplete)
            {
                if (!m_Player.m_bDashIsAvailiable)
                {
                    DashUsed();
                    m_Player.m_bDashUsed = true;

                }
            }

            // If W key is pressed.
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                // Player moves up.
                m_Player.MoveUp();

                if (m_Player.m_fMoveDistance == 4.0f)
                {
                    m_Player.m_bDashIsAvailiable = false;
                }
				m_fTimer = 0.0f;
			}

            // If A key is pressed.
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                // Player moves left.
                m_Player.MoveLeft();

                if (m_Player.m_fMoveDistance == 4.0f)
                {
                    m_Player.m_bDashIsAvailiable = false;
                }
				m_fTimer = 0.0f;
            }

            // If S key is pressed.
            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                // Player moves down.
                m_Player.MoveDown();

                if (m_Player.m_fMoveDistance == 4.0f)
                {
                    m_Player.m_bDashIsAvailiable = false;
                }
				m_fTimer = 0.0f;
			}

            // If D key is pressed.
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                // Player moves right.
                m_Player.MoveRight();

                if (m_Player.m_fMoveDistance == 4.0f)
                {
                    m_Player.m_bDashIsAvailiable = false;
                }
				m_fTimer = 0.0f;
			}
        }

        // Checks if the player is detected by any enemies.
        EnemyDetect();

		if (m_Player.m_bPlayerIsDetected)
		{
			for (int iCount =0; iCount < m_BaseGuard.Length; ++iCount)
			{
				m_BaseGuard[iCount].SetDetectColors();
			}
			for (int iCount = 0; iCount < m_BaseGuard_90.Length; ++iCount)
			{
				m_BaseGuard_90[iCount].SetDetectColors();
			}
		}
    }

    public void RestartScene()
    {
        Scene m_CurrentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(m_CurrentScene.name);
    }

    public void DashToggle()
    {
        m_Player.m_bMoveComplete = false;

        // If Dash had not been used.
        if (!m_Player.m_bDashUsed)
        {
            // If Dash is availiable and the Player is not Dashing on its next move. Turn Dash on.
            if (m_Player.m_bDashIsAvailiable && !m_Player.m_bDashNextMove)
            {
                m_UIImages[2].gameObject.SetActive(false);
                m_UIImages[3].gameObject.SetActive(true);
                m_Player.DashOn();
            }
            // If Dash is availiable and the Player is Dashing on its next move. Turn Dash off.
            else if (m_Player.m_bDashIsAvailiable && m_Player.m_bDashNextMove)
            {
                m_UIImages[2].gameObject.SetActive(true);
                m_UIImages[3].gameObject.SetActive(false);
                m_Player.DashOff();
            }
        }
        // If Dash is availiable and if Dash has been used.
        if (m_Player.m_bDashIsAvailiable && m_Player.m_bDashUsed)
        {
            m_UIImages[2].gameObject.SetActive(false);
            m_UIImages[3].gameObject.SetActive(false);
            m_Player.DashOff();
        }
    }

    public void DashUsed()
    {
        m_UIImages[2].gameObject.SetActive(false);
        m_UIImages[3].gameObject.SetActive(false);
        m_Player.DashOff();
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public void : Sets the player's speed.
    /// </summary>
    public void SetPlayerSpeed()
    {
        // If player is moving and player's position is equal to it's end position. (sqrMagnitude < 0.005f is use to check if the player is close enough).
        if (m_Player.m_bIsMoving && (m_Player.transform.position - m_Player.m_v3TargetPosition).sqrMagnitude < 0.005f)
        {
            // Set player's position to be player's target position.
            m_Player.transform.position = m_Player.m_v3TargetPosition;
            // Player is not moving.
            m_Player.m_bIsMoving = false;
            m_Player.MoveComplete();
        }

		float m_fSpeed = 0;

		if (m_BaseGuard.Length > 0)
		{
			// Setting speed variable that is equal to the guards magnitude multipied by deta time (the guards speed).
			m_fSpeed = m_BaseGuard[0].GetComponent<UnityEngine.AI.NavMeshAgent>().velocity.magnitude * Time.deltaTime;
		}
		else if (m_BaseGuard_90.Length > 0)
		{
			// Setting speed variable that is equal to the guards magnitude multipied by deta time (the guards speed).
			m_fSpeed = m_BaseGuard_90[0].GetComponent<UnityEngine.AI.NavMeshAgent>().velocity.magnitude * Time.deltaTime;
		}

        // If speed is equal to zero.
        if (m_fSpeed == 0)
        {
            // Make speed a really low number. This is to speed up the player after the game has been paused and resumed.
            m_fSpeed = 0.005f;
        }

        // If player's move distance is equal to two.
        if (m_Player.m_fMoveDistance == 2)
        {
            // Setting player to move from it's position to the target position at the guard's magnitude multiplied by delta time.
            m_Player.transform.position = Vector3.MoveTowards(m_Player.transform.position, m_Player.m_v3TargetPosition, m_fSpeed);
        }
        // Else if player's move distance is equal to four.
        else if (m_Player.m_fMoveDistance == 4)
        {
            // Setting player to move from it's position to the target position at the guard's magnitude multiplied by delta time multipled by two (so the player dashes).
            m_Player.transform.position = Vector3.MoveTowards(m_Player.transform.position, m_Player.m_v3TargetPosition, m_fSpeed * 2);

            m_Player.m_bMoveComplete = true;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void EnemyMovement()
    {
        // Switch statement to check which level is currently loaded.
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level_01":
                m_BaseGuard[0].GotoNextPoint();
                break;

            case "Level_02":
                m_BaseGuard[0].GotoNextPoint();
                break;

            case "Level_03":
                m_BaseGuard[0].GotoNextPoint();
                break;

            case "Level_04":
                m_BaseGuard_90[0].GotoNextPoint();
                break;

            case "Level_05":
                m_BaseGuard[0].GotoNextPoint();
                m_BaseGuard_90[0].GotoNextPoint();
                break;

            case "Level_06":
                m_BaseGuard[0].GotoNextPoint();
                break;

            case "Level_07":
                m_BaseGuard[0].GotoNextPoint();
                m_BaseGuard[1].GotoNextPoint();
                m_BaseGuard_90[0].GotoNextPoint();
                break;

            case "Level_08":
                m_BaseGuard[0].GotoNextPoint();
                m_BaseGuard[1].GotoNextPoint();
                m_BaseGuard[2].GotoNextPoint();
                break;

            case "Level_09":
                m_BaseGuard_90[0].GotoNextPoint();
                m_BaseGuard_90[1].GotoNextPoint();
                break;

            // Default: Send Debug Log to inform that the Default Option was Triggered.
            default:
                Debug.Log("GameManager EnemySpawn Switch Statement Default Option Triggered (Something is wrong!).");
                break;
        }
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public void : Checks what level is currently loaded and then performs detection accordingly.
    /// </summary>
    public void EnemyDetect()
    {
        if (!m_Player.m_bIsMoving)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Level_01":
                    m_BaseGuard[0].Detect();
                    break;

                case "Level_02":
                    m_BaseGuard[0].Detect();
                    break;

                case "Level_03":
                    m_BaseGuard[0].Detect();
                    break;

                case "Level_04":
                    m_BaseGuard_90[0].Detect();
                    break;

                case "Level_05":
                    m_BaseGuard[0].Detect();
					m_BaseGuard_90[0].Detect();
					break;

                case "Level_06":
                    m_BaseGuard[0].Detect();
                    break;

                case "Level_07":
                    m_BaseGuard[0].Detect();
                    m_BaseGuard[1].Detect();
                    m_BaseGuard_90[0].Detect();
                    break;

                case "Level_08":
                    m_BaseGuard[0].Detect();
                    m_BaseGuard[1].Detect();
                    m_BaseGuard[2].Detect();
                    break;

                case "Level_09":
                    m_BaseGuard_90[0].Detect();
                    m_BaseGuard_90[1].Detect();
                    break;

                // Default: Send Debug Log to inform that the Default Option was Triggered.
                default:
                    Debug.Log("GameManager EnemyDetect Switch Statement default Option Triggered (Something is wrong!).");
                    break;
            }
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Please note: All functions below this line are developer functions and only will be used while the game is being developed.
    //              They do not need to be deleted or changed, but will have no impact on the game on release.
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public void : Any functions that should only be enabled when developing the game.
    /// </summary>
    public void DeveloperFunctions()
    {
        // (If Developer Mode is Active.
        if (m_bIsDeveloperModeActive)
        {
            m_Player.m_bDashIsAvailiable = true;

            // Allows the user to change to any scene at any time.
            SceneChange();
        }
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// public void : Enables quick scene change when needed.
    /// </summary>
    public void SceneChange()
    {
        // If F1 is pressed. Load Scene One.
        if (Input.GetKey(KeyCode.F1))
        {
            SceneManager.LoadScene("Level_01");
        }

        // If F2 is pressed. Load Scene Two.
        if (Input.GetKey(KeyCode.F2))
        {
            SceneManager.LoadScene("Level_02");
        }

        // If F3 is pressed. Load Scene Three.
        if (Input.GetKey(KeyCode.F3))
        {
            SceneManager.LoadScene("Level_03");
        }

        // If F4 is pressed. Load Scnee Four.
        if (Input.GetKey(KeyCode.F4))
        {
            SceneManager.LoadScene("Level_04");
        }

        // If F5 is pressed. Load Scene Five.
        if (Input.GetKey(KeyCode.F5))
        {
            SceneManager.LoadScene("Level_05");
        }

        // If F5 is pressed. Load Scene Five.
        if (Input.GetKey(KeyCode.F6))
        {
            SceneManager.LoadScene("Level_06");
        }

        // If F5 is pressed. Load Scene Five.
        if (Input.GetKey(KeyCode.F7))
        {
            SceneManager.LoadScene("Level_07");
        }

        // If F5 is pressed. Load Scene Five.
        if (Input.GetKey(KeyCode.F8))
        {
            SceneManager.LoadScene("Level_08");
        }

        // If F5 is pressed. Load Scene Five.
        if (Input.GetKey(KeyCode.F9))
        {
            SceneManager.LoadScene("Level_09");
        }

        // If P is pressed. Quit Game.
        if (Input.GetKeyDown(KeyCode.P))
        {
            Application.Quit();
        }
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
