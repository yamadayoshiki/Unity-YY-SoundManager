using System.Collections.Generic;
using UnityEngine;

namespace YY.Sound
{
	/// <summary>
	/// �Q�[��SE�Đ��v���C���[�N���X
	/// </summary>
	public class GameSeAudioPlayer : GameAudioPlayerBase
	{
		/// <summary>
		/// �Q�[��SE�̏��N���X
		/// </summary>
		public class GameSeInfo
		{
			//�I�[�f�B�I�f�[�^
			public AudioClip AudioClip;
			//�I�[�f�B�I�̖��O
			public string AudioName = "";
			//�Đ��ς݂��̃t���O�@true:�Đ��ς� / false:���Đ�
			public bool IsDone = false;
			//�Đ����ɂȂ��Ă���̌o�߃t���[����
			public int FrameCount;

			//�R���X�g���N�^
			public GameSeInfo(AudioClip audioClip)
			{
				AudioClip = audioClip;
				AudioName = audioClip.name;
				FrameCount = 0;
				IsDone = false;
			}
		}

		/// <summary>
		/// �������������Đ����ȏ�̃��N�G�X�g���ꂽ���ɍĐ���x��������t���[����
		/// </summary>
		[SerializeField, Range(1, 10)]
		private int m_DelayFrameCount = 2;

		/// <summary>
		/// SE�Đ��\��L���[�ɓo�^�ł���ő�v�f��
		/// </summary>
		[SerializeField, Range(1, 10)]
		private int m_MaxQueuItemCount = 4;

		/// <summary>
		/// SE�̍Đ��e�[�u��
		/// </summary>
		private readonly Dictionary<string, Queue<GameSeInfo>> m_PlaySeTable = new Dictionary<string, Queue<GameSeInfo>>();

		protected override void Start()
		{
			base.Start();

			//�~�L�T�[��ݒ�
			m_AudioSource.outputAudioMixerGroup = SoundManager.Instance.GetAudioMixerManager().GameSeGroup;
		}

		private void Update()
		{
			foreach (var q in m_PlaySeTable.Values)
			{
				//�v�f���Ȃ���΃R���e�B�j���[
				if (q.Count == 0) continue;

				while (true)
				{
					//�v�f���Ȃ���΃��[�v���甲����
					if (q.Count == 0) break;

					//�Đ��ς݂Ȃ�v�f������o���č폜
					if (q.Peek().IsDone)
					{
						var _ = q.Dequeue();
					}
					else { break; }
				}

				//�v�f���Ȃ���΃R���e�B�j���[
				if (q.Count == 0) continue;

				//���Đ��ŃL���[�̐擪��1���ɑ΂���
				var info = q.Peek();
				info.FrameCount++;
				//���Ԃ��o�߂��Ă�����Đ����č폜
				if (info.FrameCount > m_DelayFrameCount)
				{
					//�ꎞ��~���͍Đ����Ȃ�
					if (IsPaused) return;
					//�Đ�
					m_AudioSource.PlayOneShot(info.AudioClip);
					var _ = q.Dequeue();
				}
			}
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

			//���̂𐶐�
			var info = new GameSeInfo(audioClip);

			//�o�^����Ă��邩
			if (!m_PlaySeTable.ContainsKey(info.AudioName))
			{
				m_AudioSource.PlayOneShot(audioClip);
				info.IsDone = true;//�Đ��ς݂ɂ���

				//���̂�o�^
				var q = new Queue<GameSeInfo>();
				q.Enqueue(info);
				m_PlaySeTable[info.AudioName] = q;
			}
			else
			{
				var infoList = m_PlaySeTable[info.AudioName];
				//�Đ��\��L���[�o�^���𒴂��ĂȂ���
				if (infoList.Count <= m_MaxQueuItemCount)
				{
					m_PlaySeTable[info.AudioName].Enqueue(info);
				}
				else
				{
					Debug.LogError("���ʉ��̍ő�o�^���𒴂��Ă��܂��B" + info.AudioName);
				}
			}
		}

		/// <summary>
		/// �T�E���h�̒�~
		/// </summary>
		public override void StopSound()
		{
			m_AudioSource.Stop();
		}

		/// <summary>
		/// �Q�[��SE�̍Đ�
		/// AudioSource PlayOneShot���g�p
		/// </summary>
		public void PlayGameSe(int soundID)
		{
			//�ꎞ��~���͍Đ����Ȃ�
			if (IsPaused) return;

			//�I�[�f�B�I�N���b�v���擾
			AudioClip audioClip = SoundManager.Instance.GetSEAudioClip(soundID);

			//null�`�F�b�N
			if (audioClip == null)
			{
				Debug.LogError("�T�E���h�f�[�^������܂���BSoundId:" + soundID);
				return;
			}

			//���̂𐶐�
			var info = new GameSeInfo(audioClip);

			//�o�^����Ă��邩
			if (!m_PlaySeTable.ContainsKey(info.AudioName))
			{
				m_AudioSource.PlayOneShot(audioClip);
				info.IsDone = true;//�Đ��ς݂ɂ���

				//���̂�o�^
				var q = new Queue<GameSeInfo>();
				q.Enqueue(info);
				m_PlaySeTable[info.AudioName] = q;
			}
			else
			{
				var infoList = m_PlaySeTable[info.AudioName];
				//�Đ��\��L���[�o�^���𒴂��ĂȂ���
				if (infoList.Count <= m_MaxQueuItemCount)
				{
					m_PlaySeTable[info.AudioName].Enqueue(info);
				}
				else
				{
					Debug.LogError("���ʉ��̍ő�o�^���𒴂��Ă��܂��B" + info.AudioName);
				}
			}
		}

		/// <summary>
		/// �Q�[��SE�̍Đ�
		/// </summary>
		/// <param name="audioClip"></param>
		public void PlayGameSe(AudioClip audioClip)
		{
			//�ꎞ��~���͍Đ����Ȃ�
			if (IsPaused) return;

			m_AudioSource.PlayOneShot(audioClip);
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