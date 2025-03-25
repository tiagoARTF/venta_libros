using System;
using System.IO;

namespace venta_libros
{
    class Program
    {
        public static string[,] inventario;
        public static int numLibros = 0;
        public static string[,] ventas = new string[0, 3];
        public static int numVentas = 0;

        public static string archivoInventario = "inventarioo.csv";
        public static string archivoVentas = "ventas.csv";

        static void Main(string[] args)
        {
            Menu.CargarDatosDesdeCSV();

            int opcion = 0;

            Menu.MostrarAscii();

            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n============================================\n Bienvenido al Sistema de Libros \n============================================");
                Console.ResetColor();
                Menu.MostrarMenu();
                bool entradaValida = false;
                while (!entradaValida)
                {
                    Console.Write("Seleccione una opción: ");
                    string entrada = Console.ReadLine();

                    try
                    {
                        opcion = Convert.ToInt32(entrada);
                        entradaValida = true;
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ingresa un valor numerico");
                        Console.ResetColor();
                    }
                }

                if (opcion == 1)
                {
                    Menu.RegistrarVenta();
                }
                else if (opcion == 2)
                {
                    Menu.MostrarInventario();
                    Console.WriteLine("Precione cualquier tecla para continuar");
                    Console.ReadKey();
                    Console.Clear();
                    Menu.MostrarAscii();
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
                else if (opcion != 6)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    Console.ResetColor();
                }

            } while (opcion != 6);

            Menu.GuardarDatosCSV();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Gracias por usar el sistema de ventas. ¡Hasta la próxima!");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
