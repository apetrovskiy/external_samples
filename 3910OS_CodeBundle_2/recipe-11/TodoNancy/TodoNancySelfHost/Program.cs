﻿namespace TodoNancySelfHost
 {
   using System;
   using Nancy.Hosting.Self;
   using TodoNancy;

   class Program
   {
     static void Main(string[] args)
     {
       TodosModule artificiaReference;
       var nancyHost = new NancyHost(new Uri("http://localhost:8080/"));
       nancyHost.Start();

       Console.ReadKey();

       nancyHost.Stop();
     }
   }
 }
