﻿module Suave.Tests.TestUtilities

#nowarn "25"

open System
open System.IO
open System.Threading
open System.Net
open System.Net.Http
open System.Net.Http.Headers
open System.Reflection

open Suave
open Suave.Types
open Suave.Web
open Suave.Http
open Suave.Logging

open FsCheck

open Fuchu

type Arbs =
  static member String () = Arb.Default.String () |> Arb.filter (fun str -> str <> null)

let fsCheckConfig = { Config.Default with Arbitrary = [ typeof<Arbs> ] }

let currentPath =
  Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)

let readText relativePath =
  File.ReadAllText(Path.Combine(currentPath, relativePath))

let readBytes relativePath =
  File.ReadAllBytes(Path.Combine(currentPath, relativePath))

let openRead relativePath =
  File.OpenRead (Path.Combine(currentPath, relativePath))

let defaultConfig =
  { defaultConfig with
      logger = Loggers.saneDefaultsFor LogLevel.Warn }
