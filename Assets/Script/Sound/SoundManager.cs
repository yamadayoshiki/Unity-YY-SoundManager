using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// サウンドマネージャー
/// </summary>
[RequireComponent(typeof(AudioMixerManager))]
public class SoundManager : MonoBehaviour
{
	//シングルトン
	public static SoundManager Instance = null;

	//オーディオミキサーマネージャー
	[SerializeField]
	private AudioMixerManager m_AudioMixerManager = null;

	/// <summary>
	/// サウンドマネージャーの設定データ
	/// </summary>
	[SerializeField]
	private SoundManagerSetting m_SoundManagerSetting = null;

	/// <summary>
	/// BGM再生オーディオリスト
	/// </summary>
	[SerializeField]
	private List<AudioSource> m_BGMAudioSourceList = new List<AudioSource>();

	/// <summary>
	/// SE再生オーディオリスト
	/// </summary>
	[SerializeField]
	private List<AudioSource> m_MenuSeAudioSorceList = new List<AudioSource>();

	/// <summary>
	/// 次のSEオーディオ番号
	/// </summary>
	private int m_NextMenuSeAudioPlayerNumber = 0;

	/// <summary>
	/// サウンドデータの読み込み先パス
	/// </summary>
	[SerializeField]
	private const string m_SoundDataFilePth = "Data/Sound/";

	/// <summary>
	/// 各サウンドデータリスト
	/// </summary>
	[SerializeField]
	private SoundDataList m_BgmSoundList = null;
	[SerializeField]
	private SoundDataList m_SeSoundList = null;

	/// <summary>
	/// 一時停止フラグ
	/// </summary>
	public bool IsPaused { get; private set; }

	/// <summary>
	/// フェード時のトゥイーンを保持するリスト
	/// </summary>
	private List<Tween> m_FadeTweenList = new List<Tween>();

	/// <summary>
	/// 一時停止インターフェイスリスト
	/// </summary>
	private List<IAudioPausable> m_AudioPausableList = new List<IAudioPausable>();

	/// <summary>
	/// AudioPausebleListに追加
	/// </summary>
	/// <param name="audioPausable"></param>
	public void SetAudioPausebleList(IAudioPausable audioPausable)
	{
		m_AudioPausableList.Add(audioPausable);
	}

	/// <summary>
	/// AudioPausebleListから削除
	/// </summary>
	/// <param name="audioPausable"></param>
	public void RemoveAudioPausebleList(IAudioPausable audioPausable)
	{
		m_AudioPausableList.Remove(audioPausable);
	}

