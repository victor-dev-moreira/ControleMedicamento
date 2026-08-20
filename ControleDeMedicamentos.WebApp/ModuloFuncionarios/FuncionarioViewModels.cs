using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionarios;

public record ListarFuncionarioViewModel(int Id, string Nome, string Telefone);

public record CadastrarFuncionarioViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "O campo \"Telefone\" deve estar no formato (DDD) 90000-0000.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"CPF\" é obrigatório.")]
    [RegularExpression(@"^\d{2}\.?\d{3}\.?\d{3}\/?\d{4}-?\d{2}$", ErrorMessage = "O campo \"CPF\" deve conter 14 dígitos.")]
    string Cpf);

public record EditarFuncionarioViewModel(
    int Id,

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" é obrigatório.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "O campo \"Telefone\" deve estar no formato (DDD) 90000-0000.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"CPF\" é obrigatório.")]
    [RegularExpression(@"^\d{2}\.?\d{3}\.?\d{3}\/?\d{4}-?\d{2}$", ErrorMessage = "O campo \"CPF\" deve conter 14 dígitos.")]
    string Cpf);

public record ExcluirFuncionarioViewModel(
    int Id,
    string Nome);

