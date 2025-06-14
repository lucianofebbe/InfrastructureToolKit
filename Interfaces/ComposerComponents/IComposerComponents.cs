namespace InfrastructureToolKit.Interfaces.ComposerComponents
{
    // Interface base para componentes de composição (Composer), garantindo um método de limpeza/reset
    public interface IComposerComponents
    {
        // Método para resetar as configurações ou estados internos do componente
        void Clear();
    }
}
