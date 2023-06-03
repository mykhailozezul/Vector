class Vector
    {
        public double _x { get; set; }
        public double _y { get; set; }
        public double _z { get; set; }
        public Vector(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public static Vector CrossProduct(Vector v1, Vector v2)
        {
            Vector result = new Vector(
                    (v1._y * v2._z) - (v1._x * v2._y),
                    (v1._z * v2._x) - (v1._x * v2._z),
                    (v1._x * v2._y) - (v1._y * v2._x)
                );
            return result;
        }

        public static double DotProduct(Vector v1, Vector v2)
        {
            double result = v1._x * v2._x + v1._y * v2._y + v1._z * v2._z;

            return result;
        }

        public static Vector Add(Vector v1, Vector v2)
        {
            Vector result = new Vector(
                    v1._x + v2._x,
                    v1._y + v2._y,
                    v1._z + v2._z
                );

            return result;
        }

        public static Vector Subtract(Vector v1, Vector v2)
        {
            Vector result = new Vector(
                    v2._x - v1._x,
                    v2._y - v1._y,
                    v2._z - v1._z
                 );

            return result;
        }

        public static Vector Normalize(Vector v)
        {
            double magnitude = Vector.Magnitude(v);
            Vector result = new Vector(
                    v._x / magnitude,
                    v._y / magnitude,
                    v._z / magnitude
                );

            return result;
        }

        public static double Magnitude(Vector v)
        {
            double result = Math.Sqrt(Math.Pow(v._x, 2) + Math.Pow(v._y, 2) + Math.Pow(v._z, 2));

            return result;
        }

        public static double Dist(Vector v1, Vector v2)
        {
            double result = Vector.Magnitude(Vector.Subtract(v1, v2));

            return result;
        }

        public static Vector Rotate(Vector vect, double roll, double pitch, double yaw)
        {
            List<double> matrixRoll = new List<double> { 1, 0, 0, 
                                                         0, Math.Cos(Vector.DegToRad(roll)) , -Math.Sin(Vector.DegToRad(roll)) ,
                                                         0, Math.Sin(Vector.DegToRad(roll)) , Math.Cos(Vector.DegToRad(roll))};

            List<double> matrixPitch = new List<double> { Math.Cos(Vector.DegToRad(pitch)), 0, Math.Sin(Vector.DegToRad(pitch)),
                                                          0, 1, 0,
                                                         -Math.Sin(Vector.DegToRad(pitch)), 0, Math.Cos(Vector.DegToRad(pitch))};

            List<double> matrixYaw = new List<double> { Math.Cos(Vector.DegToRad(yaw)), -Math.Sin(Vector.DegToRad(yaw)), 0,
                                                        Math.Sin(Vector.DegToRad(yaw)), Math.Cos(Vector.DegToRad(yaw)), 0,
                                                        0, 0, 1 };

            Vector MultVectorWithMatrix(Vector v, List<double> matrix)
            {
                Vector result = new Vector(
                    v._x * matrix[0] + v._y * matrix[1] + v._z * matrix[2],
                    v._x * matrix[3] + v._y * matrix[4] + v._z * matrix[5],
                    v._x * matrix[6] + v._y * matrix[7] + v._z * matrix[8]
                );
                return result;
            }

            var rotVector = MultVectorWithMatrix(vect, matrixRoll);
            rotVector = MultVectorWithMatrix(rotVector, matrixPitch);
            rotVector = MultVectorWithMatrix(rotVector, matrixYaw);

            return rotVector;
        }

        public static double FindAngle(Vector v1, Vector v2)
        {
            double dotProduct = Vector.DotProduct(v1, v2);
            double v1Mag = Vector.Magnitude(v1);
            double v2Mag = Vector.Magnitude(v2);
            double cosAngle = dotProduct / (v1Mag * v2Mag);
            return Math.Acos(cosAngle) * 180 / Math.PI;
        }

        public static double DegToRad(double angle)
        {
            return angle * Math.PI / 180;
        }

        public void OutPut()
        {
            Console.WriteLine($"x: {_x}, y: {_y}, z: {_z}");
        }
    }