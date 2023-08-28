using EspacioClase;

Cadeteria cadeteria = new Cadeteria("cadeteria.csv", "cadetes.csv"); // Inicialia0a la Cadeteria con los archivos

while (true)
{
    Console.WriteLine("==== Gestión de Pedidos ====");
    Console.WriteLine("1) Dar de alta pedidos");
    Console.WriteLine("2) Asignar pedidos a cadetes");
    Console.WriteLine("3) Cambiar estado de pedidos");
    Console.WriteLine("4) Reasignar pedido a otro cadete");
    Console.WriteLine("5) Mostrar informe de pedidos");
    Console.WriteLine("6) Salir");

    Console.Write("Seleccione una opción: ");
    string opcion = Console.ReadLine();

    switch (opcion.ToLower())
    {
        case "1":
            // Lógica para dar de alta pedidos
            Console.WriteLine("Ingrese el id del pedido a realizar");
            string inputId = Console.ReadLine();
            int.TryParse(inputId, out int id);
            cadeteria.AltaPedido(id);
            break;
        case "2":
            // Lógica para asignar pedidos a cadetes
            Console.WriteLine("Estamos asignando el pedido a un cadete disponible...");
            cadeteria.AsignarPedido();
            break;
        case "3":
            // Lógica para cambiar estado de pedidos
            cadeteria.CambiarEstado();
            break;
        case "4":
            // Lógica para reasignar pedido a otro cadete
            Console.WriteLine("Ingrese el id del pedido a reasignar");
            string inputIdPedido = Console.ReadLine();
            int.TryParse(inputIdPedido, out int idPedido);
            Console.WriteLine("Ingrese el id del nuevo Cadete");
            string inputIdNuevoCadete = Console.ReadLine();
            int.TryParse(inputIdNuevoCadete, out int idNuevoCadete);

            cadeteria.ReasignarPedido(idPedido,idNuevoCadete);
            break;
        case "5":
            // Lógica para mostrar informe de pedidos
            Console.WriteLine("Aqui tienes el informe:");
            Informe informe = new Informe(cadeteria);
            informe.MostrarInforme();
            break;
        case "6":
            Console.WriteLine("Saliendo del programa.");
            return;
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }

    Console.WriteLine(); // Separador entre iteraciones
}
        
