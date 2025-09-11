// Nome: Felipe Antônio de Oliveira Almeida     RA: 22130
// Nome: Miguel de Castro Chaga Silva           RA: 22145

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITabelaHash<Tipo> where Tipo : IComparable<Tipo>         // IRegistro<Tipo>
{
    int Hash(string chave);
    void Inserir(Tipo item);
    bool Remover(Tipo item);
    bool Existe(Tipo item, out int onde);
    List<string> Conteudo();
    Tipo Buscar(string chave);
    List<string> Chaves { get; }
}
