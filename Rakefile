require 'albacore'
require 'cruxrake'

CruxRake::Solution.new do |s|
  s.file = 'src/Crux.Common.sln'
end

desc 'Packages all nugets'
nugets_pack :package => [ :versionizer ] do |p|
  p.files   = FileList['src/**/*.{csproj,fsproj,nuspec}'].exclude(/Tests/)
  p.out     = 'build/packages'
  p.exe     = 'src/.nuget/NuGet.exe'
  p.with_metadata do |m|
    m.description = 'Common libraries for crux applications'
    m.authors = 'Robert Scaduto'
    m.version = ENV['NUGET_VERSION']
  end
end

