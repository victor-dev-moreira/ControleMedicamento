using ControleDeMedicamentos.WebApp.ModuloFornecedores;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos;

public record ListarMedicamentoViewModel(int Id, string Nome, string Descricao, int QuantidadeEmEstoque, Fornecedor FornecedorM);

public record CadastrarMedicamentoViewModel(
    string Nome,
    string Descricao,
    int FornecedorId
);

public record EditarMedicamentoViewModel(
    int Id,
    string Nome,
    string Descricao,
    int FornecedorId
);

public record ExcluirMedicamentoViewModel(
    int Id,
    string Nome
);
