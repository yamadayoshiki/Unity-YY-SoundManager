using UnityEngine;
using TMPro;

public class DisplayAudioName : MonoBehaviour
{
	/// <summary>
	/// オーディオの名前を表示するためのTextUI
	/// </summary>
	[SerializeField]
	private TextMeshProUGUI m_Text = null;

	/// <summary>
	/// オーディオの名前を設定する
	/// </summary>
	/// <param name="name"></param>
	public void SetAudioName(string name)
	{
		m_Text.text = name;
	}
}
