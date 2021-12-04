using UnityEngine;

namespace YY.Sound
{
	/// <summary>
	/// 3D�T�E���h�̐ݒ�f�[�^
	/// </summary>
	[CreateAssetMenu(fileName = "Game3DSoundSetting", menuName = "Create/Sound/Game3DSoundSetting")]
	public class Game3DSoundSetteing : ScriptableObject
	{
		/// <summary>
		/// �h�b�v���[���ʂ̓x��
		/// </summary>
		[SerializeField, Header("�h�b�v���[���ʂ̓x��"), Range(0.0f, 5.0f)]
		private float m_DopplerLevel = 1.0f;
		public float DopplerLeveget { get { return m_DopplerLevel; } }

		/// <summary>
		/// �X�s�[�J�[��Ԃ�3D�X�e���I��}���`�`�����l���T�E���h�ɑ΂���L����̊p�x
		/// </summary>
		[SerializeField, Header("�X�s�[�J�[��Ԃ�3D�X�e���I��}���`�`�����l���T�E���h�ɑ΂���L����̊p�x"), Range(0, 360)]
		private int m_Spread = 0;
		public int Spread { get { return m_Spread; } }

		/// <summary>
		/// ���ʂ���������Ȃ��Œ዗��
		/// </summary>
		[SerializeField, Header("���ʂ���������Ȃ��Œ዗��")]
		private float m_MinDistance = 1.0f;
		public float MinDistance { get { return m_MinDistance; } }

		/// <summary>
		/// ������܂�̂��~�߂鋗��
		/// </summary>
		[SerializeField, Header("������܂�̂��~�߂鋗��")]
		private float m_MaxDistance = 500.0f;
		public float MaxDistance { get { return m_MaxDistance; } }
	}
}