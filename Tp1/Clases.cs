namespace EspacioClase;



class Cliente
    {
        private string nombre;
        private string direccion;
        private int telefono;
        private string datosReferenciaDireccion;
        
        public string Nombre { get => nombre;}
        public string Direccion { get => direccion;}
        public int Telefono { get => telefono;}
        public string DatosReferenciaDireccion { get => datosReferenciaDireccion;}

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
        private int nro;
        private string obs;
        private Cliente infoCliente;
        private string estado; // Rec

        public string Obs { get => obs; set => obs = value; }
        public Cliente InfoCliente { get => infoCliente;}
        public string Estado { get => estado; set => estado = value; }
        public int Nro { get => nro; }

        public Pedidos(int nro)
        {
            this.nro = nro;
            this.obs = null;
            this.infoCliente = CrearClienteAleatorio(); // Crear cliente aleatorio
            Estado = "EnPreparacion";
            
        }

        private Cliente CrearClienteAleatorio()
        {
            Random random = new Random();
            string nombre = "Cliente" + random.Next(1, 100);
            string direccion = "Dirección" + random.Next(1, 100);
            int telefono = random.Next(100000000, 999999999);
            string datosReferenciaDireccion = "Referencia" + random.Next(1, 100);

            return new Cliente(nombre, direccion, telefono, datosReferenciaDireccion);
        }
        public void VerDireccionCliente()
        {
            Console.WriteLine("La direccion del cliente "+ infoCliente.Nombre + "es: "+ infoCliente.Direccion);
        }

        public void VerDatosCliente()
        {
            Console.WriteLine("----Info del Cliente---");
            Console.WriteLine("Nombre: "+ infoCliente.Nombre);
            Console.WriteLine("Telefono: "+ infoCliente.Telefono);
            Console.WriteLine("Direccion: "+ infoCliente.Direccion);
        }

    }

class Cadete
    {
        private int id;
        private string nombre;
        private string direccion;
        private int telefono;
        private List<Pedidos> listaPedidos = new List<Pedidos>();

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public List<Pedidos> ListaPedidos { get => listaPedidos; set => listaPedidos = value; }
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
            foreach (Pedidos pedido in ListaPedidos)
            {
                if (pedido.Estado == "Entregado")
                {
                    cantPedidosEntregados++;
                }
            }
            return cantPedidosEntregados*precioPorEnvio;
         }
        public void AgregarPedido(Pedidos nuevoPedido)
        {
            
            nuevoPedido.Estado = "EnCamino";
            ListaPedidos.Add(nuevoPedido);
            Console.WriteLine("El cadete ha recibido el pedido y esta " + nuevoPedido.Estado + ".");
        }
        public int PedidosEntregados(){
            int cantPedidosRealizados = 0;
            foreach (Pedidos pedido in ListaPedidos)
            {
                if (pedido.Estado == "Entregado")
                {
                    cantPedidosRealizados += 1;
                }
            }
            return cantPedidosRealizados;
        }
        

    }

