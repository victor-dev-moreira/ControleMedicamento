namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public record MedicamentoRequisicaoEntradaViewModel(
    int Id,
    string Nome
);

public record FuncionarioRequisicaoEntradaViewModel(
    int Id,
    string Nome
);
public record ListarRequisicaoEntradaViewModel(
    int Id,
    string NomeMedicamento,
    string NomeFuncionario,
    int Quantidade,
    DateTime Data
);

public record CadastrarRequisicaoEntradaViewModel(
    int MedicamentoId,
    int FuncionarioId,
    int Quantidade
)
{
    public List<MedicamentoRequisicaoEntradaViewModel> Medicamentos { get; init; } = [];
    public List<FuncionarioRequisicaoEntradaViewModel> Funcionarios { get; init; } = [];
}
