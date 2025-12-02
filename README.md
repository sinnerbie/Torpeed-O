# Objetivo: 
Utilizando como base un juego programado previamente donde el jugador controla un torpedo e intenta esquivar varias obstáculos; hacer que el jugador puede controlar el torpedo con gestiones físicas mediante dos sensores ultrasónicos paralelos.
# Equipo: 
Este proyecto se está desarrollando por un único estudiante.
# Plan de sprints: 
Intentar aumentar la funcionalidad de los controles de gestión y aumentar la jugabilidad hasta que sea igual o mejor que los controles de teclado.

# Seguimiento del proyecto:
## 11 de Nov 2025 
Compartido el repositiorio del proyecto "Torpeed-O" con el profesor
## 18 de Nov 2025 
Creado script "ArduinoControl" para leer el serial monitor del arduino y pasarlo a variables del juego
## 20 de Nov 2025
Circuito Arduino conectado correctamente a Unity, inputs de los sensores y el potenciometro convertido en variables sin problema de lección, control del desplazamiento horizontal del jugador con los sensores funcional pero no optimo; los sensores no mandan la velocidad del desplazamiento y la sistema de pasar los datos del Arduino a Unity causa bajadas del framerate significativas (entre 5 y 20 FPS)
## 25 de Nov 2025
Resuelto la bajada de framerate utilizando la sistema de Threads de Unity para leer el serial monitor en el fondo de la programa para que no ocupa todo el RAM, implementado un umbral de distancia donde el torpedo no moverá y que no se cambia de dirección sin querer, los animaciones del jugador se activan con los controles de arduino
## 2 de Dic 2025
Mejorado controles de movimiento horizontal para ir más rápido dependiendo de cuánto de cerca se ha puesto la mano del jugador. La velocidad general del juego también se ajusta con las manos, si las dos están por debajo de 10cm de distancia al sensor, se aumentará la velocidad del juego. El minijuego tambien se puede controlar con un potenciometro pero falta incluir un botón para activarlo.
