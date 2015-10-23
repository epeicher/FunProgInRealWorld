
// First, reference the locations where F# Data and 
// F# Data Toolbox are located (using '#I' is required here!)
#I @"../packages/FSharp.Data.Toolbox.Twitter.0.9/lib/net40"
#I @"../packages/FSharp.Data.2.2.5/lib/net40"

// The Twitter reference needs to come before FSharp.Data.dll
// (see the big warning box below for more!)
#r "FSharp.Data.Toolbox.Twitter.dll"
#r "FSharp.Data.dll"
open FSharp.Data.Toolbox.Twitter

let key = "FNlSAC5VD9zI7011InVKfQ"
let secret = "S0VktxiFZ2JN2BUPQAFRbkstTsGTYHaehIJyhg0Vg"

let connector = Twitter.Authenticate(key, secret) 

let twitter = connector.Connect("7554814")

open System.Windows.Forms
open FSharp.Control
open FSharp.WebBrowser

// Create a windonw wtih web browser
let frm = new Form(TopMost = true, Visible = true, Width = 500, Height = 400)
let btn = new Button(Text = "Pause", Dock = DockStyle.Top)
let web = new WebBrowser(Dock = DockStyle.Fill)
frm.Controls.Add(web)
frm.Controls.Add(btn)
web.Output.StartList()

// Display timeline
let timeline = twitter.Timelines.Timeline("dotnetmalaga")
for tweet in timeline do
    web.Output.AddItem "<strong>%s</strong>: %s" tweet.User.Name tweet.Text

// Display live search data 
web.Output.StartList()

let search = twitter.Streaming.FilterTweets ["fsharp"]
search.TweetReceived |> Observable.guiSubscribe (fun status ->
    match status.Text, status.User with
    | Some text, Some user ->
        web.Output.AddItem "<strong>%s</strong>: %s" user.Name text
    | _ -> ()  )
search.Start()

search.Stop()