using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class DetalleMuestraSismica
    {
        private float valor;
        private TipoDeDato tipoDato;

        public float Valor { get => valor; set => valor = value; }
        public TipoDeDato TipoDato { get => tipoDato; set => tipoDato = value; }

        public DetalleMuestraSismica(float valor, TipoDeDato tipoDato)
        {
            this.valor = valor;
            this.tipoDato = tipoDato;
        }

        public DetalleMuestraSismica()
        {
        }

        public object getDatos()
        {
            return new
            {
                valor = this.Valor,
                tipoDato = this.TipoDato?.getDenominacion(),
                unidadMedida = this.TipoDato?.getUnidadMedida()
            };
        }
    }
}
