CREATE TABLE [dbo].[Entregas] (
    [IdEntrega]     INT             IDENTITY (1, 1) NOT NULL,
    [DtEntrega]     DATE            NOT NULL,
    [NmProduto]     VARCHAR (50)    NOT NULL,
    [VlUnitario]    NUMERIC (10, 2) NOT NULL,
    [QtdProduto]    INT             NOT NULL,
    [IdLoteEntrega] INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([IdEntrega] ASC),
    CONSTRAINT [FK_Entregas_LoteEntregas] FOREIGN KEY ([IdLoteEntrega]) REFERENCES [dbo].[LoteEntregas] ([IdLoteEntrega]),
    CONSTRAINT [CK_QtdPositiva] CHECK ([QtdProduto]>(0))
);


CREATE TABLE [dbo].[LoteEntregas] (
    [IdLoteEntrega]    INT             IDENTITY (1, 1) NOT NULL,
    [NmArquivoLote]    VARCHAR (50)    NOT NULL,
    [DtImportacao]     DATETIME        NOT NULL,
    [NrRegistros]      INT             NOT NULL,
    [NrTotalProdutos]  INT             NOT NULL,
    [VlTotalImportado] NUMERIC (10, 2) NOT NULL,
    [DtEntregaMenor]   DATE            NOT NULL,
    PRIMARY KEY CLUSTERED ([IdLoteEntrega] ASC)
);