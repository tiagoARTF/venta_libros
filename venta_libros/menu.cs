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
                      _    _ _                _      
                     | |  (_) |__ _ _ ___ _ _(_)__ _ 
                     | |__| | '_ \ '_/ -_) '_| / _` |
                     |____|_|_.__/_| \___|_| |_\__,_|
                                 
                 ");
            Console.ResetColor();

        }
        public static void MostrarMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===== MENÚ PRINCIPAL =====\n");
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
            {   
                Console.WriteLine((i + 1).ToString().PadLeft(3) + " |  " + Program.inventario[i, 0].PadRight(30) + " |  " + Program.inventario[i, 1].PadLeft(6) + " |  $" + Program.inventario[i, 2]);

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
            MostrarInventario();
            Console.WriteLine("\n=== REGISTRAR VENTA ===");

            int IdLibro = 0;
            do
            {
                try
                {
                    Console.Write("\nIngrese el número del libro a vender: ");
                    IdLibro = Convert.ToInt32(Console.ReadLine()) - 1;

                    if (IdLibro < 0 || IdLibro > Program.numLibros)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: Ingresa un número entre 1 y " + Program.numLibros + ".");
                        Console.ResetColor();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Ingrese un número válido.");
                    Console.ResetColor();
                }
            } while (IdLibro < 0 || IdLibro > Program.numLibros); 

            int stockActual = Convert.ToInt32(Program.inventario[IdLibro, 1]);

            int cantidad = 0;

            do
            {
                try
                {
                    Console.Write("Ingrese la cantidad a vender: ");
                    cantidad = Convert.ToInt32(Console.ReadLine());

                    if (cantidad < 1 || cantidad > stockActual)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: Ingresa una cantidad entre 1 y " + stockActual + ".");
                        Console.ResetColor();
                    }
                }
                catch (FormatException)
                { 
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Ingrese una cantidad válida.");
                    Console.ResetColor();
                }
            } while (cantidad < 1 || cantidad > stockActual); 


            
            stockActual -= cantidad;
            Program.inventario[IdLibro, 1] = stockActual.ToString();

         
            string[,] temp = new string[Program.numVentas + 1, 3];

            
            for (int i = 0; i < Program.numVentas; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp[i, j] = Program.ventas[i, j];
                }
            }

            try
            {
                int precioUnitario = Convert.ToInt32(Program.inventario[IdLibro, 2]);
                int precioTotal = cantidad * precioUnitario;

                
                temp[Program.numVentas, 0] = Program.inventario[IdLibro, 0];
                temp[Program.numVentas, 1] = cantidad.ToString();
                temp[Program.numVentas, 2] = precioTotal.ToString();

                Program.ventas = temp;
                Program.numVentas++;

                
                using (StreamWriter sw = new StreamWriter(Program.archivoVentas, true))
                {
                    sw.WriteLine(Program.inventario[IdLibro, 0] + "," + cantidad + "," + precioTotal);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nVenta registrada: " + cantidad + " unidades de " + Program.inventario[IdLibro, 0] + " por " + "$" + precioTotal);
                Console.ResetColor();

                
                if (stockActual < 5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ALERTA: El stock de " + Program.inventario[IdLibro, 0] + " es bajo " + "( " + stockActual + " unidades). Considera reabastecer.");
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
            
        }

        public static void AgregarLibro()
        {
            Console.Clear();
            MostrarAscii();
            Console.WriteLine("\n=== AGREGAR LIBRO ===");

            Console.Write("Ingrese el nombre del nuevo libro: ");
            string titulo = Console.ReadLine().Trim();

            
            int cantidad = 0;
            do
            {
                Console.Write("Ingrese la cantidad de stock del nuevo libro: ");
                try
                {
                    cantidad = Convert.ToInt32(Console.ReadLine());
                    if (cantidad < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: La cantidad debe ser mayor a 0.");
                        Console.ResetColor();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Ingrese un número entero válido para la cantidad.");
                    Console.ResetColor();
                }
            } while (cantidad < 1);

            
            int precio = 0;
            do
            {
                Console.Write("Ingrese el precio por unidad (solo números enteros, sin puntos ni comas): ");
                try
                {
                    precio = Convert.ToInt32(Console.ReadLine());
                    if (precio < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: El precio debe ser mayor a 0.");
                        Console.ResetColor();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: El precio debe ser un número entero sin puntos ni comas.");
                    Console.ResetColor();
                }
            } while (precio < 1);

            
            string[,] nuevoInventario = new string[Program.numLibros + 1, 3];

            for (int i = 0; i < Program.numLibros; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    nuevoInventario[i, j] = Program.inventario[i, j];
                }
            }

            
            nuevoInventario[Program.numLibros, 0] = titulo;
            nuevoInventario[Program.numLibros, 1] = cantidad.ToString();
            nuevoInventario[Program.numLibros, 2] = precio.ToString();

            
            Program.inventario = nuevoInventario;
            Program.numLibros++;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("El libro '" + titulo + "' ha sido agregado correctamente.");
            Console.ResetColor();

            
            GuardarInventarioCSV();

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }


        public static void ActualizarStock()
        {
            Console.Clear();
            MostrarAscii();
            MostrarInventario();
            Console.WriteLine("\n=== ACTUALIZAR STOCK ===");

            
            int i = 0;
            do
            {
                Console.Write("Ingrese el número del libro al que desea actualizar stock: ");
                try
                {
                    i = Convert.ToInt32(Console.ReadLine()) - 1;
                    if (i < 0 || i >= Program.numLibros)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: Ingresa un número entre 1 y " + Program.numLibros + ".");
                        Console.ResetColor();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Ingrese un número válido.");
                    Console.ResetColor();
                }
            } while (i < 0 || i >= Program.numLibros);

            
            int cantidadAdicional = 0;
            do
            {
                Console.Write("Ingrese la cantidad adicional para " + Program.inventario[i, 0] + ": ");
                try
                {
                    cantidadAdicional = Convert.ToInt32(Console.ReadLine());
                    if (cantidadAdicional < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: La cantidad debe ser mayor a 0.");
                        Console.ResetColor();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Ingrese un número entero válido.");
                    Console.ResetColor();
                }
            } while (cantidadAdicional < 1);

            
            int stockActual = Convert.ToInt32(Program.inventario[i, 1]);
            stockActual += cantidadAdicional;
            Program.inventario[i, 1] = stockActual.ToString();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Stock actualizado. Ahora" + Program.inventario[i, 0] + "tiene " + stockActual + " unidades.");
            Console.ResetColor();

            
            Console.Write("Precio actual de " + Program.inventario[i, 0] + ": " + "$ " + Program.inventario[i, 2] + "\n");
            Console.Write("¿Desea actualizar el precio? (S/N): ");

            if (Console.ReadLine().Trim().ToUpper() == "S")
            {
                int nuevoPrecio = 0;
                do
                {
                    Console.Write("Ingrese el nuevo precio (solo números enteros, sin puntos ni comas): ");
                    try
                    {
                        nuevoPrecio = Convert.ToInt32(Console.ReadLine());
                        if (nuevoPrecio < 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error: El precio debe ser mayor a 0.");
                            Console.ResetColor();
                        }
                    }
                    catch (FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: El precio debe ser un número entero sin puntos ni comas.");
                        Console.ResetColor();
                    }
                } while (nuevoPrecio < 1);

                Program.inventario[i, 2] = nuevoPrecio.ToString();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nStock de " + Program.inventario[i, 0] + " actualizado correctamente.");
            Console.ResetColor();

            GuardarInventarioCSV();

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void MostrarVentas()
        {
            Console.Clear();
            MostrarAscii();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===== Ventas Registradas =====");
            Console.ResetColor();

            if (Program.numVentas == 0)
            {
                Console.WriteLine("No se han registrado ventas.");
            }
            else
            {
                Console.WriteLine("Id  | Titulo                          | Cantidad | Precio");
                Console.WriteLine("--------------------------------------------------------");

                int totalVentas = 0;

                for (int i = 0; i < Program.numVentas; i++)
                {
                    int precio = Convert.ToInt32(Program.ventas[i, 2]);
                    totalVentas += precio;
                    Console.WriteLine((i + 1).ToString().PadLeft(3) + " |  " + Program.ventas[i, 0].PadRight(30) + " |  " + Program.ventas[i, 1].PadLeft(3) + "   |  $" + precio);
                }

                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Total de ventas: " + "$ " + totalVentas);
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            
        }

        public static void CargarDatosDesdeCSV()
        {
            if (!File.Exists(Program.archivoInventario))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El archivo no existe.");
                Console.ResetColor();
                return;
            }

            string[] lineas = File.ReadAllLines(Program.archivoInventario);

            if (lineas.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("El archivo está vacío.");
                Console.ResetColor();
                return; 
            }

            string[] primeraFila = lineas[0].Split(',');
            int numColumnas = primeraFila.Length;

            Program.numLibros = lineas.Length;
            Program.inventario = new string[Program.numLibros, numColumnas];

            for (int i = 0; i < lineas.Length; i++)
            {
                string[] datos = lineas[i].Split(',');

                if (datos.Length == numColumnas) 
                {
                    for (int j = 0; j < numColumnas; j++)
                    {
                        Program.inventario[i, j] = datos[j].Trim();
                    }
                }
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

        public static void CargarVentasCSV()
        {
            if (File.Exists(Program.archivoVentas)) 
            {
                string[] lineas = File.ReadAllLines(Program.archivoVentas);
                Program.numVentas = lineas.Length;
                Program.ventas = new string[Program.numVentas, 3];

                for (int i = 0; i < Program.numVentas; i++)
                {
                    string[] datos = lineas[i].Split(',');
                    if (datos.Length == 3)
                    {
                        Program.ventas[i, 0] = datos[0]; 
                        Program.ventas[i, 1] = datos[1]; 
                        Program.ventas[i, 2] = datos[2]; 
                    }
                }
            }
        }
    }
}