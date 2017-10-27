//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//  Author:         Taliesin Millhouse & Emma Cameron
//  Date Created:   27th October 2016
//  Brief:          MusicManager Class Managers the Music.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

public class SoundManager : MonoBehaviour
{
	/// <summary>
	/// static bool : To check if the Background Music is playing.
	/// </summary>
	static bool m_bStartBackgroundMusic = false;

	/// <summary>
	/// public Player : Initializing the Player.
	/// </summary>
	public Player m_Player;

	/// <summary>
	/// public AudioSource : Initializing the first AudioSource: SOUNDTRACK
	/// </summary>
	public AudioSource m_AudioSourceOne;

	/// <summary>
	/// public AudioSource : Initializing the second AudioSource: KEIKO AUDIO
	/// </summary>
	public AudioSource m_AudioSourceTwo;

	/// <summary>
	/// public AudioSource : Initializing the third AudioSource: GUARD AUDIO
	/// </summary>
	public AudioSource m_AudioSourceThree;

	/// <summary>
	/// public AudioClip Array : Initializing the array of AudioClips.
	/// </summary>
	public AudioClip[] m_AudioClip;

	public bool m_bSoundIsPlaying = true;

	public float m_fTimer = 0.0f;

	//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	/// <summary>
	/// void Awake : Is run once at the start of when the script is first called (will only run once).
	/// </summary>
	void Awake()
	{
		// Setting the SoundManager to not be destroyed on the load of a new scene.
		DontDestroyOnLoad(this.gameObject);

		// If no Background Music is playing.
		if (!m_bStartBackgroundMusic)
		{
			m_AudioSourceOne.clip = m_AudioClip[6];
			m_AudioSourceOne.Play();
			m_AudioSourceOne.loop = false;
		
			// BackgroundMusic is playing.
			m_bStartBackgroundMusic = true;
		}

		//// If no Background Music is playing.
		//if (!m_bStartBackgroundMusic)
		//{
		//	m_AudioSourceOne.clip = m_AudioClip[0];
		//	m_AudioSourceOne.Play();
		//	m_AudioSourceOne.loop = true;
		//
		//	// BackgroundMusic is playing.
		//	m_bStartBackgroundMusic = true;
		//}

		// Finding GameObjects with the name SoundManager and placing them into an array.
		GameObject[] m_GameObjectArray = GameObject.FindGameObjectsWithTag("SoundManager");

		//If the GameOjectArray's length is greater than one.
		if (m_GameObjectArray.Length > 1)
		{
			// Destroy that GameObject.
			Destroy(this.gameObject);
			//for (int iCount = m_GameObjectArray.Length; iCount > 2; --iCount)
			//{
			//	Debug.Log("Hit" + iCount);
			//	Destroy(m_GameObjectArray[iCount]);
			//}

			//for (int iCount = 2; iCount < m_GameObjectArray.Length; ++iCount)
			//{
			//	Debug.Log("Hit" + iCount);
			//	if (iCount == m_GameObjectArray.Length)
			//	{
			//		Debug.Log("Lala");
			//		return;
			//	}
			//	else
			//	{
			//		Destroy(m_GameObjectArray[iCount]);
			//	}
			//}
		}

		//m_fTimer += Time.deltaTime;
		//
		//if (m_fTimer > 15.0f)
		//{
		//	BackgroundMusic();
		//}
	}

	void Update()
	{
		BackgroundMusic();
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	public void BackgroundMusic()
	{
		// Play Intro clip
		if (!m_AudioSourceOne.isPlaying)
		{
			m_AudioSourceOne.clip = m_AudioClip[0];
			m_AudioSourceOne.Play();
			m_AudioSourceOne.loop = true;
		}
	}

//    /// <summary>
//    /// public void MovementSounds : Plays the Player & Enemy movement sounds.
//    /// </summary>
//    public void MovementSounds()
//    {
//        // If Player's move distance is equal to two.
//        if (m_Player.m_fMoveDistance == 2)
//        {
//            // Play Player's move sound.
//            m_AudioSourceTwo.clip = m_AudioClip[1];
//            m_AudioSourceTwo.Play();
//        }
//        // Else if Player's move distance is equal to four.
//        else if (m_Player.m_fMoveDistance == 4)
//        {
//            // Play Player's dash sound.
//            m_AudioSourceTwo.clip = m_AudioClip[2];
//            m_AudioSourceTwo.Play();
//        }

//		if (SceneManager.GetActiveScene().name == "Level_01" || SceneManager.GetActiveScene().name == "Level_09")
//		{

//		}
//		else
//		{
//			// Play Enemy's move sound.
//			m_AudioSourceThree.clip = m_AudioClip[3];
//			m_AudioSourceThree.Play();
//		}
//    }

////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
////-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

//    /// <summary>
//    /// public void : Plays the detection sound when the player is detected.
//    /// </summary>
//    public void DetectionSound()
//    {
//        m_AudioSourceThree.clip = m_AudioClip[4];
//        m_AudioSourceThree.Play();
//    }
//    /// <summary>
//    /// public void : Plays the Level Complete sound when the player is wins/advances a level.
//    /// </summary>
//    public void LevelCompleteSound()
//	{
//		m_AudioSourceThree.clip = m_AudioClip[5];
//		m_AudioSourceThree.Play();
//	}
//    /// <summary>
//    /// public void : Plays the Game Intro sound when Game begins.
//    /// </summary>
//    public void IntroSound()
//    {
//        m_AudioSourceThree.clip = m_AudioClip[6];
//        m_AudioSourceThree.Play();
//    }

//    /// <summary>
//    /// public void : Switches Sound On/Off
//    /// </summary>
//    public void SoundOnOffToggle()
//    {
//        if (m_bSoundIsPlaying)
//        {
//            Debug.Log("SoundOff");
//            SoundOff();
//        }
//        else if (!m_bSoundIsPlaying)
//        {
//            Debug.Log("SoundOn");
//            SoundOn();
//        }
//    }
//    /// <summary>
//    /// public void : Switches Sound On, sets volume default levels
//    /// </summary>
//    public void SoundOn()
//    {
//        m_AudioSourceOne.volume = 1.0f;
//        m_AudioSourceTwo.volume = 1.0f;
//        m_AudioSourceThree.volume = 1.0f;
//        m_bSoundIsPlaying = true;
//    }
//    /// <summary>
//    /// public void : Switches Sound Off, sets volume default levels to nill.
//    /// </summary>
//    public void SoundOff()
//    {
//        m_AudioSourceOne.volume = 0.0f;
//        m_AudioSourceTwo.volume = 0.0f;
//        m_AudioSourceThree.volume = 0.0f;
//        m_bSoundIsPlaying = false;
//    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}