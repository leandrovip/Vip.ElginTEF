using System;

namespace Vip.ElginTEF.Events;

public class ExibirQrCodePixEventArgs : EventArgs
{
    #region Propriedades

    public byte[] QrCode { get; set; }

    #endregion

    #region Construtor

    public ExibirQrCodePixEventArgs(byte[] qrCode)
    {
        QrCode = qrCode;
    }

    #endregion

    #region Métodos Estáticos

    public static ExibirQrCodePixEventArgs Map(string resultado)
    {
        var qrCode = "";
        var retorno = resultado.Split(';');
        if (retorno.Length > 0)
            qrCode = retorno[1];

        return new ExibirQrCodePixEventArgs(StringToByteArray(qrCode));
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