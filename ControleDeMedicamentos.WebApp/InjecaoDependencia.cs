using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFornecedores;
using ControleDeMedicamentos.WebApp.ModuloFuncionarios;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using ControleDeMedicamentos.WebApp.ModuloPacientes;
using ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public static class InjecaoDependencia
{
    public static void AddInfraestruturaEmJson(this IServiceCollection services)
    {
        // builder.Services.AddSingleton(); // Adicionar e injetar apenas UMA instância requisitada

        // Delegates

        // builder.Services.AddTransient();  // Adicionar e injetar uma instância por ocorrência da dependência

        // Expressão lambda
        services.AddScoped(_ =>
        {
            ContextoJson contexto = new ContextoJson();

            contexto.Carregar();

            return contexto;
        });

        services.AddScoped<RepositorioFornecedorEmArquivo>();  // Adicionar e injetar uma instância por requisição/conexão
        services.AddScoped<RepositorioPacienteEmArquivo>();  // Adicionar e injetar uma instância por requisição/conexão
        services.AddScoped<RepositorioFuncionarioEmArquivo>();  // Adicionar e injetar uma instância por requisição/conexão
        services.AddScoped<RepositorioRequisicaoSaidaEmArquivo>();  // Adicionar e injetar uma instância por requisição/conexão
        services.AddScoped<RepositorioMedicamentoEmArquivo>();  // Adicionar e injetar uma instância por requisição/conexão
        services.AddScoped<RepositorioRequisicaoEntradaEmArquivo>();  // Adicionar e injetar uma instância por requisição/conexão
    }
}
