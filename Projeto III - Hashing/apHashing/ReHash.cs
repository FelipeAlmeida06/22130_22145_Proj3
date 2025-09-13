// Nome: Felipe Antônio de Oliveira Almeida     RA: 22130
// Nome: Miguel de Castro Chaga Silva           RA: 22145

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ReHash<Tipo> where Tipo : IRegistro<Tipo>, IComparable<Tipo>
{
    // Verifica se número é primo
    private static bool IsPrime(int number)
    {
        if (number <= 1) return false;              // números menores ou iguais a 1 não são primos
        if (number == 2) return true;               // número é primo
        if (number % 2 == 0) return false;          // pares maiores que 2

        for (int i = 3; i * i <= number; i += 2)    // divisores ímpares
        {
            if (number % i == 0) return false;      // se for divisível, não é primo
        }

        return true;   // número é primo
    }

    // Encontra o próximo número primo
    private static int GetNextPrime(int number)
    {
        int nextPrime = number + 1;       // próximo número
        while (!IsPrime(nextPrime))       // enquanto não for primo
        {
            nextPrime++;        // incrementa o próximo primo
        }
        return nextPrime;       // número primo encontrado
    }

    // Redimensiona a tabela de hash
    public static ITabelaHash<Tipo> Redimensionar(ITabelaHash<Tipo> tabelaAntiga)
    {
        // novo tamanho: próximo primo depois do dobro do número de chaves atuais
        int novoTamanho = GetNextPrime(tabelaAntiga.Chaves.Count * 2);

        // cria uma nova instância da tabela com o novo tamanho
        ITabelaHash<Tipo> novaTabela;
        if (tabelaAntiga.GetType() == typeof(SondagemLinear<Tipo>))
        {
            novaTabela = new SondagemLinear<Tipo>(novoTamanho);                     // tabela de hash Sondagem Linear
        }
        else if (tabelaAntiga.GetType() == typeof(SondagemQuadratica<Tipo>))
        {
            novaTabela = new SondagemQuadratica<Tipo>(novoTamanho);                 // tabela de hash Sondagem Quadrática
        }
        else if (tabelaAntiga.GetType() == typeof(DuploHash<Tipo>))
        {
            novaTabela = new DuploHash<Tipo>(novoTamanho);                          // tabela de hash Duplo Hash
        }
        else if (tabelaAntiga.GetType() == typeof(BucketHash<Tipo>))
        {
            novaTabela = new BucketHash<Tipo>(novoTamanho);                         // tabela de hash Bucket Hash
        }
        else
        {
            throw new InvalidOperationException("Tipo de tabela de hash não suportado para rehash.");   // erro
        }

        // reinsere todos os itens da tabela antiga na nova tabela
        foreach (var chave in tabelaAntiga.Chaves)     // percorre todas as chaves
        {
            var item = tabelaAntiga.Buscar(chave);     // busca o item associado à chave
            if (item != null)    // se existe
            {
                novaTabela.Inserir(item);     // reinsere na nova tabela de hash
            }
        }

        return novaTabela;    // retorna a tabela rehashizada
    }
    
}
