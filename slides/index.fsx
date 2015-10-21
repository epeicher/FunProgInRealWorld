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
* Expressiones en contraste a estamentos.
* Se utilizan funciones sin efectos laterales: *Referencia transparencial*.
* Estructuras de datos inmutables. 
* Funciones de alto nivel: Aceptan o devuelven funciones.

> Las funciones son *ciudadanos de primera clase*

---

### ¿Por qué me debería interesar la Programación Funcional?
* La ausencia de efectos laterales implica:
    - Fácilmente paralelizables ya que el orden de ejecución de funciones no importa.    
    - Refactorizar es más simple.
    - Funciones de alto nivel son mucho más fácilmente reutilizables.
* El incremento en número de *cores* implica la necesidad de concurrencia. 
En este aspecto los lenguajes funcionales destacan sobre los imperativos.

***

# Introducción a F#

---

## Breve contexto histórico
- En 1970 se desarrolla el lenguaje ML *(Meta Language)* en la Universidad de Edinburgh con fines académicos.
- Surgen los dialectos CAML *(Categorical Abstract Machine Language)* en Francia.
- En 2005 Don Syme a través de *Microsoft Research Cambridge* publica F# un lenguaje para .NET basado en la familia de lenguages CAML.

---

## ¿Por qué F#?
- Interoperable con la plataforma .NET
- Multiplataforma
- Código Abierto
- Conciso
- Robusto
- Concurrente
- Tiene un *REPL*!

---

## Características Principales
- Fuertemente tipado con inferencia de tipos
- Multiparadigma
- **Proveedores de tipos**: *Information Rich Programming*
- Programación Asíncrona
- Programación Paralela
- Expresiones Computacionales
 
***

# Enséñame el código

---

### Sintaxis e Inferencia de tipos
*)
let f x = x + 2
let f x = x + 2.0
let f x = x + "Hey you"
let f x y =                     // Parámetros separados por espacios
    let z = x**2.0 + y**2.0     // No hay llaves, se utiliza la indentación
    sqrt z                      // Valor de retorno

(**
---

# Y el Hola Mundo!?

---

## Ups, lo había olvidado
*)
printf "Hola Mundo"
(**
---

### Definición de tipos
*)
type tuple = int*string
type record = { Nombre:string; Edad:int }
type discriminatedUnion = 
    | On
    | Off
    | Disabled of string
(**
---

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