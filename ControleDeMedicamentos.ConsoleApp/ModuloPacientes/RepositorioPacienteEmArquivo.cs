using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;
namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

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
