namespace apHashing
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbDuploHash = new System.Windows.Forms.RadioButton();
            this.rbSondQuadrat = new System.Windows.Forms.RadioButton();
            this.rbSondLinear = new System.Windows.Forms.RadioButton();
            this.rbBucketHash = new System.Windows.Forms.RadioButton();
            this.lbPalavra = new System.Windows.Forms.Label();
            this.lbDica = new System.Windows.Forms.Label();
            this.lbListaDados = new System.Windows.Forms.Label();
            this.txtPalavra = new System.Windows.Forms.TextBox();
            this.txtDica = new System.Windows.Forms.TextBox();
            this.lsbListagem = new System.Windows.Forms.ListBox();
            this.btnIncluir = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnListar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDuploHash);
            this.groupBox1.Controls.Add(this.rbSondQuadrat);
            this.groupBox1.Controls.Add(this.rbSondLinear);
            this.groupBox1.Controls.Add(this.rbBucketHash);
            this.groupBox1.Location = new System.Drawing.Point(13, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 192);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Técnicas de Hashing";
            // 
            // rbDuploHash
            // 
            this.rbDuploHash.AutoSize = true;
            this.rbDuploHash.Location = new System.Drawing.Point(7, 149);
            this.rbDuploHash.Name = "rbDuploHash";
            this.rbDuploHash.Size = new System.Drawing.Size(99, 20);
            this.rbDuploHash.TabIndex = 3;
            this.rbDuploHash.TabStop = true;
            this.rbDuploHash.Text = "Duplo Hash";
            this.rbDuploHash.UseVisualStyleBackColor = true;
            // 
            // rbSondQuadrat
            // 
            this.rbSondQuadrat.AutoSize = true;
            this.rbSondQuadrat.Location = new System.Drawing.Point(7, 114);
            this.rbSondQuadrat.Name = "rbSondQuadrat";
            this.rbSondQuadrat.Size = new System.Drawing.Size(164, 20);
            this.rbSondQuadrat.TabIndex = 2;
            this.rbSondQuadrat.TabStop = true;
            this.rbSondQuadrat.Text = "Sondagem Quadrática";
            this.rbSondQuadrat.UseVisualStyleBackColor = true;
            // 
            // rbSondLinear
            // 
            this.rbSondLinear.AutoSize = true;
            this.rbSondLinear.Location = new System.Drawing.Point(7, 74);
            this.rbSondLinear.Name = "rbSondLinear";
            this.rbSondLinear.Size = new System.Drawing.Size(135, 20);
            this.rbSondLinear.TabIndex = 1;
            this.rbSondLinear.TabStop = true;
            this.rbSondLinear.Text = "Sondagem Linear";
            this.rbSondLinear.UseVisualStyleBackColor = true;
            // 
            // rbBucketHash
            // 
            this.rbBucketHash.AutoSize = true;
            this.rbBucketHash.Location = new System.Drawing.Point(7, 32);
            this.rbBucketHash.Name = "rbBucketHash";
            this.rbBucketHash.Size = new System.Drawing.Size(104, 20);
            this.rbBucketHash.TabIndex = 0;
            this.rbBucketHash.TabStop = true;
            this.rbBucketHash.Text = "Bucket Hash";
            this.rbBucketHash.UseVisualStyleBackColor = true;
            // 
            // lbPalavra
            // 
            this.lbPalavra.AutoSize = true;
            this.lbPalavra.Location = new System.Drawing.Point(314, 23);
            this.lbPalavra.Name = "lbPalavra";
            this.lbPalavra.Size = new System.Drawing.Size(57, 16);
            this.lbPalavra.TabIndex = 1;
            this.lbPalavra.Text = "Palavra:";
            // 
            // lbDica
            // 
            this.lbDica.AutoSize = true;
            this.lbDica.Location = new System.Drawing.Point(314, 80);
            this.lbDica.Name = "lbDica";
            this.lbDica.Size = new System.Drawing.Size(38, 16);
            this.lbDica.TabIndex = 2;
            this.lbDica.Text = "Dica:";
            // 
            // lbListaDados
            // 
            this.lbListaDados.AutoSize = true;
            this.lbListaDados.Location = new System.Drawing.Point(12, 235);
            this.lbListaDados.Name = "lbListaDados";
            this.lbListaDados.Size = new System.Drawing.Size(96, 16);
            this.lbListaDados.TabIndex = 3;
            this.lbListaDados.Text = "Lista de dados";
            // 
            // txtPalavra
            // 
            this.txtPalavra.Location = new System.Drawing.Point(397, 23);
            this.txtPalavra.Name = "txtPalavra";
            this.txtPalavra.Size = new System.Drawing.Size(209, 22);
            this.txtPalavra.TabIndex = 4;
            // 
            // txtDica
            // 
            this.txtDica.Location = new System.Drawing.Point(397, 80);
            this.txtDica.Name = "txtDica";
            this.txtDica.Size = new System.Drawing.Size(351, 22);
            this.txtDica.TabIndex = 5;
            // 
            // lsbListagem
            // 
            this.lsbListagem.FormattingEnabled = true;
            this.lsbListagem.ItemHeight = 16;
            this.lsbListagem.Location = new System.Drawing.Point(13, 254);
            this.lsbListagem.Name = "lsbListagem";
            this.lsbListagem.Size = new System.Drawing.Size(735, 196);
            this.lsbListagem.TabIndex = 6;
            // 
            // btnIncluir
            // 
            this.btnIncluir.Location = new System.Drawing.Point(317, 151);
            this.btnIncluir.Name = "btnIncluir";
            this.btnIncluir.Size = new System.Drawing.Size(75, 23);
            this.btnIncluir.TabIndex = 7;
            this.btnIncluir.Text = "Incluir";
            this.btnIncluir.UseVisualStyleBackColor = true;
            this.btnIncluir.Click += new System.EventHandler(this.btnIncluir_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Location = new System.Drawing.Point(424, 151);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(75, 23);
            this.btnExcluir.TabIndex = 8;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = true;
            // 
            // btnAlterar
            // 
            this.btnAlterar.Location = new System.Drawing.Point(531, 151);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(75, 23);
            this.btnAlterar.TabIndex = 9;
            this.btnAlterar.Text = "Alterar";
            this.btnAlterar.UseVisualStyleBackColor = true;
            // 
            // btnListar
            // 
            this.btnListar.Location = new System.Drawing.Point(640, 151);
            this.btnListar.Name = "btnListar";
            this.btnListar.Size = new System.Drawing.Size(75, 23);
            this.btnListar.TabIndex = 10;
            this.btnListar.Text = "Listar";
            this.btnListar.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 473);
            this.Controls.Add(this.btnListar);
            this.Controls.Add(this.btnAlterar);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnIncluir);
            this.Controls.Add(this.lsbListagem);
            this.Controls.Add(this.txtDica);
            this.Controls.Add(this.txtPalavra);
            this.Controls.Add(this.lbListaDados);
            this.Controls.Add(this.lbDica);
            this.Controls.Add(this.lbPalavra);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Manutenção de Palavras e Dicas";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbDuploHash;
        private System.Windows.Forms.RadioButton rbSondQuadrat;
        private System.Windows.Forms.RadioButton rbSondLinear;
        private System.Windows.Forms.RadioButton rbBucketHash;
        private System.Windows.Forms.Label lbPalavra;
        private System.Windows.Forms.Label lbDica;
        private System.Windows.Forms.Label lbListaDados;
        private System.Windows.Forms.TextBox txtPalavra;
        private System.Windows.Forms.TextBox txtDica;
        private System.Windows.Forms.ListBox lsbListagem;
        private System.Windows.Forms.Button btnIncluir;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnListar;
    }
}

