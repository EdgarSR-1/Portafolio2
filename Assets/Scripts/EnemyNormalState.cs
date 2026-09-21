using UnityEngine;

public class EnemyNormalState : IEnemyState
{
    private Target enemy;
    private float tiempo;

    public EnemyNormalState(Target enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        tiempo = 0f;
        enemy.SetColor(enemy.ColorOriginal);
    }

    public void Update()
    {
        tiempo += Time.deltaTime;

        if (tiempo >= enemy.TiempoAtaque - enemy.WarningAtaque)
        {
            enemy.ChangeState(
                new EnemyWarningState(enemy)
            );
        }
    }

    public void Exit()
    {
    }
}