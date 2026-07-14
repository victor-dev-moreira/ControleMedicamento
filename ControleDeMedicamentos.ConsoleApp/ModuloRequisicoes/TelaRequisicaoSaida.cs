using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes;

public class TelaRequisicaoSaida : TelaBase<RequisicaoSaida>, ITelaOpcoes, ITelaCrud
{
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
    private readonly RepositorioPacienteEmArquivo repositorioPaciente;

    public TelaRequisicaoSaida(
    RepositorioRequisicaoSaidaEmArquivo repositorioRequisicaoSaida,
    RepositorioMedicamentoEmArquivo repositorioMedicamento,
    RepositorioPacienteEmArquivo repositorioPaciente
    ) : base("Requisição de Saida", repositorioRequisicaoSaida)
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioPaciente = repositorioPaciente;
    }

    public override string? ObterOpcaoMenu()
    {
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Requisição de Saida");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Cadastrar Requisição de Saida");
        Console.WriteLine($"4 - Visualizar Requisições de Saida");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");

        string? opcaoMenuInterno = Console.ReadLine()?.ToUpper();
        return opcaoMenuInterno;
    }
    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Visualização de Requisições de Saida");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -10} | {3, -15} | {4, -25}",
            "Id", "Medicamento", "Qtd", "Data", "Paciente"
        );

        List<RequisicaoSaida> registros = repositorio.SelecionarTodos();

        foreach (RequisicaoSaida r in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -10} | {3, -15} | {4, -25}",
                r.Id,
                r.MedicamentoSaida.Nome,
                r.QuantidadeSaida,
                r.Data.ToShortDateString(),
                r.Paciente.Nome
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override RequisicaoSaida ObterDadosCadastrais()
    {
        VisualizarMedicamentos();

        Console.WriteLine("---------------------------------");

        Console.Write("Digite o ID do medicamento que deseja requisitar: ");
        int idMedicamento = Convert.ToInt32(Console.ReadLine());

        Medicamento medicamento = repositorioMedicamento.SelecionarPorId(idMedicamento)!;

        Console.Write("Digite a quantidade que deseja requisitar a saida: ");
        int quantidade = Convert.ToInt32(Console.ReadLine());

        VisualizarPaciente();

        Console.Write("Digite o ID do paciente que deseja requisitar: ");
        int idPaciente = Convert.ToInt32(Console.ReadLine());

        Paciente paciente = repositorioPaciente.SelecionarPorId(idPaciente)!;

        return new RequisicaoSaida(medicamento, quantidade, paciente);
    }

    private void VisualizarMedicamentos()
    {
        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -20}",
            "Id", "Nome", "Fornecedor", "Descrição"
        );

        List<Medicamento> registros = repositorioMedicamento.SelecionarTodos();

        foreach (Medicamento m in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -20} | {3, -20}",
                m.Id, m.Nome, m.Fornecedor.Nome, m.Descricao
            );
        }
    }

    public void VisualizarPaciente()
    {
        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -16} | {3, -16} | {4, -12}",
            "Id", "Nome", "Telefone", "Cartão SUS", "CPF"
        );

        List<Paciente> registros = repositorioPaciente.SelecionarTodos();

        foreach (Paciente m in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -16} | {3, -16} | {4, -12}",
                m.Id, m.Nome, m.Telefone, m.CartaoSus, m.Cpf
            );
        }
    }
}
