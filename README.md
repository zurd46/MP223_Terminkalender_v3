# ÜK M223 Raumreservierungs-System

Ein Webapplikations-Projekt für die Verwaltung und Bearbeitung von Raumreservierungen. Die App wurde mit Bootstrap, JavaScript und dem CSS Framework erstellt. Mit dieser Anwendung können Benutzer Räume reservieren, bestehende Reservierungen anzeigen, bearbeiten oder löschen und Schlüssel für die Verwaltung und Einsicht von Reservierungen verwenden.

---

## Funktionen

- **Reservierungen erstellen**: Neue Reservierungen mit Teilnehmern, Datum, Zeit und Zimmernummer hinzufügen.
- **Bearbeiten und Löschen**: Reservierungen über einen Private Key bearbeiten oder löschen.
- **Öffentliche Ansicht**: Reservierungsdetails über einen Public Key einsehen.
- **Reservierungsprüfung**: Sicherstellen, dass keine Raumüberschneidungen bei neuen Reservierungen auftreten.
- **Schlüsselverwaltung**: Private und Public Keys ermöglichen den Zugriff auf Reservierungen. Der Private Key erlaubt Bearbeitung und Löschung, während der Public Key nur Einsicht gewährt.

---

## Voraussetzungen

- **.NET 8** oder höher
- **SQLite** (wird im Projekt integriert geliefert)
- **Verwendete Plugins in Visual Studio Code**:
  - SQLite v0.14.1
  - SQLite3 Editor v1.0.197
  - .NET Install Tool v2.2.3
  - C# v2.55.29
  - C# Dev Kit v1.14.14

**Versionen:**
- net8.0

---

## Installation

### 1. Repository klonen

```bash
git clone https://github.com/zurd46/MP223_Terminkalender_v3.git
cd MP223_Terminkalender_v3
```

### 2. Projektabhängigkeiten wiederherstellen

```bash
dotnet restore
```

### 3. Datenbank initialisieren

Die SQLite-Datenbank ist im Projektordner enthalten. Falls nicht, stellen Sie sicher, dass die Datei `reservations.db` vorhanden ist oder erstellen Sie eine neue SQLite-Datenbank. 

### 4. Projekt starten

#### Über das Terminal:

1. Navigieren Sie in das Projektverzeichnis:
   ```bash
   cd MP223_Terminkalender_v3
   ```
2. Starten Sie die Anwendung:
   ```bash
   dotnet run
   ```

#### Über Visual Studio Code:

1. Öffnen Sie den Projektordner in Visual Studio Code.
2. Drücken Sie `F5` oder klicken Sie auf den **Run**-Button oben rechts.

---

### 5. Zugriff auf die Webanwendung

Nach dem Start der Anwendung wird in der Konsole eine URL angezeigt, z. B.:

```plaintext
Now listening on: http://localhost:5000
```

Öffnen Sie diese URL in Ihrem Browser, um auf die Web-App zuzugreifen.

---

## Verzeichnisstruktur

Die grundlegende Verzeichnisstruktur des Projekts sieht wie folgt aus:

```
MP223_Terminkalender_v3/
├── Controllers/
│   └── HomeController.cs
│   └── ReservationController.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Doku/
│   └── ERD_Datenbank.png
│   └── Raumreservierungs_ULM.png
│   └── ÜK_MP223_Dokumentation.docx
├── Migrations (Datenbankmigrationen)
├── Models/
│   └── Reservation.cs
├── Properties/
│   └── launchSettings.json
├── Views/
│   └── Home/
│       ├── Index.html
│   └── Reservation/
│       ├── Create.html
│       ├── Edit.html
│       ├── InvalidKey.html
│       ├── Success.html
│       ├── View.html
├── wwwroot/
│   ├── css/
│       ├── styles.css
├── Program.cs
├── MP223_Terminkalender_v3.csproj
├── MP223_Terminkalender_v3.sln
├── README.md
├── reservations.db
```

---

## Autoren

- Daniel Zurmühle
- Levin Lustenberger
- Matheus Lischer

---

