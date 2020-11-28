# CoreApi - API em .Net Core 3.1
# AngularClient - Aplicação Web em Angular

## Banco

* Foi utilizado o LocalDB do Sql Server para o desenvolvimento.
* O script DDL de banco está na pasta ScriptsBanco

### Comando para gerar a reversa do banco

> dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master" Microsoft.EntityFrameworkCore.SqlServer --context-dir Contexts --context MasterContext --output-dir Entities/Master --force