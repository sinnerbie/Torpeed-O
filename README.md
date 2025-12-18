# Objetivo:
Utilizando como base un juego programado previamente donde el jugador controla un torpedo e intenta esquivar varias obstáculos; hacer que el jugador puede controlar el torpedo con gestiones físicas mediante dos sensores ultrasónicos paralelos.
# Equipo:
Este proyecto se está desarrollando por un único estudiante.
# Plan de sprints
Intentar aumentar la funcionalidad de los controles de gestión y aumentar la jugabilidad hasta que sea igual o mejor que los controles de teclado.

# Seguimiento del proyecto
## 11 de Nov 2025 
Compartido el repositiorio del proyecto "Torpeed-O" con el profesor.
## 18 de Nov 2025 
Creado script "ArduinoControl" para leer el serial monitor del arduino y pasarlo a variables del juego.
## 20 de Nov 2025
Circuito Arduino conectado correctamente a Unity, inputs de los sensores y el potenciometro convertido en variables sin problema de lección, control del desplazamiento horizontal del jugador con los sensores funcional pero no optimo; los sensores no mandan la velocidad del desplazamiento y la sistema de pasar los datos del Arduino a Unity causa bajadas del framerate significativas (entre 5 y 20 FPS).
## 25 de Nov 2025
Resuelto la bajada de framerate utilizando la sistema de Threads de Unity para leer el serial monitor en el fondo de la programa para que no ocupa todo el RAM, implementado un umbral de distancia donde el torpedo no moverá y que no se cambia de dirección sin querer, los animaciones del jugador se activan con los controles de arduino.
## 2 de Dic 2025
Mejorado controles de movimiento horizontal para ir más rápido dependiendo de cuánto de cerca se ha puesto la mano del jugador. La velocidad general del juego también se ajusta con las manos, si las dos están por debajo de 10cm de distancia al sensor, se aumentará la velocidad del juego. El minijuego tambien se puede controlar con un potenciometro pero falta incluir un botón para activarlo.
## 11 de Dic 2025
Incorporado la utilización de un botón en el circuito arduino para confirmar la selección del cofre en el minijuego. Se ha ajustado el umbral máximo del potenciometro para asegurar que se puede seleccionar el útlimo cofre. Ajustado la posición del pivote del torpedo para mejorar el feedback del cambio de dirección.

# Resolución de Problemas
* El _framerate_ bajó por menos de 10 fotogramas por segundo debido a la lección del serial del circuito arduino. Esto se resolvió utilizando la sistema de _threads_ de Unity, que permite recorrer una función pesada en el fondo de la programa para mejorar la optimización del código.
* Un evento que se llama desde el script que lee el serial del arduino no se llamaba correctamente. Esta problema prevenió la lección precisa del potenciometro causando que no se podía cambiar la selección del cofre en el minijuego. La causa fué la sistema de _threads_ de Unity, que no permite la ejecución de eventos desde el fondo. Se resolvió con una función en el _FixedUpdate_ del script que referencia el circuito de arduino para pasar el valor del potenciometro al script del minijuego solo si el script del minijuego no sea nulo.

# Tareas
## Acabadas:
* Bucle del circuito arduino que escribe los valores de los sensores, el potenciometro y el botón a la consola del circuito.
* Lección de la consola serial del arduino por Unity.
* Conversión del texto de la consola a variables de Unity.
* Desplazamiento horizontal con los valores de los sensores ultra-sónicos.
* Subida (y bajada) de velocidad con los sensores ultra-sónicos.
* Selección de cofre del minijuego con el potenciometro.
* Confirmación de selección de cofre con el botón del circuito.
* Ajustar umbral del potenciometro para no saltar el cofre último.
* Ajustar velocidad de animación de giro del torpedo.
* Ajustar acceleración de desplazamiento horizontal del torpedo.
## Por hacer:
* Nada :)
# Resultado


https://github.com/user-attachments/assets/9090d655-1eb5-4375-bd06-760e23893fc8

