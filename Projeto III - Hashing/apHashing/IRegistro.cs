// Nome: Felipe Antônio de Oliveira Almeida     RA: 22130
// Nome: Miguel de Castro Chaga Silva           RA: 22145

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public interface IRegistro<Tipo> where Tipo : IComparable<Tipo>
{
    Tipo LerRegistro(StreamReader arquivo);
    void EscreverRegistro(StreamWriter arquivo);
    int CompareTo(Tipo outro);
    string Chave { get; }
}
