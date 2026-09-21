using UnityEngine;

public class EnemyWarningState : IEnemyState
{
    private Target enemy;
    private float tiempo;

    public EnemyWarningState(Target enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        tiempo = 0f;
    }

    public void Update()
    {
        tiempo += Time.deltaTime;

        float progreso = tiempo / enemy.WarningAtaque;

        enemy.SetColor(
            Color.Lerp(enemy.ColorOriginal, Color.red, progreso)
        );

        if (tiempo >= enemy.WarningAtaque)
        {
            enemy.Atacar();

            enemy.ChangeState(
                new EnemyNormalState(enemy)
            );
        }
    }

    public void Exit()
    {
        enemy.SetColor(enemy.ColorOriginal);
    }
}