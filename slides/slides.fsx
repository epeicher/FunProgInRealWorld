(**
- title : FunProgInRealWorld
- description : Introducción a la Programación Funcional
- author : Roberto Aranda López
- theme : moon
- transition : default

***

### Programación Funcional
- Qué es
- Por qué me debería interesar

---

### Introducción a F#
- Un poquitín de historia
- Por qué F#
- Características principales

---

### Enséñame el código
- Sintaxis 
- Inferencia de tipos
- Definición de tipos
- *Pattern matching*
- *Currying* y Aplicaciones Parciales
- Composición y *Pipelining*
- Proveedores de tipos
- Programación Asíncrona
- Expresiones Computacionales

***

# Demonstraciones

***

### Conectándonos al Banco Mundial en 5 minutos

---
*)
#r "../packages/FSharp.Data.2.2.5/lib/net40/FSharp.Data.dll"
#load "../packages/FSharp.Charting.0.90.12/FSharp.Charting.fsx"

open FSharp.Data
open FSharp.Charting

let data = WorldBankData.GetDataContext()

let pib = data.Countries.Spain.Indicators.``GDP (constant 2005 US$)``
Chart.Line pib
(**

***

### Diseño Orientado al Dominio

***

### Nuestras viejas amigas las Bases de Datos

***

### Voy a mandar un Tweet

***

### Qué es eso de *Machine Learning*

***

# Resumen

***

# ¿Preguntas?

*)