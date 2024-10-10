# .NET Interview Template

## Context
This sample application is supposed to check if a given application name is installed by looking into the Windows registry.
It then should report the installation status to a mock web API.

## My Solution
As the requirements were simple, I kept the code simple as well and fit within the given template without extending it.

It first searches the registry under the current user top-level key and if not found, then searches under the local machine top-level key. Since this only needs to read the local machine keys, it does not require admin privileges.

I have verified that all tests pass.

Thank you for your time and the opportunity.


