namespace ImageProcessor
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.btnTheme = new Bunifu.Framework.UI.BunifuImageButton();
            this.label4 = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnClose = new Bunifu.Framework.UI.BunifuImageButton();
            this.pnlFunctions = new System.Windows.Forms.Panel();
            this.pnlProps = new System.Windows.Forms.Panel();
            this.lblProcessando = new System.Windows.Forms.Label();
            this.lblIntensidade = new System.Windows.Forms.Label();
            this.lblIntenseInfo = new System.Windows.Forms.Label();
            this.sldIntensidade = new Bunifu.Framework.UI.BunifuSlider();
            this.btnConfirmar = new Bunifu.Framework.UI.BunifuImageButton();
            this.cmbEfeito = new System.Windows.Forms.ComboBox();
            this.lblImgRes = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlTools = new System.Windows.Forms.Panel();
            this.btnReset = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnProps = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnSave = new Bunifu.Framework.UI.BunifuImageButton();
            this.btnOpen = new Bunifu.Framework.UI.BunifuImageButton();
            this.dragTitle = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.tmProcess = new System.Windows.Forms.Timer(this.components);
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnTheme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.pnlFunctions.SuspendLayout();
            this.pnlProps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnConfirmar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnReset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpen)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.btnTheme);
            this.pnlTitle.Controls.Add(this.label4);
            this.pnlTitle.Controls.Add(this.picLogo);
            this.pnlTitle.Controls.Add(this.btnClose);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(800, 40);
            this.pnlTitle.TabIndex = 2;
            // 
            // btnTheme
            // 
            this.btnTheme.BackColor = System.Drawing.Color.Transparent;
            this.btnTheme.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTheme.Image = global::ImageProcessor.Properties.Resources.theme;
            this.btnTheme.ImageActive = null;
            this.btnTheme.Location = new System.Drawing.Point(732, 8);
            this.btnTheme.Name = "btnTheme";
            this.btnTheme.Size = new System.Drawing.Size(25, 23);
            this.btnTheme.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnTheme.TabIndex = 4;
            this.btnTheme.TabStop = false;
            this.btnTheme.Zoom = 10;
            this.btnTheme.Click += new System.EventHandler(this.btnTheme_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Corbel", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(56, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Image Processor";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picLogo
            // 
            this.picLogo.Image = global::ImageProcessor.Properties.Resources.logo;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(50, 40);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 2;
            this.picLogo.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = global::ImageProcessor.Properties.Resources.remove_symbol;
            this.btnClose.ImageActive = null;
            this.btnClose.Location = new System.Drawing.Point(763, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 23);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 1;
            this.btnClose.TabStop = false;
            this.btnClose.Zoom = 10;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlFunctions
            // 
            this.pnlFunctions.Controls.Add(this.pnlProps);
            this.pnlFunctions.Controls.Add(this.picImage);
            this.pnlFunctions.Controls.Add(this.panel1);
            this.pnlFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFunctions.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlFunctions.Location = new System.Drawing.Point(0, 40);
            this.pnlFunctions.Name = "pnlFunctions";
            this.pnlFunctions.Size = new System.Drawing.Size(800, 410);
            this.pnlFunctions.TabIndex = 3;
            // 
            // pnlProps
            // 
            this.pnlProps.Controls.Add(this.lblProcessando);
            this.pnlProps.Controls.Add(this.lblIntensidade);
            this.pnlProps.Controls.Add(this.lblIntenseInfo);
            this.pnlProps.Controls.Add(this.sldIntensidade);
            this.pnlProps.Controls.Add(this.btnConfirmar);
            this.pnlProps.Controls.Add(this.cmbEfeito);
            this.pnlProps.Controls.Add(this.lblImgRes);
            this.pnlProps.Controls.Add(this.label5);
            this.pnlProps.Controls.Add(this.lblFileName);
            this.pnlProps.Controls.Add(this.label3);
            this.pnlProps.Controls.Add(this.label2);
            this.pnlProps.Controls.Add(this.label1);
            this.pnlProps.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlProps.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlProps.Location = new System.Drawing.Point(600, 0);
            this.pnlProps.Name = "pnlProps";
            this.pnlProps.Size = new System.Drawing.Size(200, 410);
            this.pnlProps.TabIndex = 2;
            this.pnlProps.Visible = false;
            // 
            // lblProcessando
            // 
            this.lblProcessando.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessando.Location = new System.Drawing.Point(4, 285);
            this.lblProcessando.Name = "lblProcessando";
            this.lblProcessando.Size = new System.Drawing.Size(184, 16);
            this.lblProcessando.TabIndex = 13;
            this.lblProcessando.Text = "Processando";
            this.lblProcessando.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProcessando.Visible = false;
            // 
            // lblIntensidade
            // 
            this.lblIntensidade.AutoEllipsis = true;
            this.lblIntensidade.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIntensidade.Location = new System.Drawing.Point(89, 210);
            this.lblIntensidade.Name = "lblIntensidade";
            this.lblIntensidade.Size = new System.Drawing.Size(100, 16);
            this.lblIntensidade.TabIndex = 12;
            this.lblIntensidade.Text = "0%";
            this.lblIntensidade.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIntenseInfo
            // 
            this.lblIntenseInfo.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIntenseInfo.Location = new System.Drawing.Point(3, 210);
            this.lblIntenseInfo.Name = "lblIntenseInfo";
            this.lblIntenseInfo.Size = new System.Drawing.Size(80, 16);
            this.lblIntenseInfo.TabIndex = 10;
            this.lblIntenseInfo.Text = "Intensidade:";
            this.lblIntenseInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sldIntensidade
            // 
            this.sldIntensidade.BackColor = System.Drawing.Color.Transparent;
            this.sldIntensidade.BackgroudColor = System.Drawing.Color.DarkGray;
            this.sldIntensidade.BorderRadius = 0;
            this.sldIntensidade.IndicatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(129)))), ((int)(((byte)(146)))));
            this.sldIntensidade.Location = new System.Drawing.Point(7, 229);
            this.sldIntensidade.MaximumValue = 100;
            this.sldIntensidade.Name = "sldIntensidade";
            this.sldIntensidade.Size = new System.Drawing.Size(182, 30);
            this.sldIntensidade.TabIndex = 11;
            this.sldIntensidade.Value = 0;
            this.sldIntensidade.ValueChanged += new System.EventHandler(this.sldIntensidade_ValueChanged);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.Image = global::ImageProcessor.Properties.Resources.correct_symbol;
            this.btnConfirmar.ImageActive = null;
            this.btnConfirmar.Location = new System.Drawing.Point(155, 170);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(33, 27);
            this.btnConfirmar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnConfirmar.TabIndex = 8;
            this.btnConfirmar.TabStop = false;
            this.btnConfirmar.Zoom = 10;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // cmbEfeito
            // 
            this.cmbEfeito.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEfeito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEfeito.FormattingEnabled = true;
            this.cmbEfeito.Items.AddRange(new object[] {
            "Clarear",
            "Escurecer",
            "Soma Norm.",
            "Sub. Norm.",
            "Soma Trunc.",
            "Sub. Trunc"});
            this.cmbEfeito.Location = new System.Drawing.Point(7, 170);
            this.cmbEfeito.Name = "cmbEfeito";
            this.cmbEfeito.Size = new System.Drawing.Size(142, 27);
            this.cmbEfeito.TabIndex = 7;
            this.cmbEfeito.SelectedIndexChanged += new System.EventHandler(this.cmbEfeito_SelectedIndexChanged);
            // 
            // lblImgRes
            // 
            this.lblImgRes.AutoEllipsis = true;
            this.lblImgRes.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImgRes.Location = new System.Drawing.Point(57, 51);
            this.lblImgRes.Name = "lblImgRes";
            this.lblImgRes.Size = new System.Drawing.Size(132, 16);
            this.lblImgRes.TabIndex = 6;
            this.lblImgRes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Resol.:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoEllipsis = true;
            this.lblFileName.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.Location = new System.Drawing.Point(54, 32);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(134, 16);
            this.lblFileName.TabIndex = 4;
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Nome:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Efeitos && Correções";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Propriedades:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picImage
            // 
            this.picImage.Location = new System.Drawing.Point(56, 6);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(538, 392);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImage.TabIndex = 1;
            this.picImage.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlTools);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(50, 410);
            this.panel1.TabIndex = 0;
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.btnReset);
            this.pnlTools.Controls.Add(this.btnProps);
            this.pnlTools.Controls.Add(this.btnSave);
            this.pnlTools.Controls.Add(this.btnOpen);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTools.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTools.Location = new System.Drawing.Point(0, 0);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(50, 410);
            this.pnlTools.TabIndex = 1;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.Image = global::ImageProcessor.Properties.Resources.trash;
            this.btnReset.ImageActive = null;
            this.btnReset.Location = new System.Drawing.Point(12, 375);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(25, 23);
            this.btnReset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnReset.TabIndex = 3;
            this.btnReset.TabStop = false;
            this.btnReset.Zoom = 10;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnProps
            // 
            this.btnProps.BackColor = System.Drawing.Color.Transparent;
            this.btnProps.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProps.Image = global::ImageProcessor.Properties.Resources.magic_wand;
            this.btnProps.ImageActive = null;
            this.btnProps.Location = new System.Drawing.Point(12, 80);
            this.btnProps.Name = "btnProps";
            this.btnProps.Size = new System.Drawing.Size(25, 23);
            this.btnProps.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnProps.TabIndex = 2;
            this.btnProps.TabStop = false;
            this.btnProps.Zoom = 10;
            this.btnProps.Click += new System.EventHandler(this.btnProps_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Image = global::ImageProcessor.Properties.Resources.save_file_option;
            this.btnSave.ImageActive = null;
            this.btnSave.Location = new System.Drawing.Point(12, 44);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(25, 23);
            this.btnSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnSave.TabIndex = 1;
            this.btnSave.TabStop = false;
            this.btnSave.Zoom = 10;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.Transparent;
            this.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpen.Image = global::ImageProcessor.Properties.Resources.open_folder;
            this.btnOpen.ImageActive = null;
            this.btnOpen.Location = new System.Drawing.Point(12, 8);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(25, 23);
            this.btnOpen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnOpen.TabIndex = 0;
            this.btnOpen.TabStop = false;
            this.btnOpen.Zoom = 10;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // dragTitle
            // 
            this.dragTitle.Fixed = true;
            this.dragTitle.Horizontal = true;
            this.dragTitle.TargetControl = this.pnlTitle;
            this.dragTitle.Vertical = true;
            // 
            // tmProcess
            // 
            this.tmProcess.Interval = 250;
            this.tmProcess.Tick += new System.EventHandler(this.tmProcess_Tick);
            // 
            // Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlFunctions);
            this.Controls.Add(this.pnlTitle);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "Image Processor";
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnTheme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.pnlFunctions.ResumeLayout(false);
            this.pnlProps.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnConfirmar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnReset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Panel pnlFunctions;
        private Bunifu.Framework.UI.BunifuDragControl dragTitle;
        private System.Windows.Forms.Panel pnlProps;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlTools;
        private Bunifu.Framework.UI.BunifuImageButton btnOpen;
        private Bunifu.Framework.UI.BunifuImageButton btnSave;
        private Bunifu.Framework.UI.BunifuImageButton btnReset;
        private Bunifu.Framework.UI.BunifuImageButton btnProps;
        private Bunifu.Framework.UI.BunifuImageButton btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuImageButton btnConfirmar;
        private System.Windows.Forms.ComboBox cmbEfeito;
        private System.Windows.Forms.Label lblImgRes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblIntenseInfo;
        private Bunifu.Framework.UI.BunifuSlider sldIntensidade;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblIntensidade;
        private System.Windows.Forms.Label lblProcessando;
        private System.Windows.Forms.Timer tmProcess;
        private Bunifu.Framework.UI.BunifuImageButton btnTheme;
    }
}

