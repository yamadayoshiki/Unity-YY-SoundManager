using UnityEngine;
using System;
using System.Linq;
using YY.Sound;

public class TestSePlayer : MonoBehaviour
{
	/// <summary>
	/// SEサウンドID
	/// </summary>
	[SerializeField]
	private SESoundID m_SoundID;

	/// <summary>
	/// オーディオの名前を設定するクラス
	/// </summary>
	[SerializeField]
	private DisplayAudioName m_DisplayAudioName = null;

	/// <summary>
	/// SEの再生
	/// </summary>
	public void PlaySE()
	{
		//サウンドマネージャーからサウンドIDを渡して紐づいているオーディオクリップを取得
		var clip = SoundManager.Instance.GetSEAudioClip((int)m_SoundID);
		//オーディオの名前を設定する
		m_DisplayAudioName.SetAudioName(clip.name);
		//SEを再生
		SoundManager.Instance.PlayMenuSE((int)m_SoundID);
	}

	/// <summary>
	/// 次のSEを再生
	/// </summary>
	public void NextSE()
	{
		//次のサウンドIDを求める
		var soundID = m_SoundID += 1;
		//Enumの最大値を取得
		var maxValue = Enum.GetValues(typeof(SESoundID)).Cast<SESoundID>().Max();
		//Enumの最小値を取得
		var minValue = Enum.GetValues(typeof(SESoundID)).Cast<SESoundID>().Min();

		//最大値より大きい場合最小の値にする
		if (soundID > maxValue)
		{
			soundID = minValue;
		}
		//サウンドIDの確定
		m_SoundID = soundID;
		//再生されているSEを停止
		StopSE();
		//SEを再生
		PlaySE();
	}

	/// <summary>
	/// 前のSEを再生
	/// </summary>
	public void PrevSE()
	{
		//次のサウンドIDを求める
		var soundID = m_SoundID -= 1;
		//Enumの最大値を取得
		var maxValue = Enum.GetValues(typeof(SESoundID)).Cast<SESoundID>().Max();
		//Enumの最小値を取得
		var minValue = Enum.GetValues(typeof(SESoundID)).Cast<SESoundID>().Min();

		//最小値より小さい場合最大値にする
		if (soundID < minValue)
		{
			soundID = maxValue;
		}
		//サウンドIDの確定
		m_SoundID = soundID;
		//再生されているSEを停止
		StopSE();
		//SEを再生
		PlaySE();
	}

	/// <summary>
	/// SEの停止
	/// </summary>
	public void StopSE()
	{
		//SEの名前表示を消す
		m_DisplayAudioName.SetAudioName("");
		//SEの再生を停止する
		SoundManager.Instance.StopMenuSe();
	}
}
