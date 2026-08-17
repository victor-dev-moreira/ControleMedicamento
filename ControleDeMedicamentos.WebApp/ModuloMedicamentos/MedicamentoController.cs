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
        List<ListarMedicamentoViewModel> viewModels = new List<ListarMedicamentoViewModel>();

        foreach (Medicamento m in medicamentos)
        {
            ListarMedicamentoViewModel vm = new ListarMedicamentoViewModel(
                m.Id,
                m.Nome,
                m.Descricao,
                m.QuantidadeEmEstoque,
                m.Fornecedor
            );
            viewModels.Add(vm);
        }
        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarTodos();

        ViewBag.Fornecedores = fornecedores;

        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMedicamentoViewModel cadastrarVm)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(cadastrarVm.FornecedorId);

        if (fornecedor == null)
            return NotFound();

        Medicamento medicamento = new Medicamento(cadastrarVm.Nome, cadastrarVm.Descricao, fornecedor);
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


        EditarMedicamentoViewModel viewModel = new EditarMedicamentoViewModel(
            id,
            medicamento.Nome,
            medicamento.Descricao,
            medicamento.Fornecedor.Id
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarMedicamentoViewModel editarVm)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(editarVm.FornecedorId);

        if (fornecedor == null)
            return NotFound();

        Medicamento medicamentoAtualizado = new Medicamento(editarVm.Nome, editarVm.Descricao, fornecedor);

        bool conseguiuEditar = repositorioMedicamento.Editar(editarVm.Id, medicamentoAtualizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }


    [HttpGet]

    public ActionResult Excluir(ExcluirMedicamentoViewModel excluirVm)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(excluirVm.Id);
        if (medicamento == null)
            return NotFound();

        ExcluirMedicamentoViewModel viewModelExcluir = new ExcluirMedicamentoViewModel(
            medicamento.Id,
            medicamento.Nome
        );

        return View(viewModelExcluir);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExclusao(ExcluirMedicamentoViewModel excluirVm)
    {
        bool conseguiuExcluir = repositorioMedicamento.Excluir(excluirVm.Id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}
