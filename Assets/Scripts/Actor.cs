using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// グリッドセルの基本情報を保持する構造体
public struct GridPosition
{
	public int X { get; }
	public int Y { get; }

	public GridPosition(int x, int y)
	{
		X = x;
		Y = y;
	}

	public Vector3 ToWorldPosition(float cellSize = 1f)
	{
		return new Vector3(X * cellSize, Y * cellSize, 0);
	}

	public static GridPosition FromWorldPosition(Vector3 worldPosition, float cellSize = 1f)
	{
		return new GridPosition(
			Mathf.RoundToInt(worldPosition.x / cellSize),
			Mathf.RoundToInt(worldPosition.y / cellSize)
		);
	}
}

// グリッドシステムを管理するクラス
public class GridSystem : MonoBehaviour
{
	[SerializeField] private float cellSize = 1f;
	private Dictionary<GridPosition, GridCell> gridCells = new Dictionary<GridPosition, GridCell>();

	// グリッドの初期化
	public void InitializeGrid(int width, int height)
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var position = new GridPosition(x, y);
				gridCells[position] = new GridCell(position);
			}
		}
	}

	// グリッドセルの取得
	public GridCell GetCell(GridPosition position)
	{
		return gridCells.TryGetValue(position, out var cell) ? cell : null;
	}
}

// グリッドセルの情報を管理するクラス
public class GridCell
{
	public GridPosition Position { get; }
	public bool IsWalkable { get; set; } = true;
	public EntityBase OccupyingEntity { get; private set; }
	public TileType TileType { get; set; }

	// セルの追加情報（アイテム、罠など）
	private List<IGridComponent> components = new List<IGridComponent>();

	public GridCell(GridPosition position)
	{
		Position = position;
	}

	public bool SetEntity(EntityBase entity)
	{
		if (!IsWalkable || OccupyingEntity != null) return false;
		OccupyingEntity = entity;
		return true;
	}

	public void RemoveEntity()
	{
		OccupyingEntity = null;
	}

	public void AddComponent(IGridComponent component)
	{
		components.Add(component);
	}

	public T GetComponent<T>() where T : IGridComponent
	{
		return components.OfType<T>().FirstOrDefault();
	}
}

// エンティティの基底クラス
public abstract class EntityBase : MonoBehaviour
{
	protected GridPosition currentPosition;
	protected GridSystem gridSystem;

	public virtual bool TryMove(GridPosition newPosition)
	{
		var targetCell = gridSystem.GetCell(newPosition);
		if (targetCell == null || !targetCell.IsWalkable || targetCell.OccupyingEntity != null)
			return false;

		// 現在のセルから削除
		var currentCell = gridSystem.GetCell(currentPosition);
		currentCell?.RemoveEntity();

		// 新しいセルに設定
		targetCell.SetEntity(this);
		currentPosition = newPosition;

		// World座標の更新
		transform.position = newPosition.ToWorldPosition(gridSystem.CellSize);

		return true;
	}

	public virtual void Initialize(GridSystem grid, GridPosition startPosition)
	{
		gridSystem = grid;
		currentPosition = startPosition;
		transform.position = startPosition.ToWorldPosition(gridSystem.CellSize);

		var cell = gridSystem.GetCell(startPosition);
		cell?.SetEntity(this);
	}
}

// プレイヤークラスの例
public class Player : EntityBase
{
	public PlayerStats Stats { get; private set; }
	public Inventory Inventory { get; private set; }

	protected override void Awake()
	{
		base.Awake();
		Stats = new PlayerStats();
		Inventory = new Inventory();
	}

	public override bool TryMove(GridPosition newPosition)
	{
		var success = base.TryMove(newPosition);
		if (success)
		{
			// 移動後の処理（アイテム取得、トラップ発動など）
			var cell = gridSystem.GetCell(newPosition);
			var items = cell.GetComponent<ItemComponent>();
			items?.OnPlayerEnter(this);
		}
		return success;
	}
}

// グリッドコンポーネントのインターフェース
public interface IGridComponent
{
	void OnPlayerEnter(Player player);
}

// アイテムコンポーネントの例
public class ItemComponent : IGridComponent
{
	public Item Item { get; }

	public ItemComponent(Item item)
	{
		Item = item;
	}

	public void OnPlayerEnter(Player player)
	{
		player.Inventory.AddItem(Item);
	}
}
