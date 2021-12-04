using UnityEngine;

namespace YY.Sound
{
	/// <summary>
	/// Game���Ŏg��SE��BGM��炷���N���X
	/// 3D�T�E���h�ɂ��Ή��ł���悤�ɐ���
	/// </summary>
	[RequireComponent(typeof(AudioSource))]
	public abstract class GameAudioPlayerBase : MonoBehaviour, IAudioPausable
	{
		/// <summary>
		/// �I�[�f�B�I�\�[�X
		/// </summary>
		protected AudioSource m_AudioSource = null;

		/// <summary>
		/// 3D�T�E���h�̐ݒ�f�[�^
		/// </summary>
		[SerializeField]
		protected Game3DSoundSetteing m_3DSoundSetting = null;

		/// <summary>
		/// ���s���ɍĐ����J�n���邩
		/// </summary>
		[SerializeField]
		protected bool m_IsPlayOnAwake = false;

		/// <summary>
		/// �ꎞ��~���Ă��邩
		/// </summary>
		public bool IsPaused { get; private set; }

		protected virtual void Awake()
		{
			//�I�[�f�B�I�\�[�X�̎擾
			m_AudioSource = GetComponent<AudioSource>();
			//�I�[�f�B�I�\�[�X�̏�����
			InitializeAudioSource();
		}

		protected virtual void Start()
		{
			//�T�E���h�}�l�[�W���[��AudioPausebleList�ɒǉ�
			SoundManager.Instance.SetAudioPausebleList(this);

			//�t���O��true�Ȃ�Đ�
			if (m_IsPlayOnAwake)
			{
				PlaySound();
			}
		}

		/// <summary>
		/// �T�E���h�̍Đ�
		/// </summary>
		public abstract void PlaySound();

		/// <summary>
		/// �T�E���h�̒�~
		/// </summary>
		public abstract void StopSound();

		/// <summary>
		/// �I�[�f�B�I���ꎞ��~
		/// </summary>
		public virtual void Pause()
		{
			IsPaused = true;
			m_AudioSource.Pause();
		}

		/// <summary>
		/// �I�[�f�B�I�̍Đ����ĊJ
		/// </summary>
		public virtual void Resume()
		{
			IsPaused = false;
			m_AudioSource.UnPause();
		}

		public virtual void OnDestroy()
		{
			//�T�E���h�}�l�[�W���[��AudioPausebleList����폜
			SoundManager.Instance.RemoveAudioPausebleList(this);
		}

		/// <summary>
		/// �I�[�f�B�I�\�[�X�̏�����
		/// </summary>
		protected virtual void InitializeAudioSource()
		{
			//PlayOnAwake�ōĐ������ꍇ�̂��߂Ɉ�x��~�̏������Ă�
			m_AudioSource.Stop();
			m_AudioSource.playOnAwake = false;
			m_AudioSource.volume = 1.0f;
			m_AudioSource.pitch = 1.0f;
			m_AudioSource.spatialBlend = 0.0f;
		}
	}
}