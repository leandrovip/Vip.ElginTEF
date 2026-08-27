<h2 align="center"><strong>Vip.ElginTEF</strong> 💻</h2>

<p align="center">
  <a href="https://raw.githubusercontent.com/leandrovip/Vip.ElginTEF/master/LICENSE">
    <img src="https://img.shields.io/github/license/leandrovip/Vip.ElginTEF" />
  </a>
  <a href="https://www.nuget.org/packages/Vip.ElginTEF/">
    <img alt="Nuget" src="https://img.shields.io/nuget/dt/Vip.ElginTEF?label=NuGet%20downloads&style=flat-square">
  </a>
  <a href="https://www.nuget.org/packages/Vip.ElginTEF/">
     <img alt="NuGet" src="https://img.shields.io/nuget/v/Vip.ElginTEF.svg">
  </a>
</p>

Biblioteca .NET Standard 2.0 para controle e fluxo de operações **TEF Elgin / TEF HUB** no modo DLL (`E1_Tef01.dll`). Abstrai P/Invoke (`StdCall`/`Cdecl`), serialização JSON, loop de coleta (`ChamarFluxoPagamento`) e confirmação/finalização da transação.

## Funcionalidades

- `RealizarPagamento` — crédito, débito, voucher, frota, private label (à vista e parcelado)
- `RealizarPagamentoPIX` — com evento `OnExibirQrCodePix` e timeout/cancelamento
- `RealizarAdm` — cancelamento, pendência, reimpressão, extrato (via `TipoOperacaoAdm`)
- `CancelarOperacaoTEF()` — cancela transação em andamento (principalmente PIX)
- `ConfigurarDados()` / `ObterProdutoTEF()` / `ObterConfiguracaoTCP()`
- Eventos para UI: `OnMensagemUsuario`, `OnMensagemRetorno`, `OnReceberInformacao`, `OnExibirQrCodePix`

## Pré-requisitos

- .NET Standard 2.0 (consumível de `net48`, `net6+`, `net8+`)
- Windows (DLL nativa `E1_Tef01.dll` + dependências Qt5/`lib_ppelgin` fornecidas pela Elgin)
- DLL no caminho indicado em `TefService.CaminhoLib` (padrão `.\E1_Tef01.dll`)

## Instalação

```powershell
dotnet add package Vip.ElginTEF
# ou via Package Manager
Install-Package Vip.ElginTEF
```

## Configuração e Ativação

Todas as propriedades de `Configuracao` devem ser definidas **antes** de `Ativar()`. Alterar `ModeloLib` ou `CaminhoLib` com `Ativo == true` lança `VipException`.

| Propriedade | Padrão | Descrição |
|---|---|---|
| `TextoPinpad` | `VipERP PDV` | Texto exibido no PINPad |
| `VersaoAC` | `V1.0.0` | Versão da automação comercial |
| `NomeEstabelecimento` | `Elgin` | Nome do estabelecimento |
| `Loja` | `01` | Código da loja |
| `IdentificadorPontoCaptura` | `T0004` | Identificador do ponto de captura |
| `IpClientTCP` | `127.0.0.1` | IP do Client TCP (ElginTef) |
| `PortaClientTCP` | `60906` | Porta do Client TCP |
| `ModeloLib` | `StdCall` | `StdCall` (padrão) ou `Cdecl` |
| `CaminhoLib` | `.\E1_Tef01.dll` | Caminho da DLL nativa |
| `Timeout` | `240` | Timeout em segundos (usado no PIX) |

```csharp
using Vip.ElginTEF;
using Vip.ElginTEF.Enums;

var tef = new TefService();

tef.Configuracao.TextoPinpad = "Meu PDV";
tef.Configuracao.VersaoAC = "1.1.600";
tef.Configuracao.NomeEstabelecimento = "VIP";
tef.Configuracao.Loja = "001";
tef.Configuracao.IdentificadorPontoCaptura = "T0004";
tef.Configuracao.IpClientTCP = "127.0.0.1";
tef.Configuracao.PortaClientTCP = 60906;

tef.ModeloLib = ModeloLib.StdCall;
tef.CaminhoLib = @".\E1_Tef01.dll";
tef.Timeout = 240; // segundos, relevante para PIX

tef.Ativar();                          // valida File.Exists e carrega a DLL
var cfg = tef.ConfigurarDados();       // ConfigurarDadosPDV + SetClientTCP
if (!cfg.Retorno)
    throw new Exception(cfg.Mensagem);

// quando encerrar o PDV
tef.Desativar();
tef.Dispose();
```

## Eventos

Assine **antes** de chamar `RealizarPagamento` / `RealizarPagamentoPIX` / `RealizarAdm`:

