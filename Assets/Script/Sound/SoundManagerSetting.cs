using UnityEngine;

/// <summary>
/// サウンドマネージャーの設定データクラス
/// </summary>
[CreateAssetMenu(fileName = "SoundManagerSetting", menuName = "Create/SoundManagerSetting")]
public class SoundManagerSetting : ScriptableObject
{
	[SerializeField, Header("BGMの再生可能数")]
	private int m_BgmAudioPlayerNum = 2;
	public int BgmAudioPlayerNum
	{
		get { return m_BgmAudioPlayerNum; }
	}

	[SerializeField, Header("MenuSEの再生可能数")]
	private int m_MenuSEAudioPlayerNum = 3;
	public int MenuSEAudioPlayerNum
	{
		get { return m_MenuSEAudioPlayerNum; }
	}
}