import os
import re
import google.generativeai as genai

genai.configure(api_key=os.environ["GEMINI_API_KEY"])
# Usando o flash para velocidade ou pro para maior raciocínio arquitetural
model = genai.GenerativeModel('gemini-3-flash-preview')

prompt = """
Você é um Arquiteto .NET Senior. Crie uma solução completa de CRUD de Usuário em .NET 10.
IMPORTANTE: Você deve gerar os arquivos .sln e .csproj para cada projeto.

Estrutura de pastas:
- ./Solution.sln
- ./Domain/Domain.csproj
- ./Application/Application.csproj
- ./Infrastructure/Infrastructure.csproj
- ./WebApi/WebApi.csproj
- ./Tests/Tests.csproj

Regras:
1. Use Clean Architecture e DDD.
2. Configure as referências de projeto nos .csproj (ex: Application referencia Domain).
3. Use o Result Pattern e xUnit.
4. O banco deve ser SQL Server com EF Core (InMemory para testes).

Retorne os arquivos no formato:
---FILE: caminho/do/arquivo---
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
