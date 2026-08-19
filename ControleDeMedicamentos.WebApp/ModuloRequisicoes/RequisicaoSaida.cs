using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using ControleDeMedicamentos.WebApp.ModuloPacientes;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class RequisicaoSaida : EntidadeBase
{
    public List<MedicamentoPrescrito> MedicamentosPrescritos { get; set; } = null!;
    public DateTime Data { get; set; } = DateTime.Now;
    public Paciente Paciente { get; set; } = null!;
    public RequisicaoSaida()
    {
    }
    public RequisicaoSaida(Paciente paciente, List<MedicamentoPrescrito> medicamentosPrescritos) : this()
    {
        MedicamentosPrescritos = medicamentosPrescritos;
        Paciente = paciente;

        foreach (MedicamentoPrescrito mp in MedicamentosPrescritos)
            mp.Medicamento.RegistrarRequisicaoSaida(this);

    }
    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        RequisicaoSaida requisicaoAtualizada = (RequisicaoSaida)entidadeAtualizada;

        MedicamentosPrescritos = requisicaoAtualizada.MedicamentosPrescritos;
        Paciente = requisicaoAtualizada.Paciente;
    }
    public int ObterQuantidade(Medicamento medicamento)
    {
        foreach (MedicamentoPrescrito mp in MedicamentosPrescritos)
        {
            if (mp.Medicamento.Id == medicamento.Id)
                return mp.Quantidade;
        }

        return 0;
    }
    public override List<string> Validar()
    {
        List<string> erros = [];

        if (Paciente == null)
            erros.Add("O campo \"Paciente\" deve ser preenchido.");

        if (MedicamentosPrescritos.Count == 0)
            erros.Add("É necessário selecionar ao menos um medicamento.");

        foreach (MedicamentoPrescrito mp in MedicamentosPrescritos)
        {
            if (mp.Medicamento == null)
            {
                erros.Add("O campo \"Medicamento\" deve ser preenchido.");
            }
            else
            {
                if (mp.Quantidade <= 0)
                    erros.Add($"A \"Quantidade\" do medicamento \"{mp.Medicamento.Nome}\" deve ser maior que zero.");

                if (mp.Medicamento.QuantidadeEmEstoque < 0)
                    erros.Add($"Não há estoque suficiente para o medicamento \"{mp.Medicamento.Nome}\".");
            }
        }

        return erros;
    }


}
