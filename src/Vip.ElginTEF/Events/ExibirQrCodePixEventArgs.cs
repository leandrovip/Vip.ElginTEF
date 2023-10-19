using System;
using System.Text;
using Newtonsoft.Json.Linq;
using Vip.ElginTEF.Extensions;

namespace Vip.ElginTEF.Events;

public class ExibirQrCodePixEventArgs : EventArgs
{
    #region Propriedades

    public string Identificador { get; set; }
    public byte[] QrCode { get; set; }

    #endregion

    #region Construtor

    public ExibirQrCodePixEventArgs(string identificador, byte[] qrCode)
    {
        Identificador = identificador.TrimVip();
        QrCode = qrCode;
    }

    #endregion

    #region Métodos Estáticos

    public static ExibirQrCodePixEventArgs Map(string resultado)
    {
        var identificador = "";
        var qrCode = "";

        var retorno = resultado.Split(';');
        if (retorno.Length > 0)
            qrCode = retorno[1];

        if (retorno.Length > 1)
        {
            var base64bytes = Convert.FromBase64String(retorno[2]);
            var decode = Encoding.UTF8.GetString(base64bytes);

            var objeto = JObject.Parse(decode);
            if (objeto.IsNotNull())
                identificador = objeto["id"]?.ToString();
        }

        return new ExibirQrCodePixEventArgs(identificador, StringToByteArray(qrCode));
    }

    private static byte[] StringToByteArray(string hex)
    {
        var numberChars = hex.Length;
        var bytes = new byte[numberChars / 2];
        for (var i = 0; i < numberChars; i += 2)
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

        return bytes;
    }

    #endregion
}