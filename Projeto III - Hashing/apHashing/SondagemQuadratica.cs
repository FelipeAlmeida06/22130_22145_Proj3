// Nome: Felipe Antônio de Oliveira Almeida     RA: 22130
// Nome: Miguel de Castro Chaga Silva           RA: 22145

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SondagemQuadratica<Tipo> : ITabelaHash<Tipo>
    where Tipo : IComparable<Tipo>, IRegistro<Tipo>
{
    private const int tamanhoDaTabela = 131;
    private Tipo[] dados;
    private List<string> chaves;

    public List<string> Chaves => chaves;

    public SondagemQuadratica()
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

    public bool Existe(Tipo item, out int onde)
    {
        onde = -1;
        int indiceInicial = Hash(item.Chave);
        int indice = indiceInicial;
        int i = 1;

        while (dados[indice] != null)
        {
            if (dados[indice].Equals(item))
            {
                onde = indice;
                return true;
            }

            indice = indiceInicial + i * i;
            i++;
        }

        return false;
    }

    public int Hash(string chave)
    {
        int hash = Math.Abs(chave.GetHashCode());
        int indice = hash % tamanhoDaTabela;

        return indice;
    }

    public void Inserir(Tipo item)
    {
        int indice = Hash(item.Chave);
        int i = 1;

        while (dados[indice] != null)
        {
            indice = (indice + i * i);
            i++;
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
        int i = 1;
        while (dados[indice] != null)
        {
            if (dados[indice].Chave == chave)
                return dados[indice];
            indice = (indice + i * i);
            i++;
        }

        return default(Tipo);
    }
}
