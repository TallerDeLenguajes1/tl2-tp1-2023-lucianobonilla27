namespace EspacioClase;

class Pedidos
{
    int nro;
    string obs;
    Cliente cliente;
    string estado;

    public int Nro { get => nro; set => nro = value; }
    public string Obs { get => obs; set => obs = value; }
    public string Estado { get => estado; set => estado = value; }
    internal Cliente Cliente { get => cliente; set => cliente = value; }


}


class Cliente
{
    string nombre;
    string direccion;
    int telefono;
    string datosReferenciaDireccion;
}

class Cadete
{
    int id;
    string nombre;
    string direccion;
    int telefono;
    List<Pedidos> listadoPedidos;

}

class Cadeteria
{
    string nombre;
    int telefono;
    List<Cadete> listadoCadetes;
}