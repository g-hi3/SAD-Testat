# SAD-Testat
Dieses Repository enthält den Code für das Autovermietungssystem "CarRent" als Teil des Testats für das Fach *Softwarearchitektur und -design*.

## Anforderungen
Die Anforderungen für diese Software werden unterteilt in Anforderungen aus der Aufgabe und Anforderungen vom Kunden.

**Anforderungen aus der Aufgabe**:
- Die Daten sollen mittels [Repository Pattern][1] in einer Datenbank gespeichert werden können.
- Die Business Logik soll auf dem Backend laufen und [REST API][2]s anbieten.
- Es soll zuerst ein [Monolith](3) erstellt werden und später auf eine [Micro Service Architektur][3] überführt werden.
- *Optional*: Der Web-Client benutzt die [REST API][2] um die Funktionen auszuführen.

**Anforderungen vom Kunden**:
- Der Sachbearbeiter kann Kunden mit Namen und Adresse und Kundennummer im System verwalten, d.h. erfassen, bearbeiten, löschen und den Kunden mit dessen Namen oder Kundennummer suchen.
- Der Sachbearbeiter kann zudem die Autos von CarRent verwalten und nach denen suchen.
- Jedes Auto kann einer bestimmten Klasse zwischen Luxusklasse, Mittelklasse oder Einfachklasse zugeordnet werden und besitzt zudem eine Marke, einen Typ und eine eindeutige Identifikation.
- Jede Klasse besitzt eine Tagesgebühr.
- Bei einer neuen Reservation kann der Kunde ein Auto aus einer bestimmten Klasse wählen.
Er muss zudem die Anzahl der Tage angeben, die er das Auto gerne mieten möchte.
Dabei werden die Gesamtkosten berechnet.
Wird die Reservation gespeichert, so wird sie mit einer Reservationsnummer abgelegt.
- Bei Abholung des Autos wird die Reservation in einen Mietvertrag umgewandelt.

### Analyse der gegebenen Anforderungen

> Der **Sachbearbeiter** kann *Kunden* mit `Namen` und `Adresse` und `Kundennummer` im System verwalten, d.h. *erfassen*, *bearbeiten*, *löschen* und den Kunden mit dessen `Namen` oder `Kundennummer` *suchen*.

Wir finden in der ersten Anforderung die Rolle **Sachbearbeiter** mit den Verben *erfassen*, *bearbeiten*, *löschen* und *suchen*.

Die Begriffe *erfassen*, *bearbeiten*, *löschen* und *suchen* können auf die vier [**CRUD-Operationen**][4] zurückgeführt werden.
Gemäss [Wikipedia (CRUD)][4] können wir sowohl für unsere Datenbank, als auch die REST API diese Operationen strukturiert umsetzen.

Verb | CRUD-Operation | Datenbank (SQL) | REST (HTTP)
--- | --- | --- | ---
erfassen | Create | `INSERT` | `PUT` oder `POST`
suchen | Read (Retrieve) | `SELECT` | `GET`
bearbeiten | Update | `UPDATE` | `PATCH` oder `PUT`
löschen | Delete | `DELETE` | `DELETE`

Ausserdem können wir deduzieren, dass es eine Entität **Kunde** gibt, welche über die Eigenschaften `Name`, `Adresse` und `Kundennummer` verfügt.

---

> Der **Sachbearbeiter** kann zudem die *Auto*s von CarRent *verwalten* und nach denen *suchen*.

Die Rolle **Sachbearbeiter** kann die Aktionen *verwalten* und *suchen* für die Entität *Auto* durchführen.

---

> Jedes **Auto** kann einer bestimmten `Klasse` zwischen *Luxusklasse*, *Mittelklasse* oder *Einfachklasse* zugeordnet werden und besitzt zudem eine `Marke`, einen `Typ` und eine eindeutige `Identifikation`.

Die Entität **Auto** wird hier weiter beschrieben.
Sie soll über die Eigenschaften `Marke`, `Typ`, `Identifikation` und `Klasse` (nicht zu verwechseln mit der Klasse in der Software-Entwicklung).

Über den `Typ` ist nichts Genaueres bekannt.
Wir werden im Verlauf des Projekts einige Autotypen bestimmen.

Die `Identifikation` soll eindeutig sein.
Da nichts Genaueres beschrieben ist, gehen wir davon aus, dass die `Identifikation` über alle **Auto**s eindeutig sein soll.

Genannte `Klasse`n sind *Luxusklasse*, *Mittelklasse* und *Einfachklasse*.
Wir werden in diesem Projekt mit diesen als Enumeration arbeiten.
Wir behalten uns aber ausserdem die Möglichkeit offen, weitere Klassen zu definieren oder bestehende Klassen abzuändern.

Es sind keine spezifischen `Marke`n genannt.
Wir werden die `Marke`n dann definieren, wenn ein **Auto** mit einer neuen `Marke` erfasst wird.

---

> Jede **Klasse** besitzt eine `Tagesgebühr`.

Die `Tagesgebühr` für ein Auto wird anhand seiner **Klasse** bestimmt.
So wie der Satz geschrieben ist, wird erwartet, dass **Klasse** ebenfalls eine Entität ist, was uns die Umsetzung in einem Datenbanksystem vereinfacht.

---

