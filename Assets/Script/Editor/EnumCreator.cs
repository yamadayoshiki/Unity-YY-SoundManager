//  EnumCreator.cs
//  http://kan-kikuchi.hatenablog.com/entry/EnumCreator
//
//  Created by kan.kikuchi on 2016.08.30.

using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Enum�𐶐�����N���X
/// </summary>
public static class EnumCreator
{

	//�R�[�h�S���ƃ^�u����
	private static string _code = "", _tab = "";

	//=================================================================================
	//����
	//=================================================================================

	//���������Aenum�̏㕔�������쐬
	private static void Init(string nameSpace, string summary, bool isFlags, string enumName)
	{
		//�R�[�h�S���ƃ^�u���������Z�b�g
		_code = "";
		_tab = "";

		//�l�[���X�y�[�X�����͂���Ă���ΐݒ�
		if (!string.IsNullOrEmpty(nameSpace))
		{
			_code += "namespace " + nameSpace + "{\n";
			_tab += "\t";
		}

		//�T�v�����͂���Ă���ΐݒ�
		if (!string.IsNullOrEmpty(summary))
		{
			_code +=
			  _tab + "/// <summary>\n" +
			  _tab + "/// " + summary + "\n" +
			  _tab + "/// </summary>\n";
		}

		//�t���O�����̐ݒ�
		if (isFlags)
		{
			_code += _tab + "[System.Flags]\n";
		}

		//enum����ݒ�
		_code += _tab + "public enum " + enumName + "{\n";

		//�C���f���g��������
		_tab += "\t\t";
	}

	//enum�������o��
	private static void Export(string exportPath, string nameSpace, string enumName)
	{
		//�l�[���X�y�[�X�����͂���Ă���ΐݒ�
		if (!string.IsNullOrEmpty(nameSpace))
		{
			_code += "\t}\n";
		}

		_code += "}";

		//�t�@�C���̏����o��
		File.WriteAllText(exportPath, _code, Encoding.UTF8);
		AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);

		Debug.Log(enumName + "�̍쐬���������܂���");
	}

	//=================================================================================
	//Enum����
	//=================================================================================

	/// <summary>
	/// Enum�𐶐�����
	/// </summary>
	public static void Create(string enumName, List<string> itemNameList, string exportPath, string summary = "", string nameSpace = "", bool isFlags = false)
	{
		//������
		Init(nameSpace, summary, isFlags, enumName);

		//�萔���̍ő咷���擾���A�󔒐�������(�C�R�[�������Ԃ悤��)
		int nameLengthMax = 0;
		if (itemNameList.Count > 0)
		{
			nameLengthMax = itemNameList.Select(name => name.Length).Max();
		}

		//�e���ڂ�ݒ�
		for (int i = 0; i < itemNameList.Count; i++)
		{
			_code += _tab + itemNameList[i];
			_code += " " + String.Format("{0, " + (nameLengthMax - itemNameList[i].Length + 1).ToString() + "}", "=");

			if (isFlags)
			{
				_code += " 1 << " + i.ToString() + ",\n";
			}
			else
			{
				_code += " " + i.ToString() + ",\n";
			}
		}

		//�����o��
		Export(exportPath, nameSpace, enumName);
	}

	/// <summary>
	/// Enum�𐶐�����
	/// </summary>
	public static void Create(string enumName, Dictionary<string, int> itemDict, string exportPath, string summary = "", string nameSpace = "")
	{
		//������
		Init(nameSpace, summary, false, enumName);

		//�萔���̍ő咷���擾���A�󔒐�������
		int nameLengthMax = 0;
		if (itemDict.Keys.Count > 0)
		{
			nameLengthMax = itemDict.Keys.Select(name => name.Length).Max();
		}

		//�e���ڂ�ݒ�
		foreach (KeyValuePair<string, int> item in itemDict)
		{
			_code += _tab + item.Key;
			_code += " " + String.Format("{0, " + (nameLengthMax - item.Key.Length + 1).ToString() + "}", "=");
			_code += " " + item.Value.ToString() + ",\n";
		}

		//�����o��
		Export(exportPath, nameSpace, enumName);
	}


}