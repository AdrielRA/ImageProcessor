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
        internal static Main instance;
        public Main()
        {
            InitializeComponent();
            instance = this;
            MainController.loadThemes();
            MainController.setTheme("dracula");            
            MainController.clearTemp();
        }

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

        private void btnControl(bool active)
        {
            btnSave.Enabled = btnOpen.Enabled = btnProps.Enabled = btnReset.Enabled = btnConfirmar.Enabled = sldIntensidade.Enabled = active;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MainController.pendingChanges)
            {
                DialogResult res = MessageBox.Show("Existem alterações pendentes!\n\nDeseja salvá-las?", "Atenção:", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    btnSave_Click(btnSave, new EventArgs());
                }
            }
            Application.Exit();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (MainController.pendingChanges)
            {
                DialogResult res = MessageBox.Show("Existem alterações pendentes!\n\nDeseja salvá-las?", "Atenção:", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    btnSave_Click(btnSave, new EventArgs());
                }
                else if(res == DialogResult.Cancel) { return; }
            }
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Abrir Imagem";
                dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                if (dlg.ShowDialog() == DialogResult.OK)
                {          
                    MainController.original = MainController.atual = new Bitmap(dlg.FileName);
                    MainController.pathOriginal = dlg.FileName;
                    
                    picImage.Image = new Bitmap(dlg.FileName);
                    pnlProps.Visible = true;

                    string name = dlg.FileName.Split('\\').LastOrDefault();
;
                    lblFileName.Text = name;
                    lblImgRes.Text = picImage.Image.Width + " X " + picImage.Image.Height;

                    cmbEfeito.SelectedIndex = 0;
                    sldIntensidade.Value = 100;
                    lblIntensidade.Text = "100%";
                }
            }
        }

        private void btnProps_Click(object sender, EventArgs e)
        {
            if (MainController.original != null)
                pnlProps.Visible = !pnlProps.Visible;
            else MessageBox.Show("Abra uma imagem primeiro!");
        }

        private void cmbEfeito_SelectedIndexChanged(object sender, EventArgs e)
        {
            int[] opsToHide = { 4, 5 };
            sldIntensidade.Visible = lblIntenseInfo.Visible = lblIntensidade.Visible = !opsToHide.Contains(cmbEfeito.SelectedIndex);
        }

        private void sldIntensidade_ValueChanged(object sender, EventArgs e)
        {
            lblIntensidade.Text = sldIntensidade.Value + "%";
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (sldIntensidade.Value > 0)
            {
                lblProcessando.Visible = MainController.pendingChanges = true;
                btnControl(false);
                tmProcess.Start();
                Processar();
            }
        }

        private Bitmap openSecondImg()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Abrir Imagem";
                dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    MainController.pathSecondaria = dlg.FileName;
                    return new Bitmap(dlg.FileName);
                }
            }

            return null;
        }

        private async Task Processar()
        {
            Bitmap img = new Bitmap(picImage.Image);
            switch (cmbEfeito.SelectedIndex)
            {
                case 0:
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
                    MainController.secondaria = openSecondImg();
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

            updateImg(img);            
        }

        private void updateImg(Bitmap img)
        {
            picImage.Image = img;
            MainController.atual = img;
            lblProcessando.Visible = false;
            tmProcess.Stop();
            btnControl(true);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MainController.original != null)
            {
                if (MainController.pendingChanges)
                {
                    DialogResult res = MessageBox.Show("Descartar alterações atuais?", "Atenção:", MessageBoxButtons.YesNo);
                    if(res == DialogResult.Yes)
                    {
                        picImage.Image = MainController.atual = new Bitmap(MainController.original);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(MainController.original != null)
            {
                if (MainController.pendingChanges)
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "JPG files (*.jpg)|*.jpg|All files (*.*)|*.*";
                    dialog.FilterIndex = 0;

                    dialog.AddExtension = true;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap bmp = new Bitmap(picImage.Image);
                        bmp.Save(dialog.FileName, ImageFormat.Jpeg);
                        cmbEfeito.SelectedIndex = 0;
                        sldIntensidade.Value = 100;
                        lblIntensidade.Text = "100%";
                        MainController.pendingChanges = false;
                        MainController.original = new Bitmap(picImage.Image);
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

        private void tmProcess_Tick(object sender, EventArgs e)
        {
            lblProcessando.Text = lblProcessando.Text.Replace("Processando", "") == "..." ? "Processando" : lblProcessando.Text + ".";
        }

        private void btnTheme_Click(object sender, EventArgs e)
        {
            MainController.setTheme(MainController.activeTheme == "light" ? "dracula" : "light");
        }
    }
}
