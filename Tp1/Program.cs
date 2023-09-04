using EspacioClase;
AccesoADatos informacion = new AccesoCSV();
Cadeteria cadeteria = informacion.CargarDatos("cadeteria.csv","cadetes.csv");

while (true)
{
    Console.WriteLine("==== Gestión de Pedidos ====");
    Console.WriteLine("1) Dar de alta pedidos");
    Console.WriteLine("2) Asignar cadete a pedidio");
    Console.WriteLine("3) Cambiar estado de pedidos");
    Console.WriteLine("4) Jornal a cobrar de Cadete");
    Console.WriteLine("5) Mostrar informe de pedidos");
    Console.WriteLine("6) Salir");

    Console.Write("Seleccione una opción: ");
    string opcion = Console.ReadLine();

    switch (opcion)
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
            cadeteria.AsignarCadeteAPedido();
            break;
        case "3":
            // Lógica para cambiar estado de pedidos
            cadeteria.CambiarEstado();
            break;
        case "4":
            // Lógica para obtener las ganancias de cada cadete
            Console.WriteLine("Ingrese el id del Cadete");
            string inputIdCadete = Console.ReadLine();
            int.TryParse(inputIdCadete, out int idCadete);
            Console.WriteLine($"El cadete debe cobrar ${cadeteria.JornalACobrar(idCadete)}");
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
        
