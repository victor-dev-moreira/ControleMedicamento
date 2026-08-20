using System.ComponentModel.DataAnnotations;
using ControleDeMedicamentos.WebApp.ModuloFornecedores;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos;

public record ListarMedicamentoViewModel(int Id, string Nome, string Descricao, int QuantidadeEmEstoque, Fornecedor FornecedorM);

public record FornecedorMedicamentoViewModel(
    int Id,
    string Nome
);
public record CadastrarMedicamentoViewModel(

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Descrição\" é obrigatório.")]
    [StringLength(255, MinimumLength = 5, ErrorMessage = "O campo \"Descrição\" deve conter entre 5 e 255 caracteres.")]
    string Descricao,
    int FornecedorId
)
{
    public List<FornecedorMedicamentoViewModel> Fornecedores { get; init; } = [];
};

public record EditarMedicamentoViewModel(
    int Id,

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Descrição\" é obrigatório.")]
    [StringLength(255, MinimumLength = 5, ErrorMessage = "O campo \"Descrição\" deve conter entre 5 e 255 caracteres.")]
    string Descricao,
    int FornecedorId
)
{
    public List<FornecedorMedicamentoViewModel> Fornecedores { get; init; } = [];
};

public record ExcluirMedicamentoViewModel(
    int Id,
    string Nome
);
