#r @"./bin/Debug/FSharp.Data.TypeProviders.dll"
#r "./bin/Debug/System.ServiceModel.dll"
#r "./bin/Debug/System.Runtime.Serialization.dll"

open System
open System.ServiceModel
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Data.TypeProviders

type SettSchedService = WsdlService<"http://sparrowaeq.office.sbs/Sequel.UW.WS.SettlementScheduleWcfService/SettlementScheduleWcfService.svc?wsdl">

let client = SettSchedService.GetDefaultEndPoint()


