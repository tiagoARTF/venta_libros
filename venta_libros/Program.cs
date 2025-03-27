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

        public static string archivoInventario = "inventario.csv";
        public static string archivoVentas = "ventas.csv";

        static void Main(string[] args)
        {
            Menu.CargarDatosDesdeCSV();
            Menu.CargarVentasCSV();

            int opcion = 0;

            do
            {
                Menu.MostrarAscii();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n============================================\n Bienvenido al sistema de ventas de Libros \n============================================");
                Console.ResetColor();
                Menu.MostrarMenu();

                do
                {
                    Console.Write("Seleccione una opción: ");
                    string entrada = Console.ReadLine();

                    try
                    {
                        opcion = Convert.ToInt32(entrada); 

                        if (opcion < 1 || opcion > 6)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Opción fuera de rango. Ingresa una opción entre 1 y 6.");
                            Console.ResetColor();
                            
                        }
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ingresa un valor numérico");
                        Console.ResetColor();
                        opcion = 0;
                        
                    }
                } while (opcion < 1 || opcion > 6);  

                
                if (opcion == 1)
                {
                    Menu.RegistrarVenta();
                }
                else if (opcion == 2)
                {
                    Menu.MostrarInventario();
                    Console.WriteLine("\nPrecione cualquier tecla para continuar");
                    Console.ReadKey();
                    Console.Clear();
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

            } while (opcion != 6);  

            Menu.GuardarInventarioCSV();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Gracias por usar el sistema de ventas. ¡Hasta la próxima!");
            Console.ResetColor();
            Console.ReadKey();

        }
    }
}
