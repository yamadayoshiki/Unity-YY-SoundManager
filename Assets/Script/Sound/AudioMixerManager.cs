using UnityEngine;
using UnityEngine.Audio;

namespace YY.Sound
{
	/// <summary>
	/// �I�[�f�B�I�~�L�T�[�}�l�[�W���[
	/// </summary>
	public class AudioMixerManager : MonoBehaviour
	{
		/// <summary>
		/// �I�[�f�B�I�~�L�T�[
		/// </summary>
		[SerializeField]
		private AudioMixer m_AudioMixer = null;
		public AudioMixer GetAudioMixer()
		{
			return m_AudioMixer;
		}

		/// <summary>
		/// �I�[�f�B�I�~�L�T�[�O���[�v
		/// </summary>
		[SerializeField]
		private AudioMixerGroup m_Master = null;
		[SerializeField]
		private AudioMixerGroup m_MasterBGM = null;
		[SerializeField]
		private AudioMixerGroup m_MasterSE = null;
		[SerializeField]
		private AudioMixerGroup m_GameSE = null;
		[SerializeField]
		private AudioMixerGroup m_MenuSE = null;
		[SerializeField]
		private AudioMixerGroup m_Jingle = null;
		[SerializeField]
		private AudioMixerGroup m_Voice = null;

		/// <summary>
		/// �e�~�L�T�[�̃{�����[���̒l
		/// </summary>
		public float MasterVolumeByLinear
		{
			get { return m_Master.GetVolumeByLinear(); }
			set { m_Master.SetVolumeByLinear(value); }
		}
		public float BgmVolumeByLinear
		{
			get { return m_Master.GetVolumeByLinear(); }
			set { m_MasterBGM.SetVolumeByLinear(value); }
		}
		public float SeVolumeByLinear
		{
			get { return m_Master.GetVolumeByLinear(); }
			set { m_MasterSE.SetVolumeByLinear(value); }
		}
		public float GameSeVolumeByLinear
		{
			get { return m_Master.GetVolumeByLinear(); }
			set { m_GameSE.SetVolumeByLinear(value); }
		}
		public float MenuSeVolumeByLinear
		{
			get { return m_Master.GetVolumeByLinear(); }
			set { m_MenuSE.SetVolumeByLinear(value); }
		}
		public float JingleVolumeByLinear
		{
			get { return m_Master.GetVolumeByLinear(); }
			set { m_Jingle.SetVolumeByLinear(value); }
		}
		public float VoiceVolumeByLinear
		{
			get { return m_Master.GetVolumeByLinear(); }
			set { m_Voice.SetVolumeByLinear(value); }
		}

		/// <summary>
		/// �e�~�L�T�[�O���[�v�̃Q�b�^�[
		/// </summary>
		public AudioMixerGroup BgmGroup { get { return m_MasterBGM; } }
		public AudioMixerGroup SeGroup { get { return m_MasterSE; } }
		public AudioMixerGroup GameSeGroup { get { return m_GameSE; } }
		public AudioMixerGroup MenuSeGroup { get { return m_MenuSE; } }
		public AudioMixerGroup JingleGroup { get { return m_Jingle; } }
		public AudioMixerGroup VoiceGroup { get { return m_Voice; } }
	}
}