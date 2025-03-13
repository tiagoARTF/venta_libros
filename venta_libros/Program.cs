using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace venta_libros
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] inventario = {
            { "Homo Deus", "10" },
            { "Lo que el viento se llevó", "8" },
            { "Cien años de soledad", "5" },
            { "El nombre del viento", "12" },
            { "Harry Potter", "15" }
        };

            int numLibros = inventario.GetLength(0); // Número de libros en la matriz
            int opcion;

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

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("===== MENÚ PRINCIPAL =====");
                Console.ResetColor();
                Console.WriteLine("1. Registrar Venta\n2. Mostrar Inventario\n3. Agregar Libro\n4. Actualizar Stock\n5. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                if (opcion == 1) 
                {
                    Console.WriteLine("\nLibros disponibles:");
                    for (int i = 0; i < numLibros; i++)
                    {
                        Console.WriteLine($"{i + 1}. {inventario[i, 0]} - Stock: {inventario[i, 1]}");
                    }

                    Console.Write("Ingrese el número del libro que desea vender: ");
                    int indice = Convert.ToInt32(Console.ReadLine()) - 1;

                    if (indice >= 0 && indice < numLibros)
                    {
                        Console.Write($"Ingrese la cantidad vendida de '{inventario[indice, 0]}': ");
                        int cantidadVendida = Convert.ToInt32(Console.ReadLine());

                        int stockActual = Convert.ToInt32(inventario[indice, 1]);

                        if (cantidadVendida <= stockActual)
                        {
                            stockActual -= cantidadVendida;
                            inventario[indice, 1] = stockActual.ToString(); 

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Venta registrada. Nuevo stock de '{inventario[indice, 0]}': {stockActual}");
                            Console.ResetColor();

                            if (stockActual < 5)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"ALERTA: Reabastecer el libro '{inventario[indice, 0]}'");
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
                else if (opcion == 2) // Mostrar Inventario
                {
                    Console.WriteLine("\nInventario Actual:");
                    for (int i = 0; i < numLibros; i++)
                    {
                        Console.WriteLine($"{i + 1}. {inventario[i, 0]} - Stock: {inventario[i, 1]}");
                    }
                }
                else if (opcion == 3) // Agregar un nuevo libro
                {
                    Console.Write("Ingrese el nombre del nuevo libro: ");
                    string nuevoLibro = Console.ReadLine();

                    Console.Write("Ingrese la cantidad en stock: ");
                    string nuevoStock = Console.ReadLine();

                    // Crear una nueva matriz con un tamaño mayor
                    int nuevoTamano = numLibros + 1;
                    string[,] nuevoInventario = new string[nuevoTamano, 2];

                    // Copiar los datos de la matriz original
                    for (int i = 0; i < numLibros; i++)
                    {
                        nuevoInventario[i, 0] = inventario[i, 0];
                        nuevoInventario[i, 1] = inventario[i, 1];
                    }

                    // Agregar el nuevo libro en la última posición
                    nuevoInventario[nuevoTamano - 1, 0] = nuevoLibro;
                    nuevoInventario[nuevoTamano - 1, 1] = nuevoStock;

                    // Reemplazar la matriz original con la nueva
                    inventario = nuevoInventario;
                    numLibros = nuevoTamano;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Libro '{nuevoLibro}' agregado con éxito.");
                    Console.ResetColor();
                }
                else if (opcion == 4) // Actualizar cantidad de un libro existente
                {
                    Console.WriteLine("\nLibros disponibles:");
                    for (int i = 0; i < numLibros; i++)
                    {
                        Console.WriteLine($"{i + 1}. {inventario[i, 0]} - Stock: {inventario[i, 1]}");
                    }

                    Console.Write("Ingrese el número del libro cuyo stock desea actualizar: ");
                    int indiceActualizar = Convert.ToInt32(Console.ReadLine()) - 1;

                    if (indiceActualizar >= 0 && indiceActualizar < numLibros)
                    {
                        Console.Write($"Ingrese la nueva cantidad en stock para '{inventario[indiceActualizar, 0]}': ");
                        inventario[indiceActualizar, 1] = Console.ReadLine();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Stock actualizado correctamente. Nuevo stock de '{inventario[indiceActualizar, 0]}': {inventario[indiceActualizar, 1]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Número de libro no válido.");
                        Console.ResetColor();
                    }
                }
                else if (opcion != 5) // Opción inválida
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    Console.ResetColor();
                }

            } while (opcion != 5);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Gracias por usar el sistema de ventas. ¡Hasta la próxima!");
            Console.ResetColor();


            Console.ReadKey();
        }
    }
}
