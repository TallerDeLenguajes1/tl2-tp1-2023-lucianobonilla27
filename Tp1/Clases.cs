namespace EspacioClase;
using System.Text.Json;
using System.Text.Json.Serialization;





public class Cliente
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
public class Pedidos
    {
        private int nro;
        private string obs;
        private Cliente infoCliente;
        private string estado; 
        private Cadete cadeteAsignado;

        public string Obs { get => obs; set => obs = value; }
        public Cliente InfoCliente { get => infoCliente;}
        public string Estado { get => estado; set => estado = value; }
        public int Nro { get => nro; }
        public Cadete CadeteAsignado { get => cadeteAsignado; set => cadeteAsignado = value; }

    public Pedidos(int nro)
        {
            this.nro = nro;
            this.obs = null;
            this.infoCliente = CrearClienteAleatorio(); // Crear cliente aleatorio
            Estado = "EnCamino";
            
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

public class Cadete
    {
        private int id;
        private string nombre;
        private string direccion;
        private int telefono;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public Cadete(int id, string nombre, string direccion, int telefono)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }
    
    }

public class Cadeteria
    {
        private string nombre;
        private int telefono;
        private List<Cadete> listaCadetes = new List<Cadete>();
        private int nroPedidosCreados;
        private List<Pedidos> listaPedidos = new List<Pedidos>();

        public List<Cadete> ListaCadetes { get => listaCadetes; set => listaCadetes = value; }
        public int NroPedidosCreados { get => nroPedidosCreados; set => nroPedidosCreados = value; }
    
        public List<Pedidos> ListaPedidos { get => listaPedidos; set => listaPedidos = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Telefono { get => telefono; set => telefono = value; }

    public Cadeteria(string nombre,int telefono, int nroPedidosCreados)
            {
                this.Nombre = nombre;
                this.Telefono = telefono;
                this.nroPedidosCreados = nroPedidosCreados;
            }

        
        

        public void AsignarCadeteAPedido()
        {
            if (ListaCadetes.Count > 0)
            {
                Random random = new Random();
                int indiceAleatorio = random.Next(0, ListaCadetes.Count);
                Cadete cadeteAleatorio = ListaCadetes[indiceAleatorio]; // Elijo un cadete de manera aleatoria

                Pedidos nuevoPedido = new Pedidos(NroPedidosCreados + 1); // Crea una instancia de Pedido ; NOTA: necesito AGREGAR OBS
                NroPedidosCreados += 1; // Incremento la cantidad de pedidos creados

                nuevoPedido.CadeteAsignado = cadeteAleatorio; // al nuevo pedido le asigno un cadete
                listaPedidos.Add(nuevoPedido);

                Console.WriteLine("Pedido nro "+nuevoPedido.Nro+" asignado al cadete: " + cadeteAleatorio.Nombre);
            }
            else
            {
                Console.WriteLine("No hay cadetes disponibles para asignar el pedido.");
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

                foreach (Pedidos pedido in ListaPedidos)
                {
                    
                    if (idPedido == pedido.Nro)
                    {
                        pedido.Estado = nuevoEstado;
                        Console.WriteLine("El pedido nro " + idPedido + " cambio de estado a : " + nuevoEstado);
                        return;
                    }
                    
                }
                
            }
            else
            {
                Console.WriteLine("Ingrese un ID válido.");
            }
        }
        public void AltaPedido(int idPedido){ // Esta funcion da de alta un pedio por una id recibida
            foreach (Pedidos pedido in listaPedidos)
            {
                var pedidoAlta = ListaPedidos.FirstOrDefault(pedido => pedido.Nro == idPedido);
                if (pedidoAlta != null)
                {
                    ListaPedidos.Remove(pedidoAlta);
                    Console.WriteLine("El pedido " + idPedido + " ha sido dado de alta correctamente");
                    return;
                }
            }
            Console.WriteLine("No se encontro el pedido " + idPedido + ".");
        }

        public void AgregarPedido(Pedidos nuevoPedido)
        {
            
            nuevoPedido.Estado = "EnCamino";
            ListaPedidos.Add(nuevoPedido);
            Console.WriteLine("El cadete ha recibido el pedido y esta " + nuevoPedido.Estado + ".");
        }
         public int JornalACobrar(int id) {
            int cantPedidosEntregados = 0;
            const int precioPorEnvio = 500;
            foreach (Pedidos pedido in ListaPedidos)
            {
                if (pedido.Estado == "Entregado" && pedido.CadeteAsignado.Id == id)
                {
                    cantPedidosEntregados++;
                }
            }
            return cantPedidosEntregados*precioPorEnvio;
         }
         public int PedidosEntregados(Cadete c){
            int cantPedidosRealizados = 0;
            foreach (Pedidos pedido in ListaPedidos)
            {
                if (pedido.CadeteAsignado.Id == c.Id){    
                    if (pedido.Estado == "Entregado")
                    {
                        cantPedidosRealizados += 1;
                    }
                }    
            }
            return cantPedidosRealizados;
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
            foreach (Pedidos p in Cadeteria.ListaPedidos)
            {
                if (p.Estado == "Entregado")
                {
                    TotalEnvios++;
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
           
        }
    }

// Clase base AccesoADatos
public abstract class AccesoADatos
{
    public abstract Cadeteria CargarDatos(string archivoInfoCadeteria, string archivoCadetes);
    
}

// Clase derivada AccesoCSV
public class AccesoCSV : AccesoADatos
{
    public override Cadeteria CargarDatos(string archivoInfoCadeteria, string archivoCadetes)
    {
    Cadeteria cadeteria = null; // Declarar cadeteria fuera de los bloques try

    try // Cargar datos de la cadeteria
    {
        using (StreamReader reader = new StreamReader(archivoInfoCadeteria))
        {
            string[] datos = reader.ReadLine().Split(',');
            string nombre = datos[0];
            int telefono = int.Parse(datos[1]);
            int nroPedidosCreados = int.Parse(datos[2]);
            cadeteria = new Cadeteria(nombre, telefono, nroPedidosCreados);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error al cargar la información de la cadetería: " + ex.Message);
    }

    try // Cargar datos de los cadetes
    {
        if (cadeteria != null) // Asegurarse de que cadeteria no sea nula
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
                    cadeteria.ListaCadetes.Add(cadete);
                }
            }
        }
        else
        {
            Console.WriteLine("Error: La cadetería no se ha cargado correctamente.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error al cargar la lista de cadetes: " + ex.Message);
    }
    return cadeteria;
    }

    
}


public class AccesoJson : AccesoADatos
{
    

    public override Cadeteria CargarDatos(string archivoInfoCadeteria, string archivoCadetes)
    {
        Cadeteria cadeteria = null;

        try
        {
            string jsonInfoCadeteria = File.ReadAllText(archivoInfoCadeteria);
            cadeteria = JsonSerializer.Deserialize<Cadeteria>(jsonInfoCadeteria);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al cargar la información de la cadetería desde JSON: " + ex.Message);
        }

        try
        {
            if (cadeteria != null)
            {
                string jsonCadetes = File.ReadAllText(archivoCadetes);
                var cadetes = JsonSerializer.Deserialize<Cadete[]>(jsonCadetes);
                cadeteria.ListaCadetes.AddRange(cadetes);
            }
            else
            {
                Console.WriteLine("Error: La cadetería no se ha cargado correctamente desde JSON.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al cargar la lista de cadetes desde JSON: " + ex.Message);
        }

        return cadeteria;
    }
}

