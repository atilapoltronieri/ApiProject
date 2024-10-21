# ApiProject

## Database

Configurar database connection no **appsettings.json**. 
Auto migration ativada. 
Bata iniciar a aplicação.

## Docker

Na pasta principal

### docker-compose up --build

## Perguntas ao PO

### Usuário

- Será utilizado algum SSO, interno ou externo, para usuários?
- Por não informar definição, e pedir que não haja CRUD, há dois usuários para testes disponíveis: Id 1, usuário manager, e Id 2, usuário não manager.

### Projeto

- Não foi citado alteração de Projeto. Caso haja, deverá ter log igual as tarefas?
- Poderá haver projetos de mesmo nome?
- Deve haver restrição de usuário para deleção de projeto? Manager ou seu criador, por exemplo.

### Tarefas

- Se tarefas possuírem comentários, esses devem ser excluídos antes da tarefa ser excluída?
- Se sim, não há pedido para remoção de comentário. Podemos excluir todos os comentários junto da tarefa? (Cascade delete no banco de dados, por exemplo)
- Se a tarefa for excluída, seus logs devem permanecer no banco de dados para fins de relatório?

### Relatório de Desempenho

- Se uma tarefa for concluída, mudar o status para outro e depois for finalizada novamente, deve contar todas as vezes que ela foi finalizada nos últimos 30 dias ou apenas uma vez?
- Se uma terefa for excluída e o log ficar, esta ainda deve ser mantida dentro do relatório de desempenho?
- Deseja que o relatório seja convertido para algum formato específico, como PDF ou TXT, e seja baixado ao invés de aparecer como string?
- Há algum formato específico de relatório que este deva ser apresentado?

## Final

### Possíveis melhorias.

- Deixaria a responsabilidade do usuário para um SSO dedicado, ou um microserviço interno, e o projeto apenas o consuma.
- Deixar um microserviço responsável pelos relatórios, com seu próprio usuário de banco com acesso de leituras as tabelas de log.
- Extrairia a connection string do app.config e passaria para um local seguro, como uma variável do ambiente de publicação ou dentro de um vault para maior segurança.
- Expandiria o middleware de log para interceptar outros eventos automaticamente e não precisar sujar todo código com log.
- Extenderia as exceptions criadas para melhor retorno de informação do usuário e manter uma inner exception para log no servidor.
- Criaria testes de integração com acesso real a um banco de testes que fosse rodado todas as vezes antes do projeto subir para produção. Não rodaria na pipeline.
- Migraria o código do TaskLog para trigers no banco de dado, reduzindo código e melhorando desempenho.