$ErrorActionPreference = 'Stop';
$packageName= 'smartcontextmenu'
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/AlexanderPro/SmartContextMenu/releases/download/v1.10.0/SmartContextMenu_v1.10.0.zip'

$packageArgs = @{
  packageName   = $packageName
  destination   = $toolsDir
  fileType      = 'zip'
  url           = $url
  softwareName  = 'SmartContextMenu*'
  checksum      = '74f764aa2632a4242877a8ccc16cc659777b52c45ea7f1f466085a0a186076dd'
  checksumType  = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
