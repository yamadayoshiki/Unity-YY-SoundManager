using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YY.Sound;

public class SoundController : MonoBehaviour
{
	/// <summary>
	/// �Q�[��BGM�v���C���[
	/// </summary>
	[SerializeField]
	private GameBgmAudioPlayer m_BGMPlayer = null;

	/// <summary>
	/// �Q�[��SE�v���C���[
	/// </summary>
	[SerializeField]
	private GameSeAudioPlayer m_SEPlayer = null;

	void Start()
	{
		m_BGMPlayer = GameObject.Find("BGMPlayer").GetComponent<GameBgmAudioPlayer>();
		m_SEPlayer = GameObject.Find("SEPlayer").GetComponent<GameSeAudioPlayer>();
	}

	void Update()
	{
		//BGM�̈ꎞ��~
		if (!m_BGMPlayer.IsPaused && Input.GetKeyDown(KeyCode.Space))
		{
			m_BGMPlayer.Pause();
		}
		//BGM�̈ꎞ��~����
		else if (m_BGMPlayer.IsPaused && Input.GetKeyDown(KeyCode.Space))
		{
			m_BGMPlayer.Resume();
		}

		//SE�̍Đ�
		if (Input.GetKeyDown(KeyCode.Z))
		{
			m_SEPlayer.PlayGameSe((int)SESoundID.ExplosionSE);
		}
	}
}
