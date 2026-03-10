import os
import re
import google.generativeai as genai

genai.configure(api_key=os.environ["GEMINI_API_KEY"])
model = genai.GenerativeModel('gemini-3-flash-preview')

# Captura o log de erro do build vindo do shell
error_log = os.environ.get("BUILD_ERRORS", "Erro não capturado.")

prompt = f"""
Você é um Arquiteto .NET Senior. Sua missão é CORRIGIR o código que você mesmo gerou.
O 'dotnet build' falhou com os seguintes erros:
{error_log}

INSTRUÇÕES:
1. Analise o erro e corrija os arquivos .cs necessários.
2. Mantenha os padrões DDD, Clean Architecture e .NET 10.
3. Garanta que namespaces e referências de projetos estejam consistentes.

Retorne os arquivos corrigidos no formato:
---FILE: caminho/do/arquivo.cs---
CONTEUDO
---END---
"""

response = model.generate_content(prompt)
files = re.findall(r'---FILE: (.*?)---(.*?)---END---', response.text, re.DOTALL)

if not files:
    print("O Agente não propôs correções ou o formato de resposta falhou.")

for file_path, content in files:
    file_path = file_path.strip()
    full_path = os.path.join(os.getcwd(), file_path)
    os.makedirs(os.path.dirname(full_path), exist_ok=True)
    with open(full_path, "w", encoding="utf-8") as f:
        f.write(content.strip())
