public class EnemyDeadState : IEnemyState
{
    private Target enemy;

    public EnemyDeadState(Target enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        enemy.IniciarMuerte();
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}