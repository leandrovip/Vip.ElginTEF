using System;
using System.Text;
using Vip.ElginTEF.Enums;
using Vip.ElginTEF.Interfaces;
using Vip.ElginTEF.Models;

namespace Vip.ElginTEF.Services
{
    public static class Manager
    {
        public static ILibrary GetLibrary(ModeloLib modelo, Configuracao configuracao, string caminhoLib, Encoding encoding)
        {
            switch (modelo)
            {
                case ModeloLib.Cdecl:   return new TefCdecl(configuracao, caminhoLib, encoding);
                case ModeloLib.StdCall: return new TefStdCall(configuracao, caminhoLib, encoding);
                default:                throw new NotImplementedException("Modelo não implementado");
            }
        }
    }
}