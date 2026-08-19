using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloFuncionarios;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class RequisicaoEntradaController : Controller
{
    private readonly RepositorioRequisicaoEntradaEmArquivo repositorio;
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;

    private readonly RepositorioFuncionarioEmArquivo repositorioFuncionario;


    public RequisicaoEntradaController()
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

        repositorio = new RepositorioRequisicaoEntradaEmArquivo(contexto);
        repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contexto);
        repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contexto);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarRequisicaoEntradaViewModel> viewModels = [];

        foreach (RequisicaoEntrada requisicao in repositorio.SelecionarTodos())
        {
            ListarRequisicaoEntradaViewModel viewModel = new ListarRequisicaoEntradaViewModel(
                requisicao.Id,
                requisicao.Medicamento.Nome,
                requisicao.Funcionario.Nome,
                requisicao.Quantidade,
                requisicao.Data
            );

            viewModels.Add(viewModel);
        }

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarRequisicaoEntradaViewModel viewModel = new CadastrarRequisicaoEntradaViewModel(
            0,
            0,
            0
        ) with
        {
            Medicamentos = ObterMedicamentos(),
            Funcionarios = ObterFuncionario()
        };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarRequisicaoEntradaViewModel cadastrarVm)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(cadastrarVm.MedicamentoId);
        if (medicamento == null)
            return NotFound();

        Funcionario? fornecedor = repositorioFuncionario.SelecionarPorId(cadastrarVm.FuncionarioId);
        if (fornecedor == null)
            return NotFound();

        RequisicaoEntrada requisicaoEntrada = new RequisicaoEntrada(
            medicamento,
            cadastrarVm.Quantidade,
            fornecedor
        );

        repositorio.Cadastrar(requisicaoEntrada);

        return RedirectToAction(nameof(Listar));
    }

    private List<MedicamentoRequisicaoEntradaViewModel> ObterMedicamentos()
    {
        List<MedicamentoRequisicaoEntradaViewModel> medicamentos = [];

        foreach (Medicamento medicamento in repositorioMedicamento.SelecionarTodos())
        {
            MedicamentoRequisicaoEntradaViewModel viewModel = new MedicamentoRequisicaoEntradaViewModel(
                medicamento.Id,
                medicamento.Nome
            );

            medicamentos.Add(viewModel);
        }

        return medicamentos;
    }

    private List<FuncionarioRequisicaoEntradaViewModel> ObterFuncionario()
    {
        List<FuncionarioRequisicaoEntradaViewModel> funcionarios = [];

        foreach (Funcionario funcionario in repositorioFuncionario.SelecionarTodos())
        {
            FuncionarioRequisicaoEntradaViewModel viewModelFuncionario = new FuncionarioRequisicaoEntradaViewModel(
                funcionario.Id,
                funcionario.Nome
            );

            funcionarios.Add(viewModelFuncionario);
        }

        return funcionarios;
    }

}

