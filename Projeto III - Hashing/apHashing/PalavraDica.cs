// Nome: Felipe Antônio de Oliveira Almeida     RA: 22130
// Nome: Miguel de Castro Chaga Silva           RA: 22145

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PalavraDica : IComparable<PalavraDica>, IRegistro<PalavraDica>
{
    public string Palavra { get; private set; }   // private set indica que valor Palavra só pode ser definido dentro da classe
    public string Dica { get; set; }  // permite leitura e modificação do valor Dica de fora da classe

    // construtor
    public PalavraDica(string palavra, string dica)
    {
        Palavra = palavra;
        Dica = dica;
    }

    public PalavraDica LerRegistro(StreamReader arquivo)
    {
        var linha = arquivo.ReadLine();
        if (linha == null) return null;
        string palavra = linha.Length >= 30 ? linha.Substring(0, 30).Trim() : linha.Trim();
        string dica = linha.Length > 30 ? linha.Substring(30).Trim() : string.Empty;
        return new PalavraDica(palavra, dica);
    }

    public void EscreverRegistro(StreamWriter arquivo)
    {
        arquivo.WriteLine(FormatoDeArquivo());
    }

    public string Chave => Palavra;     // implementa a propriedade "Chave" da interface IRegistro
    public string Dados => Dica;        // implementa a propriedade "Dados" da interface IRegistro

    // método que implementa a interface IComparable, ele permite que os objetos dessa classe sejam comparados para ordenação
    public int CompareTo(PalavraDica outra)
    {
        // a comparação é feita de forma insensível a se começarem com caracteres maiúsculas ou caracteres minúsculas. 
        return string.Compare(this.Palavra, outra?.Palavra, StringComparison.OrdinalIgnoreCase); // 'A' e 'a' são considerados iguais
    }

    // sobrescreve ToString da classe Object
    // ele realiza uma formatação personalizada dos objetos 'Palavra e Dica'
    public override string ToString()
    {
        // Trim() remove espaços em branco no início e no fim da palavra (e lida com null usando o operador ?)
        return $"{Palavra?.Trim()} - {Dica}";
    }

    // define um formato de arquivo para representar o objeto ao salvá-lo
    public string FormatoDeArquivo()
    {
        // retorna uma string com 'Palavra e Dica' para ser salvo em uma linha de um arquivo de texto
        // Palavra (fixa em 30 caracteres) seguida por Dica (até o final da linha)
        string palavraFormatada = Palavra?.PadRight(30, ' ').Substring(0, 30) ?? new string(' ', 30);
        return $"{palavraFormatada}{Dica}";
    }
}
