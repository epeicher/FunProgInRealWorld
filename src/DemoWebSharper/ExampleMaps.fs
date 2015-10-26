namespace DemoWebSharper

open WebSharper
open WebSharper.Html.Client
open WebSharper.Google
open WebSharper.Google.Maps

[<JavaScript>]
module ExampleMaps =  
    open WebSharper.JavaScript

    let Sample buildMap =
        Div [Attr.Style "padding-bottom:20px; width:500px; height:300px;"]
        |>! OnAfterRender (fun mapElement ->
            let center = new Maps.LatLng(36.6991218, -4.4384684)
            let options = new Maps.MapOptions(center, 8)
            let map = new Maps.Map(mapElement.Dom, options)
            buildMap map)

    let SimpleMap() = 
        Sample <| fun (map: Map) ->
            let latLng = new LatLng(36.6980949,-4.4390111)
            let options = new MapOptions(latLng, 16)
            map.SetOptions options

    /// Since it's not available in v3. We make it using the ImageMapType
    /// Taken from: http://code.google.com/p/gmaps-samples-v3/source/browse/trunk/planetary-maptypes/planetary-maptypes.html?r=206
    let Moon() =
        Sample <| fun map ->
            //Normalizes the tile URL so that tiles repeat across the x axis (horizontally) like the
            //standard Google map tiles.
            let getHorizontallyRepeatingTileUrl(coord: Point, zoom: int, urlfunc: (Point * int -> string)) : string =
                let mutable x = coord.X
                let y = coord.Y
                let tileRange = float (1 <<< zoom)
                if (y < 0. || y >= tileRange)
                then null
                else
                    if x < 0. || x >= tileRange
                    then x <- (x % tileRange + tileRange) % tileRange
                    urlfunc(new Point(x, y), zoom)

            let itOptions = new ImageMapTypeOptions()

            itOptions.GetTileUrl <-
                (fun _ (coord, zoom) ->
                    getHorizontallyRepeatingTileUrl (coord, zoom,
                        (fun (coord, zoom) ->
                            let bound = Math.Pow(float 2, float zoom)
                            ("http://mw1.google.com/mw-planetary/lunar/lunarmaps_v1/clem_bw/"
                              + (string zoom) + "/" + (string coord.X) + "/" + (string (bound - coord.Y - 1.) + ".jpg")))))

            itOptions.TileSize <- new Size(256., 256.)
            itOptions.MaxZoom <- 9
            itOptions.MinZoom <- 0
            itOptions.Name <- "Moon"

            let it = new ImageMapType(itOptions)
            let center = new LatLng(0., 0.)
            let mapIds = [| As "Moon" |]
            let mapControlOptions =
                let mco = new MapTypeControlOptions()
                mco.Style <- MapTypeControlStyle.DROPDOWN_MENU
                mco.MapTypeIds <- mapIds
                mco

            let options = new MapOptions(center, 0, MapTypeId = mapIds.[0])
            options.MapTypeControlOptions <- mapControlOptions
            map.SetOptions options
            map.MapTypes.Set("Moon", it) 
            // TODO: Add the credit part
            ()
