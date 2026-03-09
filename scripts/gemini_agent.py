import os
import re
import google.generativeai as genai

genai.configure(api_key=os.environ["GEMINI_API_KEY"])
# Usando o flash para velocidade ou pro para maior raciocínio arquitetural
model = genai.GenerativeModel('gemini-3-flash-preview')

prompt = f"""
Você é um Arquiteto .NET Senior especialista em DDD e Clean Architecture.
Tarefa: {os.environ.get("ISSUE_TITLE", "Criar CRUD Usuário")}
Contexto: {os.environ.get("ISSUE_BODY", "")}

REGRAS OBRIGATÓRIAS:
1. Framework: .NET 10 e Entity Framework Core.
2. Arquitetura: Clean Architecture (Domain, Application, Infrastructure, WebApi).
3. DDD: Entidades, Value Objects (Email), Repositories e Domain Services.
4. Banco: SQL Server com Migrations, configurado para rodar In-Memory nos testes.
5. Padrões: Result Pattern para retornos, Repository Pattern, Dependency Injection.
6. Entidade: Usuario (Id int, Nome varchar(120), Email varchar(200), Status bool).
7. Testes: XUnit para testes de unidade do domínio.
8. Documente com swagger, coloque comentários

Retorne os arquivos no formato EXATO:
---FILE: caminho/do/arquivo.cs---
CONTEUDO
---END---
"""

response = model.generate_content(prompt)

# Regex para extrair os arquivos de forma limpa
files = re.findall(r'---FILE: (.*?)---(.*?)---END---', response.text, re.DOTALL)

for file_path, content in files:
    file_path = file_path.strip()
    full_path = os.path.join(os.getcwd(), file_path)
    os.makedirs(os.path.dirname(full_path), exist_ok=True)
    with open(full_path, "w", encoding="utf-8") as f:
        f.write(content.strip())
