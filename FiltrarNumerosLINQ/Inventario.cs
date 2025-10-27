using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltrarNumerosLINQ
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    namespace InventarioApp
    {
        public class Inventario
        {
            public List<Producto> Productos { get; set; }

            public Inventario(string rutaArchivo)
            {
                Productos = CargarProductos(rutaArchivo);
            }

            private List<Producto> CargarProductos(string rutaArchivo)
            {
                var productos = new List<Producto>();
                var lineas = File.ReadAllLines(rutaArchivo).Skip(1); // saltar encabezado

                foreach (var linea in lineas)
                {
                    var datos = linea.Split(',');

                    productos.Add(new Producto
                    {
                        Id = int.Parse(datos[0]),
                        Nombre = datos[1],
                        Categoria = datos[2],
                        Precio = double.Parse(datos[3]),
                        Stock = int.Parse(datos[4])
                    });
                }
                return productos;
            }

            // a) Productos con stock menor a 10
            public IEnumerable<Producto> ProductosConPocoStock()
            {
                return from p in Productos
                       where p.Stock < 10
                       select p;
            }

            // b) Productos ordenados por precio descendente
            public IEnumerable<Producto> ProductosPorPrecioDesc()
            {
                return from p in Productos
                       orderby p.Precio descending
                       select p;
            }

            // c) Total del valor del inventario
            public double ValorTotalInventario()
            {
                return (from p in Productos select p.Precio * p.Stock).Sum();
            }

            // d) Agrupar productos por categoría
            public IEnumerable<IGrouping<string, Producto>> AgruparPorCategoria()
            {
                return from p in Productos
                       group p by p.Categoria;
            }

            // e) Exportar resultados
            public void ExportarResultados(string rutaArchivo)
            {
                using (var writer = new StreamWriter(rutaArchivo))
                {
                    writer.WriteLine("=== Productos con stock menor a 10 ===");
                    foreach (var p in ProductosConPocoStock())
                        writer.WriteLine($"{p.Nombre} - Stock: {p.Stock}");

                    writer.WriteLine("\n=== Productos ordenados por precio descendente ===");
                    foreach (var p in ProductosPorPrecioDesc())
                        writer.WriteLine($"{p.Nombre} - Precio: {p.Precio}");

                    writer.WriteLine($"\n=== Valor total del inventario ===");
                    writer.WriteLine($"Q{ValorTotalInventario():0.00}");

                    writer.WriteLine("\n=== Productos agrupados por categoría ===");
                    foreach (var grupo in AgruparPorCategoria())
                    {
                        writer.WriteLine($"\nCategoría: {grupo.Key}");
                        foreach (var p in grupo)
                            writer.WriteLine($"  - {p.Nombre}");
                    }
                }
            }
        }
    }
}
