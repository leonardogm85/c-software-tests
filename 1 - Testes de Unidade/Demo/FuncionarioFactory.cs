namespace Demo
{
    public class FuncionarioFactory
    {
        public static Funcionario Criar(string nome, double salario) => new Funcionario(nome, salario);
    }
}
