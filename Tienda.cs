using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2Comprar
{
    internal class Tienda
    {
        private List<Item> inventario;
        private List<ItemCarrito> carrito;
        private double dineroDisponible;
        private double dineroInicial;

        public Tienda()
        {
            InicializarInventario();
            carrito = new List<ItemCarrito>();
        }

        private void InicializarInventario()
        {
            inventario = new List<Item>
            {
                new Item("PlayStation 5", 10, 566),
                new Item("Nintento Switch 2", 5, 800),
                new Item("Pc Gamer", 3, 1200),
                new Item("Entrada Comic Con San Diego", 2, 1400),
                new Item("Chevrolet Camaro", 1, 36900)
            };
        }

        private int ObtenerSeleccionUsuario(int maxOpciones)
        {
            while (true)
            {
                string input = Console.ReadLine();

                // Validación básica sin IsNullOrWhiteSpace
                if (input == null || input.Length == 0)
                {
                    Console.Write("Debe ingresar un valor. Por favor seleccione una opción válida: ");
                    continue;
                }

                if (int.TryParse(input, out int seleccion))
                {
                    if (seleccion >= 1 && seleccion <= maxOpciones)
                    {
                        return seleccion - 1; // Convertir a índice base 0
                    }
                    Console.Write($"Opción no válida. Por favor ingrese un número entre 1 y {maxOpciones}: ");
                }
                else
                {
                    Console.Write("Entrada no válida. Por favor ingrese un número: ");
                }
            }
        }

        public void Iniciar()
        {
            SolicitarDineroInicial();
            bool continuarComprando = true;
            
            while (continuarComprando)
            {
                MostrarMenuPrincipal();
                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1: ComprarProducto();break;
                    case 2: continuarComprando = FinalizarCompra();break;
                    case 3: VerCarrito();break;
                    default: Console.WriteLine("Opción no válida");break; 
                }

                if (continuarComprando)
                {
                    Console.WriteLine("\nPresione Enter para continuar...");
                    Console.ReadLine();
                }
            }
        }

        private void SolicitarDineroInicial()
        {
            Console.WriteLine("=== BIENVENIDO A LA TIENDA VIRTUAL ===");

            while (true)
            {
                Console.Write("Ingrese la cantidad de dinero que tiene: $");
                string input = Console.ReadLine();

                // Validación básica sin IsNullOrWhiteSpace
                if (input == null || input.Length == 0)
                {
                    Console.WriteLine("Debe ingresar un valor.");
                    continue;
                }

                if (double.TryParse(input, out double dinero) && dinero >= 0)
                {
                    dineroDisponible = dinero;
                    dineroInicial = dinero;
                    break;
                }
                Console.WriteLine("Cantidad no válida. Por favor ingrese un número positivo.");
            }
        }

        private void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine($"Dinero disponible: ${dineroDisponible}");
            Console.WriteLine("\n=== PRODUCTOS DISPONIBLES===");
            for (int i = 0; i < inventario.Count; i++)
            {
                inventario[i].MostrarInfo(i + 1);
            }
            Console.WriteLine("\n=== OPCIONES ===");
            Console.WriteLine("1.Comprar un producto");
            Console.WriteLine("2.Finalizar Compra");
            Console.WriteLine("3.Ver carrito");
            Console.WriteLine("Seleccione una opción: ");
        }
        
        private void ComprarProducto()
        {
            Console.WriteLine("Seleccione el número del producto que desea comprar: ");
            int seleccion = int.Parse(Console.ReadLine()) - 1;

            if (seleccion >= 0 && seleccion < inventario.Count)
            {
                Item productoSeleccionado = inventario[seleccion];

                if(productoSeleccionado.GetCantidadDisponible() <= 0)
                {
                    Console.WriteLine("No hay unidades disponibles de este producto.");
                }
                else if(dineroDisponible < productoSeleccionado.GetPrecio())
                {
                    Console.WriteLine("No tiene suficiente dinero para comprar este producto.");
                }
                else
                {
                    carrito.Add(new ItemCarrito(productoSeleccionado.GetNombre(), productoSeleccionado.GetPrecio()));
                    productoSeleccionado.ReducirCantidad();
                    dineroDisponible -= productoSeleccionado.GetPrecio();

                    Console.WriteLine($"¡{productoSeleccionado.GetNombre()} agregado al carrito!");
                }
            }
            else
            {
                Console.WriteLine("Seleccione inválida.");
            }
        }
        
        private bool FinalizarCompra()
        {
            Console.Clear();
            Console.WriteLine("=== RESUMEN DE COMPRA ===");
            Console.WriteLine("Productos comprados");

            double totalGastado = 0;
            foreach (var item in carrito)
            {
                item.MostrarInfo();
                totalGastado += item.GetPrecio();
            }
            Console.WriteLine($"\nTotal gastado: ${totalGastado}");
            Console.WriteLine($"Dinero restante: ${dineroDisponible}");
            Console.WriteLine($"Dinero Inicial: ${dineroInicial}");
            Console.WriteLine("\nGracias por su compra. ¡Vuelva pronto!");
            return false;
        }

        private void VerCarrito()
        {
            Console.Clear();
            Console.WriteLine("=== CARRITO DE COMPRAS ===");

            if(carrito.Count == 0)
            {
                Console.WriteLine("El carrito está vacio.");
            }
            else
            {
                double subtotal = 0;
                foreach(var item in carrito)
                {
                    item.MostrarInfo();
                    subtotal += item.GetPrecio();
                }
                Console.WriteLine($"\nSubtotal: ${subtotal}");
                Console.WriteLine($"Dinero disponible: ${dineroDisponible}");
            }
        }
    }
}