> Bei einer neuen **Reservation** kann der *Kunde* ein *Auto* aus einer bestimmten `Klasse` wählen.

Es wird eine neue Entität **Reservation** beschrieben.
Sie bezieht sich auf die Entitäten *Kunde* und *Auto*.
Wir gehen davon aus, dass es pro Reservation *1 Kunden* und *1 Auto* gibt.
Die Reservation besitzt eine Eigenschaft `Klasse`, die vom *Auto* abgeleitet wird.

> **Er** muss zudem die *Anzahl der Tage angeben, die er das Auto gerne mieten möchte*.

Der **Kunde** gibt die `Mietdauer (in Tagen)` als Eigenschaft der Reservation an.

> Dabei werden die `Gesamtkosten` berechnet.

Wir nehmen an, dass die Eigenschaft `Gesamtkosten` aus der `Klasse` der **Reservation** und aus der `Mietdauer (in Tagen)` abgeleitet wird.
Ausserdem nehmen wir an, dass die `Gesamtkosten` linear zur `Mietdauer (in Tagen)` steigt.
Wir lassen uns aber die Möglichkeit offen, eine nicht-lineare Gesamtkostenrechnung hinzu zu fügen.

> Wird die **Reservation** gespeichert, so wird sie mit einer `Reservationsnummer` abgelegt.

Die Entität **Reservation** verfügt über eine Eigenschaft `Reservationsnummer`.
Wir nehmen an, dass diese Eigenschaft als eindeutige Identifikation fungieren soll.

---

> Bei Abholung des *Autos* wird die **Reservation** in einen **Mietvertrag** umgewandelt.

Der *Kunde* kann ein *Auto* abholen (Aktionsbeschreibung).
Dabei wird die **Reservation** durch einen **Mietvertrag** ersetzt.
Wir nehmen an, dass durch die Abholung die Eigenschaften der **Reservation** auf den **Mietvertrag** übernommen werden.
Dadurch verfügt der **Mietvertrag** über die gleichen Eigenschaften wie die **Reservation**.
Wir halten uns die Möglichkeit offen, sowohl auf dem **Mietvertrag**, als auch der **Reservation** neue Eigenschaften hinzu zu fügen.

## Arbeitspakete
Für unsere Arbeit sind bereits einige Arbeitspakete definiert.

### Arbeitspaket 1 - Big Picture
> Erstellen Sie mittels [C4-Pattern](6) das Big-Picture des Systems.

**Erwartetes Resultat**:
- Ein vier-stufiges Diagram im C4-Pattern, welches die Software CarRent definiert und den definierten Anforderungen entspricht.

### Arbeitspaket 2 - Domain Model und Use Cases
> Erstellen Sie das Domain Model und schreiben Sie alle Use Cases im "Brief"-Format auf.

**Erwartetes Ergebnis**:
- Das [Domain Model][1] als UML-Diagramm
- Die [Use Cases][7] im [Brief-Format][8]

### Arbeitspaket 3 - 4+1 Views
> Erstellen Sie ein Deployment Diagramm mithilfe des Container Diagramm des Big Picture.\
> Erstellen Sie ein Komponentendiagramm und ein Klassendiagramm mithilfe des Domänenmodells.

**Erwartetes Resultat**:
- Deployment Diagramm das sich am Container-Teil des Big Picture Diagramm orientiert.
- Komponenten- und Klassendiagramm das sich am Domänenmodell orientiert.

### Arbeitspaket 4 - Implementierung
> Implementieren Sie das System mithilfe der Anforderungen und den verschiedenen [Architektur-Views][9].

**Erwartetes Resultat**:
- Source Code der Software "CarRent" das sich an den bisherigen Arbeitspaketen orientiert.

### Arbeitspaket 5 - Continuous Integration und Metriken
> Hier soll das Erstellen der JAR- und WAR-Dateien, sowie weiteren Deployables automatisch nach jedem check-in gebaut und getestet werden.
> Zudem soll das Build-System weitere Metriken (code-coverage, code-quality, ...) messen und darstellen.

**Erwartetes Resultat**:
- Gemäss Aussage aus dem Unterricht ist dieses Arbeitspaket nicht nötig und wird daher nicht beachtet.

### Arbeitspaket 6 - Dokumentation (nach arc42)
> Jedes System braucht gewisse Dokumentation.
> Erstellen Sie eine Dokumentation mit Markdown, die sich ungefähr an die Vorgaben des arc42 halten.

**Erwartetes Resultat**:
- Dokumentation als MD-Datei(en) die sich nach [arc42][10] orientiert.

**Annahmen**:
- Die Dokumentation ist ein Benutzerhandbuch.


[1]: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design
[2]: https://restfulapi.net
[3]: http://www.erp-selection.ch/microservices-vs-monolithen-welche-ist-die-richtige-architektur-fuer-ihre-business-software/
[4]: https://www.codecademy.com/articles/what-is-crud
[5]: https://de.wikipedia.org/wiki/CRUD
[6]: https://c4model.com
[7]: https://www.usability.gov/how-to-and-tools/methods/use-cases.html
[8]: http://www.utm.mx/~caff/doc/OpenUPWeb/openup/guidances/guidelines/use_case_formats_FF4AE425.html
[9]: https://de.wikipedia.org/wiki/4%2B1_Sichtenmodell
[10]: https://arc42.de
