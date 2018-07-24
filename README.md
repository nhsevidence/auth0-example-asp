# Auth0 ASP.NET (OWIN) MVC example

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
**Table of Contents**  *generated with [DocToc](https://github.com/thlorenz/doctoc)*

- [What is it?](#what-is-it)
- [Stack](#stack)
- [Set up](#set-up)
- [Resources](#resources)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## What is it?
ASP.NET (OWIN) MVC example project to test use of [Auth0](auth0.com) as 
an Identity Platform / NICE Accounts replacement
  
## Stack
- .NET Framework 4.5.1
- ASP.NET MVC
- ASP.NET Razor
- OWIN OpenId Connect

## Set up

### Auth0 setup
- Create an [Auth0](auth0.com) account and add an example application 
and example users

- In the [Auth0 Application management section](https://manage.auth0.com/#/applications)
select your example application and go to the `Settings` tab:
    - In the **Allowed Callback URLs** text area add `http://localhost:56572/signin-auth0`
    - In the **Allowed Web Origins** text area add `http://localhost:56572`
    - In the **Allowed Logout URLs** text area add `http://localhost:56572`

- In the project `Web.config` file change the following `appSettings` properties 
with the corresponding values found in the Auth0 application `Settings` tab: 
    - Change the `auth0:Domain` value to the `Domain` setting value
    - Change the `auth0:ClientId` value to the `Client ID` setting value
    - Change the `auth0:ClientSecret` value to the `Client Secret` setting value

- To test API authentication / integration run the [Web API example](https://github.com/nhsevidence/auth0-example-api)

### Local setup
- Make sure port `56572` is free
- In IIS create an Application Pool (CLR v4) and add a site using `{app_directory}\Auth0ExampleAsp` as the content directory
- Configure it to use `localhost` as hostname and `56572` as port
- Open the app in Visual Studio (2015+) and build it
- Go to `http://localhost:56572/` and see the menu at the top

## Resources

[Auth0 ASP.NET (OWIN) MVC tutorial](https://auth0.com/docs/quickstart/webapp/aspnet-owin/01-login)

[Auth0 ASP.NET (OWIN) MVC sample code](https://github.com/auth0-samples/auth0-aspnet-owin-mvc-samples)