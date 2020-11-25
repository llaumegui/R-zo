using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	float _xLocalScale;
	Transform _thisTransform;
	Vector2 _targetPos;
	[HideInInspector]
	public float Speed;
	bool _moving;

	private void Start()
	{
		_thisTransform = transform;
		_xLocalScale = _thisTransform.localScale.x;
	}

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			_targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (_targetPos.x < _thisTransform.position.x)
				_thisTransform.localScale = new Vector3(-_xLocalScale, _thisTransform.localScale.y, _thisTransform.localScale.z);
			else
				_thisTransform.localScale = new Vector3(_xLocalScale, _thisTransform.localScale.y, _thisTransform.localScale.z);

			_moving = true;
		}

		if(Input.GetMouseButtonDown(1))
		{
			_moving = false;
			_targetPos = _thisTransform.position;
		}

		if (_moving)
			Movement();
	}

	void Movement()
	{
		float step = Speed * Time.deltaTime;
		_thisTransform.position = Vector3.MoveTowards(_thisTransform.position, _targetPos, step);

		if (Vector3.Distance(_thisTransform.position, _targetPos) < .01f)
		{
			_moving = false;
		}
	}
}