	private void Awake()
	{
		//シングルトンの処理
		//シーンを跨いでも残るようにする
		if (Instance != null)
		{
			Destroy(this.gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}

		//SoundManagerの設定データの読み込み
		if (m_SoundManagerSetting == null) m_SoundManagerSetting = Resources.Load<SoundManagerSetting>("Data/Sound/SoundManagerSetting");
		if (m_SoundManagerSetting == null) Debug.LogError("サウンドマネージャー設定データの読み込みに失敗");
		else Debug.Log("サウンドマネージャー設定データの読み込みに成功");

		//サウンドデータの読み込み
		if (!LoadSoundData()) Debug.LogError("サウンドデータの読み込みに失敗");
		else Debug.Log("サウンドデータの読み込みに成功");

		//BGMオーディオソースの追加
		for (int i = 0; i < m_SoundManagerSetting.BgmAudioPlayerNum; i++)
		{
			var audio = this.gameObject.AddComponent<AudioSource>();
			audio.outputAudioMixerGroup = m_AudioMixerManager.BgmGroup;
			m_BGMAudioSourceList.Add(audio);
		}
		//MenuSEオーディオソースの追加
		for (int i = 0; i < m_SoundManagerSetting.MenuSEAudioPlayerNum; i++)
		{
			var audio = this.gameObject.AddComponent<AudioSource>();
			audio.outputAudioMixerGroup = m_AudioMixerManager.MenuSeGroup;
			m_MenuSeAudioSorceList.Add(audio);
		}

		//BGMを再生するオーディオソースのループ設定をTrueにする
		m_BGMAudioSourceList.ForEach(asb => asb.loop = true);
		m_MenuSeAudioSorceList.ForEach(asb => asb.loop = false);
	}

	/// <summary>
	/// サウンドデータの読み込み
	/// Soundの保存フォルダを増やしたらちゃんと追加する
	/// </summary>
	/// <returns> 成功たらtrue 失敗ならfalse </returns>
	private bool LoadSoundData()
	{
		var datas = Resources.LoadAll<SoundDataList>("Data/Sound/");
		foreach (var soundList in datas)
		{
			switch (soundList.SoundType)
			{
				case SoundType.BGM:
					m_BgmSoundList = soundList;
					break;
				case SoundType.SE:
					m_SeSoundList = soundList;
					break;
					//case SoundType.GameSE:
					//	m_GameSeList = soundList;
					//	break;
					//case SoundType.MenuSE:
					//	m_MenuSeList = soundList;
					//	break;
					//case SoundType.Jingle:
					//	m_JingleList = soundList;
					//	break;
			}
		}
		return true;
	}

	/// <summary>
	/// オーディオミキサーマネージャーの取得
	/// </summary>
	/// <returns></returns>
	public AudioMixerManager GetAudioMixerManager()
	{
		return m_AudioMixerManager;
	}

	/// <summary>
	/// BGMの再生
	/// </summary>
	/// <param name="clipName"> 再生するサウンドの名前 </param>
	public void PlayBGM(int soundID)
	{
		PlayBGMWithFade(soundID, 0.1f);
	}

	/// <summary>
	/// BGMの再生
	/// フェード処理をしながら
	/// </summary>
	/// <param name="clipName"> サウンドデータの名前 </param>
	/// <param name="fadeTime"> フェード処理の時間 </param>
	public void PlayBGMWithFade(int soundID, float fadeTime)
	{
		//一時停止中なら処理をしない
		if (IsPaused) return;

		//リストからAudioClipを取得
		AudioClip audioClip = GetBGMAudioClip(soundID);

		//clipがなければ中止
		if (audioClip == null)
		{
			Debug.LogError("指定したSoundID：" + soundID + "がありません");
			return;
		}

		//再生可能なAudioSoruceを取得
		AudioSource audioSourceEmpty = m_BGMAudioSourceList.FirstOrDefault(asb => asb.isPlaying == false);

		//再生可能なAudioSoruceがなければ中止
		if (audioSourceEmpty == null)
		{
			Debug.LogWarning("フェード処理中は新たなBGMを再生開始できません");
			return;
		}
		else
		{
			StopFadeTween();
			//どちらが片方が再生中ならフェードアウト処理
			AudioSource audioSourcePlaying = m_BGMAudioSourceList.FirstOrDefault(asb => asb.isPlaying == true);
			if (audioSourcePlaying != null)
			{
				AddFadeTweenList(audioSourcePlaying.StopWithFadeOut(fadeTime));
			}
			AddFadeTweenList(audioSourceEmpty.PlayWithFadeIn(audioClip, fadeTime));
		}
	}

	/// <summary>
	/// BGMの停止
	/// </summary>
	public void StopBGM()
	{
		StopBGMWithFade(0.1f);
	}

	/// <summary>
	/// BGMをフェード処理しながら停止
	/// </summary>
	/// <param name="fadeTime"></param>
	public void StopBGMWithFade(float fadeTime)
	{
		if (IsPaused) return;

		StopFadeTween();

		//再生しているBGMAudioSoruceがあったら止める
		foreach (AudioSource asb in m_BGMAudioSourceList.Where(asb => asb.isPlaying == true))
		{
			AddFadeTweenList(asb.StopWithFadeOut(fadeTime));
		}
	}

	/// <summary>
	/// メニューSEの再生
	/// </summary>
	/// <param name="soundID"></param>
	public void PlayMenuSE(int soundID)
	{
		//一時停止中なら処理をしない
		if (IsPaused) return;

		//リストからAudioClipを取得
		AudioClip audioClip = GetSEAudioClip(soundID);

		//clipがなければ中止
		if (audioClip == null)
		{
			Debug.LogError("指定したSoundID：" + soundID + "がありません");
			return;
		}

		//AudioSoruceを取得
		AudioSource audioSourceEmpty = m_MenuSeAudioSorceList[m_NextMenuSeAudioPlayerNumber];
		m_NextMenuSeAudioPlayerNumber++;
		if (m_NextMenuSeAudioPlayerNumber >= m_MenuSeAudioSorceList.Count)
		{
			m_NextMenuSeAudioPlayerNumber = 0;
		}

		audioSourceEmpty.Play(audioClip, 1.0f);

	}

	/// <summary>
	/// SEの再生の停止
	/// </summary>
	public void StopMenuSe()
	{
		if (IsPaused) return;

		//再生しているBGMAudioSoruceがあったら止める
		foreach (AudioSource asb in m_MenuSeAudioSorceList.Where(asb => asb.isPlaying == true))
		{
			asb.Stop();
		}
	}
	/// <summary>
	/// フェードのTweenを追加
	/// </summary>
	/// <param name="tween"></param>
	private void AddFadeTweenList(Tween tween)
	{
		m_FadeTweenList.Add(tween);
	}

	/// <summary>
	/// フェードのTweenを停止
	/// </summary>
	private void StopFadeTween()
	{
		m_FadeTweenList.ForEach(tween => tween.Kill());
		m_FadeTweenList.Clear();
	}

	/// <summary>
	/// ポーズ処理
	/// </summary>
	public void Pause()
	{
		IsPaused = true;

		m_FadeTweenList.ForEach(tween => tween.Pause());
		m_BGMAudioSourceList.ForEach(asb => asb.Pause());

		PauseExeptBGM();
	}

	/// <summary>
	/// 再生を再開
	/// </summary>
	public void Resume()
	{
		IsPaused = false;
		m_FadeTweenList.ForEach(tween => tween.Play());
		m_BGMAudioSourceList.ForEach(asb => asb.UnPause());

		ResumeExeptBGM();
	}

	/// <summary>
	/// BGMを除くオーディオの一時停止
	/// </summary>
	public void PauseExeptBGM()
	{
		IsPaused = true;

		m_AudioPausableList.ForEach(p => p.Pause());

	}

	/// <summary>
	/// BGMを除くオーディオの再生再開
	/// </summary>
	public void ResumeExeptBGM()
	{
		m_AudioPausableList.ForEach(p => p.Resume());
	}

	/// <summary>
	/// BGMオーディオクリップの取得
	/// </summary>
	/// <param name="soundID"></param>
	/// <returns></returns>
	public AudioClip GetBGMAudioClip(int soundID)
	{
		return m_BgmSoundList.GetAudioClip((int)soundID);
	}

	/// <summary>
	/// SEオーディオクリップの取得
	/// </summary>
	/// <param name="soundID"></param>
	/// <returns></returns>
	public AudioClip GetSEAudioClip(int soundID)
	{
		return m_SeSoundList.GetAudioClip((int)soundID);
	}

	/// <summary>
	/// 再生中のAudioSourceを取得
	/// </summary>
	/// <returns></returns>
	public AudioSource GetPlayingAudioSource()
	{
		return m_BGMAudioSourceList.FirstOrDefault(asb => asb.isPlaying == true);
	}
}

