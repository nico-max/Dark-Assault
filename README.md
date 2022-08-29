# Dark Assault

## Members:
- Benjamin Meza
- Boris León


## Game Overall

Planteamos un juego de miedo en el que se jugara bastante con la luz y la oscuridad. Inicialmente es un juego en primera persona cuya mecanica principal se encuentra en que el jugador debera superar una serie de niveles basados en habitaciones contiguas, en las que en cada habitacion se tendra un circuito que superar mientras se mantiene dentro del faro de luz correspondiente al nivel. Dentro de cada nivel en la entrada aparecera un checkpoint y un faro contiguo, en el cual una vez se pare empezará a moverse en dirección al siguiente nivel. El jugador deberá mantenerse dentro de este puesto que si sale, muere instantaneamente y vuelve al ultimo checkpoint.

(En desarrollo)
De igual manera el jugador contará únicamente a su dispocición con una linterna. Esta linterna tendra una bateria que se descargará con el tiempo de uso que se le de. La linterna viene equipada con dos modos, el modo de luz normal, que se puede encender y apagar, y un gran destello de luz que consume una cantidad de bateria constante y que contará con un tiempo de enfriamiento. Cabe resaltar que durante estetiempo de enfriamiento generado por el destello de luz la linterna quedara inutilizada para ambos modos.

También se contará con enemigos en el mapa, específicamente dos tipos de enemigos: 

El primero, un enemigo mortal el cual seguirá al jugador todo el tiempo a lo largo de los diversos niveles y habitaciones. Este enemigo tendrá dos efectos importantes en cada intento. En primer lugar, si el enemigo toca directamente al jugador, será GameOver para el mismo, pero en este caso lo devolverá al comienzo del nivel, no al ultimo checkpoint. Por otro lado, el radio de la luz del faro en el que se encuentra el jugador es vital para este, y si este enemigo entra dentro de la luz, esta comenzara a reducirce, volviendose cada segundo que pase el enemigo mortal dentro, cada vez mas pequeña. 

(En desarrollo)
El segundo tipo de enemigo sera uno que puede aparecer en gran numero a lo largo de cada habitacion. Estos seran enemigos que atormentaran al jugador a lo largo del nivel. Mientras el jugador avanza con la luz, estos enemigos pueden aparecer alrededor desde la oscuridad, generando un sonido que alerte al jugador de su presencia. Estos enemigos desapareceran al ser apuntados con la linterna. Sin embargo, si no se reacciona velozmente a ellos, pueden generar por probabilidad dos clases de efectos: el primero sera un pequeño 'stun' o aturdimiento, que dejara al jugador incapacitado por un breve periodo de tiempo. El segundo, una reducción porcentual del halo de luz en el que se encuentra. 

El objetivo final del jugador será escapar de este extraño mundo superando los diferentes circuitos que se le presentan y, claro esta, no dejar que lo atrape este enemigo mortal.

## Controls

Movimiento - WASD

Salto - Space

Encender/Apagar Linterna - Left Click

Super-destello - Right Click
