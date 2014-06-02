require 'albacore'
require 'cruxrake'
require 'pry'

CruxRake::Solution.new do |s|
  s.file = 'src/Crux.Common.sln'
end

output_location = 'build/packages'

task :ensure_output do
  FileUtils.mkdir_p output_location
end

desc 'Packages all nugets'
nugets_pack :package => [ :versionizer, :test, :ensure_output ] do |p|
  p.configuration   = 'Release'
  p.files           = FileList['src/**/*.{csproj,fsproj,nuspec}'].exclude(/Tests/)
  p.out             = output_location
  p.exe             = 'src/.nuget/NuGet.exe'
  p.gen_symbols
  p.with_metadata do |m|
    m.description = 'Common libraries for crux applications'
    m.authors = 'Robert Scaduto'
    m.version = ENV['NUGET_VERSION']
  end
end

namespace :publish do
  desc 'Copy nuget packages to local package source'
  task :local => [ :package ] do
    FileUtils.mkdir_p 'C:/Nuget.Local'
	FileUtils.cp FileList['./build/packages/*'], 'C:/Nuget.Local'
  end
end

desc 'Publish all nuget packages to public repo'
task :publish => [ :package ] do
  %w(Crux.Core Crux.Logging Crux.Caching Crux.Domain Crux.Domain.Persistence Crux.NServiceBus Crux.StructureMap Crux.WebApi).each do |p|
    sh "src/.nuget/nuget.exe push build/packages/#{p}.#{ENV['NUGET_VERSION']}.nupkg #{ENV.fetch('NUGET_API_KEY')} -Source https://www.myget.org/F/thirdwave/api/v2/package"
	sh "src/.nuget/nuget.exe push build/packages/#{p}.#{ENV['NUGET_VERSION']}.symbols.nupkg #{ENV.fetch('NUGET_API_KEY')} -Source https://www.myget.org/F/thirdwave/api/v2/package"
  end
end


