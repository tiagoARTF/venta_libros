using System;
using System.IO;

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
            //Console.WriteLine("\n============================================\n Bienvenido al Sistema de Libros \n============================================");
            Console.ResetColor();

        }
        public static void MostrarMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===== MENÚ PRINCIPAL =====");
            Console.ResetColor();
            Console.WriteLine("1. Registrar Venta\n2. Mostrar Inventario\n3. Agregar Libro\n4. Actualizar Stock\n5. Mostrar ventas\n6. Salir");

        }

        public static void MostrarInventario()
        {
            Console.Clear();
            MostrarAscii();
            Console.WriteLine("=== INVENTARIO DE LIBROS ===\n");
            Console.WriteLine("Id  | Titulo                          | Stock   | Precio  ");
            Console.WriteLine("-------------------------------------------------------");
            for (int i = 0; i < Program.numLibros; i++)
            {   //es limpio,facil de leer y mas flexible, ya que nos permite escribir expresiones dentro de las llaves. 
                //Esto permite insertar valores de variables directamente dentro de una cadena, utilizando {}.
                Console.WriteLine($"{i + 1,-3} |  {Program.inventario[i, 0],-30} |  {Program.inventario[i, 1],-6} |  ${Program.inventario[i, 2]}");
            }
            if (Program.numLibros == 0)
            {
                Console.WriteLine("No hay libros en el inventario");
            }

        }


        public static void RegistrarVenta()
        {
            Console.Clear();
            MostrarAscii();
            Console.WriteLine("=== REGISTRAR VENTA ===\n");
            MostrarInventario();

            // Leer el índice del libro con validación
            int IdLibro = LeerEntero("\nIngrese el número del libro a vender: ", 1, Program.numLibros) - 1;
            int stockActual = Convert.ToInt32(Program.inventario[IdLibro, 1]);

            // Leer la cantidad a vender con validación
            int cantidad = LeerEntero("Ingrese la cantidad a vender: ", 1, stockActual);

            // Actualizar el stock
            stockActual -= cantidad;
            Program.inventario[IdLibro, 1] = stockActual.ToString();

            // Crear nueva matriz de ventas
            string[,] temp = new string[Program.numVentas + 1, 3];

            // Copiar datos antiguos
            for (int i = 0; i < Program.numVentas; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp[i, j] = Program.ventas[i, j];
                }
            }

            try
            {
                double precioUnitario = Convert.ToDouble(Program.inventario[IdLibro, 2]);
                double precioTotal = cantidad * precioUnitario;

                // Registrar nueva venta
                temp[Program.numVentas, 0] = Program.inventario[IdLibro, 0];
                temp[Program.numVentas, 1] = cantidad.ToString();
                temp[Program.numVentas, 2] = precioTotal.ToString("F2");

                Program.ventas = temp;
                Program.numVentas++;

                // Guardar en archivo CSV
                using (StreamWriter sw = new StreamWriter(Program.archivoVentas, true))
                {
                    sw.WriteLine($"{Program.inventario[IdLibro, 0]},{cantidad},{precioTotal}");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nVenta registrada: {cantidad} unidades de '{Program.inventario[IdLibro, 0]}' por ${precioTotal}");
                Console.ResetColor();

                // Alerta de stock bajo
                if (stockActual < 5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ALERTA: El stock de '{Program.inventario[IdLibro, 0]}' es bajo ({stockActual} unidades). Considera reabastecer.");
                    Console.ResetColor();
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error al procesar el precio del libro.");
                Console.ResetColor();
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            MostrarAscii();
        }

        // Función para leer un número entero con validación
        static int LeerEntero(string mensaje, int min, int max)
        {
            while (true)
            {
                Console.Write(mensaje);
                try
                {
                    int numero = Convert.ToInt32(Console.ReadLine());
                    if (numero >= min && numero <= max)
                    {
                        return numero; // Siempre devuelve un valor si es válido
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error: Ingresa un número entre {min} y {max}.");
                        Console.ResetColor();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Ingrese un número válido.");
                    Console.ResetColor();
                }
            }
        }



        public static void AgregarLibro()
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR LIBRO ===\n");

            Console.Write("Ingrese el nombre del nuevo libro: ");
            string titulo = Console.ReadLine();

            int cantidad = 0;
            bool cantidadValida = false;

            while (!cantidadValida)
            {
                Console.Write("Ingrese la cantidad en stock: ");
                try
                {
                    cantidad = Convert.ToInt32(Console.ReadLine());
                    cantidadValida = true;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Ingrese un número entero válido para la cantidad.");
                    Console.ResetColor();
                }
            }

            int precio = 0;
            bool precioValido = false;

            while (!precioValido)
            {
                Console.Write("Ingrese el precio por unidad (solo numeros enteros, sin puntos ni comas): ");
                try
                {
                    precio = Convert.ToInt32(Console.ReadLine());
                    precioValido = true;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: El precio debe ser un número entero sin puntos ni comas.");
                    Console.ResetColor();
                }
            }


            string[,] nuevoInventario = new string[Program.numLibros + 1, 3];


            for (int i = 0; i < Program.numLibros; i++)
            {
                nuevoInventario[i, 0] = Program.inventario[i, 0];
                nuevoInventario[i, 1] = Program.inventario[i, 1];
                nuevoInventario[i, 2] = Program.inventario[i, 2];
            }


            nuevoInventario[Program.numLibros, 0] = titulo;
            nuevoInventario[Program.numLibros, 1] = cantidad.ToString();
            nuevoInventario[Program.numLibros, 2] = precio.ToString();


            Program.inventario = nuevoInventario;
            Program.numLibros++;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"El libro '{titulo}' agregado correctamente.");
            Console.ResetColor();


            GuardarDatosCSV();

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ActualizarStock()
        {
            Console.Clear();
            Console.WriteLine("=== ACTUALIZAR STOCK ===");
            MostrarInventario();

            int i = -1;
            bool librovalido = false;
            while (!librovalido)
            {
                Console.Write("Ingrese el número del libro al que desea actualizar stock: ");
                try
                {
                    i = Convert.ToInt32(Console.ReadLine()) - 1;

                    if (i >= 0 && i < Program.numLibros)
                    {
                        librovalido = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Número de libro no valido. Intete de nuevo");
                        Console.ResetColor();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Ingrese un numero valido.");
                    Console.ResetColor();
                }

            }
            int cantidadAdicional = 0;
            bool cantidadValida = false;

            while (!cantidadValida)
            {
                Console.Write($"Ingrese la cantidad adicional para '{Program.inventario[i, 0]}': ");
                try
                {
                    cantidadAdicional = Convert.ToInt32(Console.ReadLine());
                    cantidadValida = true;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Ingrese un número entero válido.");
                    Console.ResetColor();
                }
            }
            int stockActual = Convert.ToInt32(Program.inventario[i, 1]);
            stockActual += cantidadAdicional;
            Program.inventario[i, 1] = stockActual.ToString();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Stock actualizado. Ahora '{Program.inventario[i, 0]}' tiene {stockActual} unidades.");
            Console.ResetColor();

            Console.Write($"Precio actual de '{Program.inventario[i, 0]}': ${Program.inventario[i, 2]}\n");
            Console.Write("¿Desea actualizar el precio? (S/N): ");

            if (Console.ReadLine().ToUpper() == "S")
            {
                int nuevoPrecio = 0;
                bool precioValido = false;

                while (!precioValido)
                {
                    Console.Write("Ingrese el nuevo precio (solo números enteros, sin puntos ni comas): ");
                    try
                    {
                        nuevoPrecio = Convert.ToInt32(Console.ReadLine());
                        precioValido = true;
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: El precio debe ser un número entero sin puntos ni comas.");
                        Console.ResetColor();
                    }
                }

                Program.inventario[i, 2] = nuevoPrecio.ToString();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nStock de '{Program.inventario[i, 0]}' actualizado correctamente.");
            Console.ResetColor();

            GuardarDatosCSV();

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
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
                Console.WriteLine($"Total de ventas: ${totalVentas}");
            }
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void CargarDatosDesdeCSV()
        {
            if (File.Exists(Program.archivoInventario))
            {
                string[] lineas = File.ReadAllLines(Program.archivoInventario);

                if (lineas.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El archivo de inventario está vacío o solo tiene encabezados.");
                    Console.ResetColor();
                    return;
                }

                string[] primeraFila = lineas[0].Split(',');
                int numColumnas = primeraFila.Length;

                Program.numLibros = lineas.Length;

                Program.inventario = new string[Program.numLibros, numColumnas];

                int filaInventario = 0;

                for (int i = 0; i < lineas.Length; i++)
                {
                    string[] datos = lineas[i].Split(',');

                    if (datos.Length == numColumnas)
                    {
                        for (int j = 0; j < numColumnas; j++)
                        {
                            Program.inventario[filaInventario, j] = datos[j].Trim();
                        }
                        filaInventario++;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("...");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El archivo no existe.");
                Console.ResetColor();
            }
        }



        public static void GuardarInventarioCSV()
        {
            if (Program.numLibros > 0)
            {
                string[] lineas = new string[Program.numLibros];
                for (int i = 0; i < Program.numLibros; i++)
                {
                    lineas[i] = Program.inventario[i, 0] + "," + Program.inventario[i, 1] + "," + Program.inventario[i, 2];
                }
                File.WriteAllLines(Program.archivoInventario, lineas);
            }
        }

        public static void GuardarDatosCSV()
        {
            GuardarInventarioCSV();
        }
    }
}