# DotPloyed
A .NET development tool to deploy assembly, executable and .pdb files to a testing environment.

### Why?

When Visual Studio builds solutions it only builds the projects it detects code changes in. These files get an updated file creation time attribute.

Instead of tracking down the prior versions of said assemblies built in a testing environment to replace them manually, or running an updated installer that includes them, this tool will scan the development environment and detect changes that don't exist in the testing environment to then deploy the updated assemblies and etc.

#### Important Note
It does a practical file attribute comparison. So If you built projects with changes then installed the software for the first time in the testing environment - the tool won't work until you rebuild.