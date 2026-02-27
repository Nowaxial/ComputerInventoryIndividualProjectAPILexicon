# Computer Inventory API – Asset Management Solution

[![.NET 9](https://img.shields.io/badge/.NET-9.0-512bd4.svg)](https://dotnet.microsoft.com/download)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-9.0-blue.svg)](https://learn.microsoft.com/en-us/ef/core/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Ett robust REST-API byggt med **ASP.NET Core** för strukturerad hantering av IT-inventarier och hårdvarukomponenter. Projektet är designat för att lösa verkliga utmaningar inom **Asset Management**, såsom spårbarhet av serienummer, komponenthistorik och systemstatus.

![API Picture](https://i.ibb.co/rK8cBzR2/Computer-Inventory-API.png)


## 🚀 Nyckelfunktioner för IT-drift

API:et är optimerat för scenarier som liknar kommunal IT-förvaltning:
- **Fullständig Inventariehantering (CRUD):** Hantering av datorer (laptops/desktops) och tillhörande komponenter (CPU, RAM, Lagring).
- **Hårdvaruspecifikationer:** Detaljerad spårning av modeller, serienummer och konfigurationer.
- **Relationsdatabas:** Arkitektur som kopplar samman komponenter med specifika enheter via Entity Framework Core.
- **Säker Dataåtkomst:** Validering av indata för att säkerställa dataintegritet vid registrering av ny utrustning.
- **Interaktiv Dokumentation:** Inbyggt stöd för **Swagger/OpenAPI** för enkel testning av endpoints.

## 🛠 Teknisk Stack

- **Backend:** .NET 9 / ASP.NET Core Web API
- **ORM:** Entity Framework Core (Code First)
- **Databas:** Microsoft SQL Server (LocalDB för enkel utveckling)
- **Arkitektur:** Clean separation mellan Models, Data och Controllers
- **Verktyg:** Bogus (för generering av testdata), Swagger UI

## 📁 Projektstruktur

```text
├── Controllers/         # API-endpoints för Computers och Components
├── Data/                # DbContext och databasinitiering
├── Models/              # Domänmodeller (Computer, Component, Type)
├── Migrations/          # Entity Framework-versionering
└── Program.cs           # Konfiguration av tjänster och middleware
```


## 🏁 Kom igång

1. **Klona repot:**

```bash
git clone https://github.com/Nowaxial/ComputerInventoryIndividualProjectAPILexicon.git
```

2. **Uppdatera databasen:**
Kör följande i Package Manager Console:

```powershell
Update-Database
```

3. **Kör applikationen:**
Starta projektet via Visual Studio eller CLI:

```bash
dotnet run
```

4. **Testa API:et:**
Öppna din webbläsare på `https://localhost:XXXX/swagger` för att se dokumentationen.

## 🛡 Säkerhet \& Kvalitet

Projektet följer moderna principer för säker webbutveckling:

- **Separation of Concerns:** Tydlig uppdelning mellan datalager och API-logik.
- **Typad Data:** Minimering av fel genom starkt typade modeller.
- **Skalbarhet:** Förberett för integration med molntjänster som Azure SQL Database.

---
*Detta projekt utvecklades som en del av min certifiering till Systemutvecklare hos Lexicon, med fokus på att skapa verksamhetsnytta genom modern .NET-teknik.*

