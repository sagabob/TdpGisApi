[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="#">
    <img src="https://i.ibb.co/gb2tf3s/Tdp-logo-main.png" alt="Logo" width="285" height="170">
  </a>

  <h2 align="center">Tdp GIS API</h2>

  <p align="center">
    Restful APIs provides text & spatial querying services for configured geospatial features    

  </p>
</p>

## Table of Contents
* [About the Project](#about-the-project)
* [Built with](#built-with)
* [Roadmap](#roadmap)
* [Contact](#contact)
  
## About the project
A dotnet-core WebApi which provides Restful services on text & spatial searches for configured geospatial features. The searchable features can be from Cosmosdb, Mongodb & PostgreSQL (work in progress, only Cosmosdb works atm). 

For a given feature, we configure the searchable field, its geometry field and propreties that we want to return and their output names. The WebApi is currently being loaded with some geospatial features about Christchurch city, NZ. 

## Built With
* DotNet-Core 7.0
* WebAPI
* Entity Framework Core 7.0
* CosmosDb SDK 3.0
* Docker
* Deployed to Azure WebApp for containers.

## Roadmap
The application will be extended with the following features
* Add a frontend to consume these features.
* Add an admin page to provide an easy feature configuration, authentication/authorization will be implemented.
* Add Kubernetes.


## Contact
[![LinkedIn][linkedin-shield]][linkedin-url]<br/>
Bob Pham - bobpham.tdp@gmail.com<br/>


[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/bob-pham-93937973/
[tdp-logo]: tdp-logo.png
