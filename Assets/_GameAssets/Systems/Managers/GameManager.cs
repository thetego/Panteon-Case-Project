

public class GameManager : Singleton<GameManager>
{

    public ObjectPooler<Unit> UnitPool;
    public Unit UnitPrefab;

    public override void Init()
    {
        UnitPool = new ObjectPooler<Unit>(UnitPrefab, 15);
    }
}
