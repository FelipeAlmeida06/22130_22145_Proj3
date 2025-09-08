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
                        //bool primeiraLinhaArqTexto = true;

                        while (!sr.EndOfStream)
                        {
                            string linha = sr.ReadLine();
                            if (string.IsNullOrWhiteSpace(linha)) continue;

                            // Ignorar a primeira linha (cabeçalho)
                            //if (primeiraLinhaArqTexto)
                            //{
                                //primeiraLinhaArqTexto = false;
                                //continue;
                            //}

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
                MessageBox.Show("Preencha os campos de Palavra e Dica para incluir o registro.");
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

        // Será acionado quando o usuário clicar em um item da lista. O evento irá extrair a palavra e a dica da
        // linha selecionada e preencher os campos txtPalavra e txtDica
        private void lsbListagem_SelectedIndexChanged(object sender, EventArgs e)
        {

            /*
            // Verifica se um item está selecionado
            if (lsbListagem.SelectedIndex != -1)
            {
                // Obtém o texto do item selecionado
                string selectedItem = lsbListagem.SelectedItem.ToString();

                // Separa o hash da palavra/dica
                int separatorIndex = selectedItem.IndexOf(':');
                if (separatorIndex != -1)
                {
                    string palavraDica = selectedItem.Substring(separatorIndex + 1).Trim();

                    // Separa a palavra da dica. Usa LastIndexOf para garantir que o hífen da dica não cause erros.
                    int lastSeparator = palavraDica.LastIndexOf(" - ");
                    if (lastSeparator != -1)
                    {
                        string palavra = palavraDica.Substring(0, lastSeparator).Trim();
                        string dica = palavraDica.Substring(lastSeparator + 3).Trim();

                        // Para o BucketHash, remova o "|" se existir
                        if (palavra.StartsWith("|"))
                        {
                            palavra = palavra.Replace("|", "").Trim();
                        }

                        txtPalavra.Text = palavra;
                        txtDica.Text = dica;
                    }
                }
            }
            */



            if (lsbListagem.SelectedIndex != -1)
            {
                string selectedItem = lsbListagem.SelectedItem.ToString();

                int separatorIndex = selectedItem.IndexOf(':');
                if (separatorIndex != -1)
                {
                    string palavraDica = selectedItem.Substring(separatorIndex + 1).Trim();

                    int lastSeparator = palavraDica.LastIndexOf(" - ");
                    if (lastSeparator != -1)
                    {
                        string palavra = palavraDica.Substring(0, lastSeparator).Trim();
                        string dica = palavraDica.Substring(lastSeparator + 3).Trim();

                        if (palavra.StartsWith("|"))
                        {
                            palavra = palavra.Replace("|", "").Trim();
                        }

                        txtPalavra.Text = palavra;
                        txtDica.Text = dica;

                        // Desativa a edição do campo da palavra.
                        txtPalavra.Enabled = false;
                    }
                }
            }
            else
            {
                // Reativa a edição se nada estiver selecionado.
                txtPalavra.Enabled = true;
            }
        }

        // Exclusão de palavra
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // Verifica se o usuário já escolheu um arquivo
            if (string.IsNullOrWhiteSpace(dlgAbrirArquivo.FileName))
            {
                MessageBox.Show("Nenhum arquivo selecionado! Por favor, clique em 'Abrir Arquivo' para escolher um arquivo antes de excluir.");
                return;
            }

            string palavra = txtPalavra.Text.Trim();

            // Verifica se o campo de Palavra está vazio
            if (string.IsNullOrWhiteSpace(palavra))
            {
                MessageBox.Show("Preencha os campos de Palavra e Dica para excluir o registro.");
                return;
            }

            try
            {
                // Usa o método Buscar para encontrar o registro completo (Palavra + Dica)
                var itemParaRemover = tabelaDeHash.Buscar(palavra);

                // Verifica se o registro foi encontrado e tenta removê-lo
                if (itemParaRemover != null && tabelaDeHash.Remover(itemParaRemover))
                {
                    MessageBox.Show($"A palavra '{palavra}' foi removida com sucesso.");

                    // Recria o arquivo texto com o conteúdo atualizado
                    RecriarArquivoTexto();

                    // Atualiza e ordena a ListBox
                    AtualizarListBox();
                }
                else
                {
                    MessageBox.Show($"Não foi possível encontrar a palavra '{palavra}' para exclusão.");
                }

                // Limpa os campos após a exclusão bem-sucedida
                txtPalavra.Clear();
                txtDica.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir: " + ex.Message);
            }
        }


        //============================================================================================================================
        // FUNÇÕES NECESSÁRIAS PARA EVENTO DO BOTÃO EXCLUIR
        private void RecriarArquivoTexto()
        {
            // Recria o arquivo texto com o conteúdo atualizado (sem o item removido)
            var todosOsItens = tabelaDeHash.Chaves;
            using (var sw = new StreamWriter(dlgAbrirArquivo.FileName))
            {
                // Escreve o cabeçalho
                //sw.WriteLine("Palavra                       Dica");

                // Percorre todas as chaves restantes e as escreve no arquivo
                foreach (var chave in todosOsItens)
                {
                    var item = tabelaDeHash.Buscar(chave);
                    if (item != null)
                    {
                        // Usa a propriedade Dados do PalavraDica para obter a dica
                        string registro = item.Chave.PadRight(30).Substring(0, 30) + item.Dados;
                        sw.WriteLine(registro);
                    }
                }
            }
        }

        private void AtualizarListBox()
        {
            // Atualiza e ordena a ListBox
            lsbListagem.Items.Clear();
            var conteudoOrdenado = tabelaDeHash.Conteudo();
            conteudoOrdenado.Sort((a, b) => {
                var hashA = int.Parse(a.Split(':')[0].Trim());
                var hashB = int.Parse(b.Split(':')[0].Trim());
                return hashA.CompareTo(hashB);
            });

            foreach (var item in conteudoOrdenado)
            {
                lsbListagem.Items.Add(item);
            }
        }
        //============================================================================================================================

        // Alteração de dica de uma dada palavra
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            // Validar se um arquivo está aberto e se os campos estão preenchidos.
            if (string.IsNullOrWhiteSpace(dlgAbrirArquivo.FileName))
            {
                MessageBox.Show("Nenhum arquivo selecionado! Por favor, clique em 'Abrir Arquivo' para escolher um arquivo antes de alterar.");
                return;
            }

            string palavra = txtPalavra.Text.Trim();
            string novaDica = txtDica.Text.Trim();

            if (string.IsNullOrWhiteSpace(palavra) || string.IsNullOrWhiteSpace(novaDica))
            {
                MessageBox.Show("Preencha os campos de Palavra e Dica para alterar o registro.");
                return;
            }

            try
            {
                // Busca o registro completo na tabela hash usando apenas a palavra como chave.
                var itemParaAlterar = tabelaDeHash.Buscar(palavra);

                if (itemParaAlterar != null)
                {
                    // Atualiza a propriedade Dica do objeto encontrado.
                    itemParaAlterar.Dica = novaDica;

                    // Salva as alterações no arquivo de texto.
                    RecriarArquivoTexto();

                    // Atualiza a lista exibida na interface para refletir a mudança.
                    AtualizarListBox();

                    MessageBox.Show($"A dica da palavra '{palavra}' foi alterada com sucesso.");

                    // Limpa os campos após a alteração bem-sucedida.
                    txtPalavra.Clear();
                    txtDica.Clear();
                }
                else
                {
                    MessageBox.Show($"Não foi possível encontrar a palavra '{palavra}' para alteração.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar: " + ex.Message);
            }
        }

        // Listagem de todas as palavras e dicas armazenadas na tabela de hash
        private void btnListar_Click(object sender, EventArgs e)
        {

        }
    }
}
