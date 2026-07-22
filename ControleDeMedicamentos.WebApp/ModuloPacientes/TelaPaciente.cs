using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public class TelaPaciente : TelaBase<Paciente>, ITelaOpcoes, ITelaCrud
{
    public TelaPaciente(RepositorioBaseEmArquivo<Paciente> repositorio) : base("Paciente", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Visualização de Pacientes");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -16} | {3, -16} | {4, -12}",
            "Id", "Nome", "Telefone", "Cartão SUS", "CPF"
        );

        List<Paciente> registros = repositorio.SelecionarTodos();

        foreach (Paciente m in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -16} | {3, -16} | {4, -12}",
                m.Id, m.Nome, m.Telefone, m.CartaoSus, m.Cpf
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Paciente ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do paciente: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("---------------------------------");
        Console.Write("Digite o telefone do paciente ((DDD) 90000-0000): ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("---------------------------------");
        Console.Write("Digite o numero do cartão do SUS: ");
        string cartaoSUS = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("---------------------------------");
        Console.Write("Digite o CPF do paciente: ");
        string cpf = Console.ReadLine() ?? string.Empty;

        return new Paciente(nome, telefone, cartaoSUS, cpf);
    }

    protected override bool ExisteRegistroComInformacoesExclusivas(Paciente entidade, int? idIgnorado = null)
    {
        List<Paciente> registros = repositorio.SelecionarTodos();

        foreach (Paciente f in registros)
        {
            if (f.Id != idIgnorado && f.Cpf == entidade.Cpf)
            {
                Console.WriteLine("---------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Já existe um paciente cadastrado com o CPF informado.");
                Console.ResetColor();
                Console.WriteLine("---------------------------------");
                return true;
            }

            if (f.Id != idIgnorado && f.CartaoSus == entidade.CartaoSus)
            {
                Console.WriteLine("---------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Já existe um paciente cadastrado com o Cartão SUS informado.");
                Console.ResetColor();
                Console.WriteLine("---------------------------------");
                return true;
            }
        }

        return false;
    }
}