```csharp
tef.OnMensagemUsuario += (s, e) => Console.WriteLine(e.Mensagem);
tef.OnMensagemRetorno += (s, e) => Console.WriteLine($"[{e.Codigo}] {e.Mensagem} - {e.Codigo}: {Descricao(e.Codigo)}");
tef.AguardandoComandoChanged += (s, e) => Console.WriteLine(tef.AguardandoComando ? "Aguardando..." : "Livre");

tef.OnReceberInformacao += (s, e) =>
{
    // e.TipoFluxo, e.MensagemUsuario, e.Opcoes, e.TipoInformacao (Geral/Alfabetico/Numerico/DataHora/Alfanumerico)
    // e.SeEsperaOpcoes indica se deve exibir seleção (ComboBox) ou entrada livre (TextBox)
    string coleta;
    if (e.SeEsperaOpcoes)
        coleta = ExibirSelecao(e.Opcoes, e.MensagemUsuario); // seu dialog
    else
        coleta = SolicitarDigitacao(e.TipoInformacao, e.MensagemUsuario);

    tef.InformacaoColeta = coleta; // fluxo interno consome na próxima iteração
};

tef.OnExibirQrCodePix += (s, e) =>
{
    // e.QrCode é byte[] (PNG) decodificado do hex retornado pela DLL
    File.WriteAllBytes("qrcode.png", e.QrCode);
    ExibirNoPictureBox(e.QrCode);
};
```

> **Nota WinForms/WPF:** os eventos são disparados na thread da transação (background, dentro de `Task.Run`/`ChamarFluxoPagamento`). Ao tocar controles, faça marshal:
> ```csharp
> tef.OnMensagemUsuario += (s, e) => {
>     if (InvokeRequired) { BeginInvoke(new Action(() => txtLog.Text = e.Mensagem)); return; }
>     txtLog.Text = e.Mensagem;
> };
> tef.OnReceberInformacao += (s, e) => {
>     if (InvokeRequired) { Invoke(new Action(() => Coletar(s, e))); return; } // síncrono: lib aguarda InformacaoColeta
>     // ... ShowDialog ...
>     tef.InformacaoColeta = resultado;
> };
> ```

> O loop de coleta roda dentro de `ChamarFluxoPagamento()` até `ColetaRetorno == "9"` ou conclusão com `Retorno == "0"/"1"`. Alguns `ColetaPalavraChave` (`transacao_parcela`, `formapagamento`, `valortotal` etc.) são preenchidos automaticamente a partir do `PagamentoRequest`/`AdmRequest`; demais chaves caem no `OnReceberInformacao`.

## Exemplos

### 1. Pagamento — Crédito à vista

```csharp
tef.OnMensagemUsuario += Imprimir;
tef.OnReceberInformacao += Coletar;

var request = new PagamentoRequest(TipoOperacao.CartaoCredito, valor: 100.50m, quantidadeParcelas: 1);

// execute fora da UI thread (WinForms/WPF)
var response = await Task.Run(() => tef.RealizarPagamento(request));

if (response == null || !response.Retorno)
{
    MessageBox.Show($"Erro: {response?.Mensagem} ({response?.DescricaoRetorno})");
    return;
}

if (response.Tef.PodeConfirmar) // Retorno == "0"
{
    Console.WriteLine($"Autorização: {response.Tef.CodigoAutorizacao}");
    Console.WriteLine($"NSU: {response.Tef.NsuTransacao}");
    Console.WriteLine($"Bandeira: {response.Tef.NomeBandeira}");
    Console.WriteLine($"Comprovante:\n{response.Tef.ComprovanteDiferenciadoPortador}");
    // ConfirmarOperacaoTEF já foi chamado internamente; comprovante loja em response.Tef.ComprovanteDiferenciadoLoja
}
```

### 2. Pagamento — Crédito parcelado (estabelecimento)

```csharp
// QuantidadeParcelas > 1 só tem efeito para CartaoCredito (Débito é forçado para 1)
var parcelado = new PagamentoRequest(TipoOperacao.CartaoCredito, 299.90m, quantidadeParcelas: 6);
var resp = await Task.Run(() => tef.RealizarPagamento(parcelado));
// resp.Tef.NumeroParcelas, resp.Tef.TipoFinanciamento == "Estabelecimento"
```

### 3. Pagamento — Débito / Voucher / Frota / PrivateLabel

```csharp
var debito  = new PagamentoRequest(TipoOperacao.CartaoDebito, 50m);
var voucher = new PagamentoRequest(TipoOperacao.Voucher, 30m);
var frota   = new PagamentoRequest(TipoOperacao.Frota, 200m);

var resp = await Task.Run(() => tef.RealizarPagamento(debito));
```

### 4. Pagamento PIX

```csharp
tef.OnExibirQrCodePix += (s, e) =>
{
    if (pictureBox.InvokeRequired) { pictureBox.BeginInvoke(new Action(() => { pictureBox.Image = ByteArrayToImage(e.QrCode); pictureBox.Refresh(); })); return; }
    pictureBox.Image = ByteArrayToImage(e.QrCode);
    pictureBox.Refresh();
};

decimal valorPix = 75.00m;
var pixResp = await Task.Run(() => tef.RealizarPagamentoPIX(valorPix));

if (!pixResp.Retorno)
{
    // timeout vira "Tempo limite atingido" quando o retorno contém QRCODE
    MessageBox.Show(pixResp.Mensagem);
    return;
}

Console.WriteLine(pixResp.Tef.CodigoAutorizacao);
```

