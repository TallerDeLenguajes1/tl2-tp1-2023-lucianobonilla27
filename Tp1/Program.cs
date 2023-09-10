using EspacioClase;
AccesoADatos informacionCSV = new AccesoCSV();
AccesoADatos informacionJson = new AccesoJson();
Console.WriteLine("====Carga de Datos====");
Console.WriteLine("1) Cargar Datos CSV");
Console.WriteLine("2) Cargar Datos Json");
string carga = Console.ReadLine();
Cadeteria cadeteria = null;
if (carga == "1")
{
    cadeteria = informacionCSV.CargarDatos("cadeteria.csv","cadetes.csv");
}else
{
    if (carga == "2")
    {
        cadeteria = informacionJson.CargarDatos("cadeteria.Json","cadetes.Json");
    }
}
 

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
            Console.WriteLine("Ingrese el id del pedido a dar de alta");
            string inputId = Console.ReadLine();
            int.TryParse(inputId, out int id);
            if(cadeteria.AltaPedido(id)){
                Console.WriteLine("El pedido " + id + " ha sido dado de alta correctamente");
            }else
            {
               Console.WriteLine("No se encontro el pedido " + id+ ".");
            }
            break;
        case "2":
            // Lógica para asignar pedidos a cadetes
            Console.WriteLine("Estamos asignando el pedido a un cadete disponible...");
            Pedidos p = cadeteria.AsignarCadeteAPedido();
            if (p != null)
            {
                Console.WriteLine("Pedido nro "+p.Nro+" asignado al cadete: " + p.CadeteAsignado.Id);
            }else
            {
                Console.WriteLine("No hay cadetes disponibles para asignar el pedido.");
            }
            break;
        case "3":
            // Lógica para cambiar estado de pedidos
            Console.WriteLine("Ingrese el ID del pedido a cambiar de estado: ");
            if (int.TryParse(Console.ReadLine(), out int idPedido))
            {
                Console.WriteLine("Seleccione el estado al que cambiar:");
                Console.WriteLine("a) Pendiente");
                Console.WriteLine("b) En Camino");
                Console.WriteLine("c) Entregado");

                Console.Write("Opción: ");
                string opcionEstado = Console.ReadLine();

                string nuevoEstado = "";

                switch (opcionEstado.ToLower())
                {
                    case "a":
                        nuevoEstado = "Pendiente";
                        break;
                    case "b":
                        nuevoEstado = "EnCamino";
                        break;
                    case "c":
                        nuevoEstado = "Entregado";
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        return;
                }

               Pedidos p2 =  cadeteria.CambiarEstadoPedido(idPedido, nuevoEstado); // Llamar al método independiente
               if (p2 != null)
               {
                    Console.WriteLine("El pedido nro " + p2.Nro + " cambio de estado a : " + p2.Estado);
                
               }else
               {
                 Console.WriteLine("No se encontró el pedido con ID " + idPedido);
                
               }
            }
            else
            {
                Console.WriteLine("Ingrese un ID válido.");
            }
          
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
        
