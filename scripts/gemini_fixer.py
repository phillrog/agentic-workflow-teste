import os
import re
import google.generativeai as genai

genai.configure(api_key=os.environ["GEMINI_API_KEY"])
model = genai.GenerativeModel('gemini-3-flash-preview')

# Tenta ler o log de erro gerado pelo Workflow
try:
    with open("build_log.txt", "r") as f:
        error_log = f.read()
except FileNotFoundError:
    error_log = "Log de erro não encontrado no diretório raiz."

prompt = f"""
Você é um Arquiteto .NET Senior. O código anterior falhou no build ou nos testes.
ERROS ENCONTRADOS:
{error_log}

TAREFA:
1. Analise os arquivos atuais e corrija os erros apontados.
2. Se for erro de teste, ajuste a lógica na Domain para satisfazer o xUnit.
3. Se for erro de namespace ou referência, corrija a estrutura de arquivos/usings.

Retorne APENAS os arquivos corrigidos no formato:
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
