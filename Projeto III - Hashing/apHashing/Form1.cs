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
using System.Threading;
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


                // PARA TESTAR A CLASSE REHASH:
                // Cria a tabela de hash com um tamanho inicial de 5. Isso facilita o teste de redimensionamento da classe ReHash
                //ITabelaHash<PalavraDica> tabelaDeHash = null;
                //int tamanhoInicial = 5;

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
                        while (!sr.EndOfStream)
                        {
                            string linha = sr.ReadLine();
                            if (string.IsNullOrWhiteSpace(linha)) continue;
                            // Palavra nos 30 primeiros caracteres
                            string palavra = linha.Length >= 30 ? linha.Substring(0, 30).Trim() : linha.Trim();

                            // Dica no restante
                            string dica = linha.Length > 30 ? linha.Substring(30).Trim() : string.Empty;

                            var registro = new PalavraDica(palavra, dica);
                            tabelaDeHash.Inserir(registro);
                        }
                    }


                    // PARA TESTAR A CLASSE REHASH:
                    // Verifica se o rehash é necessário e o executa.
                    // Aqui, usamos a lógica de rehash baseada no fator de carga (exemplo: se a tabela está 75% cheia).
                    //double fatorCarga = (double)tabelaDeHash.Chaves.Count / tamanhoInicial;
                    //if (fatorCarga >= 0.75)
                    //{
                        //MessageBox.Show("Tabela de Hash atingiu fator de carga e será redimensionada.");

                        // Chama o método estático Redimensionar da classe ReHash
                        //tabelaDeHash = ReHash<PalavraDica>.Redimensionar(tabelaDeHash);
                        //MessageBox.Show("Tabela redimensionada com sucesso!");
                    //}

                    // Mensagem para orientar o usuário
                    MessageBox.Show("Clique no botão Listar para ver os registros armazenados na tabela de hash.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao processar arquivo: " + ex.Message);    // mensagem de erro
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

            // Verifica se os campos de palavra e dica estão vazios/em brancos
            if (string.IsNullOrWhiteSpace(palavra) || string.IsNullOrWhiteSpace(dica))
            {
                MessageBox.Show("Preencha os campos de Palavra e Dica para incluir o registro.");
                return;
            }

            try
            {
                // Verifica se a palavra já existe
                if (tabelaDeHash.Buscar(palavra) != null)
                {
                    MessageBox.Show($"A palavra \"{palavra}\" já existe e não pode ser incluída novamente.");
                    return;
                }

                // Insira um novo item com seu valor de hash
                var novo = new PalavraDica(palavra, dica);
                tabelaDeHash.Inserir(novo);
                int valorHash = tabelaDeHash.Hash(palavra);
                string newItem = $"{valorHash,5} : {palavra} - {dica}";

                // Limpa e preencha novamente o listbox com os dados
                lsbListagem.Items.Clear();
                foreach (var item in tabelaDeHash.Conteudo())
                {
                    lsbListagem.Items.Add(item.ToString());
                }

                // Ordena a lista exibida após adicionar o novo item
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

                // Limpar campos de palavra e dica
                txtPalavra.Clear();
                txtDica.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao incluir: " + ex.Message);     // mensagem de erro
            }
        }

        // Será acionado quando o usuário clicar em um item da lista. O evento irá extrair a palavra e a dica da
        // linha selecionada e preencher os campos txtPalavra e txtDica
        private void lsbListagem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbListagem.SelectedIndex != -1)       // Verifica se existe algum item selecionado
            {
                string itemSelecionado = lsbListagem.SelectedItem.ToString();    // texto do item selecionado

                int separador = itemSelecionado.IndexOf(':');
                if (separador != -1)     // encontrou separador
                {
                    string palavraDica = itemSelecionado.Substring(separador + 1).Trim();   // remove o índice e usa "palavra - dica"

                    int ultimoSeparador = palavraDica.LastIndexOf(" - ");     // último separador
                    if (ultimoSeparador != -1)     // encontrou último separador
                    {
                        string palavra = palavraDica.Substring(0, ultimoSeparador).Trim();    // coleta a palavra
                        string dica = palavraDica.Substring(ultimoSeparador + 3).Trim();      // coleta a dica

                        if (palavra.StartsWith("|"))      // se a palavra tiver um prefixo "|"
                        {
                            palavra = palavra.Replace("|", "").Trim();   // remove o caractere "|"
                        }

                        txtPalavra.Text = palavra;     // preenche o campo da palavra
                        txtDica.Text = dica;           // preenche o campo da dica

                        txtPalavra.Enabled = false;    // desativa a edição do campo da palavra
                    }
                }
            }
            else
            {
                txtPalavra.Enabled = true;             // reativa a edição se nada estiver selecionado
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

                // Verifica se o registro foi encontrado e tenta remover
                if (itemParaRemover != null && tabelaDeHash.Remover(itemParaRemover))
                {
                    MessageBox.Show($"A palavra '{palavra}' foi removida com sucesso.");

                    RecriarArquivoTexto();      // Recria o arquivo texto com o conteúdo atualizado
                    AtualizarListBox();         // Atualiza e ordena a ListBox
                }
                else
                {
                    MessageBox.Show($"Não foi possível encontrar a palavra '{palavra}' para exclusão.");    // mensagem de erro
                }

                // Limpa os campos após a exclusão do registro
                txtPalavra.Clear();
                txtDica.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir: " + ex.Message);     // mensagem de erro
            }
        }


        //============================================================================================================================
        // FUNÇÕES AUXILIARES PARA EVENTO DO BOTÃO EXCLUIR
        private void RecriarArquivoTexto()
        {
            // recria o arquivo texto com o conteúdo atualizado (sem o item removido)
            var todosOsItens = tabelaDeHash.Chaves;
            using (var sw = new StreamWriter(dlgAbrirArquivo.FileName))
            {
                // percorre todas as chaves restantes e as escreve no arquivo
                foreach (var chave in todosOsItens)
                {
                    var item = tabelaDeHash.Buscar(chave);
                    if (item != null)
                    {
                        // usa a propriedade Dados do PalavraDica para obter a dica
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
            var conteudoOrdenado = tabelaDeHash.Conteudo();       // coleta o conteúdo da tabela hash (lista no formato "índice: chave - dados")
            conteudoOrdenado.Sort((a, b) => {
                var hashA = int.Parse(a.Split(':')[0].Trim());    // extrai o índice do item A (antes do ":")
                var hashB = int.Parse(b.Split(':')[0].Trim());    // extrai o índice do item B (antes do ":")
                return hashA.CompareTo(hashB);                    // compara os índices
            });

            foreach (var item in conteudoOrdenado)    // percorre os itens já ordenados
            {
                lsbListagem.Items.Add(item);          // adiciona cada item na ListBox
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
                // busca o registro completo na tabela hash usando apenas a palavra como chave.
                var itemParaAlterar = tabelaDeHash.Buscar(palavra);

                if (itemParaAlterar != null)
                {
                    itemParaAlterar.Dica = novaDica;     // atualiza a propriedade Dica do objeto encontrado
                    RecriarArquivoTexto();     // salva as alterações no arquivo de texto
                    AtualizarListBox();        // atualiza a lista exibida na interface para refletir a mudança.

                    MessageBox.Show($"A dica da palavra '{palavra}' foi alterada com sucesso.");

                    // Limpa os campos após a alteração bem-sucedida.
                    txtPalavra.Clear();
                    txtDica.Clear();
                }
                else
                {
                    MessageBox.Show($"Não foi possível encontrar a palavra '{palavra}' para alteração.");    // mensagem de erro
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar: " + ex.Message);    // mensagem de erro
            }
        }

        // Listagem de todas as palavras e dicas armazenadas na tabela de hash
        private void btnListar_Click(object sender, EventArgs e)
        {
            if (tabelaDeHash == null)
            {
                MessageBox.Show("Nenhum arquivo foi carregado ainda.");
                return;
            }

            try
            {
                lsbListagem.Items.Clear();      // limpa o listbox
                var conteudo = tabelaDeHash.Conteudo();    // coleta o conteúdo da tabela

                // Ordena pelo valor do hash
                conteudo.Sort((a, b) =>
                {
                    var hashA = int.Parse(a.Split(':')[0].Trim());    // extrai o índice do item A (antes do ":")
                    var hashB = int.Parse(b.Split(':')[0].Trim());    // extrai o índice do item B (antes do ":")
                    return hashA.CompareTo(hashB);                    // compara os índices
                });

                // Exibe os itens no ListBox
                foreach (var item in conteudo)
                    lsbListagem.Items.Add(item);          // adiciona cada item na ListBox
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar: " + ex.Message);    // mensagem de erro
            }
        } 
    }
}
