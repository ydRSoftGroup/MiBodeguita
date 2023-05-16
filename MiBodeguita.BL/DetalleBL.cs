﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiBodeguita.Model;
namespace MiBodeguita.BL
{
    public class DetalleBL
    {
        string PathArch;

        public DetalleBL(string PathArch) {
            this.PathArch = PathArch;
        }

        public RespuestaModel Agregar(DetalleModel objModel)
        {
            try {
                var respuesta = Help.Funciones.GuardarArchivo(PathArch, ObjetoLinea(objModel), true);

                if (!respuesta)
                {
                    return new RespuestaModel(objModel.ID_Ref, "No Guardado" + objModel.NProducto, true);
                }

                return new RespuestaModel(objModel.ID_Ref, "Guardado ", false);
            } catch {
                return new RespuestaModel();
            }
        }

        private string ObjetoLinea(DetalleModel objModel) {
            return objModel.ID_Ref + "," + objModel.NProducto + "," + objModel.Precio + "," +
                        objModel.Cantidad + "," + objModel.Total;
        }

        public List<DetalleModel> Mostrar(int ID_Ref)
        {
            try {
                List<DetalleModel> mLista = new List<DetalleModel>();

                if (File.Exists(PathArch)) {
                    StreamReader Arch = new StreamReader(PathArch);
                    string Linea = Arch.ReadLine();
                    while (Linea != null) {
                        string[] Arreglo;
                        Arreglo = Linea.Split(',');
                        DetalleModel temp = new DetalleModel();
                        temp.ID_Ref =  Convert.ToInt32(Arreglo[0]);
                        if(temp.ID_Ref == ID_Ref)
                        {
                            temp.NProducto = Arreglo[1];
                            temp.Precio = Convert.ToDecimal(Arreglo[2]);
                            temp.Cantidad = Convert.ToDecimal(Arreglo[3]);
                            temp.Total = Convert.ToDecimal(Arreglo[4]);
                            mLista.Add(temp);
                        }
                        Linea = Arch.ReadLine();
                    }
                    Arch.Close();
                }
                return mLista;
            } catch {
                return new List<DetalleModel>();
            }

        }
    }
}