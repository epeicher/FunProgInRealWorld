(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,List,Html,Client,Attr,Tags,T,Concurrency,Remoting,AjaxRemotingProvider,EventsPervasives,Operators,MyFirstWebSharper,ExampleMaps,d3,Number,Arrays,google,Math;
 Runtime.Define(Global,{
  MyFirstWebSharper:{
   Client:{
    Main:function()
    {
     var x,input,x1,output,arg10,arg101,x2,arg00,arg102,arg103,arg104,arg105,arg106,arg107,arg108;
     x=List.ofArray([Attr.Attr().NewAttr("value","")]);
     input=Tags.Tags().NewTag("input",x);
     x1=Runtime.New(T,{
      $:0
     });
     output=Tags.Tags().NewTag("h1",x1);
     arg101=List.ofArray([Tags.Tags().text("Send")]);
     x2=Tags.Tags().NewTag("button",arg101);
     arg00=function()
     {
      return function()
      {
       return Concurrency.Start(Concurrency.Delay(function()
       {
        return Concurrency.Bind(AjaxRemotingProvider.Async("MyFirstWebSharper:0",[input.get_Value()]),function(_arg11)
        {
         output.set_Text(_arg11);
         return Concurrency.Return(null);
        });
       }),{
        $:0
       });
      };
     };
     EventsPervasives.Events().OnClick(arg00,x2);
     arg102=Runtime.New(T,{
      $:0
     });
     arg103=List.ofArray([Attr.Attr().NewAttr("class","text-muted")]);
     arg104=List.ofArray([Attr.Attr().NewAttr("class","jumbotron")]);
     arg105=List.ofArray([Attr.Attr().NewAttr("class","row")]);
     arg106=List.ofArray([Attr.Attr().NewAttr("class","col-md-6")]);
     arg107=List.ofArray([Attr.Attr().NewAttr("class","col-md-6")]);
     arg108=Runtime.New(T,{
      $:0
     });
     arg10=List.ofArray([input,x2,Tags.Tags().NewTag("hr",arg102),Operators.add(Tags.Tags().NewTag("h4",arg103),List.ofArray([Tags.Tags().text("The server responded:")])),Operators.add(Tags.Tags().NewTag("div",arg104),List.ofArray([output])),Operators.add(Tags.Tags().NewTag("div",arg105),List.ofArray([Operators.add(Tags.Tags().NewTag("div",arg106),List.ofArray([ExampleMaps.SimpleMap()])),Operators.add(Tags.Tags().NewTag("div",arg107),List.ofArray([ExampleMaps.Moon()]))])),Tags.Tags().NewTag("hr",arg108)]);
     return Tags.Tags().NewTag("div",arg10);
    },
    Start:function(input,k)
    {
     var arg00;
     arg00=Concurrency.Delay(function()
     {
      return Concurrency.Bind(AjaxRemotingProvider.Async("MyFirstWebSharper:0",[input]),function(_arg1)
      {
       return Concurrency.Return(k(_arg1));
      });
     });
     return Concurrency.Start(arg00,{
      $:0
     });
    }
   },
   ExampleD3:{
    Main:function()
    {
     var value,margin,margin2,width,height,height2,x,x2,y,y2,xAxis,xAxis2,yAxis,brush,area,area2,svg,value1,focus,context,brushed,value4,arg10;
     value=d3.select("head").append("link").attr("rel","stylesheet").attr("type","text/css").attr("href","chart.css");
     margin={
      Top:10,
      Right:10,
      Bottom:100,
      Left:40
     };
     margin2={
      Top:430,
      Right:10,
      Bottom:20,
      Left:40
     };
     width=800-margin.Left-margin.Right;
     height=500-margin.Top-margin.Bottom;
     height2=500-margin2.Top-margin2.Bottom;
     x=d3.time.scale().range([0,width]);
     x2=d3.time.scale().range([0,width]);
     y=d3.scale.linear().range([height,0]);
     y2=d3.scale.linear().range([height2,0]);
     xAxis=d3.svg.axis().scale(x).orient("bottom");
     xAxis2=d3.svg.axis().scale(x2).orient("bottom");
     yAxis=d3.svg.axis().scale(y).orient("left");
     brush=d3.svg.brush().x(x2);
     area=d3.svg.area().interpolate("monotone").x(function(d)
     {
      return x(d.Date);
     }).y0(Number(height)).y1(function(d)
     {
      return y(d.Price);
     });
     area2=d3.svg.area().interpolate("monotone").x(function(d)
     {
      return x2(d.Date);
     }).y0(Number(height2)).y1(function(d)
     {
      return y2(d.Price);
     });
     svg=d3.select("body").append("svg").attr("align","center").attr("width",width+margin.Left+margin.Right).attr("height",height+margin.Top+margin.Bottom);
     value1=svg.append("defs").append("clipPath").attr("id","clip").append("rect").attr("width",width).attr("height",height);
     focus=svg.append("g").attr("transform","translate("+margin.Left+","+margin.Top+")");
     context=svg.append("g").attr("transform","translate("+margin2.Left+","+margin2.Top+")");
     brushed=function()
     {
      var value2,_,x1,value3,arg00;
      if(brush.empty())
       {
        _=x2.domain();
       }
      else
       {
        x1=brush.extent();
        _=x1;
       }
      value2=x.domain(_);
      value3=focus.select("path").attr("d",area);
      arg00=focus.select(".x.axis");
      return xAxis(arg00);
     };
     value4=brush.on("brush",brushed);
     d3.csv("sp500.csv",function(data)
     {
      var objectArg,parseDate,mapping,parsedData,value2,x1,value3,value5,value6,value7,arg001,arg002,value8,arg003,value9;
      objectArg=d3.time.format("%b %Y");
      parseDate=function(arg00)
      {
       return objectArg.parse(arg00);
      };
      mapping=function(d)
      {
       return{
        Date:parseDate(d.date),
        Price:+d.price
       };
      };
      parsedData=Arrays.map(mapping,data);
      x1=d3.extent(parsedData,function(d)
      {
       return d.Date;
      });
      value2=x.domain(x1);
      value3=y.domain([0,d3.max(parsedData,function(d)
      {
       return d.Price;
      })]);
      value5=x2.domain(x.domain());
      value6=y2.domain(y.domain());
      value7=focus.append("path").datum(parsedData).attr("clip-path","url(#clip)").attr("d",area);
      arg001=focus.append("g").attr("class","x axis").attr("transform","translate("+0+","+height+")");
      xAxis(arg001);
      arg002=focus.append("g").attr("class","y axis");
      yAxis(arg002);
      value8=context.append("path").datum(parsedData).attr("d",area2);
      arg003=context.append("g").attr("class","x axis").attr("transform","translate("+0+","+height2+")");
      xAxis2(arg003);
      value9=context.append("g").attr("class","x brush").call(function(x3)
      {
       return brush(x3);
      }).selectAll("rect").attr("y",-6).attr("height",height2+7);
      return;
     });
     arg10=Runtime.New(T,{
      $:0
     });
     return Tags.Tags().NewTag("div",arg10);
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
  List=Runtime.Safe(Global.WebSharper.List);
  Html=Runtime.Safe(Global.WebSharper.Html);
  Client=Runtime.Safe(Html.Client);
  Attr=Runtime.Safe(Client.Attr);
  Tags=Runtime.Safe(Client.Tags);
  T=Runtime.Safe(List.T);
  Concurrency=Runtime.Safe(Global.WebSharper.Concurrency);
  Remoting=Runtime.Safe(Global.WebSharper.Remoting);
  AjaxRemotingProvider=Runtime.Safe(Remoting.AjaxRemotingProvider);
  EventsPervasives=Runtime.Safe(Client.EventsPervasives);
  Operators=Runtime.Safe(Client.Operators);
  MyFirstWebSharper=Runtime.Safe(Global.MyFirstWebSharper);
  ExampleMaps=Runtime.Safe(MyFirstWebSharper.ExampleMaps);
  d3=Runtime.Safe(Global.d3);
  Number=Runtime.Safe(Global.Number);
  Arrays=Runtime.Safe(Global.WebSharper.Arrays);
  google=Runtime.Safe(Global.google);
  return Math=Runtime.Safe(Global.Math);
 });
 Runtime.OnLoad(function()
 {
  return;
 });
}());
