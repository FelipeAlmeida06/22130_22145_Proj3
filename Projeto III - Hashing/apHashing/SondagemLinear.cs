// Nome: Felipe Antônio de Oliveira Almeida     RA: 22130
// Nome: Miguel de Castro Chaga Silva           RA: 22145

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SondagemLinear<Tipo> : ITabelaHash<Tipo>
    where Tipo : IRegistro<Tipo>, IComparable<Tipo>
{
    private const int tamanhoDaTabela = 131;
    Tipo[] dados;
    private List<string> chaves;

    public List<string> Chaves => chaves;

    public SondagemLinear()
    {
        dados = new Tipo[tamanhoDaTabela];
        chaves = new List<string>();
    }

    public List<string> Conteudo()
    {
        List<string> conteudo = new List<string>();

        for (int i = 0; i < dados.Length; i++)
            if (dados[i] != null)
                conteudo.Add(i + ": " + dados[i].Chave + " - " + dados[i].Dados);

        return conteudo;
    }

    public int Hash(string chave)
    {
        return Math.Abs(chave.GetHashCode()) % tamanhoDaTabela;
    }

    public bool Existe(Tipo item, out int onde)
    {
        onde = -1;
        int indice = Hash(item.Chave);

        while (dados[indice] != null)
        {
            if (dados[indice].Equals(item))
            {
                onde = indice;
                return true;
            }

            indice = (indice + 1);
        }

        return false;
    }

    public void Inserir(Tipo item)
    {
        int indice = Hash(item.Chave);

        while (dados[indice] != null)
        {
            indice = (indice + 1);
            if (indice >= tamanhoDaTabela)
                break;
        }

        dados[indice] = item;
        chaves.Add(item.Chave);
    }

    public bool Remover(Tipo item)
    {
        int indice = -1;
        if (Existe(item, out indice))
        {
            dados[indice] = default(Tipo);
            chaves.Remove(item.Chave);
            return true;
        }

        return false;
    }

    public Tipo Buscar(string chave)
    {
        int indice = Hash(chave);
        while (dados[indice] != null)
        {
            if (dados[indice].Chave == chave)
                return dados[indice];
            indice = (indice + 1);
        }
        return default(Tipo);
    }
}
