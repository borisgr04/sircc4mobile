using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace BLL.Filtros
{
    public interface FiltrosContratos
    {
        IList<vContratosInt> GetContratos(vContratosIntFiltro cFil);
    }
    public class vContratosInt
    {
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public string Objeto { get; set; }
        public string Ide_Contratista { get; set; }
        public string Contratista { get; set; }
        public decimal Valor_Contrato { get; set; }
        public DateTime Fecha_Suscripcion { get; set; }
        public string Estado { get; set; }
        public string DependenciaNec { get; set; }
        public string DependenciaDel { get; set; }
        public string Ide_Interventor { get; set; }
        public string Nom_Interventor { get; set; }
        public short Vigencia { get; set; }
        public string Dep_Nec { get; set; }
        public string Dep_Del { get; set; }
        public string Cod_Tip { get; set; }
        public string Cod_STip { get; set; }
    }
    public class vContratosIntFiltro : vContratosInt
    {
        public bool FilxFS { get; set; }
        public DateTime FS_Inicial { get; set; }
        public DateTime FS_Final { get; set; }
    }
}
