# Computer Inventory API – Asset Management Solution

[![.NET 9](https://img.shields.io/badge/.NET-9.0-512bd4.svg)](https://dotnet.microsoft.com/download)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-9.0-blue.svg)](https://learn.microsoft.com/en-us/ef/core/)

Ett specialiserat REST-API för strukturerad hantering av IT-inventarier och hårdvarukomponenter. Projektet fokuserar på att spåra datorers livscykel, från specifikationer (CPU, RAM, Lagring) till deras nuvarande status och tilldelning.

## 🚀 Huvudfunktioner

- **Inventariehantering (Asset Tracking):** Kompletta CRUD-operationer för både datorer och enskilda hårdvarukomponenter.
- **Relationsdatabas:** Automatiserad koppling mellan komponenter och specifika enheter via **Entity Framework Core**.
- **Testdata-generering:** Använder biblioteket **Bogus** för att populera databasen med realistisk hårdvaruinformation vid utveckling.
- **Interaktiv API-dokumentation:** Integrerad **Swagger (OpenAPI)** för enkel testning och utforskning av endpoints.
- **Hårdvaruspecifikationer:** Detaljerad loggning av serienummer, modeller och prestandaparametrar.

## 🛠 Teknisk Stack

- **Framework:** ASP.NET Core Web API (.NET 9)
- **Datalager:** Entity Framework Core (Code First)
- **Databas:** Microsoft SQL Server (LocalDB)
- **Data Seed:** Bogus (för stress-testning och utveckling)
- **Verktyg:** Swagger UI, LINQ

## 📂 Projektets Struktur

Projektet följer en tydlig separation av ansvarsområden:
- **Controllers:** Hanterar inkommande HTTP-anrop och affärslogik för `Computers` och `Components`.
- **Data:** Innehåller `ComputerInventoryContext` och konfiguration för databasinitiering.
- **Models:** Domänmodeller som definierar entiteterna `Computer`, `Component` och `Type`.

## 🏁 Kom igång

1. **Klona arkivet:**
   ```bash
   git clone https://github.com/Nowaxial/ComputerInventoryIndividualProjectAPILexicon.git
   ```
  

2. **Initiera databasen:**
Kör följande kommando i Package Manager Console:
  ```bash
  Update-Database
  ```

3. **Kör projektet:**
Starta via Visual Studio eller CLI:
  ```bash
  dotnet run
  ```

4. **Utforska API:et:**
Navigera till `https://localhost:XXXX/swagger` för att se de tillgängliga endpoints.

## 🛡 Säkerhet \& Designmönster

- **Clean Code:** Starkt typade modeller och LINQ-frågor för säker datahantering.
- **Resiliens:** Inbyggd logik för att hantera saknad hårdvaruinformation och validering av indata.
- **Skalbarhet:** Arkitekturen är förberedd för att flyttas till molnmiljöer som **Microsoft Azure**.

---
*Utvecklat som ett individuellt projekt under utbildningen till Systemutvecklare hos Lexicon.*

```
