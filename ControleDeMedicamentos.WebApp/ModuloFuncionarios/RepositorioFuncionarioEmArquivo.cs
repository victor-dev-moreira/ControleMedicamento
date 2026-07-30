using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
namespace ControleDeMedicamentos.WebApp.ModuloFuncionarios;

public class RepositorioPacienteEmArquivo : RepositorioBaseEmArquivo<Funcionario>
{
    public RepositorioPacienteEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }
    protected override List<Funcionario> ObterRegistros()
    {
        return contexto.Funcionarios;
    }
}
