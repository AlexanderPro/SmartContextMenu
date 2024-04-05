$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.3.1/SmartContextMenu_v1.3.1.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = '0da95204af02879ef869d5b64ca749088c15bad8e0829bfbb6e560a22c1c806c'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
