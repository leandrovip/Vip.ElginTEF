using Vip.ElginTEF.Interfaces;

namespace Vip.ElginTEF.Response;

public class BaseResponse : IBaseResponse
{
    #region Propriedades

    public int Codigo { get; private set; }
    public string Mensagem { get; private set; }
    public string DescricaoRetorno => ObterDescricaoRetorno();
    public bool Retorno => ObterResultadoRetorno();

    #endregion

    #region Métodos Publicos

    public void SetarErro(string mensagemResultado)
    {
        Codigo = 9;
        Mensagem = mensagemResultado;
    }

    public void SetarErro(int? codigo, string mensagem)
    {
        Codigo = codigo ?? 9;
        Mensagem = mensagem ?? "Não foi possível executar a operação";
    }

    #endregion

    #region Métodos Privados

    private string ObterDescricaoRetorno()
    {
        return Codigo switch
        {
            0 => "Sucesso, confirmação necessária",
            1 => "Sucesso",
            2 => "Sequencial inválido",
            3 => "Transação cancelada pelo operador",
            4 => "Transação cancelada pelo cliente",
            5 => "Parâmetros insuficientes ou inválidos",
            6 => "Problemas na conexão do ElginTef",
            7 => "Problemas entre o ElginTef e a Rede",
            8 => "Tempo limite de espera excedido",
            9 => "Problema desconhecido",
            _ => "Problema desconhecido"
        };
    }

    private bool ObterResultadoRetorno()
    {
        return Codigo.Equals(0) || Codigo.Equals(1);
    }

    #endregion
}

public class BaseResponse<T> : BaseResponse
{
    #region Propriedades

    public T Tef { get; set; }

    #endregion
}