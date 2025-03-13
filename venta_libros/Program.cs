using System;

namespace venta_libros
{
    class Program
    {
            public static string[,] inventario = {
            { "Homo Deus", "10" },
            { "Lo que el viento se llevó", "8" },
            { "Cien años de soledad", "5" },
            { "El nombre del viento", "12" },
            { "Harry Potter", "15" }
        };

            public static int numLibros = inventario.GetLength(0);
            public static string[,] ventas = new string[0, 2];
            public static int numVentas = 0;

        static void Main(string[] args)
        {
            int opcion;

            Menu.MostrarAscii();

            do
            {
                Menu.MostrarMenu(); 
                Console.Write("Seleccione una opción: ");
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

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Gracias por usar el sistema de ventas. ¡Hasta la próxima!");
            Console.ResetColor();

            Console.ReadKey();
        }
    }
}
