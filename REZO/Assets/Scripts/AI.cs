using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
	[Header("Variables")]
	[HideInInspector]
	public float Speed;
	public float MaxTimeInput;

	[Header("Animations")]
	bool _moving;

	Transform _thisTransform;
	float xLocalScale;

	[Header("Lerp")]
	Vector2 _currentPos;
	Vector2 _targetPos;


	private void Awake()
	{
		_thisTransform = GetComponent<Transform>();
	}

	private void Start()
	{
		xLocalScale = _thisTransform.localScale.x;

		StartCoroutine(TimeBeforeNextInput());
	}

	private void Update()
	{
		if (_moving)
			Movement();
	}

	IEnumerator TimeBeforeNextInput()
	{
		_moving = false;
		yield return new WaitForSeconds(Random.Range(0, MaxTimeInput));
		PlayInput();
	}

	void PlayInput()
	{
		_currentPos = _thisTransform.position;

		float x = Random.Range(GameMaster.Area.xMin, GameMaster.Area.xMax);
		float y = Random.Range(GameMaster.Area.yMin, GameMaster.Area.yMax);
		_targetPos = new Vector2(x, y);

		if (_targetPos.x < _currentPos.x)
			_thisTransform.localScale = new Vector3(-xLocalScale, _thisTransform.localScale.y, _thisTransform.localScale.z);
		else
			_thisTransform.localScale = new Vector3(xLocalScale, _thisTransform.localScale.y, _thisTransform.localScale.z);

		_moving = true;
	}

	void Movement()
	{
		float step = Speed * Time.deltaTime;
		_thisTransform.position = Vector3.MoveTowards(_thisTransform.position, _targetPos, step);

		if(Vector3.Distance(_thisTransform.position,_targetPos)<.01f)
		{
			_moving = false;
			StartCoroutine(TimeBeforeNextInput());
		}
	}
}
