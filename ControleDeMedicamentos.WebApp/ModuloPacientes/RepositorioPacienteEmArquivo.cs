using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public class RepositorioPacienteEmArquivo : RepositorioBaseEmArquivo<Paciente>
{
    public RepositorioPacienteEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Paciente> ObterRegistros()
    {
        return contexto.Pacientes;
    }
}
