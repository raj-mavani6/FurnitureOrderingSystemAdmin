-- Create Database
CREATE DATABASE IF NOT EXISTS FurnitureOrderingSystem
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE FurnitureOrderingSystem;

-- ============================================================
-- TABLE 1: Admins
-- ============================================================
CREATE TABLE IF NOT EXISTS Admins (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    Phone VARCHAR(15) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Role VARCHAR(50) NOT NULL DEFAULT 'Admin',          -- Admin, SuperAdmin
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    IsActive TINYINT(1) NOT NULL DEFAULT 1,
    LastLoginAt DATETIME NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ============================================================
-- INSERT: Admins (2 records)
-- ============================================================
INSERT INTO Admins (FirstName, LastName, Email, Phone, Password, Role, CreatedAt, IsActive) VALUES
('Rajesh', 'Patel', 'admin@furniture.com', '9876543210', 'admin123', 'SuperAdmin', NOW(), 1),