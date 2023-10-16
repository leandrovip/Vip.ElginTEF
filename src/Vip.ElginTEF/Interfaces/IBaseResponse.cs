namespace Vip.ElginTEF.Interfaces;

public interface IBaseResponse
{
    int Codigo { get; }
    string DescricaoRetorno { get; }
    string Mensagem { get; }
    bool Retorno { get; }
}