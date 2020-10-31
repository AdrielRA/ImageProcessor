using ImageProcessor.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessor
{
    public partial class Main : Form
    {
        // Permite acessar este form em outras partes do código facilmente
        internal static Main instance;

        // Contrutor com configurações iniciais da aplicação
        public Main()
        {
            InitializeComponent();
            instance = this;
            // carrega temas disponiveis em 'theme.json'
            MainController.loadThemes();
            // define tema ativo
            MainController.setTheme("dracula");  
            // apaga resquicios de imagens de execuções anteriores
            MainController.clearTemp();
        }

        // Trata a alternancia do tema da interface
        internal void onThemeChange(string theme)
        {
            switch (theme)
            {
                case "dracula":
                    picLogo.Image = Properties.Resources.logo_light;                    
                    break;
                case "light":
                    picLogo.Image = Properties.Resources.logo;
                    break;
                default:
                    break;
            }
            
            BackColor = pnlProps.BackColor = pnlTools.BackColor = MainController.theme["background"];
            ForeColor = cmbEfeito.ForeColor = MainController.theme["foreground"];
            pnlFunctions.BackColor = MainController.theme["currentLine"];
            cmbEfeito.BackColor = MainController.theme["currentLine"];
        }

        // Controla se botões estão ativos ou não
        private void btnControl(bool active)
        {
            btnSave.Enabled = btnOpen.Enabled = btnProps.Enabled = btnReset.Enabled = btnConfirmar.Enabled = sldIntensidade.Enabled = active;
        }

        // Trata evento de fechamento do form
        private void btnClose_Click(object sender, EventArgs e)
        {
            // Se existirem alterações não salvas ao fechar
            if (MainController.pendingChanges)
            {
                // Pergunta ao usuário se deseja salvá-las, ou descartá-las
                DialogResult res = MessageBox.Show("Existem alterações pendentes!\n\nDeseja salvá-las?", "Atenção:", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    // Salva alterações antes de fechar
                    btnSave_Click(btnSave, new EventArgs());
                }
            }
            // Finaliza aplicação
            Application.Exit();
        }

        // Trata evento para abrir uma imagem
        private void btnOpen_Click(object sender, EventArgs e)
        {
            // Verifica se existe alterações pendentes em imagem aberta anteriormente
            if (MainController.pendingChanges)
            {
                // Pergunta se deseja salvar alterações
                DialogResult res = MessageBox.Show("Existem alterações pendentes!\n\nDeseja salvá-las?", "Atenção:", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    // Salva alterações
                    btnSave_Click(btnSave, new EventArgs());
                }
                // Ou descarta as alterações
                else if(res == DialogResult.Cancel) { return; }
            }
            // Abre o dialogo para escolher o arquivo de imagem
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Abrir Imagem";
                dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                if (dlg.ShowDialog() == DialogResult.OK)
                { 
                    // Carrega imagem Bitmap como a original e atual
                    MainController.original = MainController.atual = new Bitmap(dlg.FileName);
                    // Mantem o caminho do arquivo para operações futuras
                    MainController.pathOriginal = dlg.FileName;
                    
                    // Carrega imagem na tela
                    picImage.Image = new Bitmap(dlg.FileName);
                    pnlProps.Visible = true;

                    string name = dlg.FileName.Split('\\').LastOrDefault();
;
                    // Preenche campos e reseta efeitos
                    lblFileName.Text = name;
                    lblImgRes.Text = picImage.Image.Width + " X " + picImage.Image.Height;

                    cmbEfeito.SelectedIndex = 0;
                    sldIntensidade.Value = 100;
                    lblIntensidade.Text = "100%";
                }
            }
        }

        // Trata click botão de efeito
        private void btnProps_Click(object sender, EventArgs e)
        {
            // Controla se exibe ou não barra de efeitos
            if (MainController.original != null)
                pnlProps.Visible = !pnlProps.Visible;
            else MessageBox.Show("Abra uma imagem primeiro!");
        }

        // Trata evento ao mudar efeito
        private void cmbEfeito_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Se o efeito selecionado estiver contido em 'opsToHide', a barra de intensidade será ocultada
            int[] opsToHide = { 4, 5 };
            sldIntensidade.Visible = lblIntenseInfo.Visible = lblIntensidade.Visible = !opsToHide.Contains(cmbEfeito.SelectedIndex);
        }

        // Trata evento ao alterar valor do slider de intensidade
        private void sldIntensidade_ValueChanged(object sender, EventArgs e)
        {
            // atualiza label que mostra porcentagem da intensidade
            lblIntensidade.Text = sldIntensidade.Value + "%";
        }

        // Trata click no botão que confirma aplicação do efeito
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            // Verifica se a intensidade do efeito é maior q zero, pois caso contrário é desnecessário processar
            if (sldIntensidade.Value > 0)
            {
                // Define que existem alterações pendentes para salvar
                lblProcessando.Visible = MainController.pendingChanges = true;
                // Desativa demais botões da interface
                btnControl(false);
                // Inicia timer da efeito de loading à label "Processando"
                tmProcess.Start();
                // Chama a task em outra thread que processa a imagem sem congelar o programa
                Processar();
            }
        }

        // Trata carregamento da imagem secundaria, necessária em alguns efeitos
        private Bitmap openSecondImg()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Abrir Imagem";
                dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // Mantém o caminho d aimagem secundaria para operações futuras
                    MainController.pathSecondaria = dlg.FileName;
                    return new Bitmap(dlg.FileName);
                }
            }

            return null;
        }

        // Tarefa assincrona que processa efeitos sem congelar a aplicação
        private async Task Processar()
        {
            // Cria uma cópia da imagem exibida na tela
            Bitmap img = new Bitmap(picImage.Image);

            // Verifica qual o efeito selecionado
            switch (cmbEfeito.SelectedIndex)
            {
                case 0:
                    // Chama implementação do efeito no controller
                    // o operador await aguarda a execução assincrona da tarefa
                    await Task.Run(() =>
                    {
                        img = MainController.clarear(sldIntensidade.Value);
                    });                    
                    break;
                case 1:
                    await Task.Run(() =>
                    {
                        img = MainController.escurecer(sldIntensidade.Value);
                    });
                    break;
                case 2:
                    // Em efeitos que necessitam da imagem secundaria, ela é carregada
                    MainController.secondaria = openSecondImg();
                    // Verifica-se se a imagem foi devidamente carregada se sim, segue normalmente a execução
                    if (MainController.secondaria != null)
                        await Task.Run(() =>
                        {
                            img = MainController.somaNormalizada(sldIntensidade.Value);
                        });
                    break;
                case 3:
                    MainController.secondaria = openSecondImg();                    
                    if(MainController.secondaria != null)
                        await Task.Run(() =>
                        {
                            img = MainController.subNormalizada(sldIntensidade.Value);
                        });
                    break;
                case 4:
                    MainController.secondaria = openSecondImg();
                    if (MainController.secondaria != null)
                        await Task.Run(() =>
                        {
                            img = MainController.somaTruncada();
                        });
                    break;
                case 5:
                    MainController.secondaria = openSecondImg();
                    if (MainController.secondaria != null)
                        await Task.Run(() =>
                        {
                            img = MainController.subTruncada();
                        });
                    break;
                default: break;
            }

            // Após o processamento finalizar a imagem resultante deve ser exibida em tela
            updateImg(img);            
        }

        // Recebe um bitmap, e o exibe em tela
        private void updateImg(Bitmap img)
        {
            // Define imagem que é exibida
            picImage.Image = img;
            // Salva esta imagem como a atual
            MainController.atual = img;
            // Oculta label que exibe "Processando"
            lblProcessando.Visible = false;
            // Para o timer
            tmProcess.Stop();
            // Reativa botões da interface
            btnControl(true);
        }

        // Trata evento do botão de resetar imagem
        private void btnReset_Click(object sender, EventArgs e)
        {
            // Verifica se alguma imagem já foi aberta
            if (MainController.original != null)
            {
                // Verifica se existem alterações para descartar
                if (MainController.pendingChanges)
                {
                    // Pergunta se deseja descartar alterações sem salvá-las
                    DialogResult res = MessageBox.Show("Descartar alterações atuais?", "Atenção:", MessageBoxButtons.YesNo);
                    if(res == DialogResult.Yes)
                    {
                        // Libera imagem atual
                        try { if (MainController.atual != null) MainController.atual.Dispose(); }
                        catch { }

                        // Recarrega imagem original e exibe em tela
                        picImage.Image = MainController.atual = new Bitmap(MainController.original);
                        // Reseta efeitos
                        cmbEfeito.SelectedIndex = 0;
                        sldIntensidade.Value = 100;
                        lblIntensidade.Text = "100%";
                        MainController.pendingChanges = false;                        
                    }
                }
                
            }
            else { 
                DialogResult res = MessageBox.Show("Carregue uma imagem primeiro!\n\nDeseja fazer isto agora?", "Falha no processo!", MessageBoxButtons.YesNo);

                if(res == DialogResult.Yes) btnOpen_Click(btnOpen, new EventArgs());
            }
        }

        // Implementa função de salvar imagem
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Se alguma imagem foi aberta
            if(MainController.original != null)
            {
                // Se existirem alterações não salvas
                if (MainController.pendingChanges)
                {
                    // Carrega dialogo de salvamento
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "JPG files (*.jpg)|*.jpg|All files (*.*)|*.*";
                    dialog.FilterIndex = 0;

                    dialog.AddExtension = true;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        // Salva bitmap resultante como imagem Jpeg
                        Bitmap bmp = new Bitmap(picImage.Image);
                        bmp.Save(dialog.FileName, ImageFormat.Jpeg);
                        // Reseta aplicação
                        cmbEfeito.SelectedIndex = 0;
                        sldIntensidade.Value = 100;
                        lblIntensidade.Text = "100%";
                        MainController.pendingChanges = false;
                        // Define imagem original como a imagem que acaba de ser salva
                        MainController.original = MainController.atual = new Bitmap(picImage.Image);
                        MainController.pathOriginal = dialog.FileName;
                        lblFileName.Text = dialog.FileName.Split('\\').LastOrDefault();
                    }
                }
                else
                {
                    MessageBox.Show("Nenhuma alteração pendende para salvar!", "Atenção:");
                }
            }
            else
            {
                MessageBox.Show("Carregue uma imagem antes!", "Falha ao salvar:");
            }
            
        }

        // Timer que cria efeito de carregamento da label "Processando"
        private void tmProcess_Tick(object sender, EventArgs e)
        {
            lblProcessando.Text = lblProcessando.Text.Replace("Processando", "") == "..." ? "Processando" : lblProcessando.Text + ".";
        }

        // Trata evento ao clicar no botão para trocar tema
        private void btnTheme_Click(object sender, EventArgs e)
        {
            MainController.setTheme(MainController.activeTheme == "light" ? "dracula" : "light");
        }
    }
}
