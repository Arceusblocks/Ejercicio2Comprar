using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2Comprar
{
    internal class Item
    {
        private string nombre;
        private int cantidadDisponible;
        private double precio;

        public Item(string nombre, int cantidadDisponible, double precio)
        {
            this.nombre = nombre;
            this.cantidadDisponible = cantidadDisponible;
            this.precio = precio;
        }

        public string GetNombre() => nombre;
        public int GetCantidadDisponible() => cantidadDisponible;
        public double GetPrecio() => precio;

        public void ReducirCantidad()
        {
            if (cantidadDisponible > 0)
                cantidadDisponible--;
        }

        public void MostrarInfo(int index)
        {
            Console.WriteLine($"{index}. {nombre} - Precio: ${precio} - Disponibles: {cantidadDisponible}");
        }
    }
}