class Cadeteria
    {
        private string nombre;
        private int telefono;
        private List<Cadete> listaCadetes = new List<Cadete>();
        private int nroPedidosCreados;

        public List<Cadete> ListaCadetes { get => listaCadetes; set => listaCadetes = value; }
        public int NroPedidosCreados { get => nroPedidosCreados; set => nroPedidosCreados = value; }
        public string Nombre { get => nombre; }
        public int Telefono { get => telefono; }

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
                        ListaCadetes.Add(cadete);
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
            if (ListaCadetes.Count > 0)
            {
                Random random = new Random();
                int indiceAleatorio = random.Next(0, ListaCadetes.Count);
                Cadete cadeteAleatorio = ListaCadetes[indiceAleatorio]; // Elijo un cadete de manera aleatoria

                Pedidos nuevoPedido = new Pedidos(NroPedidosCreados + 1); // Crea una instancia de Pedido ; NOTA: necesito AGREGAR OBS
                NroPedidosCreados += 1; // Incremento la cantidad de pedidos creados

                cadeteAleatorio.AgregarPedido(nuevoPedido); // Agrega el pedido a la lista de pedidos del cadete Elegido

                Console.WriteLine("Pedido nro "+nuevoPedido.Nro+" asignado al cadete: " + cadeteAleatorio.Nombre);
            }
            else
            {
                Console.WriteLine("No hay cadetes disponibles para asignar el pedido.");
            }
        }


        public void ReasignarPedido(int idPedido, int nuevoIdCadete) // Asignar a un cadete en particular o random?
        {
            Cadete nuevoCadete = ListaCadetes.FirstOrDefault(cadete => cadete.Id == nuevoIdCadete); // DEBO SOLUCIONAR QUE PUEDA TOMAR VALORES NULL?

            if (nuevoCadete != null)
            {
                foreach (Cadete cadete in listaCadetes)
                {
                    Pedidos pedidoAReasignar = cadete.ListaPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);

                    if (pedidoAReasignar != null)
                    {
                        cadete.ListaPedidos.Remove(pedidoAReasignar);
                        nuevoCadete.ListaPedidos.Add(pedidoAReasignar);
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

                foreach (Cadete cadete in ListaCadetes)
                {
                    for (int i = 0; i < cadete.ListaPedidos.Count; i++)
                    {
                        if (idPedido == cadete.ListaPedidos[i].Nro)
                        {
                            cadete.ListaPedidos[i].Estado = nuevoEstado;
                            Console.WriteLine("El pedido nro " + idPedido + " cambio de estado a : " + nuevoEstado);
                            return;
                        }
                    }
                }
                // Aquí puedes llamar al método en la Cadeteria para cambiar el estado del pedido con "idPedido" al "nuevoEstado"
            }
            else
            {
                Console.WriteLine("Ingrese un ID válido.");
            }
        }
        public void AltaPedido(int idPedido){ // Esta funcion da de alta un pedio por una id recibida
            foreach (Cadete cadete in ListaCadetes)
            {
                var pedidoAlta = cadete.ListaPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);
                if (pedidoAlta != null)
                {
                    cadete.ListaPedidos.Remove(pedidoAlta);
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
            foreach (Cadete cadete in Cadeteria.ListaCadetes)
            {
                foreach (Pedidos pedido in cadete.ListaPedidos)
                {
                    if (pedido.Estado == "Entregado")
                    {
                        TotalEnvios++;
                    }
                }
            }
            MontoTotalGanado = TotalEnvios*500;
            if (Cadeteria.ListaCadetes.Count != 0)
            {
                PromedioEnviosXCadete = TotalEnvios/Cadeteria.ListaCadetes.Count;
            }
        }

        

        public void MostrarInforme(){
            Console.WriteLine("--------Informe de la Cadeteria "+ Cadeteria.Nombre + "--------");
            Console.WriteLine("Cantidad de Envios REALIZADOS: "+ TotalEnvios);
            Console.WriteLine("Promedio de Envios por cadete: "+PromedioEnviosXCadete);
            Console.WriteLine("Monto total generado: "+ MontoTotalGanado);
            Console.WriteLine("");
            Console.WriteLine("*INFORMACION POR CLIENTES");
            
            foreach (Cadete cadete in Cadeteria.ListaCadetes)
            {
                Console.WriteLine("");
                Console.WriteLine("[Cadete "+cadete.Id+"]");
                int cont = 0;
                foreach (var pedido in cadete.ListaPedidos)
                {
                    if (pedido.Estado == "Entregado")
                    {
                        cont++;
                    }
                }
                Console.WriteLine("Cantidad de Pedidos Entregados: "+cont);
                Console.Write("Nros de Pedidos EnCamino:");
                foreach (Pedidos pedido in cadete.ListaPedidos)
                {
                    if (pedido.Estado == "EnCamino")
                    {
                        Console.Write(pedido.Nro+" | ");
                    }
                }
                Console.WriteLine("");
                Console.Write("Nros de Pedidos Entregados:");
                foreach (Pedidos pedido in cadete.ListaPedidos)
                {
                    if (pedido.Estado == "Entregado")
                    {
                        Console.Write(pedido.Nro+" | ");
                    }
                }
            }
        }
    }