Aqui está o texto para copiar:

# Projeto - Cidades ESGInteligentes

## 🚀 Como executar localmente

### Com Docker Compose (recomendado)

1. Clone o repositório:

```bash
git clone https://github.com/deepzyy/cidades-esginteligentes.git
cd cidades-esginteligentes


Suba o ambiente com Docker Compose:

docker-compose up --build


Acesse a API:

http://localhost:8080

Sem Docker Compose (manual)
1. Rodar SQL Server:
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=SuaSenhaForte123!" \
   -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2019-latest

2. Build da imagem da API:
docker build -t cidades-esginteligentes-api .

3. Rodar a API, conectando ao SQL Server:
docker run -p 8080:8080 --name cidades-api --link sqlserver -d cidades-esginteligentes-api

🔁 Pipeline CI/CD

O pipeline está configurado com GitHub Actions e realiza:

Build automático a cada push.

Execução de testes.

Deploy simulado em staging e produção.

🐳 Containerização

Utilizamos:

Dockerfile para criar a imagem da API.

docker-compose.yml para orquestração dos containers.

🖼️ Prints do funcionamento

<img width="424" height="85" alt="image" src="https://github.com/user-attachments/assets/881be8fe-f645-4466-9af2-89ba5dfef9c5" />
<img width="768" height="72" alt="image" src="https://github.com/user-attachments/assets/6f97b0eb-d793-460e-8630-59c77be2101f" />
<img width="625" height="59" alt="image" src="https://github.com/user-attachments/assets/736b9c8a-ae5b-4f86-9827-c5f52c0788d1" />

<img width="1863" height="439" alt="image" src="https://github.com/user-attachments/assets/c2c3f441-09e5-458b-bfa6-388bfa52a538" />


🧰 Tecnologias utilizadas

ASP.NET Core 8.0

Docker e Docker Compose

GitHub Actions

SQL Server

CI/CD

✅ Checklist de Entrega
Item	OK
Projeto compactado em .ZIP com estrutura organizada	☐
Dockerfile funcional	☑
docker-compose.yml ou arquivos Kubernetes	☑
Pipeline com etapas de build, teste e deploy	☑
README.md com instruções e prints	☑
Documentação técnica com evidências (PDF ou PPT)	☐
Deploy realizado nos ambientes staging e produção	☐
