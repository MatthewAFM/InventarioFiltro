using FiltrarNumerosLINQ.InventarioApp;
using System;
using System.IO;

namespace InventarioApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string rutaArchivo = "inventario_productos.csv"; 
            string rutaSalida = "resultados_inventario.txt";

            var inventario = new Inventario(rutaArchivo);

            Console.WriteLine("Consultas de Inventario usando LINQ\n");

            Console.WriteLine("Productos con stock menor a 10:");
            foreach (var p in inventario.ProductosConPocoStock())
                Console.WriteLine($"{p.Nombre} - Stock: {p.Stock}");

            Console.WriteLine($"\nValor total del inventario: Q{inventario.ValorTotalInventario():0.00}");

            // Exportar resultados
            inventario.ExportarResultados(rutaSalida);
            Console.WriteLine($"\nArchivo de resultados generado en: {Path.GetFullPath(rutaSalida)}");
        }
    }
}