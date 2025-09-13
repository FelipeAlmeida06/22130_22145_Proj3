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
    private const int SIZE = 131;           // tamanho fixo da tabela de hash
    private ListaSimples<Tipo>[] dados;     // classe lista ligada simples, cada posição representa uma célula da tabela hash
    private List<string> chaves;            // lista para armazenar as chaves
    private int tamanho;                    // variável para armazenar o tamanho da tabela

    public List<string> Chaves => chaves;       // propriedade para acessar a lista de chaves

    public BucketHash()
    {
        chaves = new List<string>();             // inicializa lista de chaves     
        dados = new ListaSimples<Tipo>[SIZE];    // cria um array
        for (int i = 0; i < SIZE; i++)           // inicializa cada um como lista vazia
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

    // Hash: Função primária de hash
    public int Hash(string chave)
    {
        long tot = 0;

        for (int i = 0; i < chave.Length; i++)   // percorre os caracteres da chave
            tot += 37 * tot + (char)chave[i];    // gera o valor hash

        tot = tot % dados.Length;   // reduz o limite da tabela
        if (tot < 0)
            tot += dados.Length;

        return (int)tot;    // retorna indice
    }

    // Inserir: Insere um item na tabela de hash
    public void Inserir(Tipo item)
    {
        int valorDeHash = Hash(item.Chave);

        // Verifica se o item já existe
        if (dados[valorDeHash].Buscar(item) == null)
        {
            dados[valorDeHash].InserirAposFim(item);    // insere ao final da lista
            chaves.Add(item.Chave);                     // adiciona chave
        }
    }

    // Remover: Remove um item da tabela de hash
    public bool Remover(Tipo item)
    {
        int onde = Hash(item.Chave);
        if (dados[onde].Remover(item))      // remove da lista ligada
        {
            chaves.Remove(item.Chave);      // remove chave
            return true;    // encontrou
        }
        return false;       // não encontrou
    }

    // Existe: Verifica se o item existe
    public bool Existe(Tipo item, out int onde)
    {
        onde = Hash(item.Chave);
        return dados[onde].Buscar(item) != null;    // encontrou
    }

    // Conteudo: Retorna o conteúdo da tabela de hash
    public List<string> Conteudo()
    {
        List<string> saida = new List<string>();
        for (int i = 0; i < dados.Length; i++)      
            if (!dados[i].EstaVazia)                // se não está vazio
            {
                string linha = $"{i} : ";
                NoLista<Tipo> atual = dados[i].Primeiro;    // primeiro nó da lista
                while (atual != null)               // percorre a lista
                {
                    linha += atual.Info.Chave + " - " + atual.Info.Dados;    // concatena chave e dados 
                    atual = atual.Prox;             // próximo nó
                }
                saida.Add(linha);                   // adiciona linha na saída
            }
        return saida;   // retorna lista com conteúdo
    }

    // Buscar: Busca um item a partir da sua chave
    public Tipo Buscar(string chave)
    {
        int valorDeHash = Hash(chave);      // calcula o valor de hash
        NoLista<Tipo> atual = dados[valorDeHash].Primeiro; // obtém o primeiro nó da lista

        while (atual != null)               // percorre lista
        {
            if (atual.Info.Chave == chave)  // se encontrou chave
            {
                return atual.Info;          // item
            }
            atual = atual.Prox;             // avança na lista
        }

        return default(Tipo);               // não encontrou
    }
}
