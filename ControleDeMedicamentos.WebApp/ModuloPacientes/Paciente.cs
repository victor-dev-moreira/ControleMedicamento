using System.Text.RegularExpressions;
using ControleDeMedicamentos.WebApp.Compartilhado;
namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public class Paciente : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CartaoSus { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public Paciente()
    {
    }
    public Paciente(string nome, string telefone, string cartaoSus, string cpf) : this()
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSus = cartaoSus;
        Cpf = cpf;
    }
    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Paciente pacienteAtualizado = (Paciente)entidadeAtualizada;
        Nome = pacienteAtualizado.Nome;
        Telefone = pacienteAtualizado.Telefone;
        CartaoSus = pacienteAtualizado.CartaoSus;
        Cpf = pacienteAtualizado.Cpf;
    }
    public override List<string> Validar()
    {
        List<string> erros = new List<string>();
        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" tem que ter entre 3 e 100 caracteres!");

        if (string.IsNullOrWhiteSpace(Telefone) || !Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
            erros.Add("O campo \"Telefone\" deve estar no formato (DDD) 90000-0000.");

        if (string.IsNullOrWhiteSpace(CartaoSus) || CartaoSus.Length != 15)
            erros.Add("O campo \"Cartão SUS\" deve ter 15 digitos!");

        if (string.IsNullOrWhiteSpace(Cpf) || !Regex.IsMatch(Cpf, @"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$"))
            erros.Add("O campo \"CPF\" deve conter 11 dígitos.");

        return erros;
    }
}
