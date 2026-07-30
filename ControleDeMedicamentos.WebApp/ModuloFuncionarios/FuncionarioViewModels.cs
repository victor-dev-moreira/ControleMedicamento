namespace ControleDeMedicamentos.WebApp.ModuloFuncionarios;

public record ListarFuncionarioViewModel(int Id, string Nome, string Telefone);

public record CadastrarFuncionarioViewModel(string Nome, string Telefone, string Cpf);
