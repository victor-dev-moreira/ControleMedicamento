using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionarios;

public sealed class FuncionarioController : Controller
{
    private readonly RepositorioFuncionarioEmArquivo repositorioFuncionario;
    public FuncionarioController()
    {
        ContextoJson contextoJson = new ContextoJson();
        contextoJson.Carregar();
        repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextoJson);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarTodos();
        return View(funcionarios);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(string nome, string telefone, string cpf)
    {
        Funcionario funcionarios = new Funcionario(nome, telefone, cpf);
        repositorioFuncionario.Cadastrar(funcionarios);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(int id)
    {
        Funcionario funcionarios = repositorioFuncionario.SelecionarPorId(id);
        if (funcionarios == null)
            return NotFound();

        return View(funcionarios);
    }

    [HttpPost]
    public ActionResult Editar(int id, string nome, string telefone, string cpf)
    {
        Funcionario funcionarioAtulizado = new Funcionario(nome, telefone, cpf);

        bool conseguiuEditar = repositorioFuncionario.Editar(id, funcionarioAtulizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]

    public ActionResult Excluir(int id)
    {
        Funcionario funcionarios = repositorioFuncionario.SelecionarPorId(id);
        if (funcionarios == null)
            return NotFound();

        return View(funcionarios);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExclusao(int id)
    {
        bool conseguiuExcluir = repositorioFuncionario.Excluir(id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}
