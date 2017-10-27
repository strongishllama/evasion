using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OtherSounds : MonoBehaviour
{
	public AudioSource m_AudioSourceTwo;
	public AudioSource m_AudioSourceThree;

	public AudioClip[] m_AudioClips;

	public Player m_Player;

	/// <summary>
	/// public void MovementSounds : Plays the Player & Enemy movement sounds.
	/// </summary>
	public void MovementSounds()
	{
		// If Player's move distance is equal to two.
		if (m_Player.m_fMoveDistance == 2)
		{
			// Play Player's move sound.
			//m_AudioSourceTwo.clip = m_AudioClips[1];
			//m_AudioSourceTwo.Play();
		}
		// Else if Player's move distance is equal to four.
		else if (m_Player.m_fMoveDistance == 4)
		{
			// Play Player's dash sound.
			m_AudioSourceTwo.clip = m_AudioClips[2];
			m_AudioSourceTwo.Play();
		}

		if (SceneManager.GetActiveScene().name == "Level_01" || SceneManager.GetActiveScene().name == "Level_09")
		{

		}
		else
		{
			// Play Enemy's move sound.
			m_AudioSourceThree.clip = m_AudioClips[3];
			m_AudioSourceThree.Play();
		}
	}

	/// <summary>
	/// public void : Plays the detection sound when the player is detected.
	/// </summary>
	public void DetectionSound()
	{
		m_AudioSourceThree.clip = m_AudioClips[4];
		m_AudioSourceThree.Play();
	}
	/// <summary>
	/// public void : Plays the Level Complete sound when the player is wins/advances a level.
	/// </summary>
	public void LevelCompleteSound()
	{
		m_AudioSourceThree.clip = m_AudioClips[5];
		m_AudioSourceThree.Play();
	}
}
