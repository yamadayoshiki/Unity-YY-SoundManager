using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

/// <summary>
/// AudioSoruce�֐��g���N���X
/// </summary>
public static class ExtensionAudioSource
{
	/// <summary>
	/// Play�֐��̊g��
	/// �P�s��audioClip, �{�����[���A�Đ��ʒu�̃����_�}�C�Y�܂Ŏw��ł���悤�ɂ���
	/// </summary>
	/// <param name="audioClip"> �T�E���h�f�[�^ </param>
	/// <param name="volime"> ���� 0.0f~1.0f </param>
	/// <param name="isRandomStartTime"> �Đ��ʒu�������_���ɂ��邩 </param>
	public static void Play(this AudioSource audioSource, AudioClip audioClip, float volume, bool isRandomStartTime = false)
	{
		//�I�[�f�B�I�N���b�v��null�Ȃ珈�����Ȃ�
		if (audioClip == null) return;

		//�I�[�f�B�I�\�[�X�ɃI�[�f�B�I�N���b�v�Ɖ��ʂ�ݒ�
		audioSource.clip = audioClip;
		audioSource.volume = volume;
		//�Đ��ʒu�̃����_�����L���Ȃ珈������
		if (isRandomStartTime)
		{
			audioSource.time = UnityEngine.Random.Range(0f, audioClip.length - 0.01f);
			//���ʂ�length�Ɠ��l�ɂȂ�ƃV�[�N�G���[���N�������� -0.01�b����//
		}
		//�����Đ�
		audioSource.Play();
	}

	/// <summary>
	/// Play�֐��̊g��
	/// audioClip, �{�����[��,�Đ��������Ɏ��s����֐���ݒ�
	/// </summary>
	/// <param name="audioClip"> �T�E���h�f�[�^ </param>
	/// <param name="volime"> ���� 0.0f~1.0f </param>
	/// <param name="compCallback"> �Đ��������Ɏ��s����֐� </param>
	public static Tween PlayWithCompCallback(this AudioSource audioSource, AudioClip audioClip, float volume = 1f, UnityAction compCallback = null)
	{
		float timer = 0.0f; //�^�C�}�[�̗p��
		//DOtween���g���čĐ��������ɃR�[���o�b�N���s��
		Tween tween = DOTween.To(
			() => 0.0f, //�����l��ݒ�
			t => timer = t, //�^�C�}�[�̍X�V
			Time.deltaTime, //�^�C�}�[�ɉ��Z����l
			audioClip.length) //�I���l
			.OnStart(() => audioSource.Play(audioClip, volume)) //�g�D�C�[���J�n���ɉ����Đ�
			.OnComplete(() => //�g�D�C�[���������Ɏ��s
			{	//�R�[���o�b�N���ݒ肳��Ă��Ȃ���Ώ������Ȃ�
				if (compCallback != null)
				{
					//�Đ������R�[���o�b�N�����s
					compCallback();
				}
			}
		);
		//�g�D�C�[����Ԃ�
		return tween;
	}

	/// <summary>
	/// Play�֐��̊g��
	/// �t�F�[�h����
	/// </summary>
	/// <param name="targetVolume"> �ڕW�̃{�����[�� </param>
	/// <param name="fadeTime"> �t�F�[�h���鎞�� </param>
	/// <param name="ease"> �C�[�W���O�^�C�v </param>
	/// <param name="compCallback"> �t�F�[�h�������Ɏ��s����֐� </param>
	private static Tween VolumeFade(this AudioSource audioSource, float targetVolume = 1f, float fadeTime = 1f, Ease ease = Ease.Linear, UnityAction compCallback = null)
	{
		//�g�D�C�[���𐶐�
		//DOFade���g���ĉ��ʂ𑀍삷��
		Tween tween = audioSource.DOFade(targetVolume, fadeTime)
			.SetEase(ease) //�C�[�W���O��ݒ�
			.OnComplete(() =>//�g�D�C�[���������Ɏ��s
			{   //�R�[���o�b�N���ݒ肳��Ă��Ȃ���Ώ������Ȃ�
				if (compCallback != null)
				{
					//�Đ������R�[���o�b�N�����s
					compCallback();
				}
			});
		//�g�D�C�[����Ԃ�
		return tween;
	}

	/// <summary>
	/// Play�֐��̊g��
	/// �t�F�[�h�C������
	/// </summary>
	/// <param name="audioClip"> �T�E���h�f�[�^ </param>
	/// <param name="targetVolume"> �ڕW�̃{�����[�� </param>
	/// <param name="fadeTime"> �t�F�[�h���鎞�� </param>
	/// <param name="ease"> �C�[�W���O�^�C�v </param>
	/// <param name="isRandomStartTime"> �Đ��ʒu�������_���ɂ��邩 </param>
	/// <param name="compCallback"> �Đ��������Ɏ��s����֐� </param>
	public static Tween PlayWithFadeIn(this AudioSource audioSource, AudioClip audioClip, float fadeTime = 1f, float startVolume = 0.0f, float targetVolume = 1f, Ease ease = Ease.Linear, bool isRandomStartTime = false, UnityAction compCallback = null)
	{
		//�ڕW�{�����[����0�ȉ��̏ꍇ�͍Đ����L�����Z��
		if (targetVolume <= 0.0f) return null;
		//���̍Đ�
		audioSource.Play(audioClip, startVolume, isRandomStartTime);
		//�t�F�[�h�C���������s��
		return audioSource.VolumeFade(targetVolume, fadeTime, ease, compCallback);
	}

	/// <summary>
	/// Play�֐��̊g��
	/// �t�F�[�h�A�E�g����
	/// </summary>
	/// <param name="audioClip"> �T�E���h�f�[�^ </param>
	/// <param name="targetVolume"> �ڕW�̃{�����[�� </param>
	/// <param name="fadeTime"> �t�F�[�h���鎞�� </param>
	/// <param name="ease"> �C�[�W���O�^�C�v </param>
	/// <param name="isRandomStartTime"> �Đ��ʒu�������_���ɂ��邩 </param>
	public static Tween StopWithFadeOut(this AudioSource audioSource, float fadeTime = 1f, float targetVolume = 0f, Ease ease = Ease.Linear, bool isRandomStartTime = false, UnityAction compCallback = null)
	{
		//�Đ�����ĂȂ���Ώ������Ȃ�
		if (audioSource.isPlaying == false) return null;
		//�t�F�[�h�A�E�g�������s��
		return audioSource.VolumeFade(targetVolume, fadeTime, ease
			, () =>
			{   //�Đ����~
				audioSource.Stop();
				//�Đ������R�[���o�b�N�����s
				if (compCallback != null)
				{
					compCallback();
				}
			}
		);
	}
}
