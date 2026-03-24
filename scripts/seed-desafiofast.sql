USE [DesafioFastDB];
GO

SET NOCOUNT ON;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    DELETE FROM [Presenca];
    DELETE FROM [Colaborador];
    DELETE FROM [Workshop];

    DBCC CHECKIDENT ('Colaborador', RESEED, 0);
    DBCC CHECKIDENT ('Workshop', RESEED, 0);

    INSERT INTO [Colaborador] ([Nome])
    VALUES
        ('Ana Souza'),
        ('Bruno Lima'),
        ('Carla Mendes'),
        ('Diego Costa'),
        ('Eduarda Rocha'),
        ('Felipe Alves'),
        ('Gabriela Nunes'),
        ('Henrique Martins');

    INSERT INTO [Workshop] ([Nome], [Descricao], [DataRealizacao])
    VALUES
        ('Boas práticas de API REST', 'Workshop sobre versionamento, contratos e padronização de respostas.', '2026-01-15T16:00:00'),
        ('Introdução a Angular', 'Fundamentos de componentes, services e consumo de APIs com HttpClient.', '2026-04-16T16:00:00'),
        ('Autenticação com JWT', 'Conceitos de autenticação, autorização por roles e segurança de tokens.', '2026-07-16T16:00:00'),
        ('Clean Architecture no Backend', 'Separação de camadas Domain, Application, Infrastructure e API.', '2026-10-15T16:00:00');

    -- Status: 0 = Presente, 1 = Ausente
    INSERT INTO [Presenca] ([WorkshopId], [ColaboradorId], [DataHoraCheckIn], [Status])
    VALUES
        (1, 1, '2026-01-15T16:05:00', 0),
        (1, 2, '2026-01-15T16:10:00', 0),
        (1, 3, '2026-01-15T16:20:00', 0),
        (1, 4, '2026-01-15T16:30:00', 1),

        (2, 1, '2026-04-16T16:03:00', 0),
        (2, 5, '2026-04-16T16:15:00', 0),
        (2, 6, '2026-04-16T16:40:00', 0),
        (2, 8, '2026-04-16T16:55:00', 1),

        (3, 2, '2026-07-16T16:01:00', 0),
        (3, 3, '2026-07-16T16:12:00', 0),
        (3, 7, '2026-07-16T16:25:00', 0),
        (3, 8, '2026-07-16T16:58:00', 0),

        (4, 1, '2026-10-15T16:08:00', 0),
        (4, 4, '2026-10-15T16:18:00', 0),
        (4, 5, '2026-10-15T16:33:00', 1),
        (4, 6, '2026-10-15T16:47:00', 0);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
GO
