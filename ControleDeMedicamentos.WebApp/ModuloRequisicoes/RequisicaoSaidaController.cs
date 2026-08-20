using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloPacientes;
using ControleDeMedicamentos.WebApp.ModuloMedicamentos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicoes;

public class RequisicaoSaidaController : Controller
{
    private readonly RepositorioRequisicaoSaidaEmArquivo repositorio;
    private readonly RepositorioMedicamentoEmArquivo repositorioMedicamento;
    private readonly RepositorioPacienteEmArquivo repositorioPaciente;
    public RequisicaoSaidaController(
        RepositorioRequisicaoSaidaEmArquivo repositorio,
        RepositorioMedicamentoEmArquivo repositorioMedicamento,
        RepositorioPacienteEmArquivo repositorioPaciente)
    {
        this.repositorio = repositorio;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioPaciente = repositorioPaciente;
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarRequisicaoSaidaViewModel> viewModels = [];

        foreach (RequisicaoSaida requisicao in repositorio.SelecionarTodos())
        {
            List<ListarMedicamentoPrescritoRequisicaoSaidaViewModel> medicamentosPrescritosVMs = [];

            foreach (MedicamentoPrescrito prescrito in requisicao.MedicamentosPrescritos)
            {
                ListarMedicamentoPrescritoRequisicaoSaidaViewModel prescritoVm = new(
                    prescrito.Medicamento.Id,
                    prescrito.Medicamento.Nome,
                    prescrito.Quantidade
                );

                medicamentosPrescritosVMs.Add(prescritoVm);
            }
            ListarRequisicaoSaidaViewModel viewModel = new ListarRequisicaoSaidaViewModel(
                requisicao.Id,
                requisicao.Paciente.Nome,
                medicamentosPrescritosVMs,
                requisicao.Data
            );

            viewModels.Add(viewModel);
        }

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarRequisicaoSaidaViewModel viewModel = new CadastrarRequisicaoSaidaViewModel(
            0
        ) with
        {
            MedicamentosPrescritos = ObterMedicamentos(),
            Pacientes = ObterPacientes()
        };

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarRequisicaoSaidaViewModel cadastrarVm)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(cadastrarVm.PacienteId);
        if (paciente == null)
            return NotFound();

        List<MedicamentoPrescritoRequisicaoSaidaViewModel> medicamentosModel = cadastrarVm.MedicamentosPrescritos ?? [];

        List<MedicamentoPrescrito> medicamentosPrescritos = [];

        foreach (MedicamentoPrescritoRequisicaoSaidaViewModel medicamentoModel in medicamentosModel)
        {
            if (!medicamentoModel.Selecionado)
                continue;

            Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(medicamentoModel.MedicamentoId);
            medicamentosPrescritos.Add(
                new MedicamentoPrescrito(medicamento!, medicamentoModel.Quantidade));
        }
        RequisicaoSaida requisicao = new RequisicaoSaida(paciente, medicamentosPrescritos);

        repositorio.Cadastrar(requisicao);

        return RedirectToAction(nameof(Listar));
    }

    private List<MedicamentoPrescritoRequisicaoSaidaViewModel> ObterMedicamentos(
        List<MedicamentoPrescritoRequisicaoSaidaViewModel> valores = null
    )
    {
        // Id 1 = Medicamento Prescrito { Nome = Paracetamol ...}
        Dictionary<int, MedicamentoPrescritoRequisicaoSaidaViewModel> valoresPorMedicamento = [];
        if (valores != null)
        {
            foreach (MedicamentoPrescritoRequisicaoSaidaViewModel valor in valores)
                valoresPorMedicamento[valor.MedicamentoId] = valor;
        }

        List<MedicamentoPrescritoRequisicaoSaidaViewModel> medicamentos = [];

        foreach (Medicamento medicamento in repositorioMedicamento.SelecionarTodos())
        {
            valoresPorMedicamento.TryGetValue(medicamento.Id, out MedicamentoPrescritoRequisicaoSaidaViewModel? valor);

            medicamentos.Add(new MedicamentoPrescritoRequisicaoSaidaViewModel(
                medicamento.Id,
                medicamento.Nome,
                medicamento.QuantidadeEmEstoque,
                valor?.Selecionado ?? false,
                valor?.Quantidade ?? 0
            ));
        }

        return medicamentos;
    }

    private List<PacienteRequisicaoSaidaViewModel> ObterPacientes()
    {
        List<PacienteRequisicaoSaidaViewModel> pacientes = [];

        foreach (Paciente paciente in repositorioPaciente.SelecionarTodos())
        {
            PacienteRequisicaoSaidaViewModel viewModelPaciente = new PacienteRequisicaoSaidaViewModel(
                paciente.Id,
                paciente.Nome
            );

            pacientes.Add(viewModelPaciente);
        }

        return pacientes;
    }

}

