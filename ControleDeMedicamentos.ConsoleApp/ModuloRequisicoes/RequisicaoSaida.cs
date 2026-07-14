using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamentos;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoes;

public class RequisicaoSaida : EntidadeBase
{
    public Medicamento MedicamentoSaida { get; set; } = null!;
    public DateTime Data { get; set; } = DateTime.Now;
    public Paciente Paciente { get; set; } = null!;
    public int QuantidadeSaida { get; set; }

    public RequisicaoSaida()
    {
    }
    public RequisicaoSaida(Medicamento medicamentoSaida, int quantidadeSaida, Paciente paciente) : this()
    {
        MedicamentoSaida = medicamentoSaida;
        QuantidadeSaida = quantidadeSaida;
        Paciente = paciente;
    }
    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        RequisicaoSaida requisicaoAtualizada = (RequisicaoSaida)entidadeAtualizada;

        MedicamentoSaida = requisicaoAtualizada.MedicamentoSaida;
        QuantidadeSaida = requisicaoAtualizada.QuantidadeSaida;
        Paciente = requisicaoAtualizada.Paciente;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (Paciente == null)
            erros.Add("O campo \"Paciente\" deve ser preenchido.");

        if (MedicamentoSaida == null)
            erros.Add("O campo \"Medicamento\" deve ser preenchido.");

        else if (QuantidadeSaida > MedicamentoSaida.QuantidadeEmEstoque)
            erros.Add($"A quantidade solicitada excede o estoque disponível ({MedicamentoSaida.QuantidadeEmEstoque} unidades).");

        if (QuantidadeSaida <= 0)
            erros.Add("O campo \"Quantidade\" deve ser maior que zero.");

        return erros;
    }
}
