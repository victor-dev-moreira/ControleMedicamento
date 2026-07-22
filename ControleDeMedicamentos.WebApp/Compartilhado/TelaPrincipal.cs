using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFornecedores;
using ControleDeMedicamentos.WebApp.ModuloFuncionarios;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using ControleDeMedicamentos.WebApp.ModuloPacientes;
using ControleDeMedicamentos.WebApp.ModuloRequisicoes;

namespace ControleDeMedicamentos.WebApp.Compartilhado;

public class TelaPrincipal
{
    private readonly TelaFornecedor telaFornecedor;
    private readonly TelaMedicamento telaMedicamento;
    private readonly TelaRequisicaoEntrada telaRequisicaoEntrada;
    private readonly TelaPaciente telaPaciente;
    private readonly TelaFuncionario telaFuncionario;
    private readonly TelaRequisicaoSaida telaRequisicaoSaida;
    public TelaPrincipal(ContextoJson contexto)
    {
        RepositorioRequisicaoSaidaEmArquivo repositorioRequisicaoSaida = new RepositorioRequisicaoSaidaEmArquivo(contexto);
        RepositorioFornecedorEmArquivo repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        RepositorioMedicamentoEmArquivo repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
        RepositorioRequisicaoEntradaEmArquivo repositorioRequisicao = new RepositorioRequisicaoEntradaEmArquivo(contexto);
        RepositorioPacienteEmArquivo repositorioPacienteEmArquivo = new RepositorioPacienteEmArquivo(contexto);
        RepositorioFuncionarioEmArquivo repositorioFuncionarioEmArquivo = new RepositorioFuncionarioEmArquivo(contexto);

        telaRequisicaoSaida = new TelaRequisicaoSaida(repositorioRequisicaoSaida, repositorioMedicamento, repositorioPacienteEmArquivo);
        telaFuncionario = new TelaFuncionario(repositorioFuncionarioEmArquivo);
        telaPaciente = new TelaPaciente(repositorioPacienteEmArquivo);
        telaFornecedor = new TelaFornecedor(repositorioFornecedor);
        telaMedicamento = new TelaMedicamento(repositorioMedicamento, repositorioFornecedor);
        telaRequisicaoEntrada = new TelaRequisicaoEntrada(repositorioRequisicao, repositorioMedicamento);
    }

    public ITelaOpcoes? ObterOpcaoMenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Controle de Medicamentos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Gestão de Fornecedores");
        Console.WriteLine("2 - Gestão de Medicamentos");
        Console.WriteLine("3 - Gestão de Requisições de Entrada");
        Console.WriteLine("4 - Gestão de Requisições de Saida");
        Console.WriteLine("5 - Gestão de Pacientes");
        Console.WriteLine("6 - Gestão de Funcionarios");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");

        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

        if (opcaoMenuPrincipal == "1")
            return telaFornecedor;

        if (opcaoMenuPrincipal == "2")
            return telaMedicamento;

        if (opcaoMenuPrincipal == "3")
            return telaRequisicaoEntrada;

        if (opcaoMenuPrincipal == "4")
            return telaRequisicaoSaida;

        if (opcaoMenuPrincipal == "5")
            return telaPaciente;

        if (opcaoMenuPrincipal == "6")
            return telaFuncionario;
        return null;
    }
}
