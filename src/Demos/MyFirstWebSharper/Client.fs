namespace MyFirstWebSharper

open WebSharper
open WebSharper.JavaScript
open WebSharper.Html.Client
open WebSharper.Google
open WebSharper.Google.Maps
open ExampleMaps
open ExampleD3

[<JavaScript>]
module Client =

    let Start input k =
        async {
            let! data = Server.DoSomething input
            return k data
        }
        |> Async.Start

    let Main () =
        let input = Input [Attr.Value ""]
        let output = H1 []
        Div [
            input
            Button [Text "Send"]
            |>! OnClick (fun _ _ ->
                async {
                    let! data = Server.DoSomething input.Value
                    output.Text <- data
                }
                |> Async.Start
            )
            HR []
            H4 [Attr.Class "text-muted"] -< [Text "The server responded:"]
            Div [Attr.Class "jumbotron"] -< [output]     
            Div [Attr.Class "row"]
            -< [
                Div [Attr.Class "col-md-6"] -< [ SimpleMap() ]
                Div [Attr.Class "col-md-6"] -< [ Moon() ]                
            ] 
            HR []
            //ExampleD3.Main()
        ]        
