namespace EspacioClase;



class Cliente
{
    string nombre;
    string direccion;
    int telefono;
    string datosReferenciaDireccion;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public int Telefono { get => telefono; set => telefono = value; }
    public string DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }

     public Cliente(string nombre, string direccion, int telefono, string datosReferenciaDireccion)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.datosReferenciaDireccion = datosReferenciaDireccion;
        }
}
class Pedidos
{
     
    int nro;
    string obs;
    Cliente info;
    string estado;

    public int Nro { get => nro; set => nro = value; }
    public string Obs { get => obs; set => obs = value; }
    public string Estado { get => estado; set => estado = value; }
    public Cliente Info { get => info;}

    public Pedidos(int nro){
        this.nro = nro;
        this.obs = null;
        this.info = GenerarClienteRandom();
        this.estado = "pendiente";

    }

    private Cliente GenerarClienteRandom() 
    {
       Random rand = new();
       string nombre = "cliente " + rand.Next(1,100);
       string direccion = "Dirección" + rand.Next(1, 100);
       int telefono = rand.Next(100000000, 999999999);
       string datosReferenciaDireccion = "Referencia" + rand.Next(1, 100);

       return new Cliente(nombre, direccion, telefono, datosReferenciaDireccion);
    }

    public void VerDireccionCliente(Cliente info){
        Console.WriteLine($"La direccion del {info.Nombre} es {info.Direccion}");
    }

    public void VerDatosCliente(Cliente info){
        Console.WriteLine($"-------{info.Nombre}--------");
        Console.WriteLine("Direccion: "+info.Direccion);
        Console.WriteLine("Telefono: "+info.Telefono);
        Console.WriteLine("Referencia: "+info.DatosReferenciaDireccion);


    }
}

class Cadete
{
    int id;
    string nombre;
    string direccion;
    int telefono;
    List<Pedidos> listadoPedidos;

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public int Telefono { get => telefono; set => telefono = value; }
    public List<Pedidos> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

    public Cadete(int id, string nombre, string direccion, int telefono)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }

    public int JornalACobrar() {
            int cantPedidosEntregados = 0;
            const int precioPorEnvio = 500;
            foreach (Pedidos pedido in ListadoPedidos)
            {
                if (pedido.Estado == "Aceptado")
                {
                    cantPedidosEntregados++;
                }
            }
            return cantPedidosEntregados*precioPorEnvio;
         }
  
   public void AceptarPedido(Pedidos nuevoPedido)
        {
            
            nuevoPedido.Estado = "Aceptado";
            ListadoPedidos.Add(nuevoPedido);
            Console.WriteLine("El cadete ha recibido el pedido y esta en camino");
        }

    public int PedidosEntregados(){
            int cantPedidosRealizados = 0;
            foreach (Pedidos pedido in ListadoPedidos)
            {
                if (pedido.Estado == "Aceptado")
                {
                    cantPedidosRealizados += 1;
                }
            }
            return cantPedidosRealizados;
        }
        
}

class Cadeteria
{
    string nombre;
    int telefono;
    List<Cadete> listadoCadetes =  new List<Cadete>();
    int nroPedidosCreados;

    public string Nombre { get => nombre; set => nombre = value; }
    public int Telefono { get => telefono; set => telefono = value; }
    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
    public int NroPedidosCreados { get => nroPedidosCreados; set => nroPedidosCreados = value; }

     public Cadeteria(string archivoInfoCadeteria, string archivoCadetes)
        {
            CargarInfoCadeteria(archivoInfoCadeteria);
            CargarCadetes(archivoCadetes);
        }

    private void CargarInfoCadeteria(string archivoInfoCadeteria)
    {
        try
        {
            using (StreamReader reader = new StreamReader(archivoInfoCadeteria))
            {
                string[] datos = reader.ReadLine().Split(',');
                this.nombre = datos[0];
                this.telefono = int.Parse(datos[1]);
                NroPedidosCreados = int.Parse(datos[2]); // Carga el valor de nroPedidosCreados

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al cargar la información de la cadetería: " + ex.Message);
        }
    }
     public void CargarCadetes(string archivoCadetes)
        {
            try
            {
                using (StreamReader reader = new StreamReader(archivoCadetes))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] datosCadete = line.Split(',');
                        int id = int.Parse(datosCadete[0]);
                        string nombre = datosCadete[1];
                        string direccion = datosCadete[2];
                        int telefono = int.Parse(datosCadete[3]);

                        Cadete cadete = new Cadete(id, nombre, direccion, telefono);
                        ListadoCadetes.Add(cadete);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar la lista de cadetes: " + ex.Message);
            }
        }


