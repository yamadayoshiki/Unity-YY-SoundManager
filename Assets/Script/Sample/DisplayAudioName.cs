using UnityEngine;
using TMPro;

public class DisplayAudioName : MonoBehaviour
{
	/// <summary>
	/// �I�[�f�B�I�̖��O��\�����邽�߂�TextUI
	/// </summary>
	[SerializeField]
	private TextMeshProUGUI m_Text = null;

	/// <summary>
	/// �I�[�f�B�I�̖��O��ݒ肷��
	/// </summary>
	/// <param name="name"></param>
	public void SetAudioName(string name)
	{
		m_Text.text = name;
	}
}
