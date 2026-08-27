# AGENTS.md — Vip.ElginTEF

## Stack
- `src/Vip.ElginTEF/Vip.ElginTEF.csproj` — SDK-style, `netstandard2.0`, `LangVersion latest`. Depende de `Newtonsoft.Json 13.0.3` + `Fody 6.5.1` / `ExtraConstraints.Fody 1.14.0` (weavers em `FodyWeavers.xml` — não remover).
- `src/Vip.ElginTEF.Demo/Vip.ElginTEF.Demo.csproj` — MSBuild clássico, `net48` WinForms (`WinExe`). Referencia `Vip.ElginTEF` via `ProjectReference` + `packages/Vip.Extensions.1.0.24` vendorizado via `HintPath` (NuGet legado).
- Solution: `src/Vip.ElginTEF.sln` (use `src/` como workdir, não a raiz do repo).

## Build / Pack
```pwsh
dotnet restore ./src/Vip.ElginTEF/Vip.ElginTEF.csproj --force
dotnet build   ./src/Vip.ElginTEF/Vip.ElginTEF.csproj -c Release
dotnet pack    ./src/Vip.ElginTEF/Vip.ElginTEF.csproj -c Release -p:Version=1.0.<n>
# solution completa (lib + demo — demo exige Windows/MSBuild):
dotnet build ./src/Vip.ElginTEF.sln -c Release
msbuild ./src/Vip.ElginTEF.sln /p:Configuration=Release   # alternativa para Demo
```
- CI (`.github/workflows/nuget.yml`): Ubuntu + `actions/setup-dotnet@v3` / `7.0.x`, compila **apenas** `Vip.ElginTEF.csproj` com `VERSION=1.0.$GITHUB_RUN_NUMBER + inputs.typeBuild` (sufixo `-beta`). Demo nunca compila no CI (só Windows).
- `src/nuget.config` define `repositoryPath = ..\packages` — `dotnet restore` respeita; `Demo/packages.config` é legado. Não converter Demo para `PackageReference` sem migrar os pacotes vendorizados offline.

## Gotchas de Runtime
- DLL nativa obrigatória: `TefService.CaminhoLib` padrão `.\E1_Tef01.dll`; `Ativar()` verifica com `File.Exists` e lança exceção se não existir. Cópias vendorizadas apenas em `src/Vip.ElginTEF.Demo/bin/{Debug,Release}/` — não distribuída via NuGet.
- Duas convenções de chamada via `Enums/ModeloLib`: `StdCall` (padrão, `Services/TefStdCall.cs`) e `Cdecl` (`Services/TefCdecl.cs`). Factory é `Services/Manager.GetLibrary()` → `ILibrary` sobre `Core/VipSafeHandle` (`LoadLibrary`/`GetProcAddress`). Mockar `ILibrary` para qualquer teste — nunca carregar DLL real em CI/teste unitário.
- Fluxo principal de transação em `TefService.cs:137` — `Ativar() → ConfigurarDados() → IniciarOperacaoTEF → RealizarPagamentoTEF/RealizarPixTEF/RealizarAdmTEF` em loop via `ChamarFluxoPagamento()` → `ConfirmarOperacaoTEF` → `FinalizarOperacaoTEF`. Caminho PIX adiciona `CancellationTokenSource(Timeout)` (padrão `Timeout=240s`). Setters de `ModeloLib`/`CaminhoLib` lançam `VipException` quando `Ativo==true`.
- Encoding hardcoded `Encoding.UTF8` na chamada `Manager.GetLibrary` (`TefService.cs:141`); marshaling de string usa `Core/VipLPStr.cs` + `Core/JsonContractResolver.cs`.

## Demo
- Entrada WinForms `src/Vip.ElginTEF.Demo/Program.cs` → `frmPrincipal.cs` (exige Windows, `net48`, Qt5 + deps nativas `lib_ppelgin`). Apenas teste manual.

## Convenções
- Idioma: nomes em português (mensagens, enums `TipoOperacao`, `TipoFluxo` etc.) — manter pt-BR para compatibilidade da API pública (consumidores NuGet).
- Sem projetos de teste, sem configs de lint/typecheck/formatter, sem `opencode.json`. Valide alterações com `dotnet build` acima.
- `.gitignore` contém marcadores de conflito de merge não resolvidos (`<<<<<<< HEAD`) entre `master` vs `Add .gitignore…` — limpar antes de commitar se tocar no arquivo. `.vs/` e `bin/`/`obj/` são ignorados mas ainda existem em disco com DLLs nativas commitadas em `Demo/bin/`.

## Onde Procurar
- API pública: `TefService.cs` + `Models/Configuracao.cs`, `Request/*`, `Response/*`, `Events/*`, `Interfaces/ILibrary.cs`
- Interop nativo: `Services/Tef{StdCall,Cdecl,Library}.cs`, `Core/VipSafeHandle.cs`, `Core/VipLPStr.cs`
- Metadados NuGet: `Vip.ElginTEF.csproj` (`Title`, `PackageProjectUrl`, `RepositoryUrl`)
