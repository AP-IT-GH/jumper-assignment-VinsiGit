# Opdacht: Jumper
## Namen:
Joshua Hall: s1399133 

Jarn Vaerewijck: s144013

## Documentatie:
...
De bedoeling van dit project is om een stilstaande agent te laten springen over een bewegend obstakel.  
Voor dit project moest er ook een extra functionaliteit gekozen worden, wij hebben gekozen om de agent obstakels van 2 kanten te laten ontwijken.  
Hiervoor hebben we eerste een prefab gemaakt bestaand uit een plane, de agent en 2 obstakels.  
Alle bewegingen worden gestuurd vanuit de JumperAgent script op de agent, deze reset de locatie van de obstakels en geeft ze een random snelheid aan het begin van elke episode.  
De obstakels bewegen dan naar de agent toe over de x- en z-as, als deze de agent aan raken krijgt die een negatieve score, als ze de agent niet aan raken en ze te ver gaan krijgt de agent een positieve score.  
De enige actie die de agent kan uitvoeren is springen, de agent beschikt ook over de locatie van de obstakels.