### 5. Cancelar transação em andamento (timeout manual)

```csharp
// de um botão "Cancelar" ou após timeout customizado:
tef.CancelarOperacaoTEF();

// para PIX o Timeout da propriedade já cria um CancellationTokenSource interno:
// tef.Timeout = 30; // 30s
```

### 6. Operações administrativas

```csharp
// Cancelamento precisa de dados da transação original
var admCancel = new AdmRequest(
    TipoOperacaoAdm.Cancelamento,
    usuario: "lojista",
    senha: "lojista1#",
    dataTransacao: DateTime.Parse("10/02/2025"),
    nsuTransacao: "123456",
    valorTransacao: 100.50m
);

var admResp = tef.RealizarAdm(admCancel); // síncrono (sem Task.Run no exemplo da Demo)

if (!admResp.Retorno)
    MessageBox.Show(admResp.Mensagem);

// Pendência / Reimpressão — sem dados adicionais
var pendencia   = new AdmRequest(TipoOperacaoAdm.Pendencia, "lojista", "lojista1#");
var reimpressao = new AdmRequest(TipoOperacaoAdm.Reimpressao, "lojista", "lojista1#");
// TipoOperacaoAdm.Nenhum também é válido (abre menu no PINPad)

var r2 = tef.RealizarAdm(pendencia);
```

### 7. Tratamento de retorno

`BaseResponse` / `BaseResponse<TransacaoResponse>`:

| `Codigo` | `DescricaoRetorno` | `Retorno` |
|---|---|---|
| 0 | Sucesso, confirmação necessária | `true` (`PodeConfirmar`) |
| 1 | Sucesso | `true` (`PodeFinalizar`) |
| 2 | Sequencial inválido | `false` |
| 3 | Cancelada pelo operador | `false` |
| 4 | Cancelada pelo cliente | `false` |
| 5 | Parâmetros inválidos | `false` |
| 6 | Problema conexão ElginTef | `false` |
| 7 | Problema ElginTef ↔ Rede | `false` |
| 8 | Timeout | `false` |
| 9 | Problema desconhecido | `false` |

```csharp
Console.WriteLine($"{resp.Codigo} - {resp.DescricaoRetorno}: {resp.Mensagem}");
Console.WriteLine($"CNPJ: {resp.Tef.CnpjCredenciadora}"); // já sem pontuação (RemoverPontuacao chamada no PIX/Pagamento)
```

## Demo WinForms

Projeto `src/Vip.ElginTEF.Demo` (`net48`, `WinExe`) — referência direta ao projeto da lib + `Vip.Extensions` vendorizado.

```powershell
# Windows + Visual Studio / MSBuild
dotnet build ./src/Vip.ElginTEF.sln -c Release
# ou
msbuild ./src/Vip.ElginTEF.sln /p:Configuration=Release

# Executável em src/Vip.ElginTEF.Demo/bin/Release/
# Garanta que E1_Tef01.dll + Qt5*.dll + lib_ppelgin.dll estejam ao lado do .exe
```

A Demo (`frmPrincipal.cs`) demonstra todos os fluxos com `Task.Run` para não travar a UI e dialogs `frmEscolherInformacao` / `frmDigitarInformacao` para `OnReceberInformacao`.

## Build da biblioteca

```powershell
dotnet restore ./src/Vip.ElginTEF/Vip.ElginTEF.csproj --force
dotnet build   ./src/Vip.ElginTEF/Vip.ElginTEF.csproj -c Release
dotnet pack    ./src/Vip.ElginTEF/Vip.ElginTEF.csproj -c Release -p:Version=1.0.<n>
```

CI (`.github/workflows/nuget.yml`) compila apenas a lib no Ubuntu (`dotnet 7.0.x`) e publica no NuGet com versão `1.0.$GITHUB_RUN_NUMBER[-beta]`.

## Estrutura

```
src/
  Vip.ElginTEF/                 # lib netstandard2.0 (Fody/ExtraConstraints)
    TefService.cs               # API pública — fluxo Ativar → Configurar → Realizar* → Confirmar → Finalizar
    Models/Configuracao.cs
    Request/{Pagamento,Adm,Fluxo}Request.cs
    Response/{Base,Transacao,Fluxo}Response.cs
    Events/*EventArgs.cs
    Enums/{TipoOperacao,TipoOperacaoAdm,ModeloLib,TipoFluxo,TipoInformacao}.cs
    Services/{Manager,TefStdCall,TefCdecl,TefLibrary}.cs
    Core/{VipSafeHandle,VipLPStr,JsonContractResolver}.cs
  Vip.ElginTEF.Demo/            # WinForms net48 — teste manual
  Vip.ElginTEF.sln
  nuget.config                  # repositoryPath = ..\packages
```

## Licença

MIT — ver [LICENSE](LICENSE).
