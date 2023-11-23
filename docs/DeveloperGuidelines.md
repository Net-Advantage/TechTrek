# Developer Guidance

Here is some thinking about this solution for developers.

## Entity vs DTO Naming

Name all EF Core entities with the suffix `Entity`. This is to avoid confusion with the DTOs.

DTOs are named without any suffix. This is to ensure that the name is purely descriptive of the requirements of the domain.

## Generating the Database


