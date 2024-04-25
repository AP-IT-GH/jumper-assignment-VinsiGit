# Opdacht: Jumper
## Namen:
Joshua Hall: s1399133 

Jarn Vaerewijck: s144013

## Documentatie:
Wij kozen voor 'de agent staat op het midden van een kruispunt en moet obstakels vermijden die vanuit 2 richtingen kunnen komen'.
![image](https://github.com/AP-IT-GH/jumper-assignment-VinsiGit/assets/40694625/15dd3464-4ee7-4458-8360-58bb3a6aacb2)


### config file:
```yaml
behaviors:
  JumperAgent:
    trainer_type: ppo
    hyperparameters:
      batch_size: 10
      buffer_size: 100
      learning_rate: 1.0e-3
      beta: 5.0e-4
      epsilon: 0.2
      lambd: 0.99
      num_epoch: 3
      learning_rate_schedule: linear
      beta_schedule: constant
      epsilon_schedule: linear
    network_settings:
      normalize: false
      hidden_units: 128
      num_layers: 2
    reward_signals:
      extrinsic:
        gamma: 0.99
        strength: 1.0
    max_steps: 200000
    time_horizon: 64
    summary_freq: 2000
```
### Opzet:
- Opzet: Een kruispunt waar een agent in het midden is, een obstakel kan van de x- of z-as komen en de agent kan springen.
- Doel: De agent moet over het obstakel springen.
- Agents:
  - Alle bewegingen worden gestuurd vanuit de JumperAgent script op de agent, deze reset de locatie van de obstakels en geeft ze een random snelheid aan het begin van elke episode.
  - De agent weet op elk moment zijn eigen positie en de postie van de obstakels.
- Agent Reward Functie:
  - +1.0 als de agent over het obstakel springt.
  - -1.0 als de agent het obstakel aanraakt.
- Gedrag Parameters:
  - Springen (2 mogelijke acties: springen, geen actie)
  - Visuele observaties: Geen

### Trainen:

![image](https://github.com/AP-IT-GH/jumper-assignment-VinsiGit/assets/40694625/40bcbf4c-e151-4ea2-a99f-8a742cd55fe4)


Bij het trainen is de minimale reward -1 en de maximale reward 1.
Als we de training starten zijn de eerste waarden vrijwel altijd rond de nul, wat zich vertaalt in een accuracy van ± 50%.
Dit is te verwachten van een agent die willekeurige acties neemt.
Als je naar de grafiek kijkt kan je zien dat de reward voor de eerste 30.000 stappen heel hard stijgt, het is pas dan dat de gemiddelde reward begint lineair te stijgt.

Na 200.000 stappen bereikt de training een mean reward van 0.5, wat een accuracy van rond de 70% geeft.


