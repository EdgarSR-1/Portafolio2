using UnityEngine;

public class EnemyNormalState : IEnemyState
{
    // script para el estado normal del enemigo, implementa la interfaz IEnemyState
    // usa el patrón de diseño State para manejar el comportamiento del enemigo no esta atacando
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