using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
namespace ControleDeMedicamentos.WebApp.ModuloFuncionarios;

public class RepositorioFuncionarioEmArquivo : RepositorioBaseEmArquivo<Funcionario>
{
    public RepositorioFuncionarioEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }
    protected override List<Funcionario> ObterRegistros()
    {
        return contexto.Funcionarios;
    }
}
