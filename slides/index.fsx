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
---

### ¿Qué falta en este diseño?

---
*)
type Contact' = {

  FirstName: string
  MiddleInitial: string
  LastName: string

  EmailAddress: string
  IsEmailVerified: bool  // true if ownership of email address is confirmed

  }
(**

<div class="fragment">

¿Qué valores son opcionales, son todos obligatorios?

</div>
<div class="fragment">

¿Cuáles son las restricciones?

</div>
<div class="fragment">

¿Hay algunos valores que estén releacionados?

</div>
<div class="fragment">

¿Hay alguna **lógica de dominio** que tenga que conocer?

</div>

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