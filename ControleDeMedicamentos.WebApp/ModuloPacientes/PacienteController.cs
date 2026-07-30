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
        return View(pacientes);
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

        return View(pacientes);
    }

    [HttpPost]
    public ActionResult Editar(int id, string nome, string telefone, string cartaoSus, string cpf)
    {
        Paciente funcionarioAtulizado = new Paciente(nome, telefone, cartaoSus, cpf);

        bool conseguiuEditar = repositorioPaciente.Editar(id, funcionarioAtulizado);

        if (!conseguiuEditar)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]

    public ActionResult Excluir(int id)
    {
        Paciente? pacientes = repositorioPaciente.SelecionarPorId(id);
        if (pacientes == null)
            return NotFound();

        return View(pacientes);
    }

    [HttpPost]
    [ActionName("Excluir")]
    public ActionResult ConfirmarExclusao(int id)
    {
        bool conseguiuExcluir = repositorioPaciente.Excluir(id);

        if (!conseguiuExcluir)
            return NotFound();

        return RedirectToAction(nameof(Listar));
    }
}
