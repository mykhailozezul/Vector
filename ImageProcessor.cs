namespace KernelConvolution
{
    public class ImageProcessor
    {
        public static Bitmap ApplyKernelToImage(double[] kernel, Bitmap inputImage)
        {
            Bitmap outputImage = new Bitmap(inputImage.Width, inputImage.Height);
            List<RgbColor> snapShot;
            if (kernel.Length == 9)
            {
                for (int x = 1; x < inputImage.Width - 1; x++)
                {
                    for (int y = 1; y < inputImage.Height - 1; y++)
                    {
                        snapShot = new List<RgbColor>() {
                            RgbColor.MultColorByNum(inputImage.GetPixel(x - 1, y - 1), kernel[0]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x - 1, y), kernel[1]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x - 1, y + 1), kernel[2]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x, y - 1), kernel[3]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x, y), kernel[4]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x, y + 1), kernel[5]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x + 1, y - 1), kernel[6]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x + 1, y), kernel[7]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x + 1, y + 1), kernel[8])
                        };

                        RgbColor sum = new RgbColor(0, 0, 0);
                        foreach (var item in snapShot)
                        {
                            sum = RgbColor.AddColors(sum, item);
                        }

                        outputImage.SetPixel(x,y,RgbColor.ConvertoToColor(sum));
                    }
                }
            }
            if (kernel.Length == 25)
            {
                for (int x = 2; x < inputImage.Width - 2; x++)
                {
                    for (int y = 2; y < inputImage.Height - 2; y++)
                    {
                        snapShot = new List<RgbColor>() {
                            RgbColor.MultColorByNum(inputImage.GetPixel(x - 2, y - 2), kernel[0]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x - 2, y - 1), kernel[1]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x - 2, y), kernel[2]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x - 2, y + 1), kernel[3]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x - 2, y + 2), kernel[4]),

                            RgbColor.MultColorByNum(inputImage.GetPixel(x - 1, y - 2), kernel[5]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x - 1, y - 1), kernel[6]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x - 1, y), kernel[7]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x - 1, y + 1), kernel[8]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x - 1, y + 2), kernel[9]),

                            RgbColor.MultColorByNum(inputImage.GetPixel(x, y - 2), kernel[10]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x, y - 1), kernel[11]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x, y), kernel[12]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x, y + 1), kernel[13]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x, y + 2), kernel[14]),

                            RgbColor.MultColorByNum(inputImage.GetPixel(x + 1, y - 2), kernel[15]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x + 1, y - 1), kernel[16]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x + 1, y), kernel[17]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x + 1, y + 1), kernel[18]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x + 1, y + 2), kernel[19]),

                            RgbColor.MultColorByNum(inputImage.GetPixel(x + 2, y - 2), kernel[20]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x + 2, y - 1), kernel[21]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x + 2, y), kernel[22]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x + 2, y + 1), kernel[23]),
                            RgbColor.MultColorByNum(inputImage.GetPixel(x + 2, y + 2), kernel[24])
                        };

                        RgbColor sum = new RgbColor(0, 0, 0);
                        foreach (var item in snapShot)
                        {
                            sum = RgbColor.AddColors(sum, item);
                        }

                        outputImage.SetPixel(x, y, RgbColor.ConvertoToColor(sum));
                    }
                }
            }
            

            return outputImage;
        }

        public static Bitmap ClusterImage(int k, Bitmap inputImage)
        {
            int width = inputImage.Width;
            int height = inputImage.Height;            

            List<Color> centers = new List<Color>();
            List<List<Color>> groups = new List<List<Color>>();
            List<Color> vertices = new List<Color>();

            for (int i = 1; i <= k; i++)
            {
                centers.Add(Color.FromArgb(
                    new Random().Next(0, 255),
                    new Random().Next(0, 255),
                    new Random().Next(0, 255)
                ));
                groups.Add(new List<Color>());
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    vertices.Add(inputImage.GetPixel(x, y));
                }
            }

            int iterator = 0;
            while (iterator < 5)
            {
                foreach (var vert in vertices)
                {
                    int group = 0;
                    double minDist = Dist(centers[group], vert);
                    double dist;
                    int counter = 0;
                    foreach (var center in centers)
                    {
                        dist = Dist(center, vert);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            group = counter;
                        }
                        counter++;
                    }
                    groups[group].Add(vert);
                }                                        

                for (int i = 0; i < centers.Count; i++)
                {
                    double centerX = 0;
                    double centerY = 0;
                    double centerZ = 0;
                    foreach (var vert in groups[i])
                    {
                        centerX += (double)vert.R / groups[i].Count;
                        centerY += (double)vert.G / groups[i].Count;
                        centerZ += (double)vert.B / groups[i].Count;
                    }
                    centers[i] = Color.FromArgb((int)centerX, (int)centerY, (int)centerZ);                    
                }
                iterator++;
            }

            Color vertice;
            Bitmap outputImage = new Bitmap(width, height);
            int index = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    vertice = vertices[index++];                    

                    int group = 0;
                    double minDist = Dist(centers[group], vertice);
                    double dist;

                    foreach (var center in centers)
                    {
                        dist = Dist(center, vertice);

                        if (dist < minDist)
                        {
                            minDist = dist;
                            group = centers.IndexOf(center);
                        }
                    }
                    outputImage.SetPixel(x, y, centers[group]);
                }
            }

            return outputImage;
        }

        public static double Dist(Color v1, Color v2)
        {
            return (v2.R - v1.R) * (v2.R - v1.R) + (v2.G - v1.G) * (v2.G - v1.G) + (v2.B - v1.B) * (v2.B - v1.B);
        }

    }
}