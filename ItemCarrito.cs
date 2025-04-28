using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2Comprar
{
    internal class ItemCarrito
    {
        private string nombre;
        private double precio;

        public ItemCarrito(string nombre, double precio)
        {
            this.nombre = nombre;
            this.precio = precio;
        }

        public string GetNombre() => nombre;
        public double GetPrecio() => precio;

        public void MostrarInfo()
        {
            Console.WriteLine($"- {nombre}: ${precio}");
        }
    }
}
