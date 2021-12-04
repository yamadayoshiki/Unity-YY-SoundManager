using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestBgmPlayer : MonoBehaviour
{
	/// <summary>
	/// BGM�T�E���hID
	/// </summary>
	[SerializeField]
	private BGMSoundID m_SoundID;

	/// <summary>
	/// �t�F�[�h���鎞��
	/// </summary>
	[SerializeField]
	private float m_FadeTime = 2.0f;

	/// <summary>
	/// �I�[�f�B�I�̖��O��ݒ肷��N���X
	/// </summary>
	[SerializeField]
	private DisplayAudioName m_DisplayAudioName = null;

	/// <summary>
	/// BGM�̍Đ�
	/// </summary>
	public void PlayBGM()
	{
		//�T�E���h�}�l�[�W���[����T�E���hID��n���ĕR�Â��Ă���I�[�f�B�I�N���b�v���擾
		var clip = SoundManager.Instance.GetBGMAudioClip((int)m_SoundID);
		//�I�[�f�B�I�̖��O��ݒ肷��
		m_DisplayAudioName.SetAudioName(clip.name);
		//�Đ�����Ă���ꍇ���l�����Ĉ�x��~�̏������s��
		SoundManager.Instance.StopBGMWithFade(m_FadeTime);
		//�t�F�[�h�C�����Ȃ���BGM���Đ�
		SoundManager.Instance.PlayBGMWithFade((int)m_SoundID, m_FadeTime);
	}

	/// <summary>
	/// ����BGM���Đ�
	/// </summary>
	public void NextBGM()
	{
		//����ID�����߂�
		var soundID = m_SoundID += 1;
		//Enum�̍ő�l���擾
		var maxValue = Enum.GetValues(typeof(BGMSoundID)).Cast<BGMSoundID>().Max();
		//Enum�̍ŏ��l���擾
		var minValue = Enum.GetValues(typeof(BGMSoundID)).Cast<BGMSoundID>().Min();

		//�ő�l���傫���ꍇ�ŏ��̒l�ɂ���
		if (soundID > maxValue)
		{
			soundID = minValue;
		}
		//�T�E���hID���m�肷��
		m_SoundID = soundID;
		//BGM���Đ�
		PlayBGM();
	}

	/// <summary>
	/// �O��BGM���Đ�
	/// </summary>
	public void PrevBGM()
	{
		//����ID�����߂�
		var soundID = m_SoundID += 1;
		//Enum�̍ő�l���擾
		var maxValue = Enum.GetValues(typeof(BGMSoundID)).Cast<BGMSoundID>().Max();
		//Enum�̍ŏ��l���擾
		var minValue = Enum.GetValues(typeof(BGMSoundID)).Cast<BGMSoundID>().Min();

		//�ŏ��l��菬�����ꍇ�ő�l�ɂ���
		if (soundID < minValue)
		{
			soundID = maxValue;
		}
		//�T�E���hID���m�肷��
		m_SoundID = soundID;
		//BGM���Đ�
		PlayBGM();
	}

	/// <summary>
	/// BGM�̒�~
	/// </summary>
	public void StopBGM()
	{
		//BGM�̖��O�\��������
		m_DisplayAudioName.SetAudioName("");
		//BGM���~����
		SoundManager.Instance.StopBGM();
	}
}
