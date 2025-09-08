// Nome: Felipe Antônio de Oliveira Almeida     RA: 22130
// Nome: Miguel de Castro Chaga Silva           RA: 22145

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apHashing
{
    public partial class Form1 : Form
    {

        private ITabelaHash<PalavraDica> tabelaDeHash = null;

        public Form1()
        {
            InitializeComponent();
        }

        // Abrir Arquivo de acordo com a técnica de hash escolhida
        private void btnAbrirArquivo_Click(object sender, EventArgs e)
        {
            if (dlgAbrirArquivo.ShowDialog() == DialogResult.OK)
            {
                int hashEscolhido = 0;

                if (rbBucketHash.Checked) hashEscolhido = 1;
                else if (rbSondLinear.Checked) hashEscolhido = 2;
                else if (rbSondQuadrat.Checked) hashEscolhido = 3;
                else if (rbDuploHash.Checked) hashEscolhido = 4;

                //ITabelaHash<PalavraDica> tabelaDeHash = null;

                if (hashEscolhido == 1)
                    tabelaDeHash = new BucketHash<PalavraDica>();
                if (hashEscolhido == 2)
                    tabelaDeHash = new SondagemLinear<PalavraDica>();
                if (hashEscolhido == 3)
                    tabelaDeHash = new SondagemQuadratica<PalavraDica>();
                if (hashEscolhido == 4)
                    tabelaDeHash = new DuploHash<PalavraDica>();

                try
                {
                    using (var sr = new StreamReader(dlgAbrirArquivo.FileName))
                    {
                        bool primeiraLinhaArqTexto = true;

                        while (!sr.EndOfStream)
                        {
                            string linha = sr.ReadLine();
                            if (string.IsNullOrWhiteSpace(linha)) continue;

                            // Ignorar a primeira linha (cabeçalho)
                            if (primeiraLinhaArqTexto)
                            {
                                primeiraLinhaArqTexto = false;
                                continue;
                            }

                            // Palavra nos 30 primeiros caracteres
                            string palavra = linha.Length >= 30 ? linha.Substring(0, 30).Trim() : linha.Trim();

                            // Dica no restante
                            string dica = linha.Length > 30 ? linha.Substring(30).Trim() : string.Empty;

                            var registro = new PalavraDica(palavra, dica);
                            tabelaDeHash.Inserir(registro);
                        }
                    }

                    lsbListagem.Items.Clear();
                    foreach (var item in tabelaDeHash.Conteudo())
                    {
                        lsbListagem.Items.Add(item.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao processar arquivo: " + ex.Message);
                }
            }
        }

        // Inclusão de palavra e dica
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            // Verifica se o usuário já escolheu um arquivo
            if (string.IsNullOrWhiteSpace(dlgAbrirArquivo.FileName))
            {
                MessageBox.Show("Nenhum arquivo selecionado! Por favor, clique em 'Abrir Arquivo' para escolher um arquivo antes de incluir.");
                return;
            }

            string palavra = txtPalavra.Text.Trim();
            string dica = txtDica.Text.Trim();

            if (string.IsNullOrWhiteSpace(palavra) || string.IsNullOrWhiteSpace(dica))
            {
                MessageBox.Show("Preencha os campos de Palavra e Dica.");
                return;
            }

            try
            {
                // Insert new item with its hash value
                var novo = new PalavraDica(palavra, dica);
                tabelaDeHash.Inserir(novo);
                int valorHash = tabelaDeHash.Hash(palavra);
                string newItem = $"{valorHash,5} : {palavra} - {dica}";

                // Clear and re-populate the ListBox with sorted data
                lsbListagem.Items.Clear();
                foreach (var item in tabelaDeHash.Conteudo())
                {
                    lsbListagem.Items.Add(item.ToString());
                }

                // Sort the displayed list after adding the new item
                // This is a simple bubble sort for demonstration, a more efficient sort would be better for a large number of items.
                var items = lsbListagem.Items.Cast<string>().ToList();
                items.Sort((a, b) =>
                {
                    var hashA = int.Parse(a.Split(':')[0].Trim());
                    var hashB = int.Parse(b.Split(':')[0].Trim());
                    return hashA.CompareTo(hashB);
                });

                lsbListagem.Items.Clear();
                foreach (var item in items)
                {
                    lsbListagem.Items.Add(item);
                }

                // Gravar no arquivo (sem hash)
                string registro = palavra.PadRight(30).Substring(0, 30) + dica;
                using (var sw = new StreamWriter(dlgAbrirArquivo.FileName, true))
                    sw.WriteLine(registro);

                // Limpar campos
                txtPalavra.Clear();
                txtDica.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao incluir: " + ex.Message);
            }
        }

        // Exclusão de palavra
        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        // Alteração de dica de uma dada palavra
        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        // Listagem de todas as palavras e dicas armazenadas na tabela de hash
        private void btnListar_Click(object sender, EventArgs e)
        {

        }
    }
}
