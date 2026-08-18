namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public record ListarPacienteViewModel(int Id, string Nome, string Telefone, string CartaoSus);

public record CadastrarPacienteViewModel(int Id, string Nome, string Telefone, string CartaoSus, string Cpf);

public record EditarPacienteViewModel(int Id, string Nome, string Telefone, string CartaoSus, string Cpf);

public record ExcluirPacienteViewModel(int Id, string Nome);

