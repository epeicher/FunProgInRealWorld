#r "../packages/FSharp.Data.2.2.5/lib/net40/FSharp.Data.dll"
#load "../packages/FSharp.Charting.0.90.12/FSharp.Charting.fsx"

open FSharp.Data
open FSharp.Charting

let data = WorldBankData.GetDataContext()

let pib = data.Countries.Spain.Indicators.``GDP (constant 2005 US$)``
Chart.Line pib
let unemp = data.Countries.Spain.Indicators.``Unemployment, total (% of total labor force) (national estimate)``
Chart.Line unemp

let normalize l =
    let min = Seq.min l
    let max = Seq.max l
    let m = Seq.average l
    Seq.map (fun x -> (x - m) / (max - min)) l

let negate =
    Seq.map (fun x -> -1.*x)

let pibNorm = normalize pib.Values
let unempNorm = normalize <| negate unemp.Values

let pibZipped = Seq.zip pib.Years pibNorm
let unempZipped = Seq.zip unemp.Years unempNorm

Chart.Combine(
    [ Chart.Line (pibZipped, Name=pib.Name)
      Chart.Line (unempZipped, Name=unemp.Name) ])