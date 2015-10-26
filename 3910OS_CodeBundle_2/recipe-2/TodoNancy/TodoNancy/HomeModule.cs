﻿namespace TodoNancy
{
  using Nancy;

  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => HttpStatusCode.OK;
    }
  }
}