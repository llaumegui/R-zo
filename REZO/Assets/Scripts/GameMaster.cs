using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
	public float Speed;
	public List<GameObject> AiList;
	public Transform StockAI;
	public int Npcs;

	public Vector2 AreaSize;

	public static Rect Area;

	Transform _player;

	private void Awake()
	{
		/*Camera mainCam = Camera.main;

		float halfHeight = mainCam.orthographicSize;
		float halfWidth = mainCam.aspect * halfHeight;

		Area = new Rect(mainCam.transform.position.x - halfWidth, mainCam.transform.position.y - halfHeight, halfWidth * 2, halfHeight * 2);*/
		Area = new Rect(Camera.main.transform.position.x - AreaSize.x/2, Camera.main.transform.position.y - AreaSize.y/2, AreaSize.x, AreaSize.y);

		GenerateNpcs();
	}

	void GenerateNpcs()
	{
		for(int i=0;i<Npcs;i++)
		{
			GameObject instance = Instantiate(AiList[Random.Range(0, AiList.Count)], new Vector2(Random.Range(GameMaster.Area.xMin, GameMaster.Area.xMax), Random.Range(GameMaster.Area.yMin, GameMaster.Area.yMax)), Quaternion.identity);
			instance.GetComponent<AI>().Speed = Speed;
			instance.transform.parent = StockAI;	
		}

		GameObject player = Instantiate(AiList[Random.Range(0, AiList.Count)], new Vector2(Random.Range(GameMaster.Area.xMin, GameMaster.Area.xMax), Random.Range(GameMaster.Area.yMin, GameMaster.Area.yMax)), Quaternion.identity);
		player.GetComponent<AI>().enabled = false;
		player.name = "Player";
		Player script = player.AddComponent<Player>();
		script.Speed = Speed;
		_player = player.transform;

	}



	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(new Vector2(Area.xMin, 0), new Vector2(Area.xMax, 0));
		Gizmos.DrawLine(new Vector2(0, Area.yMin), new Vector2(0, Area.yMax));

		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(Camera.main.transform.position, AreaSize);

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(_player.position, Vector3.one);	
	}
}
