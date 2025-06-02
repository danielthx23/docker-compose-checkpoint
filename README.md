# Co-Alert

---

## Sumário

- [Co-Alert](#co-alert)
  - [Sumário](#sumário)
  - [Descrição do Projeto](#descrição-do-projeto)
  - [Autores](#autores)
    - [Turma 2TDSR - Análise e Desenvolvimento de Sistemas](#turma-2tdsr---análise-e-desenvolvimento-de-sistemas)
  - [Diagrama Database](#diagramas-database)
  - [Dockerfile](#dockerfile)
  - [Instalação do Projeto via Docker](#instalação-do-projeto-via-docker)
    - [Requisitos](#requisitos)
    - [Configuração](#configuração)
  - [Instalação do Projeto via Host](#instalação-do-projeto-via-host)
    - [Requisitos](#requisitos-1)
    - [Configuração](#configuração-1)
    - [Rodar o Projeto Localmente](#rodar-o-projeto-localmente)
      - [Com IDE (Rider ou Visual Studio)](#com-ide-rider-ou-visual-studio)
      - [Sem IDE (usando linha de comando)](#sem-ide-usando-linha-de-comando)
  - [Acesso à API](#acesso-à-api)
  - [Exemplos de Teste da API](#exemplos-de-teste-da-api)
    - [Categoria de Desastre](#categoria-de-desastre)
    - [Localização](#localização)
    - [Usuário](#usuário)
    - [Postagem](#postagem)
    - [Comentário](#comentário)
    - [Like](#like)
  - [Rotas da API](#rotas-da-api)
    - [Categoria de desastre](#categoria-de-desastre-1)
    - [Comentários](#comentários)
    - [Likes](#likes)
    - [Localização](#localização-1)
    - [Postagem](#postagem-1)
    - [Usuário](#usuário-1)

---

## Descrição do Projeto

O Co-Alert surge como uma rede colaborativa de comunicação, que funciona como uma plataforma tipo fórum, permitindo aos usuários compartilhar informações em tempo real sobre desastres naturais e eventos climáticos extremos. Através de publicações detalhadas, que incluem tipo de ocorrência, localização exata e imagens, a comunidade pode acessar informações confiáveis e atualizadas, melhorando a tomada de decisões e a preparação para esses eventos.

Nosso público-alvo é composto por pessoas que vivem em áreas vulneráveis a desastres naturais, indivíduos com acesso limitado a informações oficiais e qualquer pessoa interessada em estar preparada para situações de emergência climática.

Com o Co-Alert, buscamos impactar positivamente a vida dessas pessoas, reduzindo a desinformação e os alarmes falsos, facilitando o fluxo rápido e acessível de dados relevantes, para que possam agir com segurança e antecedência.

Este projeto responde a um desafio crescente: o aumento de até 460% nos desastres climáticos no Brasil desde os anos 1990, comprovando a necessidade de ferramentas eficientes para comunicação e prevenção

Essa integração permite o acompanhamento via aplicativo, promovendo **eficiência, segurança e organização**. O sistema também gerencia cadastro/edição de dados, autenticação/autorizacão, gestão de permissões e dashboards com relatórios para tomada de decisão.

## Autores

### Turma 2TDSR - Análise e Desenvolvimento de Sistemas

* Daniel Saburo Akiyama - RM 558263
* Danilo Correia e Silva - RM 557540
* João Pedro Rodrigues da Costa - RM 558199

## Diagramas Database

![Diagrama](./CoAlert/wwwroot/imagens/diagramacoalert.png)

*Diagrama para MVP — ainda passível de várias melhorias.*

## Dockerfile

Dockerfile de desenvolvimento sem migração (mais leve):
[Dockerfile Development](./CoAlert/Dockerfile.dev)

Dockerfile de desenvolvimento com migração (mais pesada):
[Dockerfile Development Migration](./CoAlert/Dockerfile.dev-migration)

Imagem Docker Hub: [Imagem Docker Hub com as duas Tags](https://hub.docker.com/repository/docker/danielakiyama/coalert/general)

**Observação**: Em produção, o ideal é usar a imagem aspnet apenas para executar a aplicação, deixando as migrações para serem feitas fora do container ou em um container separado com o SDK. Isso torna a imagem final mais leve e segura. Contudo, optamos por utilizar o SDK na imagem para facilitar o desenvolvimento do projeto.

## Instalação do Projeto via Docker 

### Requisitos
- Docker instalado e com a engine ligada

### Configuração

1. Rode um container utilizando variaveis de ambiente e a imagem no Docker Hub pelo build

Utilize o comando abaixo, substituindo meuusuario e minhasenha com suas credenciais do Oracle DB:

```bash
  # Esse comando só funciona em bash por conta do \
  docker run -d \
  -e ORACLE_USER=seusuario \
  -e ORACLE_PASSWORD=suasenha \
  -e ORACLE_HOST=oracle.fiap.com.br \
  -e ORACLE_PORT=1521 \
  -e ORACLE_SID=ORCL \
  -e RUN_MIGRATIONS=true \
  -p 5024:5024 \
  danielakiyama/coalert:development-migration-v1.0.0
```

Em uma linha só (recomendado):
```bash
  # Funciona no CMD e Bash (recomendado)
  docker run -d -e ORACLE_USER=seusuario -e ORACLE_PASSWORD=suasenha -e ORACLE_HOST=oracle.fiap.com.br -e ORACLE_PORT=1521 -e ORACLE_SID=ORCL -e RUN_MIGRATIONS=true -p 5024:5024 danielakiyama/coalert:development-migration-v1.0.0
```

OU, se não quiser rodar as migrations:

```bash
  # Esse comando só funciona em bash por conta do \
  docker run -d \
  -e ORACLE_USER=seusuario \
  -e ORACLE_PASSWORD=suasenha \
  -e ORACLE_HOST=oracle.fiap.com.br \
  -e ORACLE_PORT=1521 \
  -e ORACLE_SID=ORCL \
  -p 5024:5024 \
  danielakiyama/coalert:development-v1.0.0
```

Em uma linha só (recomendado):
```bash
  # Funciona no CMD e Bash (recomendado)
  docker run -d -e ORACLE_USER=seusuario -e ORACLE_PASSWORD=suasenha -e ORACLE_HOST=oracle.fiap.com.br -e ORACLE_PORT=1521 -e ORACLE_SID=ORCL -p 5024:5024 danielakiyama/coalert:development-v1.0.0
```

Legendas:

```bash
docker run -d \
  -e ORACLE_USER=seusuario           # Usuário do banco Oracle
  -e ORACLE_PASSWORD=suasenha        # Senha do usuário Oracle
  -e ORACLE_HOST=oracle.fiap.com.br  # Host do banco Oracle
  -e ORACLE_PORT=1521                # Porta do banco Oracle (geralmente 1521)
  -e ORACLE_SID=ORCL                 # SID da instância Oracle
  -e RUN_MIGRATIONS=true             # Controla se as migrações serão executadas na inicialização (true/false)
  -p 5169:5169                      # Mapeia a porta 5169 do container para a mesma porta na máquina host
  danielakiyama/mottracker:<tags diferentes>  # Nome da imagem e tag Docker a ser executada, lembrando Development (leve) não roda migrations, Development-Migration (pesada) roda.
```

## Instalação do Projeto via Host

### Requisitos
- .NET SDK 8.0 instalado
- Rider / Visual Studio instalado (opcional)

### Configuração

Clone o projeto utilizando git

1. No arquivo `appsettings.Development.json`, configure e adicione a string de conexão do Oracle DB com seu usuário e senha, por exemplo:

```json
"ConnectionStrings": {
  "Oracle": "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=seuhost)(PORT=suaporta)))(CONNECT_DATA=(SERVER=DEDICATED)(SID=seusid)));User Id=seuusuario;Password=suasenha;"
}
```

Ou crie um arquivo .env no diretório do projeto contendo: 

```bash
  ORACLE_USER=seusuario
  ORACLE_PASSWORD=suasenha
  ORACLE_HOST=seuhost
  ORACLE_PORT=suaporta
  ORACLE_SID=seusid
```

2. Execute as migrations para criar as tabelas no banco Oracle:

   (entre no diretório do projeto primeiro)

```bash
  dotnet ef database update
```

Caso não tenha a ferramenta dotnet-ef instalada, instale com:

```bash
  dotnet tool install --global dotnet-ef
```

### Rodar o Projeto Localmente

Após configurar a string de conexão e aplicar as migrations, você pode rodar a API de duas maneiras:

#### 1. Com IDE (Rider ou Visual Studio)

1. Abra a solução no Rider ou Visual Studio.
2. Selecione o projeto da API como *Startup Project*.
3. Clique em **Run** com o perfil `http`.

#### 2. Sem IDE (usando linha de comando)

1. **Restaurar e compilar:**

- Para rodar o backend, entre no diretório `CoAlert_DotNet` antes de executar o comando.
  
- Caso deseje rodar o backend de dentro do diretório do projeto .net, altere o caminho e remova a flag `--project CoAlert` no comando correspondente.

```bash
dotnet restore CoAlert/CoAlert.csproj
dotnet build
```

4. **Rodar o projeto:**

```bash
dotnet run --project CoAlert --urls "http://localhost:5024"
```

## Acesso à API

- Aplicação Razor: [http://localhost:5024/](http://localhost:5024/)

![Co Alert Razor APP](./CoAlert/wwwroot/imagens/coalertrazorapp.png)

- Swagger: [http://localhost:5024/swagger](http://localhost:5024/swagger/index.html)

![Diagrama](./CoAlert/wwwroot/imagens/coalertswagger.png)

- API: [http://localhost:5024/api](http://localhost:5024/api/)

## Exemplos de Teste da API

### Categoria de Desastre

**Exemplo POST**

```json
{
  "nmTitulo": "Enchente Urbana",
  "dsCategoria": "Desastres relacionados ao acúmulo de água em áreas urbanas devido a chuvas intensas.",
  "nmTipo": "Hidrológico"
}
```

**Exemplo PUT**

```json
{
  "nmTitulo": "Enchente Urbana Moderada",
  "dsCategoria": "Inundações em áreas urbanas causadas por chuvas acima da média, com impactos localizados.",
  "nmTipo": "Hidrológico"
}
```

---

### Localização

**Exemplo POST**

```json
{
  "nmBairro": "Centro",
  "nmLogradouro": "Rua das Flores",
  "nrNumero": 123,
  "nmCidade": "São Paulo",
  "nmEstado": "SP",
  "nrCep": "01001-000",
  "nmPais": "Brasil",
  "dsComplemento": "Apartamento 101"
}
```

**Exemplo PUT**

```json
{
  "nmBairro": "Centro",
  "nmLogradouro": "Rua das Palmeiras",
  "nrNumero": 456,
  "nmCidade": "São Paulo",
  "nmEstado": "SP",
  "nrCep": "01001-001",
  "nmPais": "Brasil",
  "dsComplemento": "Bloco B, Apt. 202"
}
```

---

### Usuário

**Exemplo POST**

```json
{
  "nmUsuario": "joaosilva",
  "nrSenha": "SenhaForte123!",
  "nmEmail": "joao.silva@email.com"
}
```

**Exemplo PUT**

```json
{
  "nmUsuario": "joaosilva_atualizado",
  "nrSenha": "NovaSenhaSegura456!",
  "nmEmail": "joao.silva.novo@email.com"
}
```

---

### Postagem

**Exemplo POST**

```json
{
  "nmTitulo": "Deslizamento em encosta",
  "nmConteudo": "Foi registrado um deslizamento de terra em uma área de risco após fortes chuvas.",
  "usuarioId": COLOQUE_O_ID_DO_USUARIO,
  "categoriaDesastreId": COLOQUE_O_ID_DA_CATEGORIA,
  "localizacaoId": COLOQUE_O_ID_DA_LOCALIZACAO
}
```

**Exemplo PUT**

```json
{
  "nmTitulo": "Deslizamento em encosta atualizado",
  "nmConteudo": "Atualização: o deslizamento afetou três residências e interditou a rua principal.",
  "usuarioId": COLOQUE_O_ID_DO_USUARIO,
  "categoriaDesastreId": COLOQUE_O_ID_DA_CATEGORIA,
  "localizacaoId": COLOQUE_O_ID_DA_LOCALIZACAO
}
```

---

### Comentário

**Exemplo POST**

```json
{
  "nmConteudo": "Excelente relato, obrigado por compartilhar!",
  "usuarioId": COLOQUE_O_ID_DO_USUARIO,
  "postagemId": COLOQUE_O_ID_DA_POSTAGEM,
  "idComentarioParente": 0
}
```

**Exemplo PUT**

```json
{
  "nmConteudo": "Atualizando meu comentário: a situação foi resolvida pela Defesa Civil.",
  "usuarioId": COLOQUE_O_ID_DO_USUARIO,
  "postagemId": COLOQUE_O_ID_DA_POSTAGEM,
  "idComentarioParente": 0
}
```

---

### Like

**Exemplo POST**

```json
{
  "usuarioId": COLOQUE_O_ID_DO_USUARIO,
  "postagemId": COLOQUE_O_ID_DA_POSTAGEM,
  "comentarioId": COLOQUE_O_ID_DO_COMENTARIO
}
```

## Rotas da API

### Categoria de desastre

| Método | Rota                                 | Descrição                             |
| ------ | ------------------------------------ | ------------------------------------- |
| GET    | `/CategoriaDesastre`                 | Lista todas as categorias de desastre |
| POST   | `/CategoriaDesastre`                 | Cria uma nova categoria               |
| GET    | `/CategoriaDesastre/{id}`            | Obtém categoria por ID                |
| PUT    | `/CategoriaDesastre/{id}`            | Atualiza uma categoria                |
| DELETE | `/CategoriaDesastre/{id}`            | Deleta uma categoria                  |
| GET    | `/CategoriaDesastre/tipo/{tipo}`     | Obtém categorias por tipo             |
| GET    | `/CategoriaDesastre/titulo/{titulo}` | Obtém categorias por título           |

### Comentários

| Método | Rota                                   | Descrição                  |
| ------ | -------------------------------------- | -------------------------- |
| GET    | `/Comentario`                          | Lista todos os comentários |
| POST   | `/Comentario`                          | Cria um novo comentário    |
| GET    | `/Comentario/{id}`                     | Obtém comentário por ID    |
| PUT    | `/Comentario/{id}`                     | Atualiza um comentário     |
| DELETE | `/Comentario/{id}`                     | Deleta um comentário       |
| GET    | `/Comentario/postagem/{postagemId}`    | Comentários por postagem   |
| GET    | `/Comentario/usuario/{usuarioId}`      | Comentários por usuário    |
| GET    | `/Comentario/respostas/{comentarioId}` | Respostas de um comentário |

### Likes

| Método | Rota                              | Descrição              |
| ------ | --------------------------------- | ---------------------- |
| POST   | `/Like/toggle`                    | Alternar curtida       |
| GET    | `/Like/postagem/{postagemId}`     | Curtidas da postagem   |
| GET    | `/Like/comentario/{comentarioId}` | Curtidas do comentário |
| GET    | `/Like/usuario/{usuarioId}`       | Curtidas do usuário    |

### Localização

| Método | Rota                           | Descrição                   |
| ------ | ------------------------------ | --------------------------- |
| GET    | `/Localizacao`                 | Lista todas as localizações |
| POST   | `/Localizacao`                 | Cria uma nova localização   |
| GET    | `/Localizacao/{id}`            | Obtém localização por ID    |
| PUT    | `/Localizacao/{id}`            | Atualiza uma localização    |
| DELETE | `/Localizacao/{id}`            | Deleta uma localização      |
| GET    | `/Localizacao/cidade/{cidade}` | Localizações por cidade     |
| GET    | `/Localizacao/estado/{estado}` | Localizações por estado     |
| GET    | `/Localizacao/cep/{cep}`       | Localização por CEP         |

### Postagem

| Método | Rota                                    | Descrição                 |
| ------ | --------------------------------------- | ------------------------- |
| GET    | `/Postagem`                             | Lista todas as postagens  |
| POST   | `/Postagem`                             | Cria uma nova postagem    |
| GET    | `/Postagem/{id}`                        | Obtém postagem por ID     |
| PUT    | `/Postagem/{id}`                        | Atualiza uma postagem     |
| DELETE | `/Postagem/{id}`                        | Deleta uma postagem       |
| GET    | `/Postagem/usuario/{usuarioId}`         | Postagens por usuário     |
| GET    | `/Postagem/categoria/{categoriaId}`     | Postagens por categoria   |
| GET    | `/Postagem/localizacao/{localizacaoId}` | Postagens por localização |

### Usuário

| Método | Rota                  | Descrição               |
| ------ | --------------------- | ----------------------- |
| GET    | `/Usuario`            | Lista todos os usuários |
| POST   | `/Usuario`            | Cria um novo usuário    |
| GET    | `/Usuario/{id}`       | Obtém usuário por ID    |
| PUT    | `/Usuario/{id}`       | Atualiza um usuário     |
| DELETE | `/Usuario/{id}`       | Deleta um usuário       |
| POST   | `/Usuario/autenticar` | Autentica um usuário    |


