using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFornecedores;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamentos;

public sealed class MedicamentoController : Controller
{
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
    private readonly RepositorioFornecedorEmArquivo repositorioFornecedor;

    public MedicamentoController(
        RepositorioMedicamentoEmArquivo repositorioMedicamento,
        RepositorioFornecedorEmArquivo repositorioFornecedor)
    {
        this.repositorioFornecedor = repositorioFornecedor;
        this.repositorioMedicamento = repositorioMedicamento;
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
        CadastrarMedicamentoViewModel viewModel = new CadastrarMedicamentoViewModel(
          string.Empty,
          string.Empty,
           0
       ) with
        { Fornecedores = ObterFornecedores() };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMedicamentoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            cadastrarVm = cadastrarVm with
            { Fornecedores = ObterFornecedores() };
            return View(cadastrarVm);
        }

        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(cadastrarVm.FornecedorId);
        if (fornecedor == null)
            return NotFound();

        Medicamento medicamento = new Medicamento(
            cadastrarVm.Nome ?? string.Empty,
            cadastrarVm.Descricao ?? string.Empty,
            fornecedor
            );

        repositorioMedicamento.Cadastrar(medicamento);

        return RedirectToAction(nameof(Listar));
    }


    [HttpGet]
    public ActionResult Editar(int id)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(id);
        if (medicamento == null)
            return NotFound();

        EditarMedicamentoViewModel viewModel = new EditarMedicamentoViewModel(
            id,
            medicamento.Nome,
            medicamento.Descricao,
            medicamento.Fornecedor.Id
        ) with
        {
            Fornecedores = ObterFornecedores()
        };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarMedicamentoViewModel editarVm)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(editarVm.FornecedorId);

        if (fornecedor == null)
            return NotFound();

        Medicamento medicamentoAtualizado = new Medicamento(
            editarVm.Nome ?? string.Empty,
            editarVm.Descricao ?? string.Empty,
            fornecedor
            );

        if (!ModelState.IsValid)
            return View(editarVm);

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

    private List<FornecedorMedicamentoViewModel> ObterFornecedores()
    {
        List<FornecedorMedicamentoViewModel> fornecedores = [];

        foreach (Fornecedor fornecedor in repositorioFornecedor.SelecionarTodos())
        {
            FornecedorMedicamentoViewModel viewModelFornecedor = new FornecedorMedicamentoViewModel(
                fornecedor.Id,
                fornecedor.Nome
            );

            fornecedores.Add(viewModelFornecedor);
        }

        return fornecedores;
    }
}
