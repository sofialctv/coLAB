# coLAB
Esse repositório armazena o módulo backend da aplicação coLAB, que pode ser compreendida com mais detalhes em nossa [Wiki](https://github.com/sofialctv/coLAB/wiki).
Veja o módulo frontend [aqui](https://github.com/sofialctv/coLAB-frontend).

## Instalação
Siga os passos abaixo para configurar e executar o projeto localmente:

### 1. Clonar o repositório
Abra o terminal e execute o seguinte comando para clonar o repositório do projeto:

```bash
git clone https://github.com/seu-usuario/coLAB.git
```

### 2. Acessar o diretório do projeto

Entre no diretório recém-clonado:

```bash
cd coLAB
```

### 3. Configurar variáveis de ambiente
Verifique se a string de conexão com o banco de dados está disponível no arquivo `coLAB/appsettings.Development.json`

### 4. Inicializar com Docker Compose
Agora, para configurar e iniciar o projeto, utilize o Docker Compose. O projeto já está preparado para ser executado com o `docker-compose.yml`. 

Execute o seguinte comando no terminal:

```bash
docker-compose up --build
```

Caso não esteja aparencendo essa opção automaticamnete, pela interface do Visual Studio 22, clique com o botão direito no arquivo `docker-compose` e selecione a opção "Definir como projeto de inicialização". Clique no botão ▶️ Docker Compose na parte superior da tela.

### 5. Acessar a aplicação
Após a execução bem-sucedida do comando acima, a aplicação estará rodando localmente e a interface do Swagger estará disponível para teste de rotas.
![image](https://github.com/user-attachments/assets/d9ba70d8-a487-4788-a8e1-ab4037036bd5)
