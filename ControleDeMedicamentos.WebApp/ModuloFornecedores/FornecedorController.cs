using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedores;

public sealed class FornecedorController : Controller
{
    private readonly RepositorioFornecedorEmArquivo repositorio;
    public FornecedorController()
    {
        ContextoJson contextoJson = new ContextoJson();
        contextoJson.Carregar();
        repositorio = new RepositorioFornecedorEmArquivo(contextoJson);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Fornecedor> fornecedores = repositorio.SelecionarTodos();
        return View(fornecedores);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(string nome, string telefone, string cnpj)
    {
        Fornecedor fornecedor = new Fornecedor(nome, telefone, cnpj);
        repositorio.Cadastrar(fornecedor);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(int id)
    {
        Fornecedor fornecedor = repositorio.SelecionarPorId(id);
        if (fornecedor == null)
            return NotFound();

        return View(fornecedor);
    }

    [HttpPost]
    public ActionResult Editar(int id, string nome, string telefone, string cnpj)
    {
        Fornecedor fornecedorAtulizado = new Fornecedor(nome, telefone, cnpj);

        bool conseguiuEditar = repositorio.Editar(id, fornecedorAtulizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]

    public ActionResult Excluir(int id)
    {
        Fornecedor fornecedor = repositorio.SelecionarPorId(id);
        if (fornecedor == null)
            return NotFound();

        return View(fornecedor);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExclusao(int id)
    {
        bool conseguiuExcluir = repositorio.Excluir(id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}
