namespace ControleDeMedicamentos.WebApp.ModuloFornecedores;

public record ListarFornecedorViewModel(int Id, string Nome, string Telefone, string Cnpj);

public record CadastrarFornecedorViewModel(
    string Nome,
    string Telefone,
    string Cnpj
);

public record EditarFornecedorViewModel(
    int Id,
    string Nome,
    string Telefone,
    string Cnpj
);

public record ExcluirFornecedorViewModel(
    int Id,
    string Nome
);
