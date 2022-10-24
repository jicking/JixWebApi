# Notes
Put your shared code here (eg: models, services)
so when the need of moving them into a separate class lib arises
it will be easier.

## Bonus tip
On WvcWebApp.Core project, set default name space to omit 'Core'
via
Project Properties -> Application -> Default NameSpace ->
$(MSBuildProjectName.Replace(" ", "_").Replace("Core",""))
