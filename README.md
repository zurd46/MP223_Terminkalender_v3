# ÜK M223 Raumreservierungs-System

Ein Webapplikations-Projekt für die Verwaltung und Bearbeitung von Raumreservierungen. Mit dieser Anwendung können Benutzer Räume reservieren, bestehende Reservierungen anzeigen, bearbeiten oder löschen und Schlüssel für die Verwaltung und Einsicht von Reservierungen verwenden.

---

## Funktionen

- **Reservierungen erstellen**: Neue Reservierungen mit Teilnehmern, Datum, Zeit und Zimmernummer hinzufügen.
- **Reservierungen anzeigen**: Liste aller bestehenden Reservierungen anzeigen.
- **Bearbeiten und Löschen**: Reservierungen über einen Private Key bearbeiten oder löschen.
- **Öffentliche Ansicht**: Reservierungsdetails über einen Public Key einsehen.
- **Reservierungsprüfung**: Sicherstellen, dass keine Raumüberschneidungen bei neuen Reservierungen auftreten.
- **Schlüsselverwaltung**: Private und Public Keys ermöglichen den Zugriff auf Reservierungen. Der Private Key erlaubt Bearbeitung und Löschung, während der Public Key nur Einsicht gewährt.

---

## Voraussetzungen

- **.NET 8** oder höher
- **SQLite** (wird im Projekt integriert geliefert)

---

## Installation

1. **Repository klonen**:
   ```bash
   git clone https://github.com/zurd46/MP223_Terminkalender_v3.git
   cd MP223_Terminkalender_v3
