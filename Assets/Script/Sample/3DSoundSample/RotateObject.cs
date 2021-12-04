using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
	/// <summary>
	/// ‰ñ“]‘¬“x
	/// </summary>
	[SerializeField]
	private float m_RotateSpeed = 5.0f;

	/// <summary>
	/// ‰ñ“]Šp“x
	/// </summary>
	private float m_RotateAngle = 0.0f;

	// Start is called before the first frame update
	void Start()
	{
		m_RotateAngle = 0.0f;
	}

	// Update is called once per frame
	void Update()
	{
		//‰ñ“]‘¬“x‚©‚ç‰ñ“]Šp“x‚ğ‹‚ß‚é
		m_RotateAngle = m_RotateSpeed * Time.deltaTime;
		//‰ñ“]Šp“x‚ğ”½‰f
		Quaternion rotate = Quaternion.Euler(0.0f, m_RotateAngle, 0.0f);
		this.transform.rotation *= rotate;
	}
}
