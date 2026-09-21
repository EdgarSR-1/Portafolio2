public interface IEnemyState
{
    // patron de diseño State, para manejar el comportamiento del enemigo en diferentes estados
    // enter es para inicializar el estado, update es para actualizar el estado y exit es para limpiar el estado
    void Enter();
    void Update();
    void Exit();
}