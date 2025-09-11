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
    //ArrayList[] dados;             // ALTERAR AQUI: Implementar ListaSimples
    private ListaSimples<Tipo>[] dados;
    private List<string> chaves;
    private int tamanho;    // variável para armazenar o tamanho da tabela

    public List<string> Chaves => chaves;

    public BucketHash()
    {
        /*
        chaves = new List<string>();
        dados = new ArrayList[SIZE];
        for (int i = 0; i < SIZE; i++)
            dados[i] = new ArrayList(1);
        */


        chaves = new List<string>();
        dados = new ListaSimples<Tipo>[SIZE];
        for (int i = 0; i < SIZE; i++)
            dados[i] = new ListaSimples<Tipo>();
    }

    // Novo construtor para o rehash
    public BucketHash(int tamanhoPersonalizado)
    {
        tamanho = tamanhoPersonalizado;
        dados = new ListaSimples<Tipo>[tamanho];
        chaves = new List<string>();
        for (int i = 0; i < tamanho; i++)
        {
            dados[i] = new ListaSimples<Tipo>();
        }
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
        /*
        int valorDeHash = Hash(item.Chave);
        if (!dados[valorDeHash].Contains(item)) // Contains procura o item e retorna True ou False
        {
            dados[valorDeHash].Add(item);
            chaves.Add(item.Chave);
        }
        */


        //int valorDeHash = Hash(item.Chave);

        // Aqui não temos Contains pronto em ListaSimples, então precisamos verificar manualmente
        //if (!Existe(item, out _))
        //{
        //dados[valorDeHash].InserirAposFim(item);
        //chaves.Add(item.Chave);
        //}


        int valorDeHash = Hash(item.Chave);

        // Verifica se o item já existe usando o novo método Buscar da ListaSimples
        if (dados[valorDeHash].Buscar(item) == null)
        {
            dados[valorDeHash].InserirAposFim(item);
            chaves.Add(item.Chave);
        }
    }

    public bool Remover(Tipo item)
    {
        /*
        int onde = 0;
        if (!Existe(item, out onde))
            return false;
        dados[onde].Remove(item);
        chaves.Remove(item.Chave);
        return true;
        */


        //int onde;
        //if (!Existe(item, out onde))
        //return false;

        // Para agora, só removemos da lista de chaves.
        //chaves.Remove(item.Chave);
        //return true;


        int onde = Hash(item.Chave);
        if (dados[onde].Remover(item)) // Usa o novo método Remover
        {
            chaves.Remove(item.Chave);
            return true;
        }
        return false;
    }

    public bool Existe(Tipo item, out int onde)
    {
        /*
        onde = Hash(item.Chave);
        return dados[onde].Contains(item);
        */


        //onde = Hash(item.Chave);
        // Vamos percorrer a lista ligada manualmente
        //var atual = dados[onde];
        //var no = typeof(ListaSimples<Tipo>)
        //.GetField("primeiro", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
        //?.GetValue(atual) as NoLista<Tipo>;

        //while (no != null)
        //{
        //if (no.Info.CompareTo(item) == 0)
        //return true;
        //no = no.Prox;
        //}

        //return false;


        onde = Hash(item.Chave);
        return dados[onde].Buscar(item) != null; // Usa o novo método Buscar
    }

    public List<string> Conteudo()
    {
        /*
        List<string> saida = new List<string>();
        for (int i = 0; i < dados.Length; i++)
            if (dados[i].Count > 0)
            {
                string linha = $"{i} : ";                               // string linha = $"{i,5} : ";
                foreach (Tipo item in dados[i])
                    //linha += " | " + item.Chave + " - " + item.Dados;
                    linha += item.Chave + " - " + item.Dados;           // retirei a barra ' I '
                saida.Add(linha);
            }
        return saida;
        */

        /*
        List<string> saida = new List<string>();
        for (int i = 0; i < dados.Length; i++)
        {
            // Percorrer cada bucket
            var no = typeof(ListaSimples<Tipo>)
                .GetField("primeiro", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(dados[i]) as NoLista<Tipo>;

            if (no != null)
            {
                string linha = $"{i} : ";
                while (no != null)
                {
                    linha += no.Info.Chave + " - " + no.Info.Dados + " ";
                    no = no.Prox;
                }
                saida.Add(linha.Trim());
            }
        }
        return saida;
        */


        List<string> saida = new List<string>();
        for (int i = 0; i < dados.Length; i++)
            if (!dados[i].EstaVazia)
            {
                string linha = $"{i} : ";
                NoLista<Tipo> atual = dados[i].Primeiro;
                while (atual != null)
                {
                    linha += atual.Info.Chave + " - " + atual.Info.Dados;
                    atual = atual.Prox;
                }
                saida.Add(linha);
            }
        return saida;
    }

    public Tipo Buscar(string chave)
    {
        /*
        int valorDeHash = Hash(chave);
        foreach (Tipo item in dados[valorDeHash])
            if (item.Chave == chave)
                return item;
        return default(Tipo);
        */

        /*
        int valorDeHash = Hash(chave);

        var no = typeof(ListaSimples<Tipo>)
            .GetField("primeiro", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.GetValue(dados[valorDeHash]) as NoLista<Tipo>;

        while (no != null)
        {
            if (no.Info.Chave == chave)
                return no.Info;
            no = no.Prox;
        }

        return default(Tipo);
        */


        int valorDeHash = Hash(chave);
        NoLista<Tipo> atual = dados[valorDeHash].Primeiro; // Obtém o primeiro nó da lista

        while (atual != null)
        {
            if (atual.Info.Chave == chave)
            {
                return atual.Info;
            }
            atual = atual.Prox;
        }

        return default(Tipo);
    }
}
