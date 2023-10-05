using System.Drawing;

namespace KernelConvolution
{
    public class RgbColor
    {
        private int r { get; set; }
        private int g { get; set; }
        private int b { get; set; }

        public RgbColor(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public static RgbColor AddColors(RgbColor c1, RgbColor c2)
        {
            return new RgbColor(c1.r + c2.r, c1.g + c2.g, c1.b + c2.b);
        }

        public static Color ConvertoToColor(RgbColor c)
        {
            Color result = Color.FromArgb(
                c.r < 0 ? 0 : c.r > 255 ? 255 : c.r,
                c.g < 0 ? 0 : c.g > 255 ? 255 : c.g,
                c.b < 0 ? 0 : c.b > 255 ? 255 : c.b
            );

            return result;
        }       
        
        public static RgbColor MultColorByNum(Color c, int num)
        {
            return new RgbColor(c.R * num, c.G * num, c.B * num);
        }
    }
}
