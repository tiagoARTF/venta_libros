using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace venta_libros
{
    class Menu
    {
        public static void MostrarMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===== MENÚ PRINCIPAL =====");
            Console.ResetColor();
            Console.WriteLine("1. Registrar Venta\n2. Mostrar Inventario\n3. Agregar Libro\n4. Actualizar Stock\n5. Mostrar ventas\n6. Salir");
            Console.Write("Seleccione una opción: ");
        }

        public static void MostrarInventario()
        {
            Console.WriteLine("\nInventario Actual:");
            for (int i = 0; i < Program.numLibros; i++)
            {
                Console.WriteLine($"{i + 1}. {Program.inventario[i, 0]} - Stock: {Program.inventario[i, 1]}");
            }
        }

        public static void RegistrarVenta()
        {
            MostrarInventario();
            Console.Write("Ingrese el número del libro que desea vender: ");
            int indice = Convert.ToInt32(Console.ReadLine()) - 1;

            if (indice >= 0 && indice < Program.numLibros)
            {
                Console.Write($"Ingrese la cantidad vendida de '{Program.inventario[indice, 0]}': ");
                int cantidadVendida = Convert.ToInt32(Console.ReadLine());

                int stockActual = Convert.ToInt32(Program.inventario[indice, 1]);

                if (cantidadVendida <= stockActual)
                {
                    stockActual -= cantidadVendida;
                    Program.inventario[indice, 1] = stockActual.ToString(); // Actualizar stock

                    // Guardar la venta en la matriz dinámica
                    GuardarVenta(Program.inventario[indice, 0], cantidadVendida);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Venta registrada. Nuevo stock de '{Program.inventario[indice, 0]}': {stockActual}");
                    Console.ResetColor();

                    if (stockActual < 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"ALERTA: Reabastecer el libro '{Program.inventario[indice, 0]}'");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No hay suficiente stock para completar la venta.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Número de libro no válido.");
                Console.ResetColor();
            }
        }
        

        public static void GuardarVenta(string nombreLibro, int cantidad)
        {
            // Crear nueva matriz con más espacio
            string[,] nuevaMatriz = new string[Program.numVentas + 1, 2];

            // Copiar datos existentes
            for (int i = 0; i < Program.numVentas; i++)
            {
                nuevaMatriz[i, 0] = Program.ventas[i, 0];
                nuevaMatriz[i, 1] = Program.ventas[i, 1];
            }

            // Agregar la nueva venta
            nuevaMatriz[Program.numVentas, 0] = nombreLibro;
            nuevaMatriz[Program.numVentas, 1] = cantidad.ToString();

            // Reemplazar la matriz original
            Program.ventas = nuevaMatriz;
            Program.numVentas++;
        }

        public static void AgregarLibro()
        {
            Console.Write("Ingrese el nombre del nuevo libro: ");
            string nuevoLibro = Console.ReadLine();

            Console.Write("Ingrese la cantidad en stock: ");
            string nuevoStock = Console.ReadLine();

            // Crear una nueva matriz con un tamaño mayor
            int nuevoTamano = Program.numLibros + 1;
            string[,] nuevoInventario = new string[nuevoTamano, 2];

            // Copiar los datos de la matriz original
            for (int i = 0; i < Program.numLibros; i++)
            {
                nuevoInventario[i, 0] = Program.inventario[i, 0];
                nuevoInventario[i, 1] = Program.inventario[i, 1];
            }

            // Agregar el nuevo libro en la última posición
            nuevoInventario[nuevoTamano - 1, 0] = nuevoLibro;
            nuevoInventario[nuevoTamano - 1, 1] = nuevoStock;

            // Reemplazar la matriz original con la nueva
            Program.inventario = nuevoInventario;
            Program.numLibros = nuevoTamano;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Libro '{nuevoLibro}' agregado con éxito.");
            Console.ResetColor();
        }

        public static void ActualizarStock()
        {
            MostrarInventario();
            Console.Write("Ingrese el número del libro al que desea agregar stock: ");
            int indice = Convert.ToInt32(Console.ReadLine()) - 1;

            if (indice >= 0 && indice < Program.numLibros)
            {
                Console.Write($"Ingrese la cantidad adicional para '{Program.inventario[indice, 0]}': ");
                int cantidadAdicional = Convert.ToInt32(Console.ReadLine());

                int stockActual = Convert.ToInt32(Program.inventario[indice, 1]);
                stockActual += cantidadAdicional; // Sumamos el stock nuevo al existente
                Program.inventario[indice, 1] = stockActual.ToString(); // Actualizamos en la matriz

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Stock actualizado. Ahora '{Program.inventario[indice, 0]}' tiene {stockActual} unidades.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Número de libro no válido.");
                Console.ResetColor();
            }
        }
        public static void MostrarVentas()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===== Ventas Registradas =====");
            Console.ResetColor();

            if (Program.numVentas == 0)
            {
                Console.WriteLine("No se han registrado ventas.");
            }
            else
            {
                for (int i = 0; i < Program.numVentas; i++)
                {
                    Console.WriteLine($"{i + 1}. {Program.ventas[i, 0]} - Cantidad vendida: {Program.ventas[i, 1]}");
                }
            }
        }
        public static void MostrarAscii()
        {
            Console.ForegroundColor = ConsoleColor.Cyan; // Cambia el color del texto
            Console.WriteLine(@"
                           .--.                   .---.
                       .---|__|           .-.     |~~~|
                    .--|===|--|_          |_|     |~~~|--.
                    |  |===|  |'\     .---!~|  .--|   |--|
                    |%%|   |  |.'\    |===| |--|%%|   |  |
                    |%%|   |  |\.'\   |   | |__|  |   |  |
                    |  |   |  | \  \  |===| |==|  |   |  |
                    |  |   |__|  \.'\ |   |_|__|  |~~~|__|
                    |  |===|--|   \.'\|===|~|--|%%|~~~|--|
                    ^--^---'--^    `-'`---^-^--^--^---'--'
                 ");
            Console.WriteLine("\n==================================\n Bienvenido al Sistema de Ventas de Libros \n==================================");
            Console.ResetColor();

        }
    }
}
