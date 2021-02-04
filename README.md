
# .NET Docker example

The following samples and guidance demonstrate how to use .NET and Docker for development, testing and production.

## Building images

* [Build a .NET Docker image](dotnetapp/README.md)
* [Build an ASP.NET Core Docker image](aspnetapp/README.md)
* [Build and test a multi-project solution](complexapp/README.md)

## Hosting guidance

* [Host ASP.NET Core Images with Docker and HTTPS](host-aspnetcore-https.md)
* [Push Docker Images to Azure Container Registry](push-image-to-acr.md)
* [Push Docker Images to Docker Hub](push-image-to-dockerhub.md)
* [Deploy ASP.NET Core Applications to Azure Container Instances](deploy-container-to-aci.md)

## Scenario guidance

*Add Docker to WebApp in local
  1. In the Solution Explorer, right-click the Synergy.CrewWage.Api project, point to Add, and select Docker Support.
  2. Make sure that Linux is selected and click OK.
  3. Visual Studio creates a Dockerfile for you, which defines how to create the     container image for your web app project. 
    Now, when you start your project, it will run inside a Docker container, rather than in IIS Express on your workstation. 
    Try it out by clicking the Docker button on the toolbar.
   Hint: If you don’t see the Docker button on the toolbar, and you have a docker-compose project in Solution Explorer, then right click on the docker-compose project and click Set as StartUp Project.
  4. Visual Studio will build and run your container image, and launch a web browser showing your web app running inside the newly created container, rather than locally on your workstation.
  5. To use the Docker command line tools to verify that your container is running, click the Windows Start button and type cmd to launch a command prompt. Then type docker container ls.


## Try pre-built images

The following commands will run a .NET console app in a container:

```console
docker run --rm mcr.microsoft.com/dotnet/samples
```

The following command will run an ASP.NET Core console app in a container that you can access in your web browser at `http://localhost:8000`.

```console
docker run --rm -it -p 8000:80 mcr.microsoft.com/dotnet/samples:aspnetapp
```

## Email Service GitHub package

dotnet nuget add source https://nuget.pkg.github.com/Synergy-Marine-Group/index.json -n synergy-github -u Synergy-Marine-Group -p <your_token> --store-password-in-clear-text && dotnet restore "src/Synergy.ReliefCenter.Api/Synergy.ReliefCenter.Api.csproj"

## Environment Variables
 Name | Purpose 
 --|--
 ASPNETCORE_ENVIRONMENT | Use to specify application environment 
 ConnectionStrings__ManningDB | Use for Manning DB connection string 
 ConnectionStrings__VesselDB | Use for Vessel DB connection string 
 ConnectionStrings__SeafarerDB | Use for Seafarer DB connection string 
 ConnectionStrings__MasterDB | Use for Master DB connection string 
 SmtpConfiguration__Host | Smtp Host details
 SmtpConfiguration__Port | Smtp Port details
 SmtpConfiguration__EnableSsl | Smtp enable ssl
 SmtpConfiguration__DeliveryMethod | Smtp deliverymethod
 SmtpConfiguration__UseDefaultCredentials | Default Credentials for Smtp 
 SmtpConfiguration__FromEmail | Smtp FromEmail configuration 
 SmtpConfiguration__Login | Smtp User login name for Email
 SmtpConfiguration__Password | Smtp User Password for Email
 IdentityServerConfiguration__ShoreAuthorityUrl | Jwt Token URL for ShoreIdp 
 IdentityServerConfiguration__SeafarerAuthorityUrl | Jwt Token URL for SeafarerIdp 
 IdentityServerConfiguration__ShoreApiKey | ApiKey for userdetails Api
 CrewWage__ApiUrl | ApiUrl for external CrewWage Api
 AbodeSign__AccessKey | AccessKey for AdobeSign
 AbodeSign__ApiUrl | ApiUrl for AdobeSign 
 AbodeSign__ContractDocumentId_v1.1 | Document1 of contract for AdobeSign 
 AbodeSign__ContractDocumentId_v1.2 | Document2 of contract for AdobeSign 
 AbodeSign__ApiVersion | API Version fr AdobeSign 