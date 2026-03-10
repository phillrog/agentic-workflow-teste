import os
import re
import google.generativeai as genai

genai.configure(api_key=os.environ["GEMINI_API_KEY"])
# Usando o flash para velocidade ou pro para maior raciocínio arquitetural
model = genai.GenerativeModel('gemini-3-flash-preview')
prompt = """
Você é um Arquiteto .NET Senior. Crie um CRUD de Usuário em .NET 10 seguindo:
1. Clean Architecture: Projetos Domain, Application, Infrastructure e WebApi.
2. DDD: Entidade Usuario (Id, Nome, Email, Status) e Value Object Email.
3. Patterns: Result Pattern, Repository e Dependency Injection.
4. Infra: EF Core com SQL Server (Configurar InMemory para testes).
5. Testes: xUnit para validar regras de Nome (max 120) e Email válido.

Retorne os arquivos no formato:
---FILE: caminho/do/arquivo.cs---
CONTEUDO
---END---
"""

response = model.generate_content(prompt)
files = re.findall(r'---FILE: (.*?)---(.*?)---END---', response.text, re.DOTALL)

for file_path, content in files:
    file_path = file_path.strip()
    full_path = os.path.join(os.getcwd(), file_path)
    os.makedirs(os.path.dirname(full_path), exist_ok=True)
    with open(full_path, "w", encoding="utf-8") as f:
        f.write(content.strip())
