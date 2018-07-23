# CosmicWeather

Aplicación que crea proyecciones del clima y permite consultarlas a través de una API.
* [Enunciado](https://drive.google.com/open?id=0B3VV0CP-a1Ssa1JTQlRqUFdTZzhhTjgxbVhqQUd2X3pOLXhj)

## Contenido

* Aplicación de consola que genera modelo y lote de datos.
* API para consultar la base de datos.

## Tecnologías utilizadas

* .NET Framework 4.6.1
* Entity Framework
* SQL Server
* Azure

## Hosting

La aplicación de consola, la API y la base de datos se encuentran hosteadas en un servidor de Azure.
* [API](https://cosmicweather.azurewebsites.net/)

## Aplicación de consola

Dicha aplicación soporta dos parámetros para su ejecución: número de años a proyectar y cantidad de decimales a utilizar en los cálculos (este último parámetro modifica ampliamente los resultados obtenidos).
En caso de no especificarse alguno de ellos o ninguno, se ejecutará una proyección a 10 años con precisión igual a 4 decimales.

Ejemplo de ejecución: CosmicWeather.exe 10 4

## Estado inicial y uso de la API

La BD se encuentra cargada, resultado de la ejecución de la aplicación de consola.
Los métodos soportados por la API son los siguientes:

Obtener los climas de todos los días proyectados
* [GET WEATHERS](https://cosmicweather.azurewebsites.net/api/weathers)

Obtener el clima del día solicitado
* [GET DAY WEATHER](https://cosmicweather.azurewebsites.net/api/weathers/5)

Obtener la cantidad de períodos del clima solicitado
* Valores posibles: lluvia, sequia, optimo y normal

* [GET WEATHER PERIODS](https://cosmicweather.azurewebsites.net/api/weathers/periods/lluvia)

Obtener el día de lluvia máxima
* [GET MAX RAIN DAY](https://cosmicweather.azurewebsites.net/api/weathers/periods/lluvia/max)



## TO-DO

* Proyecto de test (unitario e integración).
* Funcionalidad para dropear la BD.
* Páginas de error en la API.
