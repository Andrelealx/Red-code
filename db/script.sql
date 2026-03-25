CREATE DATABASE TicketPrime;
GO
USE TicketPrime;
GO
-- TABELA 1: Usuarios (ID 01 e 02)
CREATE TABLE Usuarios (
    Cpf VARCHAR(11) PRIMARY KEY, -- ID 34: Validação 11 chars
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL
);

-- TABELA 2: Eventos (ID 03 e 04)
CREATE TABLE Eventos (
    Id INT PRIMARY KEY IDENTITY(1,1), -- ID 15: Autoincremento
    Nome VARCHAR(100) NOT NULL,
    CapacidadeTotal INT NOT NULL,
    DataEvento DATETIME NOT NULL,
    PrecoPadrao DECIMAL(10,2) NOT NULL
);

-- TABELA 3: Cupons (ID 08)
CREATE TABLE Cupons (
    codigo VARCHAR(50) PRIMARY KEY,
    PorcentagemDesconto DECIMAL(5,2) NOT NULL,
    valorMinimoregra DECIMAL(10,2) NOT NULL
);

-- TABELA 4: Reservas (A Tabela Central)
CREATE TABLE Reservas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioCpf VARCHAR(11) NOT NULL,
    EventoId INT NOT NULL,
    CupomUtilizado VARCHAR(50) NULL, -- ID 14: Permite Nulo
    ValorFinalPago DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_Usuario FOREIGN KEY (UsuarioCpf) REFERENCES Usuarios(Cpf), -- ID 17: FK
    CONSTRAINT FK_Evento FOREIGN KEY (EventoId) REFERENCES Eventos(Id),
    CONSTRAINT FK_Cupom FOREIGN KEY (CupomUtilizado) REFERENCES Cupons(codigo)
);