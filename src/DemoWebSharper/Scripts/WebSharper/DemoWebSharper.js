(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,Html,Client,Operators,List,Attr,Tags,T,Concurrency,Remoting,AjaxRemotingProvider,EventsPervasives,DemoWebSharper,ExampleMaps,Number,google,Math,Arrays;
 Runtime.Define(Global,{
  DemoWebSharper:{
   Client:{
    Main:function()
    {
     var input,arg10,x,output,arg101,arg102,x1,arg00,arg103,arg104,arg105,arg106,arg107,arg108,arg109;
     arg10=List.ofArray([Attr.Attr().NewAttr("value","")]);
     input=Operators.add(Tags.Tags().NewTag("input",arg10),Runtime.New(T,{
      $:0
     }));
     x=Runtime.New(T,{
      $:0
     });
     output=Tags.Tags().NewTag("h1",x);
     arg102=List.ofArray([Tags.Tags().text("Send")]);
     x1=Tags.Tags().NewTag("button",arg102);
     arg00=function()
     {
      return function()
      {
       return Concurrency.Start(Concurrency.Delay(function()
       {
        return Concurrency.Bind(AjaxRemotingProvider.Async("DemoWebSharper:0",[input.get_Value()]),function(_arg11)
        {
         output.set_Text(_arg11);
         return Concurrency.Return(null);
        });
       }),{
        $:0
       });
      };
     };
     EventsPervasives.Events().OnClick(arg00,x1);
     arg103=Runtime.New(T,{
      $:0
     });
     arg104=List.ofArray([Attr.Attr().NewAttr("class","text-muted")]);
     arg105=List.ofArray([Attr.Attr().NewAttr("class","jumbotron")]);
     arg106=List.ofArray([Attr.Attr().NewAttr("class","row")]);
     arg107=List.ofArray([Attr.Attr().NewAttr("class","col-md-6")]);
     arg108=List.ofArray([Attr.Attr().NewAttr("class","col-md-6")]);
     arg109=Runtime.New(T,{
      $:0
     });
     arg101=List.ofArray([input,x1,Tags.Tags().NewTag("hr",arg103),Operators.add(Tags.Tags().NewTag("h4",arg104),List.ofArray([Tags.Tags().text("The server responded:")])),Operators.add(Tags.Tags().NewTag("div",arg105),List.ofArray([output])),Operators.add(Tags.Tags().NewTag("div",arg106),List.ofArray([Operators.add(Tags.Tags().NewTag("div",arg107),List.ofArray([ExampleMaps.SimpleMap()])),Operators.add(Tags.Tags().NewTag("div",arg108),List.ofArray([ExampleMaps.Moon()]))])),Tags.Tags().NewTag("hr",arg109)]);
     return Tags.Tags().NewTag("div",arg101);
    },
    Start:function(input,k)
    {
     var arg00;
     arg00=Concurrency.Delay(function()
     {
      return Concurrency.Bind(AjaxRemotingProvider.Async("DemoWebSharper:0",[input]),function(_arg1)
      {
       return Concurrency.Return(k(_arg1));
      });
     });
     return Concurrency.Start(arg00,{
      $:0
     });
    }
   },
   ExampleMaps:{
    Moon:function()
    {
     var buildMap;
     buildMap=function(map)
     {
      var getHorizontallyRepeatingTileUrl,itOptions,it,center,mapIds,mco,mapControlOptions,options;
      getHorizontallyRepeatingTileUrl=function(tupledArg)
      {
       var coord,zoom,urlfunc,x,y,tileRange,_;
       coord=tupledArg[0];
       zoom=tupledArg[1];
       urlfunc=tupledArg[2];
       x=coord.x;
       y=coord.y;
       tileRange=Number(1<<zoom);
       if(y<0?true:y>=tileRange)
        {
         _=null;
        }
       else
        {
         (x<0?true:x>=tileRange)?x=(x%tileRange+tileRange)%tileRange:null;
         _=urlfunc([new google.maps.Point(x,y),zoom]);
        }
       return _;
      };
      itOptions={};
      itOptions.getTileUrl=function(coord,zoom)
      {
       return getHorizontallyRepeatingTileUrl([coord,zoom,function(tupledArg)
       {
        var coord1,zoom1,bound;
        coord1=tupledArg[0];
        zoom1=tupledArg[1];
        bound=Math.pow(Number(2),Number(zoom1));
        return"http://mw1.google.com/mw-planetary/lunar/lunarmaps_v1/clem_bw/"+Global.String(zoom1)+"/"+Global.String(coord1.x)+"/"+(Global.String(bound-coord1.y-1)+".jpg");
       }]);
      };
      itOptions.tileSize=new google.maps.Size(256,256);
      itOptions.maxZoom=9;
      itOptions.minZoom=0;
      itOptions.name="Moon";
      it=new google.maps.ImageMapType(itOptions);
      center=new google.maps.LatLng(0,0);
      mapIds=["Moon"];
      mco={};
      mco.style=google.maps.MapTypeControlStyle.DROPDOWN_MENU;
      mco.mapTypeIds=mapIds;
      mapControlOptions=mco;
      options={
       center:center,
       zoom:0,
       mapTypeId:Arrays.get(mapIds,0)
      };
      options.mapTypeControlOptions=mapControlOptions;
      map.setOptions(options);
      map.mapTypes.set("Moon",it);
      return null;
     };
     return ExampleMaps.Sample(buildMap);
    },
    Sample:function(buildMap)
    {
     var x,x1,f;
     x=List.ofArray([Attr.Attr().NewAttr("style","padding-bottom:20px; width:500px; height:300px;")]);
     x1=Tags.Tags().NewTag("div",x);
     f=function(mapElement)
     {
      var center,options,map;
      center=new google.maps.LatLng(36.6991218,-4.4384684);
      options={
       center:center,
       zoom:8
      };
      map=new google.maps.Map(mapElement.Dom,options);
      return buildMap(map);
     };
     Operators.OnAfterRender(f,x1);
     return x1;
    },
    SimpleMap:function()
    {
     var buildMap;
     buildMap=function(map)
     {
      var latLng,options;
      latLng=new google.maps.LatLng(36.6980949,-4.4390111);
      options={
       center:latLng,
       zoom:16
      };
      return map.setOptions(options);
     };
     return ExampleMaps.Sample(buildMap);
    }
   }
  }
 });
 Runtime.OnInit(function()
 {
  Html=Runtime.Safe(Global.WebSharper.Html);
  Client=Runtime.Safe(Html.Client);
  Operators=Runtime.Safe(Client.Operators);
  List=Runtime.Safe(Global.WebSharper.List);
  Attr=Runtime.Safe(Client.Attr);
  Tags=Runtime.Safe(Client.Tags);
  T=Runtime.Safe(List.T);
  Concurrency=Runtime.Safe(Global.WebSharper.Concurrency);
  Remoting=Runtime.Safe(Global.WebSharper.Remoting);
  AjaxRemotingProvider=Runtime.Safe(Remoting.AjaxRemotingProvider);
  EventsPervasives=Runtime.Safe(Client.EventsPervasives);
  DemoWebSharper=Runtime.Safe(Global.DemoWebSharper);
  ExampleMaps=Runtime.Safe(DemoWebSharper.ExampleMaps);
  Number=Runtime.Safe(Global.Number);
  google=Runtime.Safe(Global.google);
  Math=Runtime.Safe(Global.Math);
  return Arrays=Runtime.Safe(Global.WebSharper.Arrays);
 });
 Runtime.OnLoad(function()
 {
  return;
 });
}());
