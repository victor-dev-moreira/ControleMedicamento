using ControleDeMedicamentos.WebApp.ModuloMedicamentos;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class MedicamentoPrescrito
{
    public Medicamento Medicamento { get; set; } = null!;
    public int Quantidade { get; set; }

    public MedicamentoPrescrito() { }

    public MedicamentoPrescrito(Medicamento medicamento, int quantidade)
    {
        Medicamento = medicamento;
        Quantidade = quantidade;
    }
}