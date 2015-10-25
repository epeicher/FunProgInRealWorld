(**
- title : FunProgInRealWorld
- description : Introducción a la Programación Funcional
- author : Roberto Aranda López
- theme : moon
- transition : default

***

## Programación Funcional en el Mundo Real
### Introducción a F#

<img style="border: none" src="images/logo.png" alt="Logo" />

Roberto Aranda López

[@glomenar](http://www.twitter.com/glomenar)  
ese.rober@gmail.com

***

## Programación Funcional
- Qué es
- Por qué me debería interesar

---

### ¿Qué es Programación Funcional?
<section data-markdown>
    <script type="text/template">
* Un paradigma de Programación: Una manera de construir programas. <!-- .element: class="fragment" data-fragment-index="1" -->
* Evita cambios de estado. Estructuras de datos inmutables. No hay asignaciones.       <!-- .element: class="fragment" data-fragment-index="2" -->
* Se utilizan funciones sin efectos laterales: Referencia transparencial. <!-- .element: class="fragment" data-fragment-index="3" -->
* Funciones de alto nivel: Aceptan o devuelven funciones.           <!-- .element: class="fragment" data-fragment-index="4" -->

> Las funciones son ciudadanos de primera clase                   <!-- .element: class="fragment" data-fragment-index="5" -->
    </script>
</section>

***

### ¿Por qué me debería interesar la Programación Funcional?
<section data-markdown>
    <script type="text/template">
* La ausencia de efectos laterales implica que es mas simple:   <!-- .element: class="fragment" data-fragment-index="1" -->
    - Paralelización                                            <!-- .element: class="fragment" data-fragment-index="2" --> 
    - Refactorización                                           <!-- .element: class="fragment" data-fragment-index="3" --> 
    - Reutilización                                             <!-- .element: class="fragment" data-fragment-index="4" -->
* El incremento en número de cores implica la necesidad de concurrencia. <!-- .element: class="fragment" data-fragment-index="5" -->
* Lenguajes tradicionalmente imperativos usan características funcionales, por ejemplo LINQ, Expresiones Lambda        <!-- .element: class="fragment" data-fragment-index="6" -->
    </script>
</section>

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
let a = 2                       // No hay ";"
let b = "hola"                  // No hay declaración de tipos
let f x = x + 2                 // Una función
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
Aparte de los tipos básicos
*)
type tupla = string*int
type myRegistro = { Nombre:string; Edad:int }
type discriminatedUnion = 
    | On
    | Off
    | Inservible of string

type ClasePersona(Nombre:string, Edad:int) =

    let mutable peso = 80.0

    member this.Nombre = Nombre
    member this.Edad = Edad
    member this.Peso = peso

    member this.GetAñodeNacimiento() = System.DateTime.Now.Year - Edad
    member this.PierdePeso(value) = 
        peso <- peso - value
        peso

(**
---

## Uso 
*)
let valorTupla = "John",14
let valorRegistro = {Nombre="John"; Edad=14}
let valorUnionType = On
let otroValorUnionType = Inservible "quemado"
let instanciaPersona = ClasePersona("John", 14)

(**
---

## *Pattern matching*
*)
let enciendeEsteInterruptor unInterruptor = 
    match unInterruptor with
    | On -> "Ya estaba Encendido"
    | Off -> "Encendido"
    | Inservible s -> "El Interruptor está " + s

(**
---

## Currying y Aplicaciones Parciales
<section data-markdown>
    <script type="text/template">
- Currying: Transforma una función con múltiples argumentos en una función con un argumento   <!-- .element: class="fragment" data-fragment-index="1" -->
- En F# todas las funciones estan currificadas por defecto                                    <!-- .element: class="fragment" data-fragment-index="2" -->
- Se puede llamar a una función con menos argumentos de los que espera                          <!-- .element: class="fragment" data-fragment-index="3" -->
- Se devuelve una función que acepta los argumentos restantes                                   <!-- .element: class="fragment" data-fragment-index="4" -->
</script>
</section>
*)

let convierteEuros tasaCambio dinero = dinero * tasaCambio
let convierteEurosADolares = convierteEuros 1.12
let convierteEurosAPesetas = convierteEuros 166.386

(**
---

## Composición
Crear nuevas funciones a partir de otras existentes.

> **Operador composición:**  
 \>>

*)
let grita (s:string) = s.ToUpper()
let exclama s = s + "!"

let anima = exclama >> grita

let textoPancarta = anima "vamos rafa"

(*** include-value: textoPancarta ***)
(**
---

## *Pipelining*
Enlaza la salida de una función con la entrada de otra.

> ** Operador *Pipe*:**  
|>
*)
let duplica x = 2 * x
let triplica y = 3 * y

let aplicaOperaciones x = x |> duplica |> triplica |> float |> sqrt

let resultado = aplicaOperaciones 24;;

(*** include-value: resultado ***)
(**
---

## Proveedores de tipos
### Problemas que resuelve
- Los datos crudos no son tipos
- Necesitamos conocer los nombres de las propiedades
- Enorme cantidad de fuentes de datos

---

### Cómo
- Mapean diferentes fuentes de datos a tipos de F#
- Se producen a demanda
- Representan las propiedades de los datos
- Se adaptan a los cambios de esquema
- Ejemplos en las Demos

---

## Programación Asíncrona

*)
open System.Net
open System
open System.IO

let extractLinksAsync html =
    async {
        return System.Text.RegularExpressions.Regex.Matches(html, @"http://\S+""")
    }
    
let downloadAndExtractLinks url =
    async {
        let webClient = new System.Net.WebClient()
        let! html = webClient.AsyncDownloadString(Uri(url))
        let! links = extractLinksAsync html
        return url,links.Count
    }

let links = downloadAndExtractLinks "http://www.google.com/"

let ls = Async.RunSynchronously links
(**
---

## Programación Paralela
*)
let downloadGoogleLinks = downloadAndExtractLinks "http://www.google.com/"
let downloadWikipediaLinks = downloadAndExtractLinks "http://www.wikipedia.com/"

[downloadGoogleLinks; downloadWikipediaLinks] 
    |> Async.Parallel
    |> Async.RunSynchronously 
(**

***

# Demos

***

### Conectándonos al Banco Mundial en 5 minutos

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
        - ¿Hay algunos valores que estén relacionados? <!-- .element: class="fragment" data-fragment-index="4" -->
        - ¿Hay alguna lógica de dominio que tenga que conocer? <!-- .element: class="fragment" data-fragment-index="5" -->
    </script>
</section>

---

*)
module ContactExample = 
    type String1 = String1 of string
    type String50 = String50 of string
    type EmailAddress = EmailAddress of string

    type PersonalName = {
        FirstName: String50
        MiddleInitial: String1 option
        LastName: String50 }

    type VerifiedEmail = VerifiedEmail of EmailAddress

    type EmailContactInfo = 
        | Unverified of EmailAddress
        | Verified of VerifiedEmail

    type Contact = {
        Name: PersonalName 
        Email: EmailContactInfo }
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