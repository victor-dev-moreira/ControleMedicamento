# Controle de Medicamentos

Sistema de console para gerenciar o estoque de medicamentos de uma unidade de saúde: cadastra fornecedores, medicamentos, pacientes e funcionários, e controla a entrada e a saída de medicamentos do estoque.

O estoque não é um número guardado — ele é **calculado** a partir do histórico: a soma de todas as entradas menos a soma de todas as saídas de cada medicamento.

Os dados são persistidos em um arquivo JSON local, então tudo que você cadastra continua lá na próxima vez que abrir o programa.

Desenvolvido durante o curso Backend da [Academia do Programador](https://www.academiadoprogramador.net) 2026.

---

## Como executar

```bash
dotnet run --project ControleDeMedicamentos.ConsoleApp
```

Requer o **.NET 10.0 SDK**.

---

## Módulos

### 1. Fornecedores

Cadastro completo (registrar, visualizar, editar, excluir) de quem fornece os medicamentos.

- Nome (3-100 caracteres), Telefone `(XX) XXXXX-XXXX`, CNPJ (14 dígitos)
- Não permite dois fornecedores com o mesmo CNPJ

<!-- GIF: Fornecedores -->

---

### 2. Medicamentos

Cadastro completo dos medicamentos, cada um vinculado a um fornecedor. Exibe a quantidade em estoque calculada em tempo real.

- Nome (2-100 caracteres), Descrição (5-255 caracteres), Fornecedor obrigatório

<!-- GIF: Medicamentos -->

---

### 3. Pacientes

Cadastro completo de quem recebe os medicamentos.

- Nome (3-100 caracteres), Telefone `(XX) XXXXX-XXXX`, Cartão do SUS (15 dígitos), CPF (11 dígitos)
- Não permite dois pacientes com o mesmo cartão do SUS

<!-- GIF: Pacientes -->

---

### 4. Funcionários

Cadastro completo da equipe da unidade.

- Nome (3-100 caracteres), Telefone `(XX) XXXXX-XXXX`, CPF (11 dígitos)
- Não permite dois funcionários com o mesmo CPF

<!-- GIF: Funcionários -->

---

### 5. Requisições de Entrada

Registra a chegada de medicamentos ao estoque. Só permite **registrar** e **visualizar** — uma movimentação de estoque é um fato histórico e não deve ser alterada.

- Medicamento obrigatório, Quantidade maior que zero
- **Soma** a quantidade ao estoque do medicamento

<!-- GIF: Requisições de Entrada -->

---

### 6. Requisições de Saída

Registra a dispensação de um medicamento a um paciente. Também só permite **registrar** e **visualizar**.

- Paciente e Medicamento obrigatórios, Quantidade maior que zero
- **Não permite** requisição que exceda o estoque disponível
- **Subtrai** a quantidade do estoque do medicamento

<!-- GIF: Requisições de Saída -->

---

## Onde os dados ficam

O arquivo `dados.json` é gravado em:

```
%LOCALAPPDATA%\ControleDeMedicamentos-Backend\dados.json
```

Apagar esse arquivo zera o sistema.
