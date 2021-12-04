using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestBgmPlayer : MonoBehaviour
{
	/// <summary>
	/// BGMサウンドID
	/// </summary>
	[SerializeField]
	private BGMSoundID m_SoundID;

	/// <summary>
	/// フェードする時間
	/// </summary>
	[SerializeField]
	private float m_FadeTime = 2.0f;

	/// <summary>
	/// オーディオの名前を設定するクラス
	/// </summary>
	[SerializeField]
	private DisplayAudioName m_DisplayAudioName = null;

	/// <summary>
	/// BGMの再生
	/// </summary>
	public void PlayBGM()
	{
		//サウンドマネージャーからサウンドIDを渡して紐づいているオーディオクリップを取得
		var clip = SoundManager.Instance.GetBGMAudioClip((int)m_SoundID);
		//オーディオの名前を設定する
		m_DisplayAudioName.SetAudioName(clip.name);
		//再生されている場合も考慮して一度停止の処理を行う
		SoundManager.Instance.StopBGMWithFade(m_FadeTime);
		//フェードインしながらBGMを再生
		SoundManager.Instance.PlayBGMWithFade((int)m_SoundID, m_FadeTime);
	}

	/// <summary>
	/// 次のBGMを再生
	/// </summary>
	public void NextBGM()
	{
		//次のIDを求める
		var soundID = m_SoundID += 1;
		//Enumの最大値を取得
		var maxValue = Enum.GetValues(typeof(BGMSoundID)).Cast<BGMSoundID>().Max();
		//Enumの最小値を取得
		var minValue = Enum.GetValues(typeof(BGMSoundID)).Cast<BGMSoundID>().Min();

		//最大値より大きい場合最小の値にする
		if (soundID > maxValue)
		{
			soundID = minValue;
		}
		//サウンドIDを確定する
		m_SoundID = soundID;
		//BGMを再生
		PlayBGM();
	}

	/// <summary>
	/// 前のBGMを再生
	/// </summary>
	public void PrevBGM()
	{
		//次のIDを求める
		var soundID = m_SoundID += 1;
		//Enumの最大値を取得
		var maxValue = Enum.GetValues(typeof(BGMSoundID)).Cast<BGMSoundID>().Max();
		//Enumの最小値を取得
		var minValue = Enum.GetValues(typeof(BGMSoundID)).Cast<BGMSoundID>().Min();

		//最小値より小さい場合最大値にする
		if (soundID < minValue)
		{
			soundID = maxValue;
		}
		//サウンドIDを確定する
		m_SoundID = soundID;
		//BGMを再生
		PlayBGM();
	}

	/// <summary>
	/// BGMの停止
	/// </summary>
	public void StopBGM()
	{
		//BGMの名前表示を消す
		m_DisplayAudioName.SetAudioName("");
		//BGMを停止する
		SoundManager.Instance.StopBGM();
	}
}
