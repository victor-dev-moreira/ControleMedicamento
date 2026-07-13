using System;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Arquivos;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

public class TelaFuncionario : TelaBase<Funcionario>, ITelaOpcoes, ITelaCrud
{
    public TelaFuncionario(RepositorioBaseEmArquivo<Funcionario> repositorio) : base("Funcionario", repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Visualização de Funcionarios");
            Console.WriteLine("---------------------------------");
        }

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -16} | {3, -12}",
            "Id", "Nome", "Telefone", "CPF"
        );

        List<Funcionario> registros = repositorio.SelecionarTodos();

        foreach (Funcionario m in registros)
        {
            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -16} | {3, -12}",
                m.Id, m.Nome, m.Telefone, m.Cpf
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Funcionario ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do funcionario: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("---------------------------------");
        Console.Write("Digite o telefone do funcionario ((DDD) 90000-0000): ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("---------------------------------");
        Console.Write("Digite o CPF do funcionario: ");
        string cpf = Console.ReadLine() ?? string.Empty;

        return new Funcionario(nome, telefone, cpf);
    }

    protected override bool ExisteRegistroComInformacoesExclusivas(Funcionario entidade, int? idIgnorado = null)
    {
        List<Funcionario> registros = repositorio.SelecionarTodos();

        foreach (Funcionario f in registros)
        {
            if (f.Id != idIgnorado && f.Cpf == entidade.Cpf)
            {
                Console.WriteLine("---------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Já existe um funcionario cadastrado com o CPF informado.");
                Console.ResetColor();
                Console.WriteLine("---------------------------------");
                return true;
            }
        }

        return false;
    }
}
