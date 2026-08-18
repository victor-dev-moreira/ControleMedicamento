using ControleDeMedicamentos.WebApp.Compartilhado.Arquivos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloPacientes;

public sealed class PacienteController : Controller
{
    private readonly RepositorioPacienteEmArquivo repositorioPaciente;
    public PacienteController()
    {
        ContextoJson contextoJson = new ContextoJson();
        contextoJson.Carregar();
        repositorioPaciente = new RepositorioPacienteEmArquivo(contextoJson);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Paciente> pacientes = repositorioPaciente.SelecionarTodos();

        List<ListarPacienteViewModel> viewModels = new List<ListarPacienteViewModel>();

        foreach (Paciente p in pacientes)
        {
            ListarPacienteViewModel vm = new ListarPacienteViewModel(
                p.Id,
                p.Nome,
                p.Telefone,
                p.CartaoSus
            );

            viewModels.Add(vm);
        }

        return View(viewModels);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(string nome, string telefone, string cartaoSus, string cpf)
    {
        Paciente pacientes = new Paciente(nome, telefone, cartaoSus, cpf);
        repositorioPaciente.Cadastrar(pacientes);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(int id)
    {
        Paciente? pacientes = repositorioPaciente.SelecionarPorId(id);
        if (pacientes == null)
            return NotFound();

        EditarPacienteViewModel viewModel = new EditarPacienteViewModel(
            id,
            pacientes.Nome,
            pacientes.Telefone,
            pacientes.CartaoSus,
            pacientes.Cpf
        );

        return View(viewModel);
    }

    [HttpPost]
    public ActionResult Editar(EditarPacienteViewModel editarVm)
    {
        Paciente funcionarioAtulizado = new Paciente(editarVm.Nome, editarVm.Telefone, editarVm.CartaoSus, editarVm.Cpf);

        bool conseguiuEditar = repositorioPaciente.Editar(editarVm.Id, funcionarioAtulizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]

    public ActionResult Excluir(ExcluirPacienteViewModel excluirVm)
    {
        Paciente? pacientes = repositorioPaciente.SelecionarPorId(excluirVm.Id);
        if (pacientes == null)
            return NotFound();

        ExcluirPacienteViewModel viewModel = new ExcluirPacienteViewModel(
            pacientes.Id,
            pacientes.Nome
        );
        return View(viewModel);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExclusao(ExcluirPacienteViewModel excluirVm)
    {
        bool conseguiuExcluir = repositorioPaciente.Excluir(excluirVm.Id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}