       public void AsignarPedido()
        {
            if (ListadoCadetes.Count > 0)
            {
                Random random = new Random();
                int indiceAleatorio = random.Next(0, ListadoCadetes.Count);
                Cadete cadeteAleatorio = ListadoCadetes[indiceAleatorio]; // Elijo un cadete de manera aleatoria

                Pedidos nuevoPedido = new Pedidos(NroPedidosCreados + 1); // Crea una instancia de Pedido ; NOTA: necesito AGREGAR OBS
                NroPedidosCreados += 1; // Incremento la cantidad de pedidos creados

                cadeteAleatorio.AceptarPedido(nuevoPedido); // Agrega el pedido a la lista de pedidos del cadete Elegido

                Console.WriteLine("Pedido nro "+nuevoPedido.Nro+" asignado al cadete: " + cadeteAleatorio.Nombre);
            }
            else
            {
                Console.WriteLine("No hay cadetes disponibles para asignar el pedido.");
            }
        }
     public void ReasignarPedido(int idPedido, int nuevoIdCadete) // Asignar a un cadete en particular o random?
        {
            Cadete nuevoCadete = ListadoCadetes.FirstOrDefault(cadete => cadete.Id == nuevoIdCadete); // DEBO SOLUCIONAR QUE PUEDA TOMAR VALORES NULL?

            if (nuevoCadete != null)
            {
                foreach (Cadete cadete in listadoCadetes)
                {
                    Pedidos pedidoAReasignar = cadete.ListadoPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);

                    if (pedidoAReasignar != null)
                    {
                        cadete.ListadoPedidos.Remove(pedidoAReasignar);
                        nuevoCadete.ListadoPedidos.Add(pedidoAReasignar);
                        Console.WriteLine("Pedido reasignado al cadete: " + nuevoCadete.Nombre);
                        return; // Salimos del ciclo una vez encontrado y reasignado el pedido
                    }
                }
                Console.WriteLine("Pedido no encontrado en la lista de pedidos de ningún cadete.");
            }
            else
            {
                Console.WriteLine("Cadete no encontrado.");
            }
        }

        public void CambiarEstado() // Este metodo recibe por parametro la id del pedido a entregar, busca que cadete lo posee y lo cambia de estado
        {

            Console.WriteLine("Ingrese el ID del pedido a cambiar de estado: ");
            if (int.TryParse(Console.ReadLine(), out int idPedido))
            {
                Console.WriteLine("Seleccione el estado al que cambiar:");
                Console.WriteLine("a) Pendiente");
                Console.WriteLine("b) Aceptado");
                Console.WriteLine("c) Rechazado");

                Console.Write("Opción: ");
                string opcionEstado = Console.ReadLine();

                string nuevoEstado = "";

                switch (opcionEstado.ToLower())
                {
                    case "a":
                        nuevoEstado = "Pendiente";
                        break;
                    case "b":
                        nuevoEstado = "Aceptado";
                        break;
                    case "c":
                        nuevoEstado = "Rechazado";
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        return;
                }

                foreach (Cadete cadete in ListadoCadetes)
                {
                    for (int i = 0; i < cadete.ListadoPedidos.Count; i++)
                    {
                        if (idPedido == cadete.ListadoPedidos[i].Nro)
                        {
                            cadete.ListadoPedidos[i].Estado = nuevoEstado;
                            Console.WriteLine("El pedido nro " + idPedido + " cambio de estado a : " + nuevoEstado);
                            return;
                        }
                    }
                }
                
            }
            else
            {
                Console.WriteLine("Ingrese un ID válido.");
            }
        }
        public void AltaPedido(int idPedido){ // Esta funcion da de alta un pedio por una id recibida
            foreach (Cadete cadete in ListadoCadetes)
            {
                var pedidoAlta = cadete.ListadoPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);
                if (pedidoAlta != null)
                {
                    cadete.ListadoPedidos.Remove(pedidoAlta);
                    Console.WriteLine("El pedido " + idPedido + " ha sido dado de alta correctamente");
                    return;
                }
            }
            Console.WriteLine("No se encontro el pedido " + idPedido + ".");
        }

}

class Informe
    {
        private int montoTotalGanado;
        private int totalEnvios;
        private double promedioEnviosXCadete;
        private Cadeteria cadeteria ;
        public int MontoTotalGanado { get => montoTotalGanado; set => montoTotalGanado = value; }
        public int TotalEnvios { get => totalEnvios; set => totalEnvios = value; }
        public double PromedioEnviosXCadete { get => promedioEnviosXCadete; set => promedioEnviosXCadete = value; }
        public Cadeteria Cadeteria { get => cadeteria; set => cadeteria = value; }

        public Informe(Cadeteria cadeteria)
        {
            Cadeteria = cadeteria;
            foreach (Cadete cadete in Cadeteria.ListadoCadetes)
            {
                foreach (Pedidos pedido in cadete.ListadoPedidos)
                {
                    if (pedido.Estado == "Entregado")
                    {
                        TotalEnvios++;
                    }
                }
            }
            MontoTotalGanado = TotalEnvios*500;
            if (Cadeteria.ListadoCadetes.Count != 0)
            {
                PromedioEnviosXCadete = TotalEnvios/Cadeteria.ListadoCadetes.Count;
            }
        }

        

        public void MostrarInforme(){
            Console.WriteLine("--------Informe de la Cadeteria "+ Cadeteria.Nombre + "--------");
            Console.WriteLine("Cantidad de Envios REALIZADOS: "+ TotalEnvios);
            Console.WriteLine("Promedio de Envios por cadete: "+PromedioEnviosXCadete);
            Console.WriteLine("Monto total generado: "+ MontoTotalGanado);
            Console.WriteLine("");
            Console.WriteLine("*INFORMACION POR CLIENTES");
            
            foreach (Cadete cadete in Cadeteria.ListadoCadetes)
            {
                Console.WriteLine("");
                Console.WriteLine("[Cadete "+cadete.Id+"]");
                Console.WriteLine("Cantidad de Pedidos Entregados: "+ cadete.ListadoPedidos.FirstOrDefault(pedido => pedido.Estado == "Entregado"));
                Console.Write("Nros de Pedidos EnCamino:");
                foreach (Pedidos pedido in cadete.ListadoPedidos)
                {
                    if (pedido.Estado == "EnCamino")
                    {
                        Console.Write(pedido.Nro+" | ");
                    }
                }
                Console.WriteLine("");
                Console.Write("Nros de Pedidos Entregados:");
                foreach (Pedidos pedido in cadete.ListadoPedidos)
                {
                    if (pedido.Estado == "Entregado")
                    {
                        Console.Write(pedido.Nro+" | ");
                    }
                }
            }
        }
    }