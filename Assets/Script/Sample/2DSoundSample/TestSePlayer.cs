using UnityEngine;
using System;
using System.Linq;
using YY.Sound;

public class TestSePlayer : MonoBehaviour
{
	/// <summary>
	/// SE�T�E���hID
	/// </summary>
	[SerializeField]
	private SESoundID m_SoundID;

	/// <summary>
	/// �I�[�f�B�I�̖��O��ݒ肷��N���X
	/// </summary>
	[SerializeField]
	private DisplayAudioName m_DisplayAudioName = null;

	/// <summary>
	/// SE�̍Đ�
	/// </summary>
	public void PlaySE()
	{
		//�T�E���h�}�l�[�W���[����T�E���hID��n���ĕR�Â��Ă���I�[�f�B�I�N���b�v���擾
		var clip = SoundManager.Instance.GetSEAudioClip((int)m_SoundID);
		//�I�[�f�B�I�̖��O��ݒ肷��
		m_DisplayAudioName.SetAudioName(clip.name);
		//SE���Đ�
		SoundManager.Instance.PlayMenuSE((int)m_SoundID);
	}

	/// <summary>
	/// ����SE���Đ�
	/// </summary>
	public void NextSE()
	{
		//���̃T�E���hID�����߂�
		var soundID = m_SoundID += 1;
		//Enum�̍ő�l���擾
		var maxValue = Enum.GetValues(typeof(SESoundID)).Cast<SESoundID>().Max();
		//Enum�̍ŏ��l���擾
		var minValue = Enum.GetValues(typeof(SESoundID)).Cast<SESoundID>().Min();

		//�ő�l���傫���ꍇ�ŏ��̒l�ɂ���
		if (soundID > maxValue)
		{
			soundID = minValue;
		}
		//�T�E���hID�̊m��
		m_SoundID = soundID;
		//�Đ�����Ă���SE���~
		StopSE();
		//SE���Đ�
		PlaySE();
	}

	/// <summary>
	/// �O��SE���Đ�
	/// </summary>
	public void PrevSE()
	{
		//���̃T�E���hID�����߂�
		var soundID = m_SoundID -= 1;
		//Enum�̍ő�l���擾
		var maxValue = Enum.GetValues(typeof(SESoundID)).Cast<SESoundID>().Max();
		//Enum�̍ŏ��l���擾
		var minValue = Enum.GetValues(typeof(SESoundID)).Cast<SESoundID>().Min();

		//�ŏ��l��菬�����ꍇ�ő�l�ɂ���
		if (soundID < minValue)
		{
			soundID = maxValue;
		}
		//�T�E���hID�̊m��
		m_SoundID = soundID;
		//�Đ�����Ă���SE���~
		StopSE();
		//SE���Đ�
		PlaySE();
	}

	/// <summary>
	/// SE�̒�~
	/// </summary>
	public void StopSE()
	{
		//SE�̖��O�\��������
		m_DisplayAudioName.SetAudioName("");
		//SE�̍Đ����~����
		SoundManager.Instance.StopMenuSe();
	}
}
