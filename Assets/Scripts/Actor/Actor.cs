using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BokenshaChan
{
	public abstract class Actor : MonoBehaviour
	{
		[SerializeField] float moveSpeed = 1.0f;
		[SerializeField] Tilemap field;
		Rigidbody2D rb2D;
		public bool IsActing { get; protected set; }
		private void Awake()
		{
			rb2D = GetComponent<Rigidbody2D>();
			IsActing = false;
		}

		// ==============================
		// 移動関連の機能
		// ==============================
		public void Move(int xDir, int yDir)
		{
			StartCoroutine(MoveCoroutine(xDir, yDir));
		}
		protected IEnumerator MoveCoroutine(int xDir, int yDir)
		{
			if (IsActing) yield break;

			Vector3Int start = field.WorldToCell(transform.position);
			Vector3Int end = start + new Vector3Int(xDir, yDir, 0);

			// タイルの存在チェック
			if (!field.HasTile(end))
			{
				Debug.Log($"タイルがない場所です: {end}");
				yield break;
			}

			IsActing = true;
			Debug.Log($"スタート位置: {start} \n 目標地点: {end}");
			yield return StartCoroutine(SmoothMovement(end));
			IsActing = false;
		}

		/// <summary>
		/// 目標地点に時間をかけて移動
		/// </summary>
		/// <param name="end">移動先のセル</param>
		protected IEnumerator SmoothMovement(Vector3Int end)
		{
			Vector3 endPos = field.GetCellCenterWorld(end);
			while (Vector3.Distance(transform.position, endPos) > 0.01f)
			{
				Vector3 newPosition = Vector3.MoveTowards(
					transform.position,
					endPos,
					moveSpeed * Time.deltaTime
				);
				rb2D.MovePosition(newPosition);
				yield return null;
			}

			// 誤差を修正
			transform.position = endPos;
		}
	}
}