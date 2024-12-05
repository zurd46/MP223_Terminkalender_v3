# RaumreservierungsSystem

Ein Webapplikations-Projekt für die Verwaltung und Bearbeitung von Raumreservierungen. Mit dieser Anwendung können Benutzer Räume reservieren, bestehende Reservierungen anzeigen, bearbeiten oder löschen und Schlüssel für die Verwaltung und Einsicht von Reservierungen generieren.

## Funktionen

- **Reservierungen erstellen**: Neue Reservierungen mit Teilnehmern, Datum, Zeit und Zimmernummer hinzufügen.
- **Reservierungen anzeigen**: Liste aller bestehenden Reservierungen anzeigen.
- **Bearbeiten und Löschen**: Reservierungen über einen Private Key bearbeiten oder löschen.
- **Öffentliche Ansicht**: Reservierungsdetails über einen Public Key einsehen.
- **Reservierungsprüfung**: Sicherstellen, dass keine Raumüberschneidungen bei neuen Reservierungen auftreten.
- **Schlüsselverwaltung**: Automatische Generierung von Private und Public Keys für die Verwaltung und Einsicht.

## Voraussetzungen

- .NET 8 oder höher
- SQLite (integriert im Projekt)

## Installation

1. **Repository klonen**:
   ```bash
   git clone https://github.com/benutzername/raumreservierungssystem.git
   cd raumreservierungssystem
