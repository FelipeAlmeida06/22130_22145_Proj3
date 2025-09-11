using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//public class ReHash<Tipo> : ITabelaHash<Tipo>
    //where Tipo : IRegistro<Tipo>, IComparable<Tipo>

public class ReHash<Tipo> where Tipo : IRegistro<Tipo>, IComparable<Tipo>
{
    private static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        for (int i = 3; i * i <= number; i += 2)
        {
            if (number % i == 0) return false;
        }

        return true;
    }

    private static int GetNextPrime(int number)
    {
        int nextPrime = number + 1;
        while (!IsPrime(nextPrime))
        {
            nextPrime++;
        }
        return nextPrime;
    }

    
    public static ITabelaHash<Tipo> Redimensionar(ITabelaHash<Tipo> tabelaAntiga)
    {
        int novoTamanho = GetNextPrime(tabelaAntiga.Chaves.Count * 2);

        // Cria uma nova instância da tabela com o novo tamanho
        ITabelaHash<Tipo> novaTabela;
        if (tabelaAntiga.GetType() == typeof(SondagemLinear<Tipo>))
        {
            novaTabela = new SondagemLinear<Tipo>(novoTamanho);                     // ajustado para rehash
        }
        else if (tabelaAntiga.GetType() == typeof(SondagemQuadratica<Tipo>))
        {
            novaTabela = new SondagemQuadratica<Tipo>(novoTamanho);                 // ajustar
        }
        else if (tabelaAntiga.GetType() == typeof(DuploHash<Tipo>))
        {
            novaTabela = new DuploHash<Tipo>(novoTamanho);                          // ajustar
        }
        else if (tabelaAntiga.GetType() == typeof(BucketHash<Tipo>))
        {
            novaTabela = new BucketHash<Tipo>(novoTamanho);                         // ajustar
        }
        else
        {
            throw new InvalidOperationException("Tipo de tabela de hash não suportado para rehash.");
        }

        // Reinsere todos os itens da tabela antiga na nova tabela
        foreach (var chave in tabelaAntiga.Chaves)
        {
            var item = tabelaAntiga.Buscar(chave);
            if (item != null)
            {
                novaTabela.Inserir(item);
            }
        }

        return novaTabela;
    }
    
}
