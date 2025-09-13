// Nome: Felipe Antônio de Oliveira Almeida     RA: 22130
// Nome: Miguel de Castro Chaga Silva           RA: 22145

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DuploHash<Tipo> : ITabelaHash<Tipo>
    where Tipo : IRegistro<Tipo>, IComparable<Tipo>
{
    private const int tamanhoDaTabela = 131;    // tamanho fixo da tabela de hash
    private Tipo[] dados;                       // array para armazenar os registros
    private List<string> chaves;                // lista para armazenar as chaves 
    private int tamanho;                        // variável para armazenar o tamanho da tabela

    public List<string> Chaves => chaves;       // propriedade para acessar a lista de chaves

    public DuploHash()
    {
        dados = new Tipo[tamanhoDaTabela];
        chaves = new List<string>();
    }

    // Novo construtor para o rehash
    public DuploHash(int tamanhoPersonalizado)
    {
        tamanho = tamanhoPersonalizado;   // define o novo tamanho
        dados = new Tipo[tamanho];
        chaves = new List<string>();
    }

    // Conteudo: Retorna o conteúdo da tabela de hash
    public List<string> Conteudo()
    {
        List<string> conteudo = new List<string>();

        for (int i = 0; i < dados.Length; i++)    // percorre o array
            if (dados[i] != null)
                conteudo.Add(i + ": " + dados[i].Chave + " - " + dados[i].Dados);  // índice + chave + dados

        return conteudo;    // retorna a lista formatada em índice + chave + dados
    }

    // Hash: Função primária de hash
    public int Hash(string chave)
    {
        int hash1 = Math.Abs(chave.GetHashCode());   // hash padrão

        return hash1 % tamanhoDaTabela;             // reduz para os limites da tabela
    }

    // Hash: Função secundária de hash
    private int Hash2(string chave)
    {
        return 7 - (Hash(chave) % 7);  // deslocamento diferente de 0
    }

    // Inserir: Insere um item na tabela de hash
    public void Inserir(Tipo item)
    {
        int indice = Hash(item.Chave);      // calcula o indice base

        while (dados[indice] != null)       // enquanto houver colisão
        {
            indice = (indice + Hash2(item.Chave));  // duplo hash
        }

        dados[indice] = item;       // insere na posição encontrada
        chaves.Add(item.Chave);     // adiciona a chave 
    }

    // Remover: Remove um item da tabela de hash
    public bool Remover(Tipo item)
    {
        int indice = -1;
        if (Existe(item, out indice))       // verifica se o indice existe
        {
            dados[indice] = default(Tipo);  // remove o item
            chaves.Remove(item.Chave);      // remove a chave
            return true;
        }

        return false;           // caso o indice não exista
    }

    // Existe: Verifica se o item existe
    public bool Existe(Tipo item, out int onde)
    {
        onde = -1;
        int indice = Hash(item.Chave);  // calcula o indice base

        while (dados[indice] != null)   // enquanto houver itens
        {
            if (dados[indice].Equals(item))
            {
                onde = indice;  // se encontrou o item, guarda sua posição
                return true;    // o item existe
            }

            indice = (indice + Hash2(item.Chave));  // busca por duplo hash
        }

        return false;  // caso o indice não exista
    }

    // Buscar: Busca um item a partir da sua chave
    public Tipo Buscar(string chave)
    {
        int indice = Hash(chave);       // calcula o indice base

        while (dados[indice] != null)   // enquanto houver item
        {
            if (dados[indice].Chave == chave)   // encontrou a chave
                return dados[indice];           // retorna seu indice

            indice = (indice + Hash2(chave));  // busca por duplo hash
        }

        return default(Tipo);  // caso não encontre
    }

}
