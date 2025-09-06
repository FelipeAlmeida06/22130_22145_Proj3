// Nome: Felipe Antônio de Oliveira Almeida     RA: 22130
// Nome: Miguel de Castro Chaga Silva           RA: 22145

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BucketHash<Tipo> : ITabelaHash<Tipo>
            where Tipo : IRegistro<Tipo>,
                         IComparable<Tipo>
{
    private const int SIZE = 131;  // para gerar mais colisões; o ideal é primo > 100
    ArrayList[] dados;
    private List<string> chaves;

    public List<string> Chaves => chaves;

    public BucketHash()
    {
        chaves = new List<string>();
        dados = new ArrayList[SIZE];
        for (int i = 0; i < SIZE; i++)
            dados[i] = new ArrayList(1);
    }

    public int Hash(string chave)
    {
        long tot = 0;

        for (int i = 0; i < chave.Length; i++)
            tot += 37 * tot + (char)chave[i];

        tot = tot % dados.Length;
        if (tot < 0)
            tot += dados.Length;

        return (int)tot;
    }

    public void Inserir(Tipo item)
    {
        int valorDeHash = Hash(item.Chave);
        if (!dados[valorDeHash].Contains(item)) // Contains procura o item e retorna True ou False
        {
            dados[valorDeHash].Add(item);
            chaves.Add(item.Chave);
        }
    }

    public bool Remover(Tipo item)
    {
        int onde = 0;
        if (!Existe(item, out onde))
            return false;
        dados[onde].Remove(item);
        chaves.Remove(item.Chave);
        return true;
    }

    public bool Existe(Tipo item, out int onde)
    {
        onde = Hash(item.Chave);
        return dados[onde].Contains(item);
    }

    public List<string> Conteudo()
    {
        List<string> saida = new List<string>();
        for (int i = 0; i < dados.Length; i++)
            if (dados[i].Count > 0)
            {
                string linha = $"{i,5} : ";
                foreach (Tipo item in dados[i])
                    linha += " | " + item.Chave;
                saida.Add(linha);
            }
        return saida;
    }

    public Tipo Buscar(string chave)
    {
        int valorDeHash = Hash(chave);
        foreach (Tipo item in dados[valorDeHash])
            if (item.Chave == chave)
                return item;
        return default(Tipo);
    }
}
