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
    private const int tamanhoDaTabela = 131;    // tamanho fixo da tabela de hash
    private Tipo[] dados;                       // array para armazenar os registros
    private List<string> chaves;                // lista para armazenar as chaves
    private int tamanho;                        // variável para armazenar o tamanho da tabela

    public List<string> Chaves => chaves;       // propriedade para acessar a lista de chaves

    public SondagemQuadratica()
    {
        dados = new Tipo[tamanhoDaTabela];
        chaves = new List<string>();
    }

    // Novo construtor para o rehash
    public SondagemQuadratica(int tamanhoPersonalizado)
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

    // Existe: Verifica se o item existe
    public bool Existe(Tipo item, out int onde)
    {
        onde = -1;
        int indiceInicial = Hash(item.Chave);   // calcula índice base
        int indice = indiceInicial;
        int i = 1;                  // contador da sondagem quadrática

        while (dados[indice] != null)           // enquanto houver item
        {
            if (dados[indice].Equals(item))     // se encontrou o item
            {
                onde = indice;      // posição encontrada
                return true;
            }

            indice = indiceInicial + i * i;     // soma i² à posição original
            i++;
        }

        return false; // não achou
    }

    // Hash: Função primária de hash
    public int Hash(string chave)
    {
        int hash = Math.Abs(chave.GetHashCode());   // hash padrão
        int indice = hash % tamanhoDaTabela;        // reduz os limites da tabela

        return indice;
    }

    // Inserir: Insere um item na tabela de hash
    public void Inserir(Tipo item)
    {
        int indice = Hash(item.Chave);        // calcula índice base
        int i = 1;

        while (dados[indice] != null)         // se houver colisão
        {
            indice = (indice + i * i);        // aplica sondagem quadrática
            i++;
        }

        dados[indice] = item;               // insere item
        chaves.Add(item.Chave);             // adiciona chave
    }

    // Remover: Remove um item da tabela de hash
    public bool Remover(Tipo item)
    {
        int indice = -1;
        if (Existe(item, out indice))       // verifica se item existe
        {
            dados[indice] = default(Tipo);    // remove a posição
            chaves.Remove(item.Chave);        // remove a chave
            return true;
        }

        return false;      // não encontrou
    }

    // Buscar: Busca um item a partir da sua chave
    public Tipo Buscar(string chave)
    {
        int indice = Hash(chave);               // calcula índice base
        int i = 1;
        while (dados[indice] != null)           // enquanto houver item
        {
            if (dados[indice].Chave == chave)   // a chave é igual
                return dados[indice];
            indice = (indice + i * i);          // sondagem quadrática
            i++;
        }

        return default(Tipo);      // não encontrou
    }
}
