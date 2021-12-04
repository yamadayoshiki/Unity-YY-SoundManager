using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YY.Sound
{
	/// <summary>
	/// �Q�[��BGM�Đ��N���X
	/// </summary>
	public class GameBgmAudioPlayer : GameAudioPlayerBase
	{
		/// <summary>
		/// �t�F�[�h���鎞��
		/// </summary>
		[SerializeField]
		private float m_FadeTime = 1.0f;

		protected override void Awake()
		{
			//�I�[�f�B�I�\�[�X�̎擾
			m_AudioSource = GetComponent<AudioSource>();
			//�I�[�f�B�I�\�[�X�̏�����
			InitializeAudioSource();
		}

		protected override void Start()
		{
			base.Start();

			//�~�L�T�[��ݒ�
			m_AudioSource.outputAudioMixerGroup = SoundManager.Instance.GetAudioMixerManager().BgmGroup;
		}

		/// <summary>
		/// �T�E���h�̍Đ�
		/// </summary>
		public override void PlaySound()
		{
			//�ꎞ��~���͍Đ����Ȃ�
			if (IsPaused) return;

			//�I�[�f�B�I�N���b�v���擾
			AudioClip audioClip = m_AudioSource.clip;

			//null�`�F�b�N
			if (audioClip == null)
			{
				Debug.LogError("�I�[�f�B�I�N���b�v������܂���");
				return;
			}

			//�t�F�[�h�C�����Ȃ���Đ�
			m_AudioSource.PlayWithFadeIn(audioClip, m_FadeTime);
		}

		/// <summary>
		/// �T�E���h�̒�~
		/// </summary>
		public override void StopSound()
		{
			m_AudioSource.StopWithFadeOut(m_FadeTime);
		}

		/// <summary>
		/// �I�[�f�B�I�\�[�X�̏�����
		/// </summary>
		protected override void InitializeAudioSource()
		{
			base.InitializeAudioSource();

			if (m_3DSoundSetting == null) return;
			//3D�T�E���h�̔䗦�ɂ���
			m_AudioSource.spatialBlend = 1.0f;
			//3D�T�E���h�̐ݒ�
			m_AudioSource.dopplerLevel = m_3DSoundSetting.DopplerLeveget;
			m_AudioSource.spread = m_3DSoundSetting.Spread;
			m_AudioSource.minDistance = m_3DSoundSetting.MinDistance;
			m_AudioSource.maxDistance = m_3DSoundSetting.MaxDistance;
		}
	}
}
