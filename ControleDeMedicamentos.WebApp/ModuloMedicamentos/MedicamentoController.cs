using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFornecedores;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos;

public sealed class MedicamentoController : Controller
{
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
    private readonly RepositorioFornecedorEmArquivo repositorioFornecedor;

    public MedicamentoController()
    {
        ContextoJson contextoJson = new ContextoJson();
        contextoJson.Carregar();
        repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contextoJson);
        repositorioFornecedor = new RepositorioFornecedorEmArquivo(contextoJson);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();
        return View(medicamentos);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarTodos();

        ViewBag.Fornecedores = fornecedores;

        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(string nome, string descricao, int fornecedorId)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(fornecedorId);

        if (fornecedor == null)
            return NotFound();

        Medicamento medicamento = new Medicamento(nome, descricao, fornecedor);
        repositorioMedicamento.Cadastrar(medicamento);

        return RedirectToAction(nameof(Listar));
    }


    [HttpGet]
    public ActionResult Editar(int id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);
        if (medicamento == null)
            return NotFound();

        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarTodos();

        ViewBag.Fornecedores = fornecedores;

        return View(medicamento);
    }

    [HttpPost]
    public ActionResult Editar(int id, string nome, string descricao, int fornecedorId)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(fornecedorId);

        if (fornecedor == null)
            return NotFound();

        Medicamento medicamentoAtualizado = new Medicamento(nome, descricao, fornecedor);

        bool conseguiuEditar = repositorioMedicamento.Editar(id, medicamentoAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }


    [HttpGet]

    public ActionResult Excluir(int id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);
        if (medicamento == null)
            return NotFound();

        return View(medicamento);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExclusao(int id)
    {
        bool conseguiuExcluir = repositorioMedicamento.Excluir(id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}
