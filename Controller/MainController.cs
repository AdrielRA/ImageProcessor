using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace ImageProcessor.Controller
{
    class MainController
    {
        // temas disponiveis
        private static dynamic themes;
        // tema ativo no momento
        public static dynamic theme;
        // imagens utilizadas durante a execução
        public static Bitmap original, atual, secondaria;
        // informações uteis
        public static string activeTheme, pathOriginal, pathSecondaria;
        // assinála existencia de alterações não salvas
        public static bool pendingChanges;

        // Carrega arquivo theme.json
        internal static void loadThemes()
        {
            using (StreamReader r = new StreamReader("theme.json"))
            {
                string json = r.ReadToEnd();
                themes = JsonConvert.DeserializeObject<dynamic>(json);
            }
        }

        // Trata alternancia de tema claro e escuro
        public static void setTheme(string themeName)
        {
            activeTheme = themeName;            
            theme = themes[themeName];
            Main.instance.onThemeChange(themeName);
        }

        // Implementa algoritmo de soma escalar
        internal static Bitmap clarear(int multiplicador)
        {
            Bitmap img = new Bitmap(atual);
            // calcula intensidade do efeito
            int intensidade = (int)(multiplicador / 100.0 * 255);

            int width = img.Width, height = img.Height;

            try
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color pixel = img.GetPixel(x, y);

                        int r = pixel.R + intensidade < 256 ? pixel.R + intensidade : 255;
                        int g = pixel.G + intensidade < 256 ? pixel.G + intensidade : 255;
                        int b = pixel.B + intensidade < 256 ? pixel.B + intensidade : 255;

                        img.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
            }
            catch { }

            return img;
        }

        // Implementa algoritmo de subtração escalar
        internal static Bitmap escurecer(int multiplicador)
        {
            Bitmap img = new Bitmap(atual);
            int intensidade = (int)(multiplicador / 100.0 * 255);

            int width = img.Width, height = img.Height;

            try
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color pixel = img.GetPixel(x, y);

                        int r = pixel.R - intensidade > 0 ? pixel.R - intensidade : 0;
                        int g = pixel.G - intensidade > 0 ? pixel.G - intensidade : 0;
                        int b = pixel.B - intensidade > 0 ? pixel.B - intensidade : 0;

                        img.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }
            }
            catch { }

            return img;
        }

        internal static Bitmap somaNormalizada(int multiplicador)
        {
            // calcula intermediarias levando em conta a intensidade desejada
            List<int[,]> inter = intermediaria(multiplicador, "soma");
            // realiza normalização 
            return normalizar(inter, atual.Width, atual.Height);
        }

        internal static Bitmap subNormalizada(int multiplicador)
        {
            List<int[,]> inter = intermediaria(multiplicador, "sub");
            return normalizar(inter, atual.Width, atual.Height);
        }

        // Implementa algoritmo de intermediária
        private static List<int[,]> intermediaria(int multiplicador, string tipo)
        {
            Bitmap img1 = new Bitmap(atual);
            // redimensiona imagem secundária para o tamanho da primeira
            Bitmap img2 = new Bitmap(secondaria, new Size(img1.Width, img1.Height));
            // calcula intensidade
            float intensidade = multiplicador / 100f;

            int width = img1.Width, height = img2.Height;

            List<int[,]> matrix = new List<int[,]>();

            int[,] matrixR, matrixG, matrixB;

            matrixR = new int[width, height];
            matrixG = new int[width, height];
            matrixB = new int[width, height];

            try
            {

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color pixel1 = img1.GetPixel(x, y);
                        Color pixel2 = img2.GetPixel(x, y);

                        int r = pixel1.R - (tipo == "sub" ? 1 : -1) * (int)(pixel2.R * intensidade);
                        int g = pixel1.G - (tipo == "sub" ? 1 : -1) * (int)(pixel2.G * intensidade);
                        int b = pixel1.B - (tipo == "sub" ? 1 : -1) * (int)(pixel2.B * intensidade);

                        matrixR[x, y] = r;
                        matrixG[x, y] = g;
                        matrixB[x, y] = b;
                    }
                }
            }
            catch { }

            matrix.Add(matrixR);
            matrix.Add(matrixG);
            matrix.Add(matrixB);

            return matrix;
        }

        // Recebe as matrizes intermediarias e retorna imagem normalizada
        private static Bitmap normalizar(List<int[,]> inter, int width, int height)
        {
            Bitmap img = new Bitmap(width, height);
            int[,] matrixR, matrixG, matrixB;

            matrixR = inter[0];
            matrixG = inter[1];
            matrixB = inter[2];

            int[] arrayR, arrayG, arrayB;

            arrayR = matrixR.Cast<int>().ToArray();
            arrayG = matrixG.Cast<int>().ToArray();
            arrayB = matrixB.Cast<int>().ToArray();

            int minR, minG, minB;
            int maxR, maxG, maxB;

            maxR = arrayR.Max();
            maxG = arrayG.Max();
            maxB = arrayB.Max();

            minR = arrayR.Min();
            minG = arrayG.Min();
            minB = arrayB.Min();

            try
            {

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int r = (int)(255.0 / (maxR - minR) * (matrixR[x, y] - minR));
                        int g = (int)(255.0 / (maxG - minG) * (matrixG[x, y] - minG));
                        int b = (int)(255.0 / (maxB - minB) * (matrixB[x, y] - minB));

                        img.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                return img;
            }
            catch { return null; }
        }

        // Faz chamada ao script python que implementa a soma truncada
        internal static Bitmap somaTruncada(int multiplicador)
        {
            // Caminho do arquivo .py
            string pyPath = currentPath() + "operations.py";
            // Caminho da imagem atual (temporária)
            string atualPath = currentPath() + @"temp\current.jpg";
            // Salva estado atual da imagem editada para ser usada pelo arquivo .py
            saveCurrent(atualPath);
            // Verifica se a imagem atual foi salva, caso contrario pega o caminho da imagem original
            string baseImg = File.Exists(atualPath) ? atualPath : pathOriginal;

            // Executa script .py passando dinamicamente o caminho das imagens e a operação q será realizada
            runPython(new string[] { pyPath, "\"" + baseImg + "\"", "\""+ pathSecondaria + "\"", "add", multiplicador.ToString() });

            try {
                // Após a execução do .py, a imagem gerada será carregada
                using (Stream s = File.OpenRead(currentPath() + @"temp/add.png"))
                return (Bitmap)Image.FromStream(s);
            }
            // em caso de falha, mantém a imagem atual
            catch { return atual; }            
        }

        internal static Bitmap subTruncada(int multiplicador)
        {
            string pyPath = currentPath() + "operations.py";

            string atualPath = currentPath() + @"temp\current.jpg";
            saveCurrent(atualPath);
            string baseImg = File.Exists(atualPath) ? atualPath : pathOriginal;

            // Chama script .py, passando imagens e define operação como sub = subtração
            runPython(new string[] { pyPath, "\"" + baseImg + "\"", "\"" + pathSecondaria + "\"", "sub", multiplicador.ToString() });

            try {
                using (Stream s = File.OpenRead(currentPath() + @"temp/sub.png"))
                return (Bitmap)Image.FromStream(s);
            }
            catch { return atual; }
        }

        internal static Bitmap multiplicar(int multiplicador)
        {
            string pyPath = currentPath() + "operations.py";

            string atualPath = currentPath() + @"temp\current.jpg";
            saveCurrent(atualPath);
            string baseImg = File.Exists(atualPath) ? atualPath : pathOriginal;

            // Chama script .py, passando imagem e define operação como blur = desfoque e a intensidade
            runPython(new string[] { pyPath, "\"" + baseImg + "\"", "mul", (multiplicador / 10).ToString() });

            try
            {
                using (Stream s = File.OpenRead(currentPath() + @"temp/mul.png"))
                    return (Bitmap)Image.FromStream(s);
            }
            catch { return atual; }
        }

        internal static Bitmap desfoque(int multiplicador)
        {
            throw new NotImplementedException();
        }

        internal static Bitmap preset1(int multiplicador)
        {
            string pyPath = currentPath() + "operations.py";

            string atualPath = currentPath() + @"temp\current.jpg";
            saveCurrent(atualPath);
            string baseImg = File.Exists(atualPath) ? atualPath : pathOriginal;

            // Chama script .py, passando imagem e define operação como preset1 = combinação dos efeitos (desfoque, subrai, multiplica)
            runPython(new string[] { pyPath, "\"" + baseImg + "\"", "preset1", (multiplicador).ToString() });

            try
            {
                using (Stream s = File.OpenRead(currentPath() + @"temp/preset1.png"))
                    return (Bitmap)Image.FromStream(s);
            }
            catch { return atual; }
        }

        internal static Bitmap preset2(int multiplicador)
        {
            throw new NotImplementedException();
        }

        // Salva imagem no estado atual temporariamente
        private static void saveCurrent(string path)
        {
            if (atual != null)
            {
                try
                {
                    if (!Directory.Exists(currentPath() + "temp")) Directory.CreateDirectory(currentPath() + "temp");
                    new Bitmap(atual).Save(path, ImageFormat.Jpeg);
                }
                catch { }
            }
        }

        // Executa python recebendo argumentos: caminho do script .py, imagens e operação desejada
        private static void runPython(string[] args)
        {
            // É necessário q o comando 'py' será reconhecido em seu terminal para funcionar
            // 'py' é a variavel de ambiente que define o caminho do executavel python de sua máquina
            ProcessStartInfo startInfo = new ProcessStartInfo("py");
            // concatena os argumentos que será utilizados pelo script .py
            startInfo.Arguments = string.Join(" ", args);
            // desativa vizualização do terminal, para melhor experiência do usuário
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            // Executa o comando e aguarda sua finalização
            Process.Start(startInfo).WaitForExit();
        }

        // Retorna o caminho atual
        private static string currentPath()
        {
            try
            {
                string caminhoExecutavel = System.Reflection.Assembly.GetCallingAssembly().Location;
                string pastaExecutavel = new FileInfo(caminhoExecutavel).DirectoryName;
                return pastaExecutavel += @"\";

            }
            catch
            {
                return null;
            }
        }

        // Deleta diretório ./temp e imagens contidadas
        internal static void clearTemp()
        {
            try { Directory.Delete(currentPath() + "temp", true); }
            catch {}            
        }

    }
}
