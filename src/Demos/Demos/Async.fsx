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

(*
    PROGRAMACION PARALELA
*)
let downloadGoogleLinks = downloadAndExtractLinks "http://www.google.com/"
let downloadWikipediaLinks = downloadAndExtractLinks "http://www.wikipedia.com/"

// Serie

let workflowInSeries = async {
    let! first = downloadGoogleLinks
    printfn "Finished one" 
    let! second = downloadWikipediaLinks
    printfn "Finished two" 
    }

#time
Async.RunSynchronously workflowInSeries 
#time


// Paralell
#time
[downloadGoogleLinks; downloadWikipediaLinks] 
    |> Async.Parallel
    |> Async.RunSynchronously 
#time

