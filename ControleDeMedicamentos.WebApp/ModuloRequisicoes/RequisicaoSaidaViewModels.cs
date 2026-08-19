namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public record ListarMedicamentoPrescritoRequisicaoSaidaViewModel(
    int Id,
    string Nome,
    int Quantidade
);

public record PacienteRequisicaoSaidaViewModel(
    int Id,
    string Nome
);
public record ListarRequisicaoSaidaViewModel(
    int Id,
    string NomePaciente,
    List<ListarMedicamentoPrescritoRequisicaoSaidaViewModel> MedicamentosPrescritos,
    DateTime Data
);
public record MedicamentoPrescritoRequisicaoSaidaViewModel(
    int MedicamentoId,
    string NomeMedicamento,
    int QuantidadeEmEstoque,
    bool Selecionado,
    int Quantidade
);
public record CadastrarRequisicaoSaidaViewModel(int PacienteId)
{
    public List<MedicamentoPrescritoRequisicaoSaidaViewModel> MedicamentosPrescritos { get; init; } = [];
    public List<PacienteRequisicaoSaidaViewModel> Pacientes { get; init; } = [];
}
