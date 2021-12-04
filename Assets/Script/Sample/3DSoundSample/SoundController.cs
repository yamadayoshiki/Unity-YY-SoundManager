using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YY.Sound;

public class SoundController : MonoBehaviour
{
	/// <summary>
	/// ゲームBGMプレイヤー
	/// </summary>
	[SerializeField]
	private GameBgmAudioPlayer m_BGMPlayer = null;

	/// <summary>
	/// ゲームSEプレイヤー
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
		//BGMの一時停止
		if (!m_BGMPlayer.IsPaused && Input.GetKeyDown(KeyCode.Space))
		{
			m_BGMPlayer.Pause();
		}
		//BGMの一時停止解除
		else if (m_BGMPlayer.IsPaused && Input.GetKeyDown(KeyCode.Space))
		{
			m_BGMPlayer.Resume();
		}

		//SEの再生
		if (Input.GetKeyDown(KeyCode.Z))
		{
			m_SEPlayer.PlayGameSe((int)SESoundID.ExplosionSE);
		}
	}
}
