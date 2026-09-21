public class EnemyDefeatState : IEnemyState
{
    // script para el estado de muerte del enemigo, implementa la interfaz IEnemyState
    // usa el patrón de diseño State para manejar el comportamiento del enemigo es vencido
    private Target enemy;

    public EnemyDefeatState(Target enemy)
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