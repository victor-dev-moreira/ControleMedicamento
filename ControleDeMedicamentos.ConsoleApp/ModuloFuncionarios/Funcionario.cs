using System.Text.RegularExpressions;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;

public class Funcionario : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public Funcionario()
    {
    }
    public Funcionario(string nome, string telefone, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        Cpf = cpf;
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Funcionario funcionarioAtualizado = (Funcionario)entidadeAtualizada;
        Nome = funcionarioAtualizado.Nome;
        Telefone = funcionarioAtualizado.Telefone;
        Cpf = funcionarioAtualizado.Cpf;
    }
    public override List<string> Validar()
    {
        List<string> erros = new List<string>();
        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" tem que ter entre 3 e 100 caracteres!");

        if (string.IsNullOrWhiteSpace(Telefone) || !Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
            erros.Add("O campo \"Telefone\" deve estar no formato (DDD) 90000-0000.");

        if (string.IsNullOrWhiteSpace(Cpf) || !Regex.IsMatch(Cpf, @"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$"))
            erros.Add("O campo \"CPF\" deve conter 11 dígitos.");

        return erros;
    }
}
