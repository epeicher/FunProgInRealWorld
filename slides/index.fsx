﻿(**
- title : FunProgInRealWorld
- description : Introducción a la Programación Funcional
- author : Roberto Aranda López
- theme : moon
- transition : default

***

## Programación Funcional
- Qué es
- Por qué me debería interesar

---

### ¿Qué es Programación Funcional?
* Paradigma de Programación Declarativo: Qué hacer, no cómo hacerlo. Realizado con expressiones en contraste a estamentos.
* Se utilizan funciones para transformar los datos. Dada una entrada, siempre se produce la misma salida. 
* No se producen efectos laterales. Existe lo que se denomina *referencia transparencial*.
* Estructuras de datos inmutables. No se cambia el estado. No hay variables que cambien su valor.

---

### ¿Por qué me debería interesar la Programación Funcional?
* La ausencia de efectos laterales provoca:
    - Fácilmente paralelizables ya que el orden de ejecución de funciones no importa.    
    - Refactorizar es más simple.
    - Funciones de alto nivel son mucho más fácilmente reutilizables.
* El incremento en número de *cores* implica la necesidad de concurrencia. 
En este aspecto los lenguajes funcionales destacan sobre los imperativos.

***

## Introducción a F#
- Un poquitín de historia
- Por qué F#
- Características principales

---

### Breve contexto histórico
- En 1970 se desarrolla el lenguaje ML *(Meta Language)* en la Universidad de Edinburgh con fines académicos.
- Surgen los dialectos CAML *(Categorical Abstract Machine Language)* en Francia.
- En 2005, Don Syme a través del *Microsoft Research Cambridge* implementa F# basándose en la familia de lenguages CAML.

---

# Enséñame el código

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

---

*)
module JuegoCartasBoundedContext = 

    type Palo = Copas | Bastos | Espadas | Oros
                // | significa una elección -- toma uno de la lista
                
    type Valor = Uno | Dos | Tres | Cuatro | Cinco | Seis | Siete 
                | Sota | Caballo | Rey

    type Carta = Palo * Valor   // * significa un par -- uno de cada tipo
    
    type Mano = Carta list
    type Baraja = Carta list
    
    type Jugador = {Nombre:string; Mano:Mano}
    type Juego = {Baraja:Baraja; Jugadores: Jugador list}
    
    type Reparte = Baraja -> (Baraja * Carta) // X -> Y es una función
                                      // entrada de tipo X
                                      // salida de tipo Y

    type CogeCarta = (Mano * Carta)-> Mano

(**
---
*)
type Contact = {

  FirstName: string
  MiddleInitial: string
  LastName: string

  EmailAddress: string
  IsEmailVerified: bool  // true if ownership of email address is confirmed

  }
(**
<section data-markdown>
    <script type="text/template">
        ### ¿Qué falta en este diseño? <!-- .element: class="fragment" data-fragment-index="1" -->
        - ¿Qué valores son opcionales, son todos obligatorios? <!-- .element: class="fragment" data-fragment-index="2" -->
        - ¿Cuáles son las restricciones? <!-- .element: class="fragment" data-fragment-index="3" -->
        - ¿Hay algunos valores que estén releacionados? <!-- .element: class="fragment" data-fragment-index="4" -->
        - ¿Hay alguna lógica de dominio que tenga que conocer? <!-- .element: class="fragment" data-fragment-index="5" -->
    </script>
</section>

---

*)
module VerifiedEmailExample = 
    type String1 = String1 of string
    type String50 = String50 of string
    type EmailAddress = EmailAddress of string

    type PersonalName = {
        FirstName: String50
        MiddleInitial: String1 option
        LastName: String50 }

    type VerifiedEmail = VerifiedEmail of EmailAddress
    type VerificationHash = VerificationHash of string
    type VerificationService = 
        (EmailAddress * VerificationHash) ->  VerifiedEmail option

    type EmailContactInfo = 
        | Unverified of EmailAddress
        | Verified of VerifiedEmail

    type Contact = {
        Name: PersonalName 
        Email: EmailContactInfo }
(**
---

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