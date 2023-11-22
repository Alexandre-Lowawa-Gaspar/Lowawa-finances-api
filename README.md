# Lowawa-finances-api
# Olá sou o Alexandre Lowawa Gaspar, Desenvolvedor .NET.

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

Lowawa-finances-api é um projeto open source desenvolvido em ASP.NET Web API, oferecendo uma solução robusta para o rastreamento de transações financeiras pessoais. Utilizando o SQL Server para armazenamento de dados, esta API permite que os usuários registrem suas receitas, despesas e consultem informações detalhadas sobre seu histórico financeiro.

## Recursos Principais

- Registro de transações (receitas e despesas).
- Consulta de saldo atual.
- Geração de relatórios personalizados.

## Tecnologias Utilizadas

- ASP.NET Web API
- SQL Server

## Instruções de Uso

1. **Clone o repositório:**
    ```bash
    git clone https://github.com//api-financas.git
    cd api-financas
    ```

2. **Configure o ambiente:**
    - Certifique-se de ter o ASP.NET e o SQL Server instalados.
    - Atualize as configurações de conexão no arquivo `appsettings.json`.

3. **Execute a aplicação:**
    ```bash
    dotnet run
    ```

4. **Comece a rastrear suas finanças!**
5. **Modelo de Dados**
# Modelo de Dados
- Usuário (User):

  - id (integer): Identificador único do usuário.
  - username (string): Nome de usuário do usuário.
  - password (string): Senha do usuário (hash, não armazene senhas em texto plano).
* Transação (Transaction):
  * id (integer): Identificador único da transação.
  * user_id (integer): Referência ao usuário que fez a transação.
  * description (string): Descrição da transação.
  * amount (number): Valor da transação.
  * type (string): Tipo da transação (receita ou despesa).
  * date (date): Data da transação.
  * category (string, opcional): Categoria da transação.

# Exemplo de Dados
Usuário:

# json
   ```bash
 {
  "id": 1,
  "username": "usuario_exemplo",
  "password": "hash_senha"
   }
    ```

Transações:

# json
    ```bash
{
  "id": 1,
  "user_id": 1,
  "description": "Compra de mantimentos",
  "amount": 50.00,
  "type": "expense",
  "date": "2023-11-22",
  "category": "alimentacao"
}
    ```
# json

    ```bash
{
  "id": 2,
  "user_id": 1,
  "description": "Salário",
  "amount": 2000.00,
  "type": "income",
  "date": "2023-11-15",
  "category": "salario"
}
    ```

## Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir problemas, enviar solicitações de pull ou melhorar a documentação.

## Licença

Este projeto é distribuído sob a licença MIT. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.
