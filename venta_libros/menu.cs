using System;

namespace venta_libros
{
    class Menu
    {
        public static void MostrarAscii()
        {
            Console.ForegroundColor = ConsoleColor.Cyan; 
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
            Console.WriteLine("\n============================================\n Bienvenido al Sistema de Libros \n============================================");
            Console.ResetColor();

        }
        public static void MostrarMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===== MENÚ PRINCIPAL =====");
            Console.ResetColor();
            Console.WriteLine("1. Registrar Venta\n2. Mostrar Inventario\n3. Agregar Libro\n4. Actualizar Stock\n5. Mostrar ventas\n6. Salir");
            Console.Write("Seleccione una opción: ");
        }


        public static void RegistrarVenta()
        {
            Console.Clear();
            MostrarAscii();
            Console.WriteLine("=== REGISTRAR VENTA ===\n");
            MostrarInventario(false);

            Console.Write("\nIngrese el número del libro a vender: ");
            int indice = Convert.ToInt32(Console.ReadLine()) - 1;

            if (indice >= 0 && indice < Program.numLibros)
            {
                Console.Write("Ingrese la cantidad a vender: ");
                int cantidad = Convert.ToInt32(Console.ReadLine());
                int stockActual = Convert.ToInt32(Program.inventario[indice, 1]);

                if (cantidad > 0 && cantidad <= stockActual)
                {
                    // Actualizar stock
                    stockActual -= cantidad;
                    Program.inventario[indice, 1] = stockActual.ToString();

                    // Agregar a ventas (ampliamos el array)
                    string[,] nuevasVentas = new string[Program.numVentas + 1, 3];
                    for (int i = 0; i < Program.numVentas; i++)
                    {
                        nuevasVentas[i, 0] = Program.ventas[i, 0];
                        nuevasVentas[i, 1] = Program.ventas[i, 1];
                        nuevasVentas[i, 2] = Program.ventas[i, 2];
                    }

                    // Precio total = cantidad * precio unitario
                    double precioUnitario = Convert.ToDouble(Program.inventario[indice, 2]);
                    double precioTotal = cantidad * precioUnitario;

                    nuevasVentas[Program.numVentas, 0] = Program.inventario[indice, 0];
                    nuevasVentas[Program.numVentas, 1] = cantidad.ToString();
                    nuevasVentas[Program.numVentas, 2] = precioTotal.ToString("F2");

                    Program.ventas = nuevasVentas;
                    Program.numVentas++;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nVenta registrada: {cantidad} unidades de '{Program.inventario[indice, 0]}' por ${precioTotal}");
                    Console.ResetColor();

                    // Alerta de stock bajo
                    if (stockActual < 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"ALERTA: El stock de '{Program.inventario[indice, 0]}' es bajo ({stockActual} unidades). Considera reabastecer.");
                        Console.ResetColor();
                    }

                    // Guardar datos después de cada venta
                    Program.GuardarDatosCSV();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Cantidad inválida o insuficiente stock.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Número de libro inválido.");
                Console.ResetColor();
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            MostrarAscii();

        }

        public static void MostrarInventario(bool pausar = true)
        {
            Console.Clear();
            MostrarAscii();
            Console.WriteLine("=== INVENTARIO DE LIBROS ===\n");
            Console.WriteLine("Id  | Titulo                        | Stock  | Precio  ");
            Console.WriteLine("-------------------------------------------------------");
            for (int i = 0; i < Program.numLibros; i++)
            {
                Console.WriteLine($"{i + 1,-3} |  {Program.inventario[i, 0],-28} |  {Program.inventario[i, 1],-6} |  ${Program.inventario[i, 2]}");


            }
            /*if (Program.numLibros == 0)
            {
                Console.WriteLine("No hay libros en el inventario");
            }*/
            if (pausar)
            {
            Console.WriteLine("Precione cualquier tecla para continuar");
            Console.ReadKey();
            Console.Clear();
            }

        }

        public static void AgregarLibro()
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR LIBRO ===\n");

            Console.Write("Ingrese el nombre del nuevo libro: ");
            string titulo = Console.ReadLine();

            Console.Write("Ingrese la cantidad en stock: ");
            string cantidad = Console.ReadLine();

            Console.Write("Ingrese el precio por unidad: ");
            string precio = Console.ReadLine();

            // Crear una nueva matriz con un tamaño mayor
            string[,] nuevoInventario = new string[Program.numLibros + 1, 3];

            // Copiar los datos de la matriz original
            for (int i = 0; i < Program.numLibros; i++)
            {
                nuevoInventario[i, 0] = Program.inventario[i, 0];
                nuevoInventario[i, 1] = Program.inventario[i, 1];
                nuevoInventario[i, 2] = Program.inventario[i, 2];
            }

            // Agregar el nuevo libro 
            nuevoInventario[Program.numLibros, 0] = titulo;
            nuevoInventario[Program.numLibros, 1] = cantidad;
            nuevoInventario[Program.numLibros, 2] = precio;

            // Reemplazar la matriz original con la nueva
            Program.inventario = nuevoInventario;
            Program.numLibros++;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Libro '{titulo}' agregado correctamente.");
            Console.ResetColor();

            // Guardar datos después de agregar un libro
            Program.GuardarDatosCSV();

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ActualizarStock()
        {
            Console.Clear();
            Console.WriteLine("=== ACTUALIZAR STOCK ===");
            MostrarInventario();
            Console.Write("Ingrese el número del libro al que desea actualizar stock: ");
            int i = Convert.ToInt32(Console.ReadLine()) - 1;

            if (i >= 0 && i < Program.numLibros)
            {
                Console.Write($"Ingrese la cantidad adicional para '{Program.inventario[i, 0]}': ");
                int cantidadAdicional = Convert.ToInt32(Console.ReadLine());

                int stockActual = Convert.ToInt32(Program.inventario[i, 1]);
                stockActual += cantidadAdicional; // Sumamos el stock nuevo al existente
                Program.inventario[i, 1] = stockActual.ToString(); // Actualizamos en la matriz

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Stock actualizado. Ahora '{Program.inventario[i, 0]}' tiene {stockActual} unidades.");
                Console.ResetColor();
                Console.Write($"Precio actual de '{Program.inventario[i, 0]}': ${Program.inventario[i, 2]}\n");
                Console.Write("¿Desea actualizar el precio? (S/N): ");
                if (Console.ReadLine().ToUpper() == "S"){
                    Console.Write("Ingrese el nuevo precio: ");
                    string nuevoprecio = Console.ReadLine();
                    Program.inventario[i, 2] = nuevoprecio;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n Stock de: " + Program.inventario[i, 0] + "actualizado correctamente.");
                Console.ResetColor();

                Program.GuardarDatosCSV();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Número de libro no válido.");
                Console.ResetColor();
            }
            Console.WriteLine("\nPresione cualquier tecla para continuar");
            Console.ReadKey();
            Console.Clear();
        }
        public static void MostrarVentas()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===== Ventas Registradas =====");
            Console.ResetColor();
            Console.WriteLine("Id  | Titulo                        | Stock  | Precio  ");
            Console.WriteLine("-------------------------------------------------------");

            double totalVentas = 0;

            for (int i = 0; i < Program.numVentas; i++)
            {
                double precio = Convert.ToDouble(Program.ventas[i, 2]);
                totalVentas += precio;
                Console.WriteLine($"{i + 1,-3} |  {Program.ventas[i, 0],-28} |  {Program.ventas[i, 1],-9} |  ${precio}");
            }

            if (Program.numVentas == 0)
            {
                Console.WriteLine("No se han registrado ventas.");
            }
            else
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Total de ventas: ${totalVentas:F2}");
            }
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}