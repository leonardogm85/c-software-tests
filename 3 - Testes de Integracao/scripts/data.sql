USE [NerdStoreDb]

DECLARE @IdCamiseta UNIQUEIDENTIFIER = '23a4ca11-4fbe-4961-ac22-2a91f146dec6'
DECLARE @IdCaneca UNIQUEIDENTIFIER = '6a17e4f4-6303-47ef-902b-535797660099'
DECLARE @IdAdesivo UNIQUEIDENTIFIER = '42535e21-eaa7-49c0-be3c-1db1b0a91e14'

INSERT [dbo].[Categorias] ([Id], [Nome], [Codigo]) VALUES
	(@IdCamiseta, 'Camisetas', 100),
	(@IdCaneca, 'Canecas', 101),
	(@IdAdesivo, 'Adesivos', 102)

INSERT [dbo].[Produtos] ([Id], [Nome], [Descricao], [Ativo], [Valor], [DataCadastro], [Imagem], [QuantidadeEstoque], [Altura], [Largura], [Profundidade], [CategoriaId]) VALUES
	('7c7d75bf-21cf-44cf-bc9d-27acf3ba69ad', 'Camiseta Code Life Cinza', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, 60.00, GETDATE(), 'camiseta3.jpg', 20, 9, 8, 2, @IdCamiseta),
	('3a53438d-1e75-4990-95a8-2e886b0a0db0', 'Camiseta Debugar Preta', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, 70.00, GETDATE(), 'camiseta4.jpg', 30, 2, 7, 3, @IdCamiseta),
	('8179f1da-4595-499b-a217-2ef895c0c648', 'Camiseta Software Developer', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, 80.00, GETDATE(), 'camiseta1.jpg', 40, 8, 6, 4, @IdCamiseta),
	('647446ce-4406-4dcc-8453-5ec74c3aa162', 'Camiseta Code Life Preta', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, 90.00, GETDATE(), 'camiseta2.jpg', 50, 3, 5, 5, @IdCamiseta),
	('a792b43c-aa65-4947-8292-a787b1e16eb3', 'Caneca Turn Coffee in Code', 'Caneca de porcelana com impressão térmica.', 1, 10.00, GETDATE(), 'caneca3.jpg', 45, 7, 4, 6, @IdCaneca),
	('4dc75890-84f1-48be-841e-c44dfaf71fa3', 'Caneca Star Bugs Coffee', 'Caneca de porcelana com impressão térmica.', 1, 20.00, GETDATE(), 'caneca1.jpg', 35, 4, 3, 7, @IdCaneca),
	('14eca13b-42b2-45fd-9386-c7b0f0cf4542', 'Caneca Programmer Code', 'Caneca de porcelana com impressão térmica.', 1, 30.00, GETDATE(), 'caneca2.jpg', 25, 6, 2, 8, @IdCaneca),
	('36e5b0ba-cfc8-42d0-a4a6-d170a59f1001', 'Caneca No Coffee No Code', 'Caneca de porcelana com impressão térmica.', 1, 40.00, GETDATE(), 'caneca4.jpg', 15, 5, 1, 9, @IdCaneca)

INSERT [dbo].[AspNetUsers]
	([Id],
	[UserName],
	[NormalizedUserName],
	[Email],
	[NormalizedEmail],
	[EmailConfirmed],
	[PasswordHash],
	[SecurityStamp],
	[ConcurrencyStamp],
	[PhoneNumberConfirmed],
	[TwoFactorEnabled],
	[LockoutEnabled],
	[AccessFailedCount])
VALUES
	('d7c084ed-bf90-459a-9c43-9fdbd66b2429',
	'teste@teste.com',
	'TESTE@TESTE.COM',
	'teste@teste.com',
	'TESTE@TESTE.COM',
	1,
	'AQAAAAEAACcQAAAAEKobwX24LXiIN9F2mftsVH4PTWni1bEqnPvgmZyejI5mQTUWT61wAU9vhV5lKXh0tw==',
	'KDYV6LQ7BQWX4HNTABNHA6IZFHUS3X4F',
	'30e196b8-0cd8-4d0a-8a38-5df39c45cdc6',
	0,
	0,
	1,
	0)
