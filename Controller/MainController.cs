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
        private static dynamic themes;
        public static dynamic theme;
        public static Bitmap original, atual, secondaria;
        public static string activeTheme, pathOriginal, pathSecondaria;
        public static bool pendingChanges;


        internal static void loadThemes()
        {
            using (StreamReader r = new StreamReader(@"../../Resources/theme.json"))
            {
                string json = r.ReadToEnd();
                themes = JsonConvert.DeserializeObject<dynamic>(json);
            }
        }

        public static void setTheme(string themeName)
        {
            activeTheme = themeName;            
            theme = themes[themeName];
            Main.instance.onThemeChange(themeName);
        }

        internal static Bitmap clarear(int multiplicador)
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
            List<int[,]> inter = intermediaria(multiplicador, "soma");
            return normalizar(inter, atual.Width, atual.Height);
        }

        internal static Bitmap subNormalizada(int multiplicador)
        {

            List<int[,]> inter = intermediaria(multiplicador, "sub");
            return normalizar(inter, atual.Width, atual.Height);
        }

        private static List<int[,]> intermediaria(int multiplicador, string tipo)
        {
            Bitmap img1 = new Bitmap(atual);
            Bitmap img2 = new Bitmap(secondaria, new Size(img1.Width, img1.Height));

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

        internal static Bitmap somaTruncada()
        {
            string pyPath = currentPath() + "operations.py";
            runPython(new string[] { pyPath, pathOriginal, pathSecondaria, "add" });

            try { return new Bitmap(currentPath() + @"temp/add.png"); }
            catch { return null; }            
        }

        internal static Bitmap subTruncada()
        {
            string pyPath = currentPath() + "operations.py";
            runPython(new string[] { pyPath, pathOriginal, pathSecondaria, "sub" });

            try { return new Bitmap(currentPath() + @"temp/sub.png"); }
            catch { return null; }
        }

        private static void runPython(string[] args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("py");
            startInfo.Arguments = string.Join(" ", args);
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(startInfo).WaitForExit();
        }

        private static string currentPath()
        {
            try
            {
                string caminhoExecutavel = System.Reflection.Assembly.GetCallingAssembly().Location;
                string pastaExecutavel = new System.IO.FileInfo(caminhoExecutavel).DirectoryName;
                return pastaExecutavel += @"\";

            }
            catch
            {
                return null;
            }
        }

        internal static void clearTemp()
        {
            try { Directory.Delete(currentPath() + "temp", true); }
            catch { }            
        }

    }
}
