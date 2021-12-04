using UnityEngine;
using UnityEditor;
using YY.Sound;

public static class CreateSoundDataList
{
	/// <summary>
	/// �T�E���h�f�[�^���X�g���쐬
	/// </summary>
	/// <param name="filePath"> �ۑ��� </param>
	/// <param name="fileName"> �t�@�C���̖��O </param>
	/// <returns></returns>
	public static SoundDataList Create(string filePath, string fileName)
	{
		var data = ScriptableObject.CreateInstance<SoundDataList>();
		var assetsName = $"{filePath}{fileName}.asset";
		AssetDatabase.CreateAsset(data, assetsName);
		AssetDatabase.Refresh();
		return data;
	}
}
