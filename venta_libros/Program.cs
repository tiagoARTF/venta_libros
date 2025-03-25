using System;
using System.IO;

namespace venta_libros
{
    class Program
    {
            public static string[,] inventario = {
            { "Homo Deus", "10", "92.000" },
            { "Lo que el viento se llevó", "8", "53.000" },
            { "Cien años de soledad", "5", "76.000" },
            { "El nombre del viento", "12", "57.600" },
            { "Harry Potter", "15", "44.000" }
        };

            public static int numLibros = inventario.GetLength(0);
            public static string[,] ventas = new string[0, 3];
            public static int numVentas = 0;

        public static string archivoInventario = "inventario.csv";
        public static string archivoVentas = "ventas.csv";

        static void Main(string[] args)
        {
            CargardatosdesdeCSV();
            int opcion;

            Menu.MostrarAscii();

            do
            {
                
                Menu.MostrarMenu(); 
                opcion = Convert.ToInt32(Console.ReadLine());
                 
                if (opcion == 1)
                {
                    Menu.RegistrarVenta();
                }
                else if (opcion == 2)
                {
                    Menu.MostrarInventario();
                }
                else if (opcion == 3)
                {
                    Menu.AgregarLibro();
                }
                else if (opcion == 4)
                {
                    Menu.ActualizarStock();
                }
                else if (opcion == 5) 
                {
                    Menu.MostrarVentas();
                }
                else if  (opcion != 6)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    Console.ResetColor();
                }

            } while (opcion != 6);

            GuardarDatosCSV();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Gracias por usar el sistema de ventas. ¡Hasta la próxima!");
            Console.ResetColor();
            Console.ReadKey();
        }

        public static void CargardatosdesdeCSV()
        {
            if (File.Exists(archivoInventario))
            {
                string[] lineas = File.ReadAllLines(archivoInventario);
                inventario = new string[lineas.Length, 3];
                numLibros = lineas.Length;

                for (int i = 0; i < lineas.Length; i++)
                {
                    string[] datos = lineas[i].Split(',');
                    inventario[i, 0] = datos[0];
                    inventario[i, 1] = datos[1];
                    inventario[i, 2] = datos[2];
                }
            }
            else
            {
                //Si no existe el archivo, gurdar el inventario inicial
                GuardarInventarioCSV();

                //cargar ventas desde el CSV
                if (File.Exists(archivoVentas)){
                    string[] lineasVentas = File.ReadAllLines(archivoVentas);
                    ventas = new string[lineasVentas.Length, 3];
                    numVentas = lineasVentas.Length;

                    for (int i = 0; i < lineasVentas.Length; i++)
                    {
                        string[] datos = lineasVentas[i].Split(',');
                        ventas[i, 0] = datos[0];
                        ventas[i, 1] = datos[1];
                        ventas[i, 2] = datos[2];
                    }
                } 
            }
        }
        public static void GuardarInventarioCSV()
        {
            string[] lineas = new string[numLibros];
            for (int i = 0; i < numLibros; i++)
            {
                lineas[i] = inventario[i, 0] + "," + inventario[i, 1] + "," + inventario[i, 2];
            }
            File.WriteAllLines(archivoInventario, lineas);
        }

        public static void GuardarVentasCSV()
        {
            string[] lineas = new string[numVentas];
            for(int i = 0; i < numVentas; i++)
            {
                lineas[i] = ventas[i, 0] + "," + ventas[i, 1] + "," + ventas[i, 2];
            }
            File.WriteAllLines(archivoVentas, lineas);
        }
        public static void GuardarDatosCSV()
        {
            GuardarInventarioCSV();
            GuardarVentasCSV();
        }
    }
}
