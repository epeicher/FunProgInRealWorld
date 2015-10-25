#r "System.Data"
#r "System.Data.Linq"
#r "FSharp.Data.TypeProviders"

open System
open System.Data
open Microsoft.FSharp.Data.TypeProviders

[<Literal>]
let connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db\DemoDatabase.mdf;Integrated Security=True"

type SqlConnection = SqlDataConnection<ConnectionString = connectionString>
let db = SqlConnection.GetDataContext()

//db.DataContext.Log <- Console.Out
//db.DataContext.Log <- null

let table = 
    query {
        for p in db.Proyectos do
        where (p.TipoProyecto.Tipo <> "Servicio" )
        select (p.Persona.Nombre, p.TipoProyecto.Tipo, p.Lenguaje.Nombre) }

for v in Seq.take 100 table do
    printfn "%A" v


// Insert some Random data
#r @"..\packages\FsCheck.2.1.0\lib\net45\FsCheck.dll"

let personas = List.map System.Nullable [1..8] 
let tipoProyectos = List.map System.Nullable [1..6] |> List.append [Nullable<int>()]
let lenguajes = List.map System.Nullable [1..9] |> List.append [Nullable<int>()]

let generateFirstName() = 
    FsCheck.Gen.elements personas 

// a function to create a customer
let creaProyecto personaId tipoProyectoId lenguajeId =
    let c = new SqlConnection.ServiceTypes.Proyectos()
    c.PersonaId <- personaId
    c.TipoProyectoId <- tipoProyectoId
    c.LenguajeId <- lenguajeId
    c //return new record

let generateProyectos = 
    creaProyecto 
    <!> (FsCheck.Gen.elements personas)
    <*> (FsCheck.Gen.elements tipoProyectos)
    <*> (FsCheck.Gen.elements lenguajes)

let insertAll() =
    use db = SqlConnection.GetDataContext()

    // optional, turn logging on or off
    // db.DataContext.Log <- Console.Out
    // db.DataContext.Log <- null

    let insertOne counter proyecto =
        db.Proyectos.InsertOnSubmit proyecto
        // do in batches of 1000
        if counter % 1000 = 0 then
            db.DataContext.SubmitChanges()

    // generate the records
    let count = 10000
    let generator = FsCheck.Gen.sample 0 count generateProyectos

    // insert the records
    generator |> List.iteri insertOne
    db.DataContext.SubmitChanges() // commit any remaining

// do it and time it
#time
insertAll() 
#time

