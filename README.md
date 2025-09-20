# 22130_22145_Proj3

Projeto 2 - Técnicas de Hashing
Disciplina: Estruturas de Dados

Implementação completa do programa em C# para uma aplicação de gerenciamento de palavras e dicas, utilizando 4 diferentes tipos de tabelas de hash para armazenar e manipular os dados.

Possui ainda uma classe auxiliar chamada ReHash, que é responsável por redimensionar a tabela de hash quando ela atinge um certo fator de carga.

A aplicação utiliza as seguintes técnicas de tratamento de colisão:

* **Bucket Hash**.
* **Duplo Hash**.
* **Sondagem Linear**.
* **Sondagem Quadrática**.

O projeto permite que o usuário interaja por meio de uma interface gráfica. Essa interface é capaz de:
- Carregar dados de um arquivo texto (.txt).
- Adicionar novos registros.
- Buscar e listar dados.
- Alterar registros.
- Remover registros. 
- Redimensionar a tabela (Rehash).

## Conclusões

O código foi comentado para facilitar o entendimento e manutenções futuras.

O projeto foi testado para garantir um tratamento robusto de erros e interface de fácil uso